using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedievalShop.DAL.Entities
{
    [Table("ItemTypes")]
    public class ItemTypeEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<ItemEntity> Items { get; set; }
    }
}
