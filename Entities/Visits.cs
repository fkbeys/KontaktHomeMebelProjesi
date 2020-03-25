using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("_Visits")]
    public class Visits
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VisitID { get; set; }      
        public Guid VisitGuid { get; set; }       
        public DateTime CreateOn { get; set; }       
        public string CreateUser { get; set; }       
        public DateTime LastUpdate { get; set; }       
        public string UpdateUser { get; set; }       
        public int OrderId { get; set; }     
        public double DWidth { get; set; }
        public double DLenght { get; set; }
        public double DHeight { get; set; }        
        public string MaterialType { get; set; }
        public string MaterialColour { get; set; }
        public string PanelType { get; set; }
        public string PanelColour { get; set; }
        public string Accessory { get; set; }
        public string Mirror { get; set; }
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string DoorType { get; set; }
        [Required]
        public string DoorColour { get; set; }


    }
}
