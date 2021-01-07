using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Helper
{
    public static class TextHelpers
    {
        public static string CapitalizeFirstLetter(string text)
        {
            string textUpper = text;
            if (text.Length == 0)
            {
                return textUpper;
            }
            else if (text.Length == 1)
            {
                textUpper=char.ToUpper(text[0]).ToString();
                return textUpper;
            }
            else
            {
                textUpper=char.ToUpper(text[0])+text.Substring(1).ToLower();
                return textUpper;
            }
        }
        public static string OrdersTableTranslate(string value)
        {
            if (value == "OrderId")
            {
                return "Siafriş İd";
            }
            else if (value == "CreateOn")
            {
                return "Yaraadılma Tarixi";
            }
            else if (value == "CreateUser")
            {
                return "Yaradaan İstifadəçi";
            }
            else if (value == "LastUpdate")
            {
                return "Son Yenilənmə Tarixi";
            }
            else if (value == "UpdateUser")
            {
                return "Yeniləyən İstifadəçi";
            }
            else if (value == "CustomerName")
            {
                return "Müştəri Adı";
            }
            else if (value == "CustomerSurname")
            {
                return "Müştəri Soyadı";
            }
            else if (value == "CustomerFatherName")
            {
                return "Müştəri Ata Adı";
            }
            else if (value == "SellerCode")
            {
                return "Satıcı Kodu";
            }
            else if (value == "VisitDate")
            {
                return "Vizit Tarixi";
            }
            else if (value == "OrderType1")
            {
                return "Sifariş Növü1";
            }
            else if (value == "OrderType2")
            {
                return "Sifariş Növü2";
            }
            else if (value == "OrderType3")
            {
                return "Sifariş Növü3";
            }
            else if (value == "Price")
            {
                return "Qiymət";
            }
            else if (value == "Note")
            {
                return "Qeyd";
            }
            else if (value == "IsActive")
            {
                return "Aktiv ?";
            }
            else if (value == "OrderStatus")
            {
                return "Sifariş Statusu";
            }
            else if (value == "VisitorCode")
            {
                return "Vizitor Kodu";
            }
            else if (value == "VisitorName")
            {
                return "Vizitor Adı";
            }
            else if (value == "IsVisitorAdded")
            {
                return "Vizitor Təyin Edilibmi?";
            }
            else if (value == "Location")
            {
                return "Lokasioya";
            }
            else if (value == "ItemCount")
            {
                return "Miqdar";
            }
            else if (value == "ItemDescription")
            {
                return "Məhsul Açıqlama";
            }
            else if (value == "OrderStore")
            {
                return "Sifariş Mağazası";
            }
            else if (value == "VisitorStatus")
            {
                return "Vizitor Status";
            }
            else if (value == "IsDesignerAdded")
            {
                return "Dizayner Təyin Edilibmi?";
            }
            else if (value == "DesignerCode")
            {
                return "Dizayner Kodu";
            }
            else if (value == "DesignerName")
            {
                return "Dizayner Adı";
            }
            else if (value == "DesignerStatus")
            {
                return "Dizayner Status";
            }
            else if (value == "CloseReason")
            {
                return "Bağlanma Səbəbi";
            }
            else if (value == "CustomerWillAnswer")
            {
                return "Müştəri Xəbər Edəcək";
            }
            else if (value == "IsCompleted")
            {
                return "Tamamlanıb?";
            }
            else if (value == "InvoiceNo")
            {
                return "İnvoice Nömrəsi";
            }
            else if (value == "PlannerStatus")
            {
                return "Planlamacı Statusu";
            }
            else if (value == "IsPlannerAdded")
            {
                return "Planlamacı Təyin Edilibmi?";
            }
            else if (value == "PlannerCode")
            {
                return "Planlamacı Kodu";
            }
            else if (value == "PlannerName")
            {
                return "Planlamacı Adı";
            }
            else
            {
                return value;
            }

        }
        public static string VisitTaableTranslate(string value)
        {
            if (value == "VisitID")
            {
                return "Vizit İd";
            }
            else if (value == "CreateOn")
            {
                return "Yaraadılma Tarixi";
            }
            else if (value == "CreateUser")
            {
                return "Yaradaan İstifadəçi";
            }
            else if (value == "LastUpdate")
            {
                return "Son Yenilənmə Tarixi";
            }
            else if (value == "UpdateUser")
            {
                return "Yeniləyən İstifadəçi";
            }
            else if (value == "OrderId")
            {
                return "Sifariş İd";
            }
            else if (value == "DWidth")
            {
                return "Eni";
            }
            else if (value == "DLenght")
            {
                return "Uzunu";
            }
            else if (value == "DHeight")
            {
                return "Hündürlüyü";
            }
            else if (value == "MaterialType")
            {
                return "MAaterial Növü";
            }
            else if (value == "MaterialColour")
            {
                return "Material Rəngi";
            }
            else if (value == "PanelType")
            {
                return "Panel Növü";
            }
            else if (value == "PanelColour")
            {
                return "Panel Rəngi";
            }
            else if (value == "Accessory")
            {
                return "Aksesuar";
            }
            else if (value == "Mirror")
            {
                return "Ayna";
            }
            else if (value == "Note")
            {
                return "Qeyd";
            }
            else if (value == "ProductCode")
            {
                return "Məhsul Kodu";
            }
            else if (value == "ProductName")
            {
                return "Məhsul Adı";
            }
            else if (value == "DoorType")
            {
                return "Qapı Növü";
            }
            else if (value == "DoorColour")
            {
                return "Qapı Rəngi";
            }
            else if (value == "Price")
            {
                return "Vizit-Qiymət";
            }
            else if (value == "FinalPrice")
            {
                return "Vizit-Yekun Qiymət";
            }
            else if (value == "IsDeclined")
            {
                return "İmtina Edilibmi?";
            }
            else if (value == "DeclineReason")
            {
                return "İmtina Səbəbi";
            }
            else if (value == "VisitStatus")
            {
                return "Ziyarət Statusu";
            }
            else if (value == "ProductCode")
            {
                return "İstehsalat-Məhsul Kodu";
            }
            else if (value == "ProductName")
            {
                return "İstehsalat-Məhsul Adı";
            }
            else if (value == "ProductPrice")
            {
                return "İstehsalat-Məhsul Qiyməti";
            }
            else if (value == "ProductQuantity")
            {
                return "İstehsalat-Məhsul Miqdarı";
            }
            else if (value == "ProductTotal")
            {
                return "İstehsalat-Məhsul Məbləği";
            }
            else if (value == "ProductCharges")
            {
                return "İstehsalat-Məhsul Faizi";
            }
            else if (value == "OrderId")
            {
                return "Sifariş İd";
            }
            else if (value == "VisitId")
            {
                return "Vizit İd";
            }
            else if (value == "DocSum")
            {
                return "İstehsalat-Məbləği";
            }
            else if (value == "DocTotal")
            {
                return "İstehsalat-Yekun";
            }
            else if (value == "ProductType")
            {
                return "İstehsalat-Məhsul Növü";
            }
            else
            {
                return value;
            }
        }
        public static string TableTranslate(string value)
        {
            if (value == "OrderId")
            {
                return "Siafriş İd";
            }
            else if (value == "CreateOn")
            {
                return "Yaraadılma Tarixi";
            }
            else if (value == "CreateUser")
            {
                return "Yaradaan İstifadəçi";
            }
            else if (value == "LastUpdate")
            {
                return "Son Yenilənmə Tarixi";
            }
            else if (value == "UpdateUser")
            {
                return "Yeniləyən İstifadəçi";
            }
            else if (value == "CustomerName")
            {
                return "Müştəri Adı";
            }
            else if (value == "CustomerSurname")
            {
                return "Müştəri Soyadı";
            }
            else if (value == "CustomerFatherName")
            {
                return "Müştəri Ata Adı";
            }
            else if (value == "SellerCode")
            {
                return "Satıcı Kodu";
            }
            else if (value == "VisitDate")
            {
                return "Vizit Tarixi";
            }
            else if (value == "OrderType1")
            {
                return "Sifariş Növü1";
            }
            else if (value == "OrderType2")
            {
                return "Sifariş Növü2";
            }
            else if (value == "OrderType3")
            {
                return "Sifariş Növü3";
            }
            else if (value == "Price")
            {
                return "Qiymət";
            }
            else if (value == "Note")
            {
                return "Qeyd";
            }
            else if (value == "IsActive")
            {
                return "Aktiv ?";
            }
            else if (value == "OrderStatus")
            {
                return "Sifariş Statusu";
            }
            else if (value == "VisitorCode")
            {
                return "Vizitor Kodu";
            }
            else if (value == "VisitorName")
            {
                return "Vizitor Adı";
            }
            else if (value == "IsVisitorAdded")
            {
                return "Vizitor Təyin Edilibmi?";
            }
            else if (value == "Location")
            {
                return "Lokasioya";
            }
            else if (value == "ItemCount")
            {
                return "Miqdar";
            }
            else if (value == "ItemDescription")
            {
                return "Məhsul Açıqlama";
            }
            else if (value == "OrderStore")
            {
                return "Sifariş Mağazası";
            }
            else if (value == "VisitorStatus")
            {
                return "Vizitor Status";
            }
            else if (value == "IsDesignerAdded")
            {
                return "Dizayner Təyin Edilibmi?";
            }
            else if (value == "DesignerCode")
            {
                return "Dizayner Kodu";
            }
            else if (value == "DesignerName")
            {
                return "Dizayner Adı";
            }
            else if (value == "DesignerStatus")
            {
                return "Dizayner Status";
            }
            else if (value == "CloseReason")
            {
                return "Bağlanma Səbəbi";
            }
            else if (value == "CustomerWillAnswer")
            {
                return "Müştəri Xəbər Edəcək";
            }
            else if (value == "IsCompleted")
            {
                return "Tamamlanıb?";
            }
            else if (value == "InvoiceNo")
            {
                return "İnvoice Nömrəsi";
            }
            else if (value == "PlannerStatus")
            {
                return "Planlamacı Statusu";
            }
            else if (value == "IsPlannerAdded")
            {
                return "Planlamacı Təyin Edilibmi?";
            }
            else if (value == "PlannerCode")
            {
                return "Planlamacı Kodu";
            }
            else if (value == "PlannerName")
            {
                return "Planlamacı Adı";
            }
            else if (value == "VisitID")
            {
                return "Vizit İd";
            }
            else if (value == "CreateOn")
            {
                return "Yaraadılma Tarixi";
            }
            else if (value == "CreateUser")
            {
                return "Yaradaan İstifadəçi";
            }
            else if (value == "LastUpdate")
            {
                return "Son Yenilənmə Tarixi";
            }
            else if (value == "UpdateUser")
            {
                return "Yeniləyən İstifadəçi";
            }
            else if (value == "OrderId")
            {
                return "Sifariş İd";
            }
            else if (value == "DWidth")
            {
                return "Eni";
            }
            else if (value == "DLenght")
            {
                return "Uzunu";
            }
            else if (value == "DHeight")
            {
                return "Hündürlüyü";
            }
            else if (value == "MaterialType")
            {
                return "MAaterial Növü";
            }
            else if (value == "MaterialColour")
            {
                return "Material Rəngi";
            }
            else if (value == "PanelType")
            {
                return "Panel Növü";
            }
            else if (value == "PanelColour")
            {
                return "Panel Rəngi";
            }
            else if (value == "Accessory")
            {
                return "Aksesuar";
            }
            else if (value == "Mirror")
            {
                return "Ayna";
            }
            else if (value == "Note")
            {
                return "Qeyd";
            }
            else if (value == "ProductCode")
            {
                return "Məhsul Kodu";
            }
            else if (value == "ProductName")
            {
                return "Məhsul Adı";
            }
            else if (value == "DoorType")
            {
                return "Qapı Növü";
            }
            else if (value == "DoorColour")
            {
                return "Qapı Rəngi";
            }
            else if (value == "Price")
            {
                return "Vizit-Qiymət";
            }
            else if (value == "FinalPrice")
            {
                return "Vizit-Yekun Qiymət";
            }
            else if (value == "IsDeclined")
            {
                return "İmtina Edilibmi?";
            }
            else if (value == "DeclineReason")
            {
                return "İmtina Səbəbi";
            }
            else if (value == "VisitStatus")
            {
                return "Ziyarət Statusu";
            }
            else if (value == "ProductCode")
            {
                return "İstehsalat-Məhsul Kodu";
            }
            else if (value == "ProductName")
            {
                return "İstehsalat-Məhsul Adı";
            }
            else if (value == "ProductPrice")
            {
                return "İstehsalat-Məhsul Qiyməti";
            }
            else if (value == "ProductQuantity")
            {
                return "İstehsalat-Məhsul Miqdarı";
            }
            else if (value == "ProductTotal")
            {
                return "İstehsalat-Məhsul Məbləği";
            }
            else if (value == "ProductCharges")
            {
                return "İstehsalat-Məhsul Faizi";
            }
            else if (value == "OrderId")
            {
                return "Sifariş İd";
            }
            else if (value == "VisitId")
            {
                return "Vizit İd";
            }
            else if (value == "DocSum")
            {
                return "İstehsalat-Məbləği";
            }
            else if (value == "DocTotal")
            {
                return "İstehsalat-Yekun";
            }
            else if (value == "ProductType")
            {
                return "İstehsalat-Məhsul Növü";
            }
            else
            {
                return value;
            }
        }
    }
}
