﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    [Table("_UserRoles")]
    public class UserRoles
    {
        [Key]
        public int ID { get; set; }
        public string RoleName { get; set; }

        public List<UserRolesMapping> UserRolesMappings { get; set; }
    }
}
