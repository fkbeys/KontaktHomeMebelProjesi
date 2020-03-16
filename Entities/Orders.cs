using System;
using System.Collections.Generic;
using System.ComponentModel;
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
       
        public string CreateUser { get; set; }
        [Required]
        public DateTime LastUpdate { get; set; }
       
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
        public string Tel1 { get; set; }
        [Required]
        public string Tel2 { get; set; }
        [Required,StringLength(250)]
        public string Address { get; set; }
        [Required]
        public DateTime VisitDate { get; set; }   
        [DisplayName("Mətbəx")]
        public bool OrderType1 { get; set; }
        [DisplayName("Ofis Mebeli")]
        public bool OrderType2 { get; set; }
        [DisplayName("Ev Mebeli")]
        public bool OrderType3 { get; set; }
        [Required]
        public double Price { get; set; }
        [Required,StringLength(200), DataType(DataType.MultilineText)]       
        public string Note { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int OrderStatus { get; set; }
        public string VisitorCode { get; set; }
        public string VisitorName { get; set; }
        [DisplayName("Vizitor Təyin Et")]
        public bool IsVisitorAdded { get; set; }


        //TODO: OrderStatuslar: 1. Gözləmədə 2.VisitorTəyinEdilib


    }
}
