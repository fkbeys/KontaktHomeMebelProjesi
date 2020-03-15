using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("_Orders")]
    public class Orders
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        [Required]
        public DateTime CreateOn { get; set; }
        [Required]
        public string CreateUser { get; set; }
        [Required]
        public DateTime LastUpdate { get; set; }
        [Required]
        public string UpdateUser { get; set; }
        [Required,StringLength(50)]
        public string CustomerName { get; set; }
        [Required,StringLength(50)]
        public string CustomerSurname { get; set; }
        [Required,StringLength(50)]
        public string CustomerFatherName { get; set; }
        [Required]
        public string SellerCode { get; set; }
        [Required]
        public int Tel1 { get; set; }
        [Required]
        public int Tel2 { get; set; }
        [Required,StringLength(250)]
        public string Address { get; set; }
        [Required]
        public DateTime VisitDate { get; set; }
        [Required]
        public string OrderType { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required,StringLength(200)]
        public string Note { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int OrderStatus { get; set; }

    }
}
