using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.QueryResult
{
    public static class Statuses
    {
        public static string VisitorOrderStatus(int status)
        {
            if (status == 1)
            {
                return "Gözləmədə";
            }
            else if (status == 2)
            {
                return "Qəbul Edilib";
            }
            else if (status == 3)
            {
                return "Vizit Edilib";
            }
            else if (status == 4)
            {
                return "Vizit Tamamlanıb";
            }
            else
            {
                return "";
            }
        }
        public static string OrderStatus(int status)
        {
            if (status == 1)
            {
                return "Gözləmədə";
            }
            else if (status == 2)
            {
                return "Vizitor Təyin Edilib";
            }
            else if (status == 3)
            {
                return "Vizitor Qəbul Edib";
            }
            else if (status == 4)
            {
                return "Vizit Tamamlanıb";
            }
            else if (status==5)
            {
                return "Dizayner Təyin Edilib";
            }
            else if (status == 6)
            {
                return "Dizayner Qəbul Edib";
            }
            else if (status == 7)
            {
                return "Dizayn Tamamlanıb";
            }
            else if (status == 8)
            {
                return "Sifariş Fakturalaşıb";
            }
            else
            {
                return "";
            }
        }
        public static string OrderActiveStatus(bool status)
        {
            if (status==true)
            {
                return "Aktiv";
            }
            else
            {
                return "Bağlı";
            }
        }
        public static string DesignerStatus(int status)
        {
            if (status==1)
            {
                return "Gözləmədə";
            }
            else if (status==2)
            {
                return "Qəbul Edilib";
            }
            else if (status == 3)
            {
                return "Dizayn Edilir";
            }
            else if (status == 4)
            {
                return "Dizayn Tamamlanıb";
            }
            else
            {
                return "";
            }
        }
    }
}
