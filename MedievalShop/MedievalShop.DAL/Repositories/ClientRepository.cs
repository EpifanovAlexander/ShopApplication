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
    public class ClientRepository : IClientRepository<ClientEntity>
    {
        private ShopContext db;

        public ClientRepository(ShopContext context)
        {
            db = context;
        }

        public void Add(ClientEntity client)
        {
            db.Clients.Add(client);
        }

        public void Update(ClientEntity client)
        {
            db.Entry(client).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            ClientEntity client = db.Clients.Find(id);
            if (client != null) db.Clients.Remove(client);
        }

        public ClientEntity Get(int id)
        {
            return db.Clients.Find(id);
        }

        public ClientEntity GetByLoginAndPass(string login, string pass)
        {
            return db.Clients.FirstOrDefault(c => c.Login == login && c.Password == pass);
        }

        public IEnumerable<ClientEntity> GetAll()
        {
            return db.Clients;
        }

    }
}
