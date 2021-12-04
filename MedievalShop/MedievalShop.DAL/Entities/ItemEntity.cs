using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MedievalShop.DAL.Entities
{
    [Table("Items")]
    public class ItemEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public int Price { get; set; }

        public int ItemType { get; set; }

        [ForeignKey("ItemType")]
        public ItemTypeEntity Type { get; set; }

        public ICollection<PurchaseEntity> Purchases { get; set; }
    }
}
