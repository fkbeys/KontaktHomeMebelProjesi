using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace KontaktHome.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NewOrder()
        {
            return View();
        }
        public ActionResult ActiveOrders()
        {
            return View();
        }
        [WebMethod]
        public ActionResult GetActiveOrders()
        {
            List<Orders> fakturalar = new List<Orders>();
            var UserData = new object[50];
            int j = 0;
            for (int i = 0; i < 50; i++)
            {          
                UserData[j] = new object[] { j + 1, "TEST DATA", "TEST DATA", "TEST DATA", "TEST DATA", "TEST DATA", "TEST DATA", "TEST DATA" };
                j++;
            }
            return Json(UserData, JsonRequestBehavior.AllowGet);
        }
    }
}