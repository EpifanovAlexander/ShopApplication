using System.Collections.Generic;
using System.Web.Mvc;

namespace MedievalShop.Models
{
    public class ItemsViewModel
    {
        public IEnumerable<ItemViewModel> Items { get; set; }
        public PageInfo PageInfo { get; set; }
        public SelectList Types { get; set; }
    }
}