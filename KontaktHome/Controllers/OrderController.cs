using BusinessLayer;
using BusinessLayer.QueryResult;
using Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace KontaktHome.Controllers
{
    public class OrderController : Controller
    {
        private OrderManager orderManager = new OrderManager();
        private UserManager userManager = new UserManager();
        public string orderStatus { get; set; }
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
            data.VisitDate =Convert.ToDateTime(data.VisitDate.ToString("MM/dd/yyyy"));
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
        public ActionResult ActiveOrders(string status)
        {
            ViewBag.Status = status;
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
                if (item.OrderStatus==1)
                {
                    orderStatus = "Gözləmədə";
                }
                else if (item.OrderStatus == 2)
                {
                    orderStatus = "Vizitor Təyin Edilib";
                }
                UserData[j] = new object[] { j + 1, item.CreateOn.ToString("MM/dd/yyyy"), item.OrderId, item.CustomerName, item.CustomerSurname, item.CustomerFatherName, item.Tel1,orderStatus };
                j++;
            }           
            return Json(UserData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditOrder(int? Sira)
        {
            if (Sira==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Users> users = userManager.ListQueryable().Where(x => x.IsVisitor == true).ToList();
            var listvisitor = users.Select(s => new SelectListItem { Value = s.UserName, Text = s.UserDisplayName }).ToList<SelectListItem>();
            Orders order = orderManager.Find(x => x.OrderId == Sira);
            if (order == null)
            {
                return HttpNotFound();
            }          
            ViewBag.Visitor = listvisitor;
            return View(order);            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrder(Orders data, FormCollection form)
        {
            string visitorName = form["Visitor"].ToString();
            DateTime currentDate = DateTime.Now;           
            data.LastUpdate = currentDate;
            data.OrderStatus = 1;
            data.UpdateUser = "User";
            data.IsActive = true;           
            if (data.IsVisitorAdded==true)
            {
                data.OrderStatus = 2;
                data.VisitorCode = visitorName;
                BusinessLayerResult<Users> users = userManager.GetUserInformation(visitorName);
                if (users.Errors.Count == 0)
                {
                    data.VisitorName = users.Result.UserDisplayName;
                }
                else
                {
                    data.VisitorName = "User Not Found";                   
                }
            }
            else
            {
                data.OrderStatus = 1;
                data.VisitorCode = "";
            }
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Orders> order = orderManager.UpdateOrder(data);
                if (order.Errors.Count > 0)
                {
                    order.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(data);
                }
                return RedirectToAction("ActiveOrders", new { status = "1" });
            }
            return View(data);
        }
        public ActionResult VisitorOrders(string status)
        {
            ViewBag.Status = status;
            return View();
        }
        [WebMethod]
        public ActionResult GetVisitorActiveOrders()
        {
            string userName = "RESAD"; // TODO: visitor koduna gore visitorun sifarislerini getirecek
            List<Orders> fakturalar = new List<Orders>();
            fakturalar = orderManager.ListQueryable().Where(x => x.IsActive == true & x.VisitorCode== userName).ToList();
            var UserData = new object[fakturalar.Count];
            int j = 0;
            foreach (var item in fakturalar)
            {
                if (item.OrderStatus == 1)
                {
                    orderStatus = "Gözləmədə";
                }
                else if (item.OrderStatus == 2)
                {
                    orderStatus = "Vizitor Təyin Edilib";
                }
                UserData[j] = new object[] { j + 1, item.CreateOn.ToString("MM/dd/yyyy"), item.OrderId, item.CustomerName, item.CustomerSurname, item.CustomerFatherName, item.Tel1, orderStatus };
                j++;
            }
            return Json(UserData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult OrderInfo(int? Sira)
        {
            if (Sira == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders order = orderManager.Find(x => x.OrderId == Sira);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }      

    }
}