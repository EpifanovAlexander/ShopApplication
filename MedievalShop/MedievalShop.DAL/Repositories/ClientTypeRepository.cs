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
    public class ClientTypeRepository : ITypeRepository<ClientTypeEntity>
    {
        private ShopContext db;

        public ClientTypeRepository(ShopContext context)
        {
            db = context;
        }

        public IEnumerable<ClientTypeEntity> GetAll()
        {
            return db.ClientTypes;
        }

        public string GetTypeText(int id)
        {
            return db.ClientTypes.FirstOrDefault(type => type.Id == id).Name;
        }
    }
}
