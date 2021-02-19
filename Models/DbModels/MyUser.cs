//модель пользователя

namespace tel_bot_net.Models.DbModels
{
    public class MyUser
    {
        public long Id { get; set; }// chat id из бота
        public string Name { get; set; }

        public string PhoneNumber { get; set; }



    }
}
