//модель пользователя

namespace tel_bot_net.Models.DbModels
{
    public class MyUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public long ChatId { get; set; }// chat id из бота

    }
}
