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
    public class ItemService : IItemService
    {
        IUnitOfWork Database { get; set; }
        public ItemService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void AddItem(ItemDTO itemDto)
        {
            ItemEntity item = new ItemEntity
            {
                Name = itemDto.Name,
                DisplayName = itemDto.DisplayName,
                Price = itemDto.Price,
                ItemType = itemDto.ItemType
            };
            Database.Items.Add(item);
            Database.Save();
        }

        public void UpdateItem(ItemDTO itemDto)
        {
            ItemEntity item = new ItemEntity
            {
                Id = itemDto.Id,
                Name = itemDto.Name,
                DisplayName = itemDto.DisplayName,
                Price = itemDto.Price,
                ItemType = itemDto.ItemType
            };
            Database.Items.Update(item);
            Database.Save();
        }

        public void DeleteItem(int? id)
        {
            if (id == null) throw new ValidationException("Не определён id предмета", "");
            Database.Items.Delete(id.Value);
            Database.Save();
        }

        public ItemDTO GetItem(int? id)
        {
            if (id == null) throw new ValidationException("Не определён id предмета", "");
            var item = Database.Items.Get(id.Value);
            if (item == null) throw new ValidationException("Предмет не найден", "");
            string type = Database.ItemTypes.GetTypeText(item.ItemType);
            return new ItemDTO { Id = item.Id, Name = item.Name, DisplayName = item.DisplayName, ItemType = item.ItemType, Price = item.Price, ItemTypeText = type };
        }

        public IEnumerable<ItemDTO> GetItems()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ItemEntity, ItemDTO>()).CreateMapper();
            IEnumerable <ItemDTO> items = mapper.Map<IEnumerable<ItemEntity>, List<ItemDTO>>(Database.Items.GetAll());
            foreach (var item in items)
            {
                item.ItemTypeText = Database.ItemTypes.GetTypeText(item.ItemType);
            }
            return items;
        }

        public IEnumerable<ItemTypeDTO> GetItemTypes()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ItemTypeEntity, ItemTypeDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<ItemTypeEntity>, List<ItemTypeDTO>>(Database.ItemTypes.GetAll());
        }

        public void Dispose()
        {
            Database.Dispose();
        }

    }
}
