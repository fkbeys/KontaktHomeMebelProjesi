using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

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
            else if (status == 5)
            {
                return "Planlamacı Təyin Edilib";
            }
            else if (status == 6)
            {
                return "Planlamacı Qəbul Edib";
            }
            else if (status == 7)
            {
                return "Planlama Tamamlanıb";
            }
            else if (status==8)
            {
                return "Dizayner Təyin Edilib";
            }
            else if (status == 9)
            {
                return "Dizayner Qəbul Edib";
            }
            else if (status == 10)
            {
                return "Dizayn Tamamlanıb";
            }
            else if (status == 11)
            {
                return "Sifariş Yekunlaşıb";
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
        public static string PlannerStatus(int status)
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
                return "Planlama Edilir";
            }
            else if (status == 4)
            {
                return "Planlama Tamamlanıb";
            }
            else
            {
                return "";
            }
        }
        public static List<SelectListItem> ListOrderStatuses()
        {
            List<SelectListItem> orderStatuses = new List<SelectListItem>();
            orderStatuses.Add(new SelectListItem { Value = "1", Text = "Gözləmədə" });
            orderStatuses.Add(new SelectListItem { Value = "2", Text = "Vizitor Təyin Edilib" });
            orderStatuses.Add(new SelectListItem { Value = "3", Text = "Vizitor Qəbul Edib" });
            orderStatuses.Add(new SelectListItem { Value = "4", Text = "Vizit Tamamlanıb" });
            orderStatuses.Add(new SelectListItem { Value = "5", Text = "Planlamacı Təyin Edilib" });
            orderStatuses.Add(new SelectListItem { Value = "6", Text = "Planlamacı Qəbul Edib" });
            orderStatuses.Add(new SelectListItem { Value = "7", Text = "Planlama Tamamlanıb" });
            orderStatuses.Add(new SelectListItem { Value = "8", Text = "Dizayner Təyin Edilib" });
            orderStatuses.Add(new SelectListItem { Value = "9", Text = "Dizayner Qəbul Edib" });
            orderStatuses.Add(new SelectListItem { Value = "10", Text = "Dizayn Tamamlanıb" });
            orderStatuses.Add(new SelectListItem { Value = "11", Text = "Sifariş Yekunlaşıb" });
            return orderStatuses;
        }
        public static List<SelectListItem> ListVisitorStatuses()
        {
            List<SelectListItem> visitorStatuses = new List<SelectListItem>();            
            visitorStatuses.Add(new SelectListItem { Value = "1", Text = "Gözləmədə" });
            visitorStatuses.Add(new SelectListItem { Value = "2", Text = "Qəbul Edilib" });
            visitorStatuses.Add(new SelectListItem { Value = "3", Text = "Vizit Edilib" });
            visitorStatuses.Add(new SelectListItem { Value = "4", Text = "Vizit Tamamlanıb" });          
            return visitorStatuses;
        }
        public static List<SelectListItem> ListDesignerStatuses()
        {
            List<SelectListItem> designerStatuses = new List<SelectListItem>();
            designerStatuses.Add(new SelectListItem { Value = "1", Text = "Gözləmədə" });
            designerStatuses.Add(new SelectListItem { Value = "2", Text = "Qəbul Edilib" });
            designerStatuses.Add(new SelectListItem { Value = "3", Text = "Dizayn Edilir" });
            designerStatuses.Add(new SelectListItem { Value = "4", Text = "Dizayn Tamamlanıb" });
            return designerStatuses;
        }
        public static List<SelectListItem> ListPlannerStatuses()
        {
            List<SelectListItem> plannerStatus = new List<SelectListItem>();
            plannerStatus.Add(new SelectListItem { Value = "1", Text = "Gözləmədə" });
            plannerStatus.Add(new SelectListItem { Value = "2", Text = "Qəbul Edilib" });
            plannerStatus.Add(new SelectListItem { Value = "3", Text = "Planlama Edilir"});
            plannerStatus.Add(new SelectListItem { Value = "4", Text = "Planlama Tamamlanıb" });
            return plannerStatus;
        }
    }
}
