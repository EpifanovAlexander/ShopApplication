using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedievalShop.BLL.DTO;

namespace MedievalShop.BLL.Interfaces
{
    public interface IItemService
    {
        void AddItem(ItemDTO itemDto);
        void UpdateItem(ItemDTO itemDto);
        void DeleteItem(int? id);
        ItemDTO GetItem(int? id);
        IEnumerable<ItemDTO> GetItems();
        IEnumerable<ItemTypeDTO> GetItemTypes();
        void Dispose();
    }
}
