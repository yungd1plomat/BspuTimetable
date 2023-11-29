using Microsoft.EntityFrameworkCore;
using Parser.Abstractions;
using Parser.Data;
using Parser.Models;
using Regex = System.Text.RegularExpressions.Regex;

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

            // Не все занятия помечены кодом преподавателя (ошибка апи)
            // Занятия проводимые на osdo или онлайн помечены как
            // Фамилия И.О+ и имеют код преподавателя 0
            var emptyLessons = scheduleData.Lessons.Where(l => l.ProfessorId == 0);
            foreach (var lesson in emptyLessons)
            {
                // Поэтому ищем преподавателя по маске инициалов (Фамилия И.О.)
                var professor = await _dbContext.Professors.FirstOrDefaultAsync(p => lesson.ProfessorShortName.StartsWith(p.ShortName), cancellationToken);
                // Апишку писал Масленников, поэтому она может отдавать несуществующих преподавателей
                // которых нет на сайте (наставников/плюсовиков каких то/совместителей мб)
                // Они не смогут смотреть расписание ¯\_(ツ)_/¯
                if (professor is null)
                    lesson.ProfessorId = null;
                lesson.Professor = professor;
            }
            var oldLessons = await _dbContext.Lessons.Where(l => l.Group == group).ToListAsync(cancellationToken);
            if (oldLessons.Any())
                _dbContext.Lessons.RemoveRange(oldLessons);
            foreach (var lesson in scheduleData.Lessons)
            {
                lesson.Group = group;
            }
            await _dbContext.Lessons.AddRangeAsync(scheduleData.Lessons, cancellationToken);
            group.LastUpdate = scheduleData.Info.LastUpdate;
        }

        public async Task UpdateProfessorsAsync(CancellationToken cancellationToken)
        {
            var currentProfessors = await _dbContext.Professors.ToListAsync(cancellationToken);
            var updatedProfessors = await _bspuApi.GetProfessorsAsync(cancellationToken);
            foreach (var professor in updatedProfessors)
            {
                // Разработчики api не предусмотрели отдавать инициалы преподавателей, поэтому
                // парсим в формат Фамилия Имя Отчество -> Фамилия И.О
                var initials = professor.Name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var shortName = $"{initials[0]} {initials[1][0]}.{initials[2][0]}.";
                professor.ShortName = shortName;
            }
            var newProfessors = updatedProfessors.ExceptBy(currentProfessors.Select(p => p.Id), p => p.Id);
            var deletedProfessors = currentProfessors.ExceptBy(updatedProfessors.Select(p => p.Id), p => p.Id);
            
            await _dbContext.AddRangeAsync(newProfessors, cancellationToken);

            if (deletedProfessors.Any())
                _dbContext.RemoveRange(deletedProfessors, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"Parsed {newProfessors.Count()} new professors and deleted {deletedProfessors.Count()} professors");

        }

        public async Task UpdateScheduleAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Schedule update started..");
            await UpdateProfessorsAsync(cancellationToken);
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
