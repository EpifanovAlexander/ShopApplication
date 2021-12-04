using System.Collections.Generic;


namespace MedievalShop.Models
{
    public class PurchasesViewModel
    {
        public IEnumerable<PurchaseViewModel> Purchases { get; set; }
        public PageInfo PageInfo { get; set; }
        public ClientViewModel Client { get; set; }
    }
}