using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedievalShop.DAL.Entities;
using MedievalShop.DAL.EF;
using MedievalShop.DAL.Interfaces;
using System.Data.Entity;

namespace MedievalShop.DAL.Repositories
{
    public class ItemRepository : IRepository<ItemEntity>
    {
        private ShopContext db;
        public ItemRepository(ShopContext context)
        {
            db = context;
        }
        public void Add(ItemEntity item)
        {
            db.Items.Add(item);
        }
        public void Update(ItemEntity item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            ItemEntity item = db.Items.Find(id);
            if (item != null) db.Items.Remove(item);
        }

        public ItemEntity Get(int id)
        {
            return db.Items.Find(id);
        }

        public IEnumerable<ItemEntity> GetAll()
        {
            return db.Items;
        }
    }
}
