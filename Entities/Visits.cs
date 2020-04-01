using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressiveAnnotations.Attributes;

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
        [Required(ErrorMessage = "En yazılmayıb")]
        public double DWidth { get; set; }
        [Required(ErrorMessage = "Uzunluq yazılmayıb")]
        public double DLenght { get; set; }
        [Required(ErrorMessage = "Hündürlük yazılmayıb")]
        public double DHeight { get; set; }
        [RequiredIf("ProductCode!=null", ErrorMessage ="Material növü seçilməyib")]
        public string MaterialType { get; set; }
        [RequiredIf("MaterialType!=null", ErrorMessage = "Material rəngi yazılmayıb")]
        public string MaterialColour { get; set; }
        public string PanelType { get; set; }
        [RequiredIf("PanelType!=null", ErrorMessage = "Dəzgahlıq rəngi yazılmayıb")]
        public string PanelColour { get; set; }
        public string Accessory { get; set; }
        public string Mirror { get; set; }
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string DoorType { get; set; }
        [RequiredIf("DoorType!=null", ErrorMessage = "Qapi rengi yazılmayıb")]
        public string DoorColour { get; set; }
        public double Price { get; set; }
        [DisplayName("Razılaşmadı")]
        public bool IsDeclined { get; set; }
        [DataType(DataType.MultilineText)]
        public string DeclineReason { get; set; }


    }
}
