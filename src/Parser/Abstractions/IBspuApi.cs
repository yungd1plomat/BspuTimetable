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
        /// Получает список преподавателей
        /// </summary>
        /// <returns>Массив списка преподавателей</returns>
        Task<IEnumerable<Professor>> GetProfessorsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получает расписание группы
        /// </summary>
        /// <param name="groupId">Id группы</param>
        /// <returns>Информация и расписание</returns>
        Task<ScheduleData> GetScheduleAsync(long groupId, CancellationToken cancellationToken);
    }
}
