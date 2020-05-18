using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class LoginViewModel
    {
        [DisplayName("İstifadəçi adı"), Required(ErrorMessage = "{0} boş olabilməz."), StringLength(25, ErrorMessage = "{0} max. {1} simvol olmalıdır.")]
        public string Username { get; set; }

        [DisplayName("Şifrə"), Required(ErrorMessage = "{0} boş olabilməz."), DataType(DataType.Password), StringLength(25, ErrorMessage = "{0} max. {1} simvol olmalıdır.")]
        public string Password { get; set; }
    }
}
