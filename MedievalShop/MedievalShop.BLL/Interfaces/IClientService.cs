using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedievalShop.BLL.DTO;

namespace MedievalShop.BLL.Interfaces
{
    public interface IClientService
    {
        void AddClient(ClientDTO clientDto);
        void UpdateClient(ClientDTO clientDto);
        void DeleteClient(int? id);
        ClientDTO GetClient(int? id);
        ClientDTO GetClientByLoginAndPass(string login, string pass);
        IEnumerable<ClientDTO> GetClients();
        IEnumerable<ClientTypeDTO> GetClientTypes();
        void Dispose();
    }
}
