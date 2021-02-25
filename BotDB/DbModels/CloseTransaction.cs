using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotDB.DbModels
{
    public class CloseTransaction
    {
        public int Id { get; set; }

        public int OpenTransactionId { get; set; }
        [ForeignKey("OpenTransactionId")]
        public OpenTransaction OpenTransaction { get; set; }

        public string ImageName { get; set; }

        public DateTime DateTimeClose { get; set; }

    }
}
