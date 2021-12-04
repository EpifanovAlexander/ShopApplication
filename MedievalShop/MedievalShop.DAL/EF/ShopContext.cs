using MedievalShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalShop.DAL.EF
{
    public class ShopContext : DbContext
    {
        public DbSet<ItemEntity> Items { get; set; }

        public DbSet<ItemTypeEntity> ItemTypes { get; set; }

        public DbSet<ClientTypeEntity> ClientTypes { get; set; }

        public DbSet<ClientEntity> Clients { get; set; }

        public DbSet<PurchaseEntity> Purchases { get; set; }

        public ShopContext(string connectionString= @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\Мои вещи\Проги\AspMvcProject\MedievalShop\MedievalShop\App_Data\ShopDB.mdf'; Integrated Security=True")
           : base(connectionString)
        {
        }
    }
}
