using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VkBot.Models
{
    /// <summary>
    /// Класс предоставляющий информацию о преподавателях
    /// </summary>
    [Table("Professors")]
    public class Professor
    {
        /// <summary>
        /// Id преподавателя из asu.bspu.ru
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Полное имя преподавателя из asu.bspu.ru
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Короткое имя преподавателя Фамилия И.О.
        /// </summary>
        public string ShortName { get; set; }
    }
}
