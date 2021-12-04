using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalShop.BLL.DTO
{
    public class PurchaseDTO
    {
        public int PurchaseId { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public int ItemId { get; set; }
        public ClientDTO Client { get; set; }
        public ItemDTO Item { get; set; }
    }
}
