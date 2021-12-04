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
    public class PurchaseRepository : IRepository<PurchaseEntity>
    {
        private ShopContext db;
        public PurchaseRepository(ShopContext context)
        {
            db = context;
        }
        public void Add(PurchaseEntity purchase)
        {
            db.Purchases.Add(purchase);
        }
        public void Update(PurchaseEntity purchase)
        {
            db.Entry(purchase).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            PurchaseEntity purchase = db.Purchases.Find(id);
            if (purchase != null) db.Purchases.Remove(purchase);
        }

        public PurchaseEntity Get(int id)
        {
            return db.Purchases.Find(id);
        }

        public IEnumerable<PurchaseEntity> GetAll()
        {
            return db.Purchases;
        }

    }
}
