using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model.LocalModels
{
    [Table("_StockInfo")]
    public class StockInfo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int VisitNo { get; set; }
        [Required]
        public int OrderNo { get; set; }
        [Required]
        [StringLength(100)]
        public string StockName { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int SendStatus { get; set; }
        [Required]
        public int PaymentStatus { get; set; }
        [StringLength(100)]
        public string PaymentNo { get; set; }
    }
}
