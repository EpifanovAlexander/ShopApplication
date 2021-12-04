using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedievalShop.DAL.Entities
{
    [Table("ClientTypes")]
    public class ClientTypeEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<ClientEntity> Clients { get; set; }
    }
}
