using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotDB.DbModels
{
    public class Transaction
    {
        public int Id { get; set; }

        public string ImageOpenName { get; set; }

        public DateTime DateTimeOpen { get; set; }

        public string ImageCloseName { get; set; }

        public DateTime? DateTimeClose { get; set; }

        public int UserId { get; set; }
        public int ToolId { get; set; }

        public MyUser User { get; set; }
        public Tool Tool { get; set; }


    }
}
