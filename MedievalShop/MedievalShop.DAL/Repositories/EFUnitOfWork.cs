using MedievalShop.DAL.Interfaces;
using MedievalShop.DAL.Entities;
using MedievalShop.DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalShop.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ShopContext db;
        private ClientRepository clientRepository;
        private ClientTypeRepository clientTypeRepository;
        private ItemRepository itemRepository;
        private ItemTypeRepository itemTypeRepository;
        private PurchaseRepository purchaseRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new ShopContext(connectionString);
        }

        public IClientRepository<ClientEntity> Clients
        {
            get
            {
                if (clientRepository == null)
                    clientRepository = new ClientRepository(db);
                return clientRepository;
            }
        }

        public IRepository<ItemEntity> Items
        {
            get
            {
                if (itemRepository == null)
                    itemRepository = new ItemRepository(db);
                return itemRepository;
            }
        }

        public ITypeRepository<ClientTypeEntity> ClientTypes
        {
            get
            {
                if (clientTypeRepository == null)
                    clientTypeRepository = new ClientTypeRepository(db);
                return clientTypeRepository;
            }
        }

        public ITypeRepository<ItemTypeEntity> ItemTypes
        {
            get
            {
                if (itemTypeRepository == null)
                    itemTypeRepository = new ItemTypeRepository(db);
                return itemTypeRepository;
            }
        }

        public IRepository<PurchaseEntity> Purchases
        {
            get
            {
                if (purchaseRepository == null)
                    purchaseRepository = new PurchaseRepository(db);
                return purchaseRepository;
            }
        }



        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
