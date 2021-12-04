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
    public class PurchaseService : IPurchaseService
    {
        IUnitOfWork Database { get; set; }
        public PurchaseService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void AddPurchase(PurchaseDTO purchaseDto)
        {
            ClientEntity client = Database.Clients.Get(purchaseDto.ClientId);
            if (client == null) throw new ValidationException("Клиент не найден", "");

            ItemEntity item = Database.Items.Get(purchaseDto.ItemId);
            if (item == null) throw new ValidationException("Товар не найден", "");

            PurchaseEntity purchase = new PurchaseEntity
            {
                Date = DateTime.Now,
                PurchaseId = purchaseDto.PurchaseId,
                ClientId = purchaseDto.ClientId,
                ItemId = purchaseDto.ItemId
            };
            Database.Purchases.Add(purchase);
            Database.Save();
        }

        public PurchaseDTO GetPurchase(int? id)
        {
            if (id == null) throw new ValidationException("Не установлено id покупки", "");
            var purchase = Database.Purchases.Get(id.Value);
            if (purchase == null) throw new ValidationException("Покупка не найдена", "");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientEntity, ClientDTO>()).CreateMapper();
            ClientDTO client = mapper.Map<ClientEntity, ClientDTO>(Database.Clients.Get(purchase.ClientId));

            mapper = new MapperConfiguration(cfg => cfg.CreateMap<ItemEntity, ItemDTO>()).CreateMapper();
            ItemDTO item = mapper.Map<ItemEntity, ItemDTO>(Database.Items.Get(purchase.ItemId));
            return new PurchaseDTO { PurchaseId = purchase.PurchaseId, ClientId = purchase.ClientId, ItemId = purchase.ItemId, Date = purchase.Date, Client = client, Item = item };
        }


        public IEnumerable<PurchaseDTO> GetPurchases()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PurchaseEntity, PurchaseDTO>()).CreateMapper();
            IEnumerable<PurchaseDTO> purchases = mapper.Map<IEnumerable<PurchaseEntity>, List<PurchaseDTO>>(Database.Purchases.GetAll());
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientEntity, ClientDTO>()).CreateMapper();
            foreach (var purchase in purchases)
            {
                purchase.Client = mapper.Map<ClientEntity, ClientDTO>(Database.Clients.Get(purchase.ClientId));
            }
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<ItemEntity, ItemDTO>()).CreateMapper();
            foreach (var purchase in purchases)
            {
                purchase.Item = mapper.Map<ItemEntity, ItemDTO>(Database.Items.Get(purchase.ItemId));
            }
            return purchases;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

    }
}
