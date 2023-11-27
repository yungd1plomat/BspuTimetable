using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Parser.Models
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
        [JsonPropertyName("код")]
        public long Id { get; set; }

        /// <summary>
        /// Дата начала занятия
        /// </summary>
        [Column(TypeName = "timestamp without time zone")]
        [JsonPropertyName("датаНачала")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Дата окончания занятия
        /// </summary>
        [Column(TypeName = "timestamp without time zone")]
        [JsonPropertyName("датаОкончания")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Короткое имя преподавателя, которое может не быть помечено кодом
        /// </summary>
        [JsonPropertyName("преподаватель")]
        public string ProfessorShortName { get; set; }

        /// <summary>
        /// Id преподавателя из asu.bspu.ru
        /// </summary>
        [JsonPropertyName("кодПреподавателя")]
        public long? ProfessorId { get; set; }

        /// <summary>
        /// Преподаватель из asu.bspu.ru
        /// </summary>
        [JsonIgnore]
        public virtual Professor? Professor { get; set; }

        /// <summary>
        /// Дисциплина
        /// </summary>
        [JsonPropertyName("дисциплина")]
        public string Subject { get; set; }

        /// <summary>
        /// Аудитория
        /// </summary>
        [JsonPropertyName("аудитория")]
        public string Audience { get; set; }

        /// <summary>
        /// Группа
        /// </summary>
        [JsonIgnore]
        public virtual Group Group { get; set; }
    }
}
