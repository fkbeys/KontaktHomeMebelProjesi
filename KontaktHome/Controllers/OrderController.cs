using BusinessLayer;
using BusinessLayer.QueryResult;
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
        private OrderManager orderManager = new OrderManager();
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NewOrder(string status)
        {
            ViewBag.Status = status;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewOrder(Orders data)
        {
            DateTime currentDate = DateTime.Now;
            data.CreateOn = currentDate;
            data.CreateUser = "Admin";
            data.LastUpdate = currentDate;
            data.OrderStatus = 1;
            data.UpdateUser = "Admin";
            data.IsActive = true;
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Orders> order = orderManager.SaveOrder(data);
                if (order.Errors.Count > 0)
                {
                    order.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(data);
                }
                return RedirectToAction("NewOrder", new { status = "1" });
            }
            return View(data);
        }
        public ActionResult ActiveOrders()
        {
            return View();
        }
        [WebMethod]
        public ActionResult GetActiveOrders()
        {
            List<Orders> fakturalar = new List<Orders>();
            fakturalar = orderManager.ListQueryable().Where(x => x.IsActive == true).ToList();
            var UserData = new object[fakturalar.Count];
            int j = 0;
            foreach (var item in fakturalar)
            {
                UserData[j] = new object[] { j + 1, item.CreateOn.ToString(), item.OrderId, item.CustomerName, item.CustomerSurname, item.CustomerFatherName, item.Tel1,item.OrderStatus };
                j++;
            }           
            return Json(UserData, JsonRequestBehavior.AllowGet);
        }
    }
}