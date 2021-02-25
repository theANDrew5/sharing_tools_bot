using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotDB.DbModels
{
    public class OpenTransaction
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public MyUser User { get; set; }
        public int ToolId { get; set; }
        [ForeignKey("ToolIds")]
        public Tool Tool { get; set; }

        public string ImageName { get; set; }

        public DateTime DateTimeOpen { get; set; }


    }
}
