//модель пользователя

namespace tel_bot_net.Models.DbModels
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ChatId { get; set; }// chat id из бота
    }
}
