using Parser.Models;

namespace Parser.Abstractions
{
    /// <summary>
    /// Репозиторий для управления расписанием
    /// </summary>
    public interface IScheduleRepository : IDisposable
    {
        /// <summary>
        /// Обновляет расписание группы
        /// </summary>
        /// <param name="group">Группа у которой нужно обновить расписание</param>
        /// <returns></returns>
        Task UpdateByGroupAsync(Group group, CancellationToken cancellationToken);

        /// <summary>
        /// Обновляет список групп
        /// </summary>
        Task UpdateGroupsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получает полный список групп
        /// </summary>
        /// <returns>Список групп</returns>
        Task<IEnumerable<Group>> GetGroupsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Обновляет текущее расписание
        /// </summary>
        Task UpdateScheduleAsync(CancellationToken cancellationToken);
    }
}
