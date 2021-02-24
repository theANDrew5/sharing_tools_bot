using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tel_bot_net.Models.DbModels
{
    public class CloseTransaction
    {
        public int Id { get; set; }

        public int OpenTransactionId { get; set; }

        public string ImageName { get; set; }

        public DateTime DateTimeClose { get; set; }

    }
}
