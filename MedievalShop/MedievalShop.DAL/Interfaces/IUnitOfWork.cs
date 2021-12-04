using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedievalShop.DAL.Entities;

namespace MedievalShop.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IClientRepository<ClientEntity> Clients { get; }
        IRepository<ItemEntity> Items { get; }
        ITypeRepository<ClientTypeEntity> ClientTypes { get; }
        ITypeRepository<ItemTypeEntity> ItemTypes { get; }
        IRepository<PurchaseEntity> Purchases { get; }
        void Save();
    }
}
