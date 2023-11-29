using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Parser.Models
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
        [JsonPropertyName("id")]
        public long Id { get; set; }

        /// <summary>
        /// Полное имя преподавателя из asu.bspu.ru
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Короткое имя преподавателя Фамилия И.О.
        /// </summary>
        [JsonIgnore]
        public string ShortName { get; set; }
    }
}
