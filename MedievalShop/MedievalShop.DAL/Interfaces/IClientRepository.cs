using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalShop.DAL.Interfaces
{
    public interface IClientRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T GetByLoginAndPass(string login, string pass);
        void Add(T item);
        void Update(T item);
        void Delete(int id);
    }
}
