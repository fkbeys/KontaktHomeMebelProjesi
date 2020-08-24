using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("_Stores")]
    public class Stores
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StoreID { get; set; }
        [Required]
        [MaxLength(50)]
        public string StoreCode { get; set; }
        [Required]
        [MaxLength(100)]
        public string StoreName { get; set; }
        public bool IsActive { get; set; }
    }
}
