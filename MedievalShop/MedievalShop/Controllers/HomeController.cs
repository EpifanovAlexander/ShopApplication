using MedievalShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using MedievalShop.BLL.Infrastructure;
using MedievalShop.BLL.Interfaces;
using MedievalShop.BLL.DTO;


namespace MedievalShop.Controllers
{
    public class HomeController : Controller
    {
        IClientService clientService;
        IItemService itemService;
        IPurchaseService purchaseService;

        public HomeController(IClientService clientServ, IItemService itemServ, IPurchaseService purchaseServ)
        {
            clientService = clientServ;
            itemService = itemServ;
            purchaseService = purchaseServ;
        }

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ClientViewModel client)
        {
            try
            {
                ClientDTO clientDto = clientService.GetClientByLoginAndPass(client.Login, client.Password);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientViewModel>()).CreateMapper();
                client = mapper.Map<ClientDTO, ClientViewModel>(clientDto);
                FormsAuthentication.SetAuthCookie(client.Name, true);
                HttpContext.Response.Cookies["clientType"].Value = client.ClientTypeText;
                Session["clientId"] = client.Id;
                return RedirectToAction("GetAllItems", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(client);
            }
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(ClientViewModel client)
        {
            try
            {
                client.ClientType = 2;
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientViewModel, ClientDTO>()).CreateMapper();
                ClientDTO clientDto = mapper.Map<ClientViewModel, ClientDTO>(client);
                clientService.AddClient(clientDto);
                FormsAuthentication.SetAuthCookie(client.Name, true);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(client);
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }





        [Authorize]
        public ActionResult GetAllItems(string type, int? price, int page = 1)
        {
            ViewBag.Page = page;
            int maxPrice = 500000;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.PriceFilter = maxPrice;
            ViewBag.Type = "Любой";
            int pageSize = 3;

            IEnumerable<ItemDTO> itemDtos = itemService.GetItems();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ItemDTO, ItemViewModel>()).CreateMapper();
            IEnumerable<ItemViewModel> items = mapper.Map<IEnumerable<ItemDTO>, IEnumerable<ItemViewModel>>(itemDtos);

            IEnumerable<ItemTypeDTO> itemTypeDtos = itemService.GetItemTypes();
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<ItemTypeDTO, ItemTypeViewModel>()).CreateMapper();
            List<ItemTypeViewModel> types = mapper.Map<IEnumerable<ItemTypeDTO>, List<ItemTypeViewModel>>(itemTypeDtos);
            types.Insert(0, new ItemTypeViewModel { Id = 0, Name = "Любой" });
            SelectList selectTypes = new SelectList(types, "Name", "Name");

            if (price != null && price > 0)
            {
                items = items.Where(i => i.Price <= price);
                ViewBag.PriceFilter = price;
            }
            if (!String.IsNullOrEmpty(type) && !type.Equals("Любой"))
            {
                items = items.Where(i => i.ItemTypeText.Equals(type));
                ViewBag.Type = type;
                selectTypes.Where(t => t.Value == type).First().Selected = true;
            }

            if (items.Count() <= pageSize)
            {
                page = 1;
            }

            IEnumerable<ItemViewModel> itemsPerPages = items
                .OrderBy(item => item.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
   
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = items.Count() };
            ItemsViewModel ivm = new ItemsViewModel { PageInfo = pageInfo, Items = itemsPerPages, Types = selectTypes };

            return View(ivm);
        }




        [Authorize]
        public ActionResult GetAllClients(string type)
        {
            if (HttpContext.Request.Cookies["clientType"].Value != "Admin")
            {
                return RedirectToAction("Index");
            }

            IEnumerable<ClientDTO> clientDtos = clientService.GetClients();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientViewModel>()).CreateMapper();
            var clients = mapper.Map<IEnumerable<ClientDTO>, List<ClientViewModel>>(clientDtos);

            IEnumerable<ClientTypeDTO> clientTypeDtos = clientService.GetClientTypes();
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientTypeDTO, ClientTypeViewModel>()).CreateMapper();
            List<ClientTypeViewModel> types = mapper.Map<IEnumerable<ClientTypeDTO>, List<ClientTypeViewModel>>(clientTypeDtos);
            types.Insert(0, new ClientTypeViewModel { Id = 0, Name = "Любой" });
            SelectList selectTypes = new SelectList(types, "Name", "Name");

            if (!String.IsNullOrEmpty(type) && !type.Equals("Любой"))
            {
                clients = clients.Where(c => c.ClientTypeText.Equals(type)).ToList();
                selectTypes.Where(t => t.Value == type).First().Selected = true;
            }

            ClientsViewModel cvm = new ClientsViewModel { Clients = clients, Types = selectTypes };
            return View(cvm);
        }



        [Authorize]
        [HttpGet]
        public ActionResult CreateItem()
        {
            if (HttpContext.Request.Cookies["clientType"].Value != "Admin")
            {
                return RedirectToAction("Index");
            }

            IEnumerable<ItemTypeDTO> itemTypeDtos = itemService.GetItemTypes();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ItemTypeDTO, ItemTypeViewModel>()).CreateMapper();
            List<ItemTypeViewModel> types = mapper.Map<IEnumerable<ItemTypeDTO>, List<ItemTypeViewModel>>(itemTypeDtos);
            ViewBag.ItemTypes = new SelectList(types, "Id", "Name");
            return View();
        }


        [Authorize]
        [HttpPost]
        public ActionResult CreateItem(ItemViewModel item)
        {
            if (HttpContext.Request.Cookies["clientType"].Value != "Admin")
            {
                return RedirectToAction("Index");
            }

            IEnumerable<ItemTypeDTO> itemTypeDtos = itemService.GetItemTypes();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ItemTypeDTO, ItemTypeViewModel>()).CreateMapper();
            List<ItemTypeViewModel> types = mapper.Map<IEnumerable<ItemTypeDTO>, List<ItemTypeViewModel>>(itemTypeDtos);
            ViewBag.ItemTypes = new SelectList(types, "Id", "Name");

            try
            {
                mapper = new MapperConfiguration(cfg => cfg.CreateMap<ItemViewModel, ItemDTO>()).CreateMapper();
                ItemDTO itemDto = mapper.Map<ItemViewModel, ItemDTO>(item);
                itemService.AddItem(itemDto);
                return RedirectToAction("GetAllItems");
            }
            catch (Exception)
            {
                return View(item);
            }
        }



        [Authorize]
        [HttpGet]
        public ActionResult EditItem(int? id)
        {
            if (HttpContext.Request.Cookies["clientType"].Value != "Admin")
            {
                return RedirectToAction("Index");
            }

            try
            {
                IEnumerable<ItemTypeDTO> itemTypeDtos = itemService.GetItemTypes();
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ItemTypeDTO, ItemTypeViewModel>()).CreateMapper();
                List<ItemTypeViewModel> types = mapper.Map<IEnumerable<ItemTypeDTO>, List<ItemTypeViewModel>>(itemTypeDtos);
                ViewBag.ItemTypes = new SelectList(types, "Id", "Name");

                ItemDTO itemDto = itemService.GetItem(id);
                mapper = new MapperConfiguration(cfg => cfg.CreateMap<ItemDTO, ItemViewModel>()).CreateMapper();
                ItemViewModel item = mapper.Map<ItemDTO, ItemViewModel>(itemDto);
                return View(item);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditItem(ItemViewModel item)
        {
            if (HttpContext.Request.Cookies["clientType"].Value != "Admin")
            {
                return RedirectToAction("Index");
            }

            try
            {
                IEnumerable<ItemTypeDTO> itemTypeDtos = itemService.GetItemTypes();
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ItemTypeDTO, ItemTypeViewModel>()).CreateMapper();
                List<ItemTypeViewModel> types = mapper.Map<IEnumerable<ItemTypeDTO>, List<ItemTypeViewModel>>(itemTypeDtos);
                ViewBag.ItemTypes = new SelectList(types, "Id", "Name");

                mapper = new MapperConfiguration(cfg => cfg.CreateMap<ItemViewModel, ItemDTO>()).CreateMapper();
                ItemDTO itemDto = mapper.Map<ItemViewModel, ItemDTO>(item);
                itemService.UpdateItem(itemDto);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            return View(item);
        }



        [Authorize]
        public ActionResult EditItemImage(ItemViewModel item)
        {
            if (HttpContext.Request.Cookies["clientType"].Value != "Admin")
            {
                return RedirectToAction("Index");
            }

            IEnumerable<ItemTypeDTO> itemTypeDtos = itemService.GetItemTypes();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ItemTypeDTO, ItemTypeViewModel>()).CreateMapper();
            List<ItemTypeViewModel> types = mapper.Map<IEnumerable<ItemTypeDTO>, List<ItemTypeViewModel>>(itemTypeDtos);
            ViewBag.ItemTypes = new SelectList(types, "Id", "Name");
            return View("EditItem", item);
        }



        [Authorize]
        [HttpGet]
        public ActionResult DeleteItem(int id)
        {
            if (HttpContext.Request.Cookies["clientType"].Value != "Admin")
            {
                return RedirectToAction("Index");
            }

            try
            {
                ItemDTO itemDto = itemService.GetItem(id);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ItemDTO, ItemViewModel>()).CreateMapper();
                ItemViewModel item = mapper.Map<ItemDTO, ItemViewModel>(itemDto);
                return View(item);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [Authorize]
        [HttpPost, ActionName("DeleteItem")]
        public ActionResult DeleteItemConfirmed(int id)
        {
            if (HttpContext.Request.Cookies["clientType"].Value != "Admin")
            {
                return RedirectToAction("Index");
            }

            try
            {
                itemService.DeleteItem(id);
                return RedirectToAction("GetAllItems");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }



        [Authorize]
        [HttpGet]
        public ActionResult EditClient(int? id)
        {
            if (HttpContext.Request.Cookies["clientType"].Value != "Admin")
            {
                return RedirectToAction("Index");
            }

            try
            {
                IEnumerable<ClientTypeDTO> clientTypeDtos = clientService.GetClientTypes();
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientTypeDTO, ClientTypeViewModel>()).CreateMapper();
                List<ClientTypeViewModel> types = mapper.Map<IEnumerable<ClientTypeDTO>, List<ClientTypeViewModel>>(clientTypeDtos);
                ViewBag.ClientTypes = new SelectList(types, "Id", "Name");

                ClientDTO clientDto = clientService.GetClient(id);
                mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientViewModel>()).CreateMapper();
                ClientViewModel client = mapper.Map<ClientDTO, ClientViewModel>(clientDto);
                return View(client);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }


        [Authorize]
        [HttpPost]
        public ActionResult EditClient(ClientViewModel client)
        {
            if (HttpContext.Request.Cookies["clientType"].Value != "Admin")
            {
                return RedirectToAction("Index");
            }

            try
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientViewModel, ClientDTO>()).CreateMapper();
                ClientDTO clientDto = mapper.Map<ClientViewModel, ClientDTO>(client);
                clientService.UpdateClient(clientDto);
                return RedirectToAction("GetAllClients");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(client);
            }
        }



        [Authorize]
        [HttpGet]
        public ActionResult DeleteClient(int id)
        {
            if (HttpContext.Request.Cookies["clientType"].Value != "Admin")
            {
                return RedirectToAction("Index");
            }

            try
            {
                ClientDTO clientDto = clientService.GetClient(id);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientViewModel>()).CreateMapper();
                ClientViewModel client = mapper.Map<ClientDTO, ClientViewModel>(clientDto);
                return View(client);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }


        [Authorize]
        [HttpPost, ActionName("DeleteClient")]
        public ActionResult DeleteClientConfirmed(int id)
        {
            if (HttpContext.Request.Cookies["clientType"].Value != "Admin")
            {
                return RedirectToAction("Index");
            }

            try
            {
                clientService.DeleteClient(id);
                return RedirectToAction("GetAllClients");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }


        [Authorize]
        public ActionResult BuyItem(PurchaseViewModel purchase)
        {
            try
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PurchaseViewModel, PurchaseDTO>()).CreateMapper();
                PurchaseDTO purchaseDto = mapper.Map<PurchaseViewModel, PurchaseDTO>(purchase);
                purchaseService.AddPurchase(purchaseDto);
                return RedirectToAction("GetAllItems");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }



        [Authorize]
        public ActionResult GetPurchases(int page = 1)
        {
            if (HttpContext.Request.Cookies["clientType"].Value != "User")
            {
                return RedirectToAction("Index");
            }

            int clientId = Convert.ToInt32(Session["clientId"]);
            ClientDTO clientDto = clientService.GetClient(clientId);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientViewModel>()).CreateMapper();
            ClientViewModel currentClient = mapper.Map<ClientDTO, ClientViewModel>(clientDto);

            IEnumerable<PurchaseDTO> purchasesDtos = purchaseService.GetPurchases().Where(p => p.Client.Id == clientId);
            var mapperItem = new MapperConfiguration(cfg => cfg.CreateMap<ItemDTO, ItemViewModel>()).CreateMapper();
            var mapperClient = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientViewModel>()).CreateMapper();
            List<PurchaseViewModel> purchases = new List<PurchaseViewModel>();
            foreach (var purchasesDto in purchasesDtos)
            {
                ClientViewModel client = mapperClient.Map<ClientDTO, ClientViewModel>(purchasesDto.Client);
                ItemViewModel item = mapperItem.Map<ItemDTO, ItemViewModel>(purchasesDto.Item);
                purchases.Add(new PurchaseViewModel()
                {
                    PurchaseId = purchasesDto.PurchaseId,
                    Date = purchasesDto.Date,
                    ClientId = purchasesDto.ClientId,
                    ItemId = purchasesDto.ItemId,
                    Client = client,
                    Item = item
                });
            }

            int pageSize = 3;
            IEnumerable<PurchaseViewModel> purchasesPerPages = purchases
                .OrderBy(p => p.PurchaseId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = purchases.Count() };
            PurchasesViewModel pvm = new PurchasesViewModel { PageInfo = pageInfo, Client = currentClient, Purchases = purchasesPerPages };
            return View(pvm);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}