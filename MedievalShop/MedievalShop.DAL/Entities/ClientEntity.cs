using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedievalShop.DAL.Entities
{
    [Table("Clients")]
    public class ClientEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public int ClientType { get; set; }

        [ForeignKey("ClientType")]
        public ClientTypeEntity Type { get; set; }

        public ICollection<PurchaseEntity> Purchases { get; set; }
    }
}
