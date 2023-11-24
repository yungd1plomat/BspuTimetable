using Microsoft.EntityFrameworkCore;
using Parser.Abstractions;
using Parser.Data;
using Parser.Models;
using System.Reflection;

namespace Parser.Services
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly ILogger _logger;

        private readonly IBspuApi _bspuApi;

        private readonly ScheduleDbContext _dbContext;

        public ScheduleRepository(IBspuApi bspuApi, 
            ScheduleDbContext dbContext,
            ILogger<ScheduleRepository> logger)
        {
            _bspuApi = bspuApi;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<Group>> GetGroupsAsync(CancellationToken cancellationToken = default)
        {
            var groups = await _dbContext.Groups.ToListAsync(cancellationToken);
            return groups;
        }

        public async Task UpdateGroupsAsync(CancellationToken cancellationToken = default)
        {
            var currentGroups = await _dbContext.Groups.ToArrayAsync(cancellationToken);
            var updatedGroups = await _bspuApi.GetGroupsAsync(cancellationToken);
            var newGroups = updatedGroups.ExceptBy(currentGroups.Select(g => g.Id), g => g.Id);
            var deletedGroups = currentGroups.ExceptBy(updatedGroups.Select(g => g.Id), g => g.Id);

            await _dbContext.AddRangeAsync(newGroups, cancellationToken);

            if (deletedGroups.Any())
                _dbContext.RemoveRange(deletedGroups, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"Parsed {newGroups.Count()} new groups and deleted {deletedGroups.Count()} groups");
        }

        public async Task UpdateByGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            var scheduleData = await _bspuApi.GetScheduleAsync(group.Id, cancellationToken);
            if (!scheduleData.Lessons.Any() ||
                scheduleData.Info.LastUpdate == group.LastUpdate)
                return;

            var oldLessons = await _dbContext.Lessons.Where(l => l.Group == group).ToListAsync(cancellationToken);
            if (oldLessons.Any())
                _dbContext.Lessons.RemoveRange(oldLessons);
            foreach (var lesson in scheduleData.Lessons)
            {
                lesson.Group = group;
            }
            await _dbContext.Lessons.AddRangeAsync(scheduleData.Lessons, cancellationToken);
            group.LastUpdate = scheduleData.Info.LastUpdate;
            await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"Updated schedule for {group.Name}");
        }

        public async Task UpdateScheduleAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Schedule update started..");
            await UpdateGroupsAsync(cancellationToken);
            var groups = await GetGroupsAsync(cancellationToken);
            foreach (var group in groups)
            {
                await UpdateByGroupAsync(group, cancellationToken);
            }
            _logger.LogInformation("Schedule updated.");
        }

        public void Dispose()
        {
            _bspuApi?.Dispose();
            _dbContext?.Dispose();
        }
    }
}
