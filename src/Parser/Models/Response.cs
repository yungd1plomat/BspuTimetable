using System.Text.Json.Serialization;

namespace Parser.Models
{
    /// <summary>
    /// Ответ от апи сервера
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<T>
    {
        /// <summary>
        /// Содержимое ответа
        /// </summary>
        [JsonPropertyName("data")]
        public T Data { get; set;}

        /// <summary>
        /// Статус ответа
        /// </summary>
        [JsonPropertyName("state")]
        public int State { get; set;}

        /// <summary>
        /// Сообщение
        /// </summary>
        [JsonPropertyName("msg")]
        public string Message { get; set; }
    }
}
