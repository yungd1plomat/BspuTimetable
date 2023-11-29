using VkNet.Model;

namespace VkBot.Abstractions
{
    /// <summary>
    /// Репозиторий для работы с событиями
    /// </summary>
    public interface IUpdateRepository
    {
        /// <summary>
        /// Обрабатывает событие
        /// </summary>
        /// <param name="update">Событие для обработки</param>
        Task HandleUpdate(GroupUpdate update);

        /// <summary>
        /// Проверяет поддерживает ли бот событие
        /// </summary>
        /// <param name="update">Событие для обработки</param>
        bool CheckIfSupported(GroupUpdate update);
    }
}
