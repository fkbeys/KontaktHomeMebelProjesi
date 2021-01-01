using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model.LocalModels
{
    [Table("_Production")]
    public class Production
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ProductCode { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public double ProductPrice { get; set; }
        [Required]
        public double ProductQuantity { get; set; }
        [Required]
        public double ProductTotal { get; set; }
        [Required]
        public double ProductCharges { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int VisitId { get; set; }
        public double DocSum { get; set; }
        public double DocTotal { get; set; }
        public DateTime CreateOn { get; set; }
        public string CreateUser { get; set; }
        public DateTime LastupDate { get; set; }
        public string LastupUser { get; set; }
        public string ProductType { get; set; }

    }
}
