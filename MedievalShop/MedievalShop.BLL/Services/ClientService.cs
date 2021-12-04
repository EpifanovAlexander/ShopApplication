using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedievalShop.BLL.Interfaces;
using MedievalShop.BLL.DTO;
using MedievalShop.BLL.Infrastructure;
using MedievalShop.DAL.Entities;
using MedievalShop.DAL.Interfaces;
using AutoMapper;

namespace MedievalShop.BLL.Services
{
    public class ClientService : IClientService
    {
        IUnitOfWork Database { get; set; }
        public ClientService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void AddClient(ClientDTO clientDto)
        {
            ClientEntity client = Database.Clients.GetByLoginAndPass(clientDto.Login, clientDto.Password);
            if (client != null) throw new Exception("Пользователь с таким логином уже есть в базе!");
            client = new ClientEntity
            {
                Name = clientDto.Name,
                Login = clientDto.Login,
                Password = clientDto.Password,
                ClientType = clientDto.ClientType
            };
            Database.Clients.Add(client);
            Database.Save();
        }

        public void UpdateClient(ClientDTO clientDto)
        {
            ClientEntity client = Database.Clients.GetByLoginAndPass(clientDto.Login, clientDto.Password);
            if (client != null) throw new Exception("Пользователь с таким логином уже есть в базе!");
            client = new ClientEntity
            {
                Id = clientDto.Id,
                Name = clientDto.Name,
                Login = clientDto.Login,
                Password = clientDto.Password,
                ClientType = clientDto.ClientType
            };
            Database.Clients.Update(client);
            Database.Save();
        }

        public void DeleteClient(int? id)
        {
            if (id == null) throw new ValidationException("Не определён id клиента", "");
            Database.Clients.Delete(id.Value);
            Database.Save();
        }

        public ClientDTO GetClient(int? id)
        {
            if (id == null) throw new ValidationException("Не определён id клиента", "");
            var client = Database.Clients.Get(id.Value);
            if (client == null) throw new ValidationException("Клиент не найден", "");
            string type = Database.ClientTypes.GetTypeText(client.ClientType);
            return new ClientDTO { Id = client.Id, Name = client.Name, Login = client.Login, Password = client.Password, ClientType = client.ClientType, ClientTypeText = type };
        }

        public ClientDTO GetClientByLoginAndPass(string login, string pass)
        {
            var client = Database.Clients.GetByLoginAndPass(login, pass);
            if (client == null) throw new ValidationException("Клиент не найден", "");
            string type = Database.ClientTypes.GetTypeText(client.ClientType);
            return new ClientDTO { Id = client.Id, Name = client.Name, Login = client.Login, Password = client.Password, ClientType = client.ClientType, ClientTypeText = type };
        }

        public IEnumerable<ClientDTO> GetClients()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientEntity, ClientDTO>()).CreateMapper();
            IEnumerable<ClientDTO> clients = mapper.Map<IEnumerable<ClientEntity>, List<ClientDTO>>(Database.Clients.GetAll());
            foreach (var client in clients)
            {
                client.ClientTypeText = Database.ClientTypes.GetTypeText(client.ClientType);
            }
            return clients;
        }

        public IEnumerable<ClientTypeDTO> GetClientTypes()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientTypeEntity, ClientTypeDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<ClientTypeEntity>, List<ClientTypeDTO>>(Database.ClientTypes.GetAll());
        }

        public void Dispose()
        {
            Database.Dispose();
        }
       
    }
}
