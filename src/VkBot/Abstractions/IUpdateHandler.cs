using VkBot.Models;
using VkNet.Abstractions;
using VkNet.Model;

namespace VkBot.Abstractions
{
    /// <summary>
    /// Хэндлер для обработки обновлений
    /// </summary>
    public interface IUpdateHandler
    {
        /// <summary>
        /// Проверяет соответствует ли обновление хендлеру
        /// </summary>
        bool IsMatch(GroupUpdate update);

        /// <summary>
        /// Обрабатывает обновление
        /// </summary>
        Task Handle(GroupUpdate update);
    }
}
