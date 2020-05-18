using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    [Table("_UserRolesMapping")]
    public class UserRolesMapping
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
    }
}
