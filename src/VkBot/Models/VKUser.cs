using System.ComponentModel.DataAnnotations.Schema;
using VkBot.Models.Enums;

namespace VkBot.Models
{
    [Table("VKUsers")]
    public class VKUser
    {
        /// <summary>
        /// Id юзера на vk.com
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Группа пользователя
        /// </summary>
        public virtual Group? Group { get; set; }

        /// <summary>
        /// Тип пользователя (не установлено/студент/преподаватель)
        /// </summary>
        public UserType UserType { get; set; }

        /// <summary>
        /// Указывает кем из преподавателей является пользователь
        /// </summary>
        public Professor? Professor { get; set; }

        /// <summary>
        /// Является ли пользователь администратором
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Id последнего сообщения для редактирования (логики)
        /// </summary>
        public long? MessageId { get; set; }

        /// <summary>
        /// Время в минутах за которое уведомлять пользователя о занятии (null - не уведомлять)
        /// </summary>
        public long? Timer { get; set; }
    }
}
