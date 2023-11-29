using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VkBot.Models
{
    /// <summary>
    /// Класс предоставляющий информацию о группе
    /// </summary>
    [Table("Groups")]
    public class Group
    {
        /// <summary>
        /// Id группы на asu.bspu.ru
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Название группы
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Факультет
        /// </summary>
        public string Faculty { get; set; }

        /// <summary>
        /// Курс
        /// </summary>
        public int Course { get; set; }

        /// <summary>
        /// Последнее обновление расписания группы
        /// </summary>
        public DateTime LastUpdate { get; set; }
    }
}
