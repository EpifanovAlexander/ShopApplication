using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedievalShop.DAL.Interfaces;
using MedievalShop.DAL.Entities;
using MedievalShop.DAL.EF;

namespace MedievalShop.DAL.Repositories
{
    public class ItemTypeRepository : ITypeRepository<ItemTypeEntity>
    {
        private ShopContext db;

        public ItemTypeRepository(ShopContext context)
        {
            db = context;
        }

        public IEnumerable<ItemTypeEntity> GetAll()
        {
            return db.ItemTypes;
        }

        public string GetTypeText(int id)
        {
            return db.ItemTypes.FirstOrDefault(type => type.Id == id).Name;
        }
    }
}
