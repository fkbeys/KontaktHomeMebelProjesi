﻿using Entities.Helper;
using ExpressiveAnnotations.Attributes;
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
    [Table("_Orders")]
    public class Orders
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        [Required]
        public DateTime CreateOn { get; set; }

        public string CreateUser { get; set; }
        [Required]
        public DateTime LastUpdate { get; set; }

        public string UpdateUser { get; set; }

        [DisplayName("Müştəri Adı")]
        [Required(ErrorMessage = "Müştəri adı boş olabilməz"), StringLength(50, ErrorMessage = "Müştəri adı 50 simvoldan yuxarı olabilməz")]
        public string CustomerName { get; set; }

        [DisplayName("Müştəri Soyadı")]
        [Required(ErrorMessage = "Müştəri soyadı boş olabilməz"), StringLength(50, ErrorMessage = "Müştəri soyadı 50 simvoldan yuxarı olabilməz")]
        public string CustomerSurname { get; set; }

        [DisplayName("Müştəri Ataadı")]
        [Required(ErrorMessage = "Müştəri ataadı boş olabilməz"), StringLength(50, ErrorMessage = "Müştəri ataadı 50 simvoldan yuxarı olabilməz")]
        public string CustomerFatherName { get; set; }

        [DisplayName("Satıcı Kodu")]
        public string SellerCode { get; set; }

        [DisplayName("Telefon 1")]
        [Required(ErrorMessage = "Telefon nömrəsi boş olabilməz")]
        public string Tel1 { get; set; }

        [DisplayName("Telefon 2")]
        [Required(ErrorMessage = "Telefon nömrəsi boş olabilməz")]
        public string Tel2 { get; set; }
        [Required(ErrorMessage = "Ünvan boş olabiləz"), StringLength(250, ErrorMessage = "Ünvan 250 simvoldan artıq olabilməz")]
        public string Address { get; set; }

        [DisplayName("Vizit Tarixi")]
        [Required(ErrorMessage = "Vizit tarixi boş olabilməz")]
        public DateTime VisitDate { get; set; }

        [DisplayName("Mətbəx")]
        public bool OrderType1 { get; set; }
        [DisplayName("Ofis Mebeli")]
        public bool OrderType2 { get; set; }
        [DisplayName("Ev Mebeli")]
        public bool OrderType3 { get; set; }

        [Required(ErrorMessage ="Qiymət boş olabilməz")]
        public double Price { get; set; }

        [StringLength(200, ErrorMessage = "Qeyd 200 simvoldan artıq olabilməz"), DataType(DataType.MultilineText)]
        public string Note { get; set; }

        public bool IsActive { get; set; }

        public int OrderStatus { get; set; }
        [StringLength(50)]
        public string VisitorCode { get; set; }
        [StringLength(50)]
        public string VisitorName { get; set; }
        [DisplayName("Vizitor Təyin Et")]
        public bool IsVisitorAdded { get; set; }

        [StringLength(50), DisplayName("Nişangah")]
        public string Location { get; set; }
        [DisplayName("Məhsul Sayı")]
        [Required(ErrorMessage = "Məhsul sayı boş olabilməz")]
        [Range(1, 20, ErrorMessage = "Məhsul sayı 15-dən yuxarı olabilməz")]
        public double ItemCount { get; set; }

        [DisplayName("Məhsul Qeyd")]
        [StringLength(150,ErrorMessage ="Qeyd 150 simvoldan Çox olabilməz")]
        public string ItemDescription { get; set; }
        public string OrderStore { get; set; }
        public int VisitorStatus { get; set; }
        [DisplayName("Dizayner Təyin Et")]
        public bool IsDesignerAdded { get; set; }
        [StringLength(50)]
        public string DesignerCode { get; set; }
        [StringLength(50)]
        public string DesignerName { get; set; }
        public int DesignerStatus { get; set; }
        [DataType(DataType.MultilineText)]
        public string CloseReason { get; set; }
        [DisplayName("Müştəri Xəbər Edəcək")]
        public bool CustomerWillAnswer { get; set; }
        [DisplayName("Tamamla")]
        public bool IsCompleted { get; set; }

        [DisplayName("Faktura Nömrəsi")]
        [StringLength(20, ErrorMessage = "Faktura nömrəsi 20 simvoldan Çox olabilməz")]

        [CustomValidation.RequiredIf("IsCompleted", "True", ErrorMessage = "Faktura nömrəsi boş olabilməz")]
        public string InvoiceNo { get; set; } = "0";
        public int PlannerStatus { get; set; }
        [DisplayName("Planlamacı Təyin Et")]
        public bool IsPlannerAdded { get; set; }
        [StringLength(50)]
        public string PlannerCode { get; set; }
        [StringLength(50)]
        public string PlannerName { get; set; }



    }
}
