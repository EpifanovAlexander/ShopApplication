using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalShop.BLL.DTO
{
    public class ClientDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public int ClientType { get; set; }

        public string ClientTypeText { get; set; }
    }
}
