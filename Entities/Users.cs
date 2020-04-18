using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Entities
{
    [Table("_Users")]
    public class Users
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }       
        [Required, StringLength(50)]
        public string UserName { get; set; }
        [StringLength(50)]
        public string UserPassword { get; set; }
        [Required, StringLength(50)]
        public string UserDisplayName { get; set; }
        [Required]
        [DisplayName("Admin")]
        public bool IsAdmin { get; set; }
        [Required]
        [DisplayName("Vizitor")]
        public bool IsVisitor { get; set; }
        [Required]
        [DisplayName("Aktiv")]
        public bool IsActive { get; set; }
        [StringLength(50)]
        public string StoreCode { get; set; }
        [StringLength(50)]
        public string StoreName { get; set; }
        [Required]
        [DisplayName("Satıcı")]
        public bool IsSeller { get; set; }
        [Required]
        [DisplayName("Kordinator")]
        public bool IsCord { get; set; }
        [Required]
        [DisplayName("Dizayner")]
        public bool IsDesigner { get; set; }
        public int EditDate { get; set; }

        public IEnumerable<SelectListItem> myADUsers { get; set; }
    }
}
