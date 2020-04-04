﻿using BusinessLayer;
using BusinessLayer.QueryResult;
using Entities;
using KontaktHome.Filters;
using KontaktHome.Models;
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
    [Exc]
    [OutputCache(NoStore = true, Duration = 0)]
    public class OrderController : Controller
    {
        //TODO: Yeni sifarisde nisangahlar olacaq
        
        private OrderManager orderManager = new OrderManager();
        private UserManager userManager = new UserManager();
        private AnaGrupManager anagrupManager = new AnaGrupManager();
        private VisitManager visitManager = new VisitManager();
        private ImagesManager imagesManager = new ImagesManager();
        public string orderStatus { get; set; }
        // GET: Order
        [Auth]
        public ActionResult Index()
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);  // HTTP 1.1.
            //Response.Cache.AppendCacheExtension("no-store, must-revalidate");
            //Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
            //Response.AppendHeader("Expires", "0"); // Proxies.
            return View();
        }

        //Seller
        [Auth]       
        public ActionResult NewOrder(string status)
        {
            ViewBag.Status = status;
            return View();
        }
        [Auth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewOrder(Orders data)
        {
            DateTime currentDate = DateTime.Now;
            data.CreateOn = currentDate;
            data.CreateUser = CurrentSession.User.UserName;
            data.LastUpdate = currentDate;
            data.OrderStatus = 1;
            data.UpdateUser = CurrentSession.User.UserName;
            data.IsActive = true;
            data.VisitDate = Convert.ToDateTime(data.VisitDate.ToString("MM/dd/yyyy"));
            data.OrderStore = CurrentSession.User.StoreCode;
            data.SellerCode = CurrentSession.User.UserName;
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Orders> order = orderManager.SaveOrder(data);
                if (order.Errors.Count > 0)
                {
                    order.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(data);
                }
                TempData["msg"] = "Qeyd Tamamlandı!";
                TempData["typ"] = "success";
                return RedirectToAction("NewOrder");
            }
            return View(data);
        }

        //Cordinator and seller
        [Auth]
        public ActionResult ActiveOrders(string status)
        {
            ViewBag.Status = status;
            return View();
        }
        [Auth]
        [WebMethod]
        public ActionResult GetActiveOrders()
        {
            List<Orders> fakturalar = new List<Orders>();
            if (CurrentSession.User.IsSeller == true)
            {
                fakturalar = orderManager.ListQueryable().Where(x => x.IsActive == true && x.SellerCode == CurrentSession.User.UserName).ToList();
            }
            else
            {
                fakturalar = orderManager.ListQueryable().Where(x => x.IsActive == true).ToList();
            }
            var UserData = new object[fakturalar.Count];
            int j = 0;
            foreach (var item in fakturalar)
            {
                string orderStatus = Statuses.OrderStatus(item.OrderStatus);
                string orderAktivstatus = Statuses.OrderActiveStatus(item.IsActive);
                string link = "?q=" + Encrypt.EncryptString("Sira=" + item.OrderId.ToString());
                UserData[j] = new object[] { j + 1, item.CreateOn.ToString("MM/dd/yyyy"), item.OrderId, item.CustomerName, item.CustomerSurname, item.CustomerFatherName, item.Tel1, item.SellerCode, item.OrderStore, orderStatus, link, orderAktivstatus };
                j++;
            }
            return Json(UserData, JsonRequestBehavior.AllowGet);
        }
        [Auth]
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
                    if (CurrentSession.User.IsSeller == true)
                    {
                        fakturalar = orderManager.ListQueryable().Where(x => x.CreateOn >= startdate & x.CreateOn <= enddate & x.SellerCode == CurrentSession.User.UserName).ToList();
                    }
                    else
                    {
                        fakturalar = orderManager.ListQueryable().Where(x => x.CreateOn >= startdate & x.CreateOn <= enddate).ToList();
                    }
                }
                else
                {
                    if (CurrentSession.User.IsSeller == true)
                    {
                        fakturalar = orderManager.ListQueryable().Where(x => x.IsActive == true & x.CreateOn >= startdate & x.CreateOn <= enddate & x.SellerCode == CurrentSession.User.UserName).ToList();
                    }
                    else
                    {
                        fakturalar = orderManager.ListQueryable().Where(x => x.IsActive == true & x.CreateOn >= startdate & x.CreateOn <= enddate).ToList();
                    }
                }
                var UserData = new object[fakturalar.Count];
                int j = 0;
                foreach (var item in fakturalar)
                {
                    string orderStatus = Statuses.OrderStatus(item.OrderStatus);
                    string orderAktivstatus = Statuses.OrderActiveStatus(item.IsActive);
                    string link = "?q=" + Encrypt.EncryptString("Sira=" + item.OrderId.ToString());
                    UserData[j] = new object[] { j + 1, item.CreateOn.ToString("MM/dd/yyyy"), item.OrderId, item.CustomerName, item.CustomerSurname, item.CustomerFatherName, item.Tel1, item.SellerCode, item.OrderStore, orderStatus, link, orderAktivstatus };
                    j++;
                }
                return Json(UserData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Home/Index");
            }

        }

        //All
        [Auth]
        [EncryptedActionParameter]
        public ActionResult EditOrder(int? Sira)
        {
            if (Sira == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Users> users = userManager.ListQueryable().Where(x => x.IsVisitor == true && x.IsActive == true).ToList();
            var listvisitor = users.Select(s => new SelectListItem { Value = s.UserName, Text = s.UserDisplayName }).ToList<SelectListItem>();
            List<Users> designers = userManager.ListQueryable().Where(x => x.IsDesigner == true && x.IsActive == true).ToList();
            var listdesigner = designers.Select(s => new SelectListItem { Value = s.UserName, Text = s.UserDisplayName }).ToList<SelectListItem>();
            Orders order = orderManager.Find(x => x.OrderId == Sira);
            if (order == null)
            {
                return HttpNotFound();
            }
            TimeSpan gunler = DateTime.Now - order.CreateOn;
            decimal ferq = gunler.Days;
            if (CurrentSession.User.IsSeller == true)
            {
                if (order.OrderStatus != 1)
                {
                    TempData["msg"] = "Vizitor təyin edilmiş sifarişlərdə dəyişiklik ediləbilməz!";
                    TempData["typ"] = "error";
                    return RedirectToAction("ActiveOrders");
                }
                else if (ferq >= CurrentSession.User.EditDate)
                {
                    TempData["msg"] = "Sənəd dəyişiklik tarixi icazə verilən tarixdən çoxdur!";
                    TempData["typ"] = "error";
                    return RedirectToAction("ActiveOrders");
                }
            }
            ViewBag.Visitor = listvisitor;
            ViewBag.Designer = listdesigner;
            return View(order);
        }
        [Auth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrder(Orders data, FormCollection form)
        {
            DateTime currentDate = DateTime.Now;
            data.LastUpdate = currentDate;            
            data.UpdateUser = CurrentSession.User.UserName;          
            if (CurrentSession.User.IsCord == true || CurrentSession.User.IsAdmin==true)
            {         
                if (data.VisitorStatus<3)
                {
                    string visitorName = form["Visitor"].ToString();
                    if (data.IsVisitorAdded == true)
                    {
                        data.OrderStatus = 2;
                        data.VisitorCode = visitorName;
                        data.VisitorStatus = 1;
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
                        data.VisitorName = "";
                    }
                }
                if (data.DesignerStatus<3)
                {
                    string designerName = "";
                    if (data.VisitorStatus==4)
                    {
                        designerName = form["Designer"].ToString();
                        data.OrderStatus = 5;
                    }
                    if (data.IsDesignerAdded == true)
                    {
                        data.DesignerStatus = 1;
                        data.DesignerCode = designerName;
                        BusinessLayerResult<Users> users = userManager.GetUserInformation(designerName);
                        if (users.Errors.Count == 0)
                        {
                            data.DesignerName = users.Result.UserDisplayName;
                        }
                        else
                        {
                            data.DesignerName = "User Not Found";
                        }
                    }
                    else
                    {
                        data.DesignerStatus = 0;
                        data.DesignerCode = "";
                        data.DesignerName = "";
                    }
                }
               
            }
            else if (CurrentSession.User.IsSeller == true)
            {
                //TODO: seller duzelis
                TimeSpan gunler = DateTime.Now - data.CreateOn;
                decimal ferq = gunler.Days;
                if (data.OrderStatus != 1)
                {
                    TempData["msg"] = "Vizitor təyin edilmiş sifarişlərdə dəyişiklik ediləbilməz!";
                    TempData["typ"] = "error";
                    return RedirectToAction("ActiveOrders");
                }
                else if (ferq >= CurrentSession.User.EditDate)
                {
                    TempData["msg"] = "Sənəd dəyişiklik tarixi icazə verilən tarixdən çoxdur!";
                    TempData["typ"] = "error";
                    return RedirectToAction("ActiveOrders");
                }               
            }
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Orders> order = orderManager.UpdateOrder(data);
                if (order.Errors.Count > 0)
                {
                    order.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(data);
                }
                TempData["msg"] = "Qeyd Yeniləndi!";
                TempData["typ"] = "success";
                return RedirectToAction("ActiveOrders");
            }
            return View(data);
        }
        [Auth]
        [EncryptedActionParameter]
        public ActionResult OrderInfo(int? Sira)
        {
            if (Sira == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Users> users = userManager.ListQueryable().Where(x => x.IsVisitor == true && x.IsActive == true).ToList();
            var listvisitor = users.Select(s => new SelectListItem { Value = s.UserName, Text = s.UserDisplayName }).ToList<SelectListItem>();
            List<Users> designers = userManager.ListQueryable().Where(x => x.IsDesigner == true && x.IsActive == true).ToList();
            var listdesigner = designers.Select(s => new SelectListItem { Value = s.UserName, Text = s.UserDisplayName }).ToList<SelectListItem>();
            Orders order = orderManager.Find(x => x.OrderId == Sira);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.Visitor = listvisitor;
            ViewBag.Designer = listdesigner;
            return View(order);
        }

        //Visitor
        [Auth]
        public ActionResult VisitorOrders(string status)
        {
            ViewBag.Status = status;
            return View();
        }
        [Auth]
        [WebMethod]
        public ActionResult GetVisitorActiveOrders()
        {
            string userName = CurrentSession.User.UserName;
            List<Orders> fakturalar = new List<Orders>();
            fakturalar = orderManager.ListQueryable().Where(x => x.IsActive == true & x.VisitorCode == userName & x.VisitorStatus<4).ToList();
            var UserData = new object[fakturalar.Count];
            int j = 0;
            foreach (var item in fakturalar)
            {
                string orderStatus = Statuses.VisitorOrderStatus(item.VisitorStatus);
                string link = "?q=" + Encrypt.EncryptString("Sira=" + item.OrderId.ToString());
                UserData[j] = new object[] { j + 1, item.CreateOn.ToString("MM/dd/yyyy"), item.OrderId, item.CustomerName, item.CustomerSurname, item.CustomerFatherName, item.Tel1, item.Location, orderStatus, link,item.VisitorStatus };
                j++;
            }
            return Json(UserData, JsonRequestBehavior.AllowGet);

        }
        [Auth]
        [WebMethod]
        public ActionResult GetVisitorActiveOrdersWithParametr(OrderSearch data)
        {

            string userName = CurrentSession.User.UserName;
            List<Orders> fakturalar = new List<Orders>();
            DateTime startdate = Convert.ToDateTime(data.firstDate);
            DateTime enddate = Convert.ToDateTime(data.lastDate + " 23:59:59");
            if (data.allorders == true)
            {
                fakturalar = orderManager.ListQueryable().Where(x => x.CreateOn >= startdate & x.CreateOn <= enddate & x.VisitorCode == userName & x.IsActive==true).ToList();
            }
            else
            {
                fakturalar = orderManager.ListQueryable().Where(x => x.IsActive == true & x.CreateOn >= startdate & x.CreateOn <= enddate & x.VisitorCode == userName & x.VisitorStatus<4).ToList();
            }
            var UserData = new object[fakturalar.Count];
            int j = 0;
            foreach (var item in fakturalar)
            {
                string orderStatus = Statuses.VisitorOrderStatus(item.VisitorStatus);              
                string link = "?q=" + Encrypt.EncryptString("Sira=" + item.OrderId.ToString());
                UserData[j] = new object[] { j + 1, item.CreateOn.ToString("MM/dd/yyyy"), item.OrderId, item.CustomerName, item.CustomerSurname, item.CustomerFatherName, item.Tel1, item.Location, orderStatus, link,item.VisitorStatus };
                j++;
            }
            return Json(UserData, JsonRequestBehavior.AllowGet);

        }
        [Auth]
        [EncryptedActionParameter]
        public ActionResult VisitInfo(int? Sira)
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
            //else if (order.VisitorStatus == 0)
            //{
            //    return RedirectToAction("VisitorOrders");
            //}
            else
            {
                List<Visits> visits = visitManager.List(x => x.OrderId == Sira).ToList();
                OrderFileUpload uploadedFiles = new OrderFileUpload();
                CustomerVisitData data = new CustomerVisitData();
                List<VisitImages> visitimages = new List<VisitImages>();
                if (visits.Count>0)
                {
                    Visits visitGuid = visits.FirstOrDefault(x => x.OrderId == Sira);
                    visitimages = imagesManager.List(x => x.VisitGuid == visitGuid.VisitGuid);
                }                
                data.order = order;
                data.orderFiles = uploadedFiles;
                data.visitData = visits;
                data.visitImages = visitimages;
                List<STOK_ANA_GRUPLARI> anagruplar = anagrupManager.List().ToList();
                //var listanagruplar = anagruplar.Select(s => new SelectListItem { Value = s.san_kod, Text = s.san_isim }).ToList<SelectListItem>();
                data.itemGroups = new SelectList(anagruplar, "san_kod", "san_isim");
                //ViewBag.AnaGruplar = listanagruplar;
                return View(data);
            }
        }
        [Auth]
        [EncryptedActionParameter]
        public ActionResult CustomerVisit(int? Sira)
        {
            if (Sira == null)
            {
                return RedirectToAction("/Home/HasError");
            }
            Orders order = orderManager.Find(x => x.OrderId == Sira);
            if (order == null)
            {
                return HttpNotFound();
            }
            else if (order.VisitorStatus < 2)
            {
                TempData["msg"] = "3";
                return RedirectToAction("VisitorOrders");
            }
            else
            {
                List<Visits> visits = visitManager.List(x => x.OrderId == Sira).ToList();
                OrderFileUpload uploadedFiles = new OrderFileUpload();
                CustomerVisitData data = new CustomerVisitData();
                List<VisitImages> visitimages = new List<VisitImages>();
                if (visits.Count>0)
                {
                    Visits visitGuid = visits.FirstOrDefault(x => x.OrderId == Sira);
                    visitimages = imagesManager.List(x => x.VisitGuid == visitGuid.VisitGuid);
                }               
                data.order = order;
                data.orderFiles = uploadedFiles;
                data.visitData = visits;
                data.visitImages = visitimages;                
                List<STOK_ANA_GRUPLARI> anagruplar = anagrupManager.List().ToList();
                //var listanagruplar = anagruplar.Select(s => new SelectListItem { Value = s.san_kod, Text = s.san_isim }).ToList<SelectListItem>();
                data.itemGroups = new SelectList(anagruplar, "san_kod", "san_isim");
                //ViewBag.AnaGruplar = listanagruplar;
                return View(data);
            }
        }
        [Auth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerVisit(CustomerVisitData data, string visitCommand)
        {
            int visitorstatus = 0;
            if (visitCommand.Equals("Save"))
            {
                visitorstatus = 13;
            }
            else if (visitCommand.Equals("Finish"))
            {
                visitorstatus = 14;
            }
            data.order = orderManager.Find(x => x.OrderId == data.order.OrderId);
            List<STOK_ANA_GRUPLARI> anagruplar = anagrupManager.List().ToList();
            data.itemGroups = new SelectList(anagruplar, "san_kod", "san_isim");
            if (data.visitImages == null)
            {
                data.visitImages = new List<VisitImages>();
            }
            if (data.visitData!=null)
            {               
                foreach (var item in data.visitData)
                {
                    if (item.ProductCode != null)
                    {
                        var anagrupadi = anagruplar.Where(x => x.san_kod == item.ProductCode).Select(p => p.san_isim).Single();
                        item.ProductName = anagrupadi;
                    }
                    else if (data.visitData.Count==1)
                    {
                        ModelState.AddModelError("", "Məhsul seçilməyib");
                        return View(data);
                    }
                }
            }       
            List<VisitImages> visitImages = new List<VisitImages>();           
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Visits> visitData = visitManager.SaveVisit(data.visitData, data.order.OrderId, CurrentSession.User);
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
                            var imagePath = Path.Combine("~/UploadedFiles/" + visitData.Result.VisitGuid.ToString() + "/" + InputFileName);
                            file.SaveAs(ServerSavePath);
                            visitImages.Add(new VisitImages() { VisitGuid = visitData.Result.VisitGuid, ImageName = InputFileName.ToString(), ImagePath = imagePath.ToString(), ImageType = 1 });
                        }
                    }
                    BusinessLayerResult<VisitImages> imageData = imagesManager.SaveImages(visitImages);
                    if (imageData.Errors.Count > 0)
                    {

                    }
                }
                BusinessLayerResult<Orders> order = orderManager.AcceptOrder(data.order, visitorstatus);               
                TempData["msg"] = "1";
                return RedirectToAction("VisitorOrders");
            }
            else
            {                
                if (data.visitData==null)
                {
                    data.visitData = new List<Visits>();
                }
            }
            return View(data);
        }
        [Auth]
        [EncryptedActionParameter]
        public ActionResult AcceptOrder(int? Sira)
        {
            if (Sira == null)
            {
                return RedirectToAction("/Home/HasError");
            }
            if (CurrentSession.User.IsVisitor==true || CurrentSession.User.IsAdmin==true)
            {
                Orders order = orderManager.Find(x => x.OrderId == Sira);
                if (order == null)
                {
                    return RedirectToAction("/Home/HasError");
                }
                else
                {
                    if (order.VisitorStatus>=2)
                    {
                        return RedirectToAction("VisitorOrders");
                    }
                    BusinessLayerResult<Orders> data = orderManager.AcceptOrder(order, 12);
                    if (data.Errors.Count > 0)
                    {
                        data.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                        TempData["msg"] = "0";
                        return RedirectToAction("VisitorOrders");
                    }
                }
                TempData["msg"] = "2";
                return RedirectToAction("VisitorOrders");
            }
            else
            {
                return RedirectToAction("VisitorOrders");
            }           
        }

        //Designer
        [Auth]
        [EncryptedActionParameter]
        public ActionResult AcceptDesignerOrder(int? Sira)
        {
            if (Sira == null)
            {
                return RedirectToAction("/Home/HasError");
            }
            Orders order = orderManager.Find(x => x.OrderId == Sira);
            if (order == null)
            {
                return RedirectToAction("/Home/HasError");
            }
            else
            {
                if (order.DesignerStatus>=2)
                {
                    return RedirectToAction("DesignerOrders");
                }
                BusinessLayerResult<Orders> data = orderManager.AcceptOrder(order, 22);
                if (data.Errors.Count > 0)
                {
                    data.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    TempData["msg"] = "0";
                    return RedirectToAction("DesignerOrders");
                }
            }
            TempData["msg"] = "Siafriş Qəbul Edildi!";
            TempData["typ"] = "success";
            return RedirectToAction("DesignerOrders");
        }
        [Auth]
        public ActionResult DesignerOrders(string status)
        {
            ViewBag.Status = status;
            return View();
        }
        [Auth]
        [WebMethod]
        public ActionResult GetDesignerActiveOrders()
        {
            string userName = CurrentSession.User.UserName;
            List<Orders> fakturalar = new List<Orders>();
            fakturalar = orderManager.ListQueryable().Where(x => x.IsActive == true & x.DesignerCode == userName &x.VisitorStatus==4 & x.DesignerStatus<4).ToList();
            var UserData = new object[fakturalar.Count];
            int j = 0;
            foreach (var item in fakturalar)
            {
                string orderStatus = Statuses.DesignerStatus(item.DesignerStatus);               
                string link = "?q=" + Encrypt.EncryptString("Sira=" + item.OrderId.ToString());
                UserData[j] = new object[] { j + 1, item.CreateOn.ToString("MM/dd/yyyy"), item.OrderId, item.CustomerName, item.CustomerSurname, item.CustomerFatherName, item.Tel1, item.Location, orderStatus, link,item.DesignerStatus };
                j++;
            }
            return Json(UserData, JsonRequestBehavior.AllowGet);

        }
        [Auth]
        [EncryptedActionParameter]
        public ActionResult DesignerEdit(int? Sira)
        {
            if (Sira == null)
            {
                return RedirectToAction("/Home/HasError");
            }           

            Orders order = orderManager.Find(x => x.OrderId == Sira);
            if (order == null)
            {
                return RedirectToAction("/Home/HasError");
            }
            else if (order.DesignerStatus < 2)
            {
                TempData["msg"] = "Sifariş Qəbul Eilməyib!";
                TempData["typ"] = "error";
                return RedirectToAction("DesignerOrders");
            }
            else
            {
                List<Visits> visits = visitManager.List(x => x.OrderId == Sira).ToList();
                OrderFileUpload uploadedFiles = new OrderFileUpload();
                CustomerVisitData data = new CustomerVisitData();
                var visitGuid = visits.Where(x => x.OrderId == Sira).Select(m => m.VisitGuid).Single();
                data.order = order;
                data.orderFiles = uploadedFiles;
                data.visitData = visits;
                data.visitImages = imagesManager.List(x => x.VisitGuid == visitGuid);
                List<STOK_ANA_GRUPLARI> anagruplar = anagrupManager.List().ToList();
                data.itemGroups = new SelectList(anagruplar, "san_kod", "san_isim");
                return View(data);
            }
        }
        [Auth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DesignerEdit(CustomerVisitData data, string designCommand)
        {
            int designerstatus = 0;
            if (designCommand.Equals("Save"))
            {
                designerstatus = 23;
            }
            else if (designCommand.Equals("Finish"))
            {
                designerstatus = 24;
            }
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
                BusinessLayerResult<Visits> visitData = visitManager.SaveVisit(data.visitData, data.order.OrderId, CurrentSession.User);
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
                            var imagePath = Path.Combine("~/UploadedFiles/" + visitData.Result.VisitGuid.ToString() + "/" + InputFileName);
                            file.SaveAs(ServerSavePath);
                            visitImages.Add(new VisitImages() { VisitGuid = visitData.Result.VisitGuid, ImageName = InputFileName.ToString(), ImagePath = imagePath.ToString(), ImageType = 2 });
                        }
                    }
                    BusinessLayerResult<VisitImages> imageData = imagesManager.SaveImages(visitImages);
                    if (imageData.Errors.Count > 0)
                    {

                    }
                }
                BusinessLayerResult<Orders> order = orderManager.AcceptOrder(data.order, designerstatus);
                TempData["msg"] = "Qeyd Yeniləndi!";
                TempData["typ"] = "sussess";
                return RedirectToAction("DesignerOrders");
            }
            return View(data);
        }
        [Auth]
        [WebMethod]
        public ActionResult GetDesignerActiveOrdersWithParametr(OrderSearch data)
        {
            string userName = CurrentSession.User.UserName;
            List<Orders> fakturalar = new List<Orders>();
            DateTime startdate = Convert.ToDateTime(data.firstDate);
            DateTime enddate = Convert.ToDateTime(data.lastDate + " 23:59:59");
            if (data.allorders == true)
            {
                fakturalar = orderManager.ListQueryable().Where(x => x.CreateOn >= startdate & x.CreateOn <= enddate & x.DesignerCode == userName & x.IsActive==true).ToList();
            }
            else
            {
                fakturalar = orderManager.ListQueryable().Where(x => x.IsActive == true & x.CreateOn >= startdate & x.CreateOn <= enddate & x.DesignerCode == userName & x.DesignerStatus<4).ToList();
            }
            var UserData = new object[fakturalar.Count];
            int j = 0;
            foreach (var item in fakturalar)
            {
                string orderStatus = Statuses.DesignerStatus(item.DesignerStatus);
                string link = "?q=" + Encrypt.EncryptString("Sira=" + item.OrderId.ToString());
                UserData[j] = new object[] { j + 1, item.CreateOn.ToString("MM/dd/yyyy"), item.OrderId, item.CustomerName, item.CustomerSurname, item.CustomerFatherName, item.Tel1, item.Location, orderStatus, link,item.DesignerStatus };
                j++;
            }
            return Json(UserData, JsonRequestBehavior.AllowGet);

        }

        //Cordinator
        [Auth]       
        [EncryptedActionParameter]
        public ActionResult CloseOrder(int? Sira)
        {
            if (CurrentSession.User.IsCord==true || CurrentSession.User.IsAdmin==true)
            {
                if (Sira == null)
                {
                    return RedirectToAction("/Home/HasError");
                }
                List<Users> users = userManager.ListQueryable().Where(x => x.IsVisitor == true && x.IsActive == true).ToList();
                var listvisitor = users.Select(s => new SelectListItem { Value = s.UserName, Text = s.UserDisplayName }).ToList<SelectListItem>();
                List<Users> designers = userManager.ListQueryable().Where(x => x.IsDesigner == true && x.IsActive == true).ToList();
                var listdesigner = designers.Select(s => new SelectListItem { Value = s.UserName, Text = s.UserDisplayName }).ToList<SelectListItem>();
                Orders order = orderManager.Find(x => x.OrderId == Sira);
                if (order == null)
                {
                    return RedirectToAction("/Home/HasError");
                }
                ViewBag.Visitor = listvisitor;
                ViewBag.Designer = listdesigner;
                if (order.IsActive == false)
                {
                    return RedirectToAction("ActiveOrders");
                }
                return View(order);
            }
            else
            {
                TempData["msg"] = "Sifariş bağlamağa icazəniz yoxdur!";
                TempData["typ"] = "error";
                return RedirectToAction("ActiveOrders");
            }           
        }
        [Auth]       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CloseOrder(Orders data)
        {
            if (CurrentSession.User.IsCord == true || CurrentSession.User.IsAdmin == true)
            {
                data.LastUpdate = DateTime.Now;
                data.UpdateUser = CurrentSession.User.UserName;
                BusinessLayerResult<Orders> order = orderManager.CloseOrder(data);
                if (order.Errors.Count > 0)
                {
                    order.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(data);
                }
                TempData["msg"] = "Sifariş bağlandı!";
                TempData["typ"] = "success";
                return RedirectToAction("ActiveOrders");
            }
            else
            {
                TempData["msg"] = "Sifariş bağlamağa icazəniz yoxdur!";
                TempData["typ"] = "error";
                return RedirectToAction("ActiveOrders");
            }
        }
    }
}