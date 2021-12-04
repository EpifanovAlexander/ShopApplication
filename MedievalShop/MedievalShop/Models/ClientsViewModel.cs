using System.Collections.Generic;
using System.Web.Mvc;

namespace MedievalShop.Models
{
    public class ClientsViewModel
    {
        public IEnumerable<ClientViewModel> Clients { get; set; }
        public SelectList Types { get; set; }
    }
}