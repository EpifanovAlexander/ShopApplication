using System;


namespace MedievalShop.Models
{
    public class PurchaseViewModel
    {
        public int PurchaseId { get; set; }

        public DateTime Date { get; set; }

        public int ClientId { get; set; }

        public int ItemId { get; set; }

        public ClientViewModel Client { get; set; }

        public ItemViewModel Item { get; set; }
    }
}