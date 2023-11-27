using System.Text.Json.Serialization;

namespace Parser.Models
{
    public class ScheduleInfo
    {
        /// <summary>
        /// Время последнего обновления расписания
        /// </summary>
        [JsonPropertyName("dateUploadingRasp")]
        public DateTime LastUpdate { get; set; }
    }
}
