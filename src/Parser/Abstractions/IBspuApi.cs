using Parser.Models;

namespace Parser.Abstractions
{
    public interface IBspuApi : IDisposable
    {
        /// <summary>
        /// Получает список групп
        /// /api/raspGrouplist
        /// </summary>
        /// <returns>Список групп</returns>
        Task<IEnumerable<Group>> GetGroupsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получает расписание группы
        /// </summary>
        /// <param name="groupId">Id группы</param>
        /// <returns>Информация и расписание</returns>
        Task<ScheduleData> GetScheduleAsync(long groupId, CancellationToken cancellationToken = default);
    }
}
