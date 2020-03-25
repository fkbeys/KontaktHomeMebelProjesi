using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("_VisitImages")]
    public class VisitImages
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImageID { get; set; }
            
        public Guid VisitGuid { get; set; }
        
        public string ImageName { get; set; }
        
        public string ImagePath { get; set; }
    }
}
