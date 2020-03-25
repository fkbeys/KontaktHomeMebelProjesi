using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("_Users")]
    public class Users
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }       
        [Required, StringLength(50)]
        public string UserName { get; set; }
        [Required, StringLength(50)]
        public string UserPassword { get; set; }
        [Required, StringLength(50)]
        public string UserDisplayName { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
        [Required]
        public bool IsVisitor { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [StringLength(50)]
        public string StoreCode { get; set; }
        [StringLength(50)]
        public string StoreName { get; set; }
    }
}
