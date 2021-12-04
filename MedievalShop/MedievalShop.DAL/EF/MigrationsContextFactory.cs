using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalShop.DAL.EF
{
    public class MigrationsContextFactory : IDbContextFactory<ShopContext>
    {
        public ShopContext Create()
        {
            return new ShopContext(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\Мои вещи\Проги\AspMvcProject\MedievalShop\MedievalShop\App_Data\ShopDB.mdf'; Integrated Security=True");
        }
    }
}
