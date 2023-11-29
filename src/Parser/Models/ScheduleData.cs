using System.Text.Json.Serialization;

namespace Parser.Models
{
    public class ScheduleData
    {
        /// <summary>
        /// Расписание группы
        /// </summary>
        [JsonPropertyName("rasp")]
        public IEnumerable<Lesson> Lessons { get; set; }

        /// <summary>
        /// Информация о расписании
        /// </summary>
        [JsonPropertyName("info")]
        public ScheduleInfo Info { get; set; }
    }
}
