using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tel_bot_net.Models.DbModels
{
    public class OpenTransaction
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string ToolIds { get; set; }

        public string ImageName { get; set; }

        public DateTime DateTimeOpen { get; set; }


    }
}
