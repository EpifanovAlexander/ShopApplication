using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MedievalShop.Models
{
    public class ClientTypeViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Тип клиента")]
        public string Name { get; set; }

    }
}