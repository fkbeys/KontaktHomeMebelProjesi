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
    //[Exc]
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
        [Auth]
        public ActionResult Index()
        {
            return View();
        }
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
                TempData["msg"] = "1";
                return RedirectToAction("NewOrder");
            }
            return View(data);
        }
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
                string orderStatus = "";
                if (item.OrderStatus == 1)
                {
                    orderStatus = "Gözləmədə";
                }
                else if (item.OrderStatus == 2)
                {
                    orderStatus = "Vizitor Təyin Edilib";
                }
                string link = "?q=" + Encrypt.EncryptString("Sira=" + item.OrderId.ToString());
                UserData[j] = new object[] { j + 1, item.CreateOn.ToString("MM/dd/yyyy"), item.OrderId, item.CustomerName, item.CustomerSurname, item.CustomerFatherName, item.Tel1, item.SellerCode, item.OrderStore, orderStatus, link };
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
                    string orderStatus = "";
                    if (item.OrderStatus == 1)
                    {
                        orderStatus = "Gözləmədə";
                    }
                    else if (item.OrderStatus == 2)
                    {
                        orderStatus = "Vizitor Təyin Edilib";
                    }
                    string link = "?q=" + Encrypt.EncryptString("Sira=" + item.OrderId.ToString());
                    UserData[j] = new object[] { j + 1, item.CreateOn.ToString("MM/dd/yyyy"), item.OrderId, item.CustomerName, item.CustomerSurname, item.CustomerFatherName, item.Tel1, item.SellerCode, item.OrderStore, orderStatus, link };
                    j++;
                }
                return Json(UserData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Home/Index");
            }

        }
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
                    TempData["msg"] = "2";
                    return RedirectToAction("ActiveOrders");
                }
                else if (ferq >= CurrentSession.User.EditDate)
                {
                    TempData["msg"] = "3";
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
            data.OrderStatus = 1;
            data.UpdateUser = CurrentSession.User.UserName;
            data.IsActive = true;
            if (CurrentSession.User.IsCord == true)
            {
                string visitorName = form["Visitor"].ToString();
                string designerName = form["Designer"].ToString();
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
            }
            else if (CurrentSession.User.IsSeller == true)
            {
                //TODO: seller duzelis
                TimeSpan gunler = DateTime.Now - data.CreateOn;
                decimal ferq = gunler.Days;
                if (data.OrderStatus != 1)
                {
                    TempData["msg"] = "2";
                    return RedirectToAction("ActiveOrders");
                }
                else if (ferq >= CurrentSession.User.EditDate)
                {
                    TempData["msg"] = "3";
                    return RedirectToAction("ActiveOrders");
                }
                else
                {
                    data.IsVisitorAdded = false;
                    data.IsDesignerAdded = false;
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
                TempData["msg"] = "1";
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
            fakturalar = orderManager.ListQueryable().Where(x => x.IsActive == true & x.VisitorCode == userName).ToList();
            var UserData = new object[fakturalar.Count];
            int j = 0;
            foreach (var item in fakturalar)
            {
                string orderStatus = "";
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
                string link = "?q=" + Encrypt.EncryptString("Sira=" + item.OrderId.ToString());
                UserData[j] = new object[] { j + 1, item.CreateOn.ToString("MM/dd/yyyy"), item.OrderId, item.CustomerName, item.CustomerSurname, item.CustomerFatherName, item.Tel1, item.Location, orderStatus, link };
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
                string orderStatus = "";
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
                string link = "?q=" + Encrypt.EncryptString("Sira=" + item.OrderId.ToString());
                UserData[j] = new object[] { j + 1, item.CreateOn.ToString("MM/dd/yyyy"), item.OrderId, item.CustomerName, item.CustomerSurname, item.CustomerFatherName, item.Tel1, item.Location, orderStatus, link };
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
            else if (order.VisitorStatus != 2)
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
        public ActionResult CustomerVisit(CustomerVisitData data, FormCollection form)
        {
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

            //var listanagruplar = anagruplar.Select(s => new SelectListItem { Value = s.san_kod, Text = s.san_isim }).ToList<SelectListItem>();
            //ViewBag.AnaGruplar = listanagruplar;

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
                    return HttpNotFound();
                }
                else
                {
                    BusinessLayerResult<Orders> data = orderManager.AcceptOrder(order, 1);
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
                return HttpNotFound();
            }
            else
            {
                BusinessLayerResult<Orders> data = orderManager.AcceptOrder(order, 2);
                if (data.Errors.Count > 0)
                {
                    data.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    TempData["msg"] = "0";
                    return RedirectToAction("DesignerOrders");
                }
            }
            TempData["msg"] = "2";
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
            fakturalar = orderManager.ListQueryable().Where(x => x.IsActive == true & x.DesignerCode == userName).ToList();
            var UserData = new object[fakturalar.Count];
            int j = 0;
            foreach (var item in fakturalar)
            {
                string orderStatus = "";
                if (item.DesignerStatus == 1)
                {
                    orderStatus = "Gözləmədə";
                }
                else if (item.DesignerStatus == 2)
                {
                    orderStatus = "Qəbul Edilib";
                }
                else if (item.DesignerStatus == 3)
                {
                    orderStatus = "Dizayn Tamamlanıb";
                }
                string link = "?q=" + Encrypt.EncryptString("Sira=" + item.OrderId.ToString());
                UserData[j] = new object[] { j + 1, item.CreateOn.ToString("MM/dd/yyyy"), item.OrderId, item.CustomerName, item.CustomerSurname, item.CustomerFatherName, item.Tel1, item.Location, orderStatus, link };
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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //var base64EncodedBytes = System.Convert.FromBase64String(Sira);
            //int sira1 = Convert.ToInt32(System.Text.Encoding.UTF8.GetString(base64EncodedBytes));

            Orders order = orderManager.Find(x => x.OrderId == Sira);
            if (order == null)
            {
                return HttpNotFound();
            }
            else if (order.DesignerStatus != 2)
            {
                TempData["msg"] = "3";
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
        public ActionResult DesignerEdit(CustomerVisitData data, FormCollection form)
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
                TempData["msg"] = "1";
                return RedirectToAction("DesignerOrders");
            }

            //var listanagruplar = anagruplar.Select(s => new SelectListItem { Value = s.san_kod, Text = s.san_isim }).ToList<SelectListItem>();
            //ViewBag.AnaGruplar = listanagruplar;

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
                fakturalar = orderManager.ListQueryable().Where(x => x.CreateOn >= startdate & x.CreateOn <= enddate & x.DesignerCode == userName).ToList();
            }
            else
            {
                fakturalar = orderManager.ListQueryable().Where(x => x.IsActive == true & x.CreateOn >= startdate & x.CreateOn <= enddate & x.DesignerCode == userName).ToList();
            }
            var UserData = new object[fakturalar.Count];
            int j = 0;
            foreach (var item in fakturalar)
            {
                string orderStatus = "";
                if (item.DesignerStatus == 1)
                {
                    orderStatus = "Gözləmədə";
                }
                else if (item.DesignerStatus == 2)
                {
                    orderStatus = "Qəbul Edilib";
                }
                else if (item.DesignerStatus == 3)
                {
                    orderStatus = "Dizayn Tamamlanıb";
                }
                string link = "?q=" + Encrypt.EncryptString("Sira=" + item.OrderId.ToString());
                UserData[j] = new object[] { j + 1, item.CreateOn.ToString("MM/dd/yyyy"), item.OrderId, item.CustomerName, item.CustomerSurname, item.CustomerFatherName, item.Tel1, item.Location, orderStatus, link };
                j++;
            }
            return Json(UserData, JsonRequestBehavior.AllowGet);

        }
        [Auth]
        [EncryptedActionParameter]
        public ActionResult CloseOrder(int? Sira)
        {
            if (CurrentSession.User.IsCord==true || CurrentSession.User.IsAdmin==true)
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
                if (order.IsActive == false)
                {
                    return RedirectToAction("ActiveOrders");
                }
                return View(order);
            }
            else
            {
                TempData["msg"] = "4";
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
                TempData["msg"] = "5";
                return RedirectToAction("ActiveOrders");
            }
            else
            {
                TempData["msg"] = "4";
                return RedirectToAction("ActiveOrders");
            }
        }
    }
}