using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MedievalShop.DAL.Entities
{
    [Table("Purchases")]
    public class PurchaseEntity
    {
        [Key]
        public int PurchaseId { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public ClientEntity Client { get; set; }
        public int ItemId { get; set; }
        public ItemEntity Item { get; set; }
    }
}
