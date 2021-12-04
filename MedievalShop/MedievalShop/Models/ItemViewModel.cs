using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace MedievalShop.Models
{
    public class ItemViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть задано")]
        public string Name { get; set; }

        [Display(Name = "Название предмета")]
        [Required(ErrorMessage = "Поле должно быть задано")]
        public string DisplayName { get; set; }

        [Display(Name = "Цена предмета")]
        [Required(ErrorMessage = "Поле должно быть задано")]
        public int Price { get; set; }

        public int ItemType { get; set; }

        public string ItemTypeText { get; set; }
    }
}