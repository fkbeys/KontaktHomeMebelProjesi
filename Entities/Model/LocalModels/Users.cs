﻿using Entities.Model;
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
        [Required(ErrorMessage ="İstifadəçi kodu seçilməyib"), StringLength(50)]
        public string UserName { get; set; }
        [StringLength(50)]
        public string UserPassword { get; set; }
        [Required(ErrorMessage = "İstifadəçi adı seçilməyib"), StringLength(50)]
        public string UserDisplayName { get; set; }     
        [Required]
        [DisplayName("Aktiv")]
        public bool IsActive { get; set; }
        [StringLength(50)]
        public string StoreCode { get; set; }
        [StringLength(50)]
        public string StoreName { get; set; }  
        public int EditDate { get; set; }

        public IEnumerable<SelectListItem> myADUsers { get; set; }

        public List<UserRolesMapping> UserRolesMappings { get; set; }
    }
}
