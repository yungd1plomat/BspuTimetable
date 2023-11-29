using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VkBot.Models
{
    /// <summary>
    /// Класс предоставляющий информацию о занятии
    /// </summary>
    [Table("Lessons")]
    public class Lesson
    {
        /// <summary>
        /// Код занятия (id)
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Дата начала занятия
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Дата окончания занятия
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Id преподавателя из asu.bspu.ru
        /// </summary>
        public long? ProfessorId { get; set; }

        /// <summary>
        /// Преподаватель из asu.bspu.ru
        /// </summary>
        public virtual Professor? Professor { get; set; }

        /// <summary>
        /// Дисциплина
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Аудитория
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Группа
        /// </summary>
        public virtual Group Group { get; set; }
    }
}
