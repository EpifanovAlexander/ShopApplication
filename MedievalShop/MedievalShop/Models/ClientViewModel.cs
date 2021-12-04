using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace MedievalShop.Models
{
    public class ClientViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Имя клиента")]
        [Required(ErrorMessage = "Поле должно быть задано")]
        public string Name { get; set; }

        [Display(Name = "Логин клиента")]
        [Required(ErrorMessage = "Поле должно быть задано")]
        public string Login { get; set; }

        [Display(Name = "Пароль клиента")]
        [Required(ErrorMessage = "Поле должно быть задано")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int ClientType { get; set; }

        public string ClientTypeText { get; set; }
    }
}