using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedievalShop.BLL.DTO;

namespace MedievalShop.BLL.Interfaces
{
    public interface IPurchaseService
    {
        void AddPurchase(PurchaseDTO purchaseDto);
        PurchaseDTO GetPurchase(int? id);
        IEnumerable<PurchaseDTO> GetPurchases();
        void Dispose();
    }
}
