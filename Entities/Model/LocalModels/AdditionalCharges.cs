using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model.LocalModels
{
    [Table("_Charges")]
    public class AdditionalCharges
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string  charge_name { get; set; }
        [Required]
        public double charge_value { get; set; }
    }
}
