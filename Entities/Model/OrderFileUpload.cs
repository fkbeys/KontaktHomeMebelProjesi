using Entities.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Entities
{
    public class OrderFileUpload
    {
        //[Required(ErrorMessage = "Please select file.")]
        [Display(Name = "Fayl Seç")]
        [AllowFileSize(FileSize = 10 * 1024 * 1024, ErrorMessage = "Maksimum icazə verilən ölçü 5 MB")]
        public HttpPostedFileBase[] orderFiles { get; set; }
    }
}
