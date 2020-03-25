using BusinessLayer;
using BusinessLayer.QueryResult;
using Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace KontaktHome.Controllers
{
    public class OrderController : Controller
    {
        //TODO: Yeni sifarisde nisangahlar olacaq
        //TODO: Error provider hazirlanacaq
        private OrderManager orderManager = new OrderManager();
        private UserManager userManager = new UserManager();
        private AnaGrupManager anagrupManager = new AnaGrupManager();
        private VisitManager visitManager = new VisitManager();
        private ImagesManager imagesManager = new ImagesManager();
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
            data.VisitDate = Convert.ToDateTime(data.VisitDate.ToString("MM/dd/yyyy"));
            data.OrderStore = "Magaza1";
            data.SellerCode = "Admin";
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
                if (item.OrderStatus == 1)
                {
                    orderStatus = "Gözləmədə";
                }
                else if (item.OrderStatus == 2)
                {
                    orderStatus = "Vizitor Təyin Edilib";
                }
                UserData[j] = new object[] { j + 1, item.CreateOn.ToString("MM/dd/yyyy"), item.OrderId, item.CustomerName, item.CustomerSurname, item.CustomerFatherName, item.Tel1,item.SellerCode,item.OrderStore, orderStatus };
                j++;
            }
            return Json(UserData, JsonRequestBehavior.AllowGet);
        }
        [WebMethod]
        public ActionResult GetActiveOrdersWithParametr(OrderSearch data)
        {
            try
            {
                List<Orders> fakturalar = new List<Orders>();
                DateTime startdate = Convert.ToDateTime(data.firstDate);
                DateTime enddate = Convert.ToDateTime(data.lastDate + " 23:59:59");
                if (data.allorders == true)
                {
                    fakturalar = orderManager.ListQueryable().Where(x => x.CreateOn >= startdate & x.CreateOn <= enddate).ToList();
                }
                else
                {
                    fakturalar = orderManager.ListQueryable().Where(x => x.IsActive == true & x.CreateOn >= startdate & x.CreateOn <= enddate).ToList();
                }
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
                    UserData[j] = new object[] { j + 1, item.CreateOn.ToString("MM/dd/yyyy"), item.OrderId, item.CustomerName, item.CustomerSurname, item.CustomerFatherName, item.Tel1, item.SellerCode, item.OrderStore, orderStatus };
                    j++;
                }
                return Json(UserData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Home/Index");
            }
          
        }
        public ActionResult EditOrder(int? Sira)
        {
            if (Sira == null)
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
            if (data.IsVisitorAdded == true)
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
            try
            {
                string userName = "RESAD"; // TODO: visitor koduna gore visitorun sifarislerini getirecek
                List<Orders> fakturalar = new List<Orders>();
                fakturalar = orderManager.ListQueryable().Where(x => x.IsActive == true & x.VisitorCode == userName).ToList();
                var UserData = new object[fakturalar.Count];
                int j = 0;
                foreach (var item in fakturalar)
                {
                    if (item.VisitorStatus == 1)
                    {
                        orderStatus = "Gözləmədə";
                    }
                    else if (item.VisitorStatus == 2)
                    {
                        orderStatus = "Qəbul Edilib";
                    }
                    else if (item.VisitorStatus == 3)
                    {
                        orderStatus = "Vizit Edilib";
                    }
                    UserData[j] = new object[] { j + 1, item.CreateOn.ToString("MM/dd/yyyy"), item.OrderId, item.CustomerName, item.CustomerSurname, item.CustomerFatherName, item.Tel1,item.Location, orderStatus };
                    j++;
                }
                return Json(UserData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return RedirectToAction("Home/Index");
            }
          
        }
        [WebMethod]
        public ActionResult GetVisitorActiveOrdersWithParametr(OrderSearch data)
        {
            try
            {   string userName = "RESAD"; // TODO: visitor koduna gore visitorun sifarislerini getirecek
                List<Orders> fakturalar = new List<Orders>();
                DateTime startdate = Convert.ToDateTime(data.firstDate);
                DateTime enddate = Convert.ToDateTime(data.lastDate + " 23:59:59");
                if (data.allorders == true)
                {
                    fakturalar = orderManager.ListQueryable().Where(x => x.CreateOn >= startdate & x.CreateOn <= enddate & x.VisitorCode == userName).ToList();
                }
                else
                {
                    fakturalar = orderManager.ListQueryable().Where(x => x.IsActive == true & x.CreateOn >= startdate & x.CreateOn <= enddate & x.VisitorCode == userName).ToList();
                }                
                var UserData = new object[fakturalar.Count];
                int j = 0;
                foreach (var item in fakturalar)
                {
                    if (item.VisitorStatus == 1)
                    {
                        orderStatus = "Gözləmədə";
                    }
                    else if (item.VisitorStatus == 2)
                    {
                        orderStatus = "Qəbul Edilib";
                    }
                    else if (item.VisitorStatus == 3)
                    {
                        orderStatus = "Vizit Edilib";
                    }
                    UserData[j] = new object[] { j + 1, item.CreateOn.ToString("MM/dd/yyyy"), item.OrderId, item.CustomerName, item.CustomerSurname, item.CustomerFatherName, item.Tel1, item.Location, orderStatus };
                    j++;
                }
                return Json(UserData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Home/Index");
            }
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
        public ActionResult CustomerVisit(int? Sira)
        {
            try
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
                List<Visits> visits= visitManager.List(x => x.OrderId == Sira).ToList();
                OrderFileUpload uploadedFiles = new OrderFileUpload();
                CustomerVisitData data = new CustomerVisitData();
                data.order = order;
                data.orderFiles = uploadedFiles;
                data.visitData = visits;
                List<STOK_ANA_GRUPLARI> anagruplar = anagrupManager.List().ToList();
                //var listanagruplar = anagruplar.Select(s => new SelectListItem { Value = s.san_kod, Text = s.san_isim }).ToList<SelectListItem>();
                data.itemGroups = new SelectList(anagruplar, "san_kod", "san_isim");
                //ViewBag.AnaGruplar = listanagruplar;
                return View(data);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }          
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerVisit(CustomerVisitData data, FormCollection form)
        {
            data.order = orderManager.Find(x => x.OrderId == data.order.OrderId);
            List<STOK_ANA_GRUPLARI> anagruplar = anagrupManager.List().ToList();
            data.itemGroups = new SelectList(anagruplar, "san_kod", "san_isim");
            foreach (var item in data.visitData)
            {
                var anagrupadi = anagruplar.Where(x => x.san_kod == item.ProductCode).Select(p => p.san_isim).Single();
                item.ProductName = anagrupadi;
            }
            List<VisitImages> visitImages = new List<VisitImages>();         
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Visits> visitData = visitManager.SaveVisit(data.visitData,data.order.OrderId);
                if (visitData.Errors.Count > 0)
                {
                    visitData.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(data);
                }
                if (data.orderFiles.orderFiles.Count() > 0)
                {

                    bool exists = Directory.Exists(Server.MapPath("~/UploadedFiles/" + visitData.Result.VisitGuid.ToString()));
                    if (!exists)
                        Directory.CreateDirectory(Server.MapPath("~/UploadedFiles/" + visitData.Result.VisitGuid.ToString()));
                    foreach (HttpPostedFileBase file in data.orderFiles.orderFiles)
                    {
                        if (file != null)
                        {
                            var InputFileName = Path.GetFileName(file.FileName);
                            var ServerSavePath = Path.Combine(Server.MapPath("~/UploadedFiles/" + visitData.Result.VisitGuid.ToString() + "/") + InputFileName);
                            file.SaveAs(ServerSavePath);
                            visitImages.Add(new VisitImages() { VisitGuid = visitData.Result.VisitGuid, ImageName = InputFileName.ToString(), ImagePath = ServerSavePath.ToString() });
                        }
                    }
                    BusinessLayerResult<VisitImages> imageData = imagesManager.SaveImages(visitImages);
                    if (imageData.Errors.Count > 0)
                    {

                    }
                }
                return RedirectToAction("VisitorOrders", new { status = "1" });
            }

            //var listanagruplar = anagruplar.Select(s => new SelectListItem { Value = s.san_kod, Text = s.san_isim }).ToList<SelectListItem>();
            //ViewBag.AnaGruplar = listanagruplar;
           
            return View(data);
        }

    }
}