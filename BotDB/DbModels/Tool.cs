using System.Collections.Generic;


namespace BotDB.DbModels
{
    public class Tool
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string InventoryNumber { get; set; }

        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        


    }
}
