﻿//модель пользователя

using System.Collections.Generic;

namespace BotDB.DbModels
{
    public class MyUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public long ChatId { get; set; }// chat id из бота

        public List<Transaction> Transactions { get; set; }

    }
}
