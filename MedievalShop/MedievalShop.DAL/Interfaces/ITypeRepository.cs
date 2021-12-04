using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalShop.DAL.Interfaces
{
    public interface ITypeRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        string GetTypeText(int id);
    }
}
