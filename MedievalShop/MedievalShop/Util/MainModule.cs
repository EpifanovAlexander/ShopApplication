using Ninject.Modules;
using MedievalShop.BLL.Services;
using MedievalShop.BLL.Interfaces;

namespace MedievalShop.Util
{
    public class MainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IClientService>().To<ClientService>();
            Bind<IItemService>().To<ItemService>();
            Bind<IPurchaseService>().To<PurchaseService>();
        }
    }
}