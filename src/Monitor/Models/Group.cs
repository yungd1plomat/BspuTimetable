using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Parser.Models
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
        [Key]
        [JsonPropertyName("id")]
        public long Id { get; set; }

        /// <summary>
        /// Название группы
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Факультет
        /// </summary>
        [JsonPropertyName("facul")]
        public string Faculty { get; set; }

        /// <summary>
        /// Курс
        /// </summary>
        [JsonPropertyName("kurs")]
        public int Course { get; set; }

        /// <summary>
        /// Последнее обновление расписания группы
        /// </summary>
        [JsonIgnore]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime LastUpdate { get; set; }
    }
}
