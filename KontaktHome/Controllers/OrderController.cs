using BusinessLayer;
using BusinessLayer.Managers;
using BusinessLayer.Managers.LocalManagers;
using BusinessLayer.Managers.MikroManagers;
using BusinessLayer.QueryResult;
using Entities;
using Entities.Helper;
using Entities.Messages;
using Entities.Model;
using Entities.Model.LocalModels;
using Entities.Model.MikroModels;
using KontaktHome.Filters;
using KontaktHome.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Razor.Generator;
using System.Web.Services;

namespace KontaktHome.Controllers
{
    [Exc]
    [Auth]
    [OutputCache(NoStore = true, Duration = 0)]
    public class OrderController : Controller
    {
        private OrderManager orderManager = new OrderManager();
        private UserManager userManager = new UserManager();
        private AnaGrupManager anagrupManager = new AnaGrupManager();
        private VisitManager visitManager = new VisitManager();
        private ImagesManager imagesManager = new ImagesManager();
        private StoresManager storesManager = new StoresManager();
        private UserRolesManager userRoleManager = new UserRolesManager();
        private UserRolesMappingManager userRolesMappingManager = new UserRolesMappingManager();
        private ChangeLogManager changeLogManager = new ChangeLogManager();
        private ProductsManager productManager = new ProductsManager();
        private AdditionalChargesManager additionalChargesManager = new AdditionalChargesManager();
        private ProductionManager productionManager = new ProductionManager();
        private StoklarManager stoklarManager = new StoklarManager();
        private SatisFiyatiManager satisFiyatiManager = new SatisFiyatiManager();
        private UrunManager urunManager = new UrunManager();
        private UrunReceteleriManager urunReceteleriManager = new UrunReceteleriManager();
        private CariManager cariManager = new CariManager();
        private SifarisManager sifarisManager = new SifarisManager();
        private LocationGroupManager locationGroupManager = new LocationGroupManager();
        private LocationSubGroupManager locationSubGroupManager = new LocationSubGroupManager();
        private LocationNameManager locationNameManager = new LocationNameManager();
        private SormMerkeziManager sormMerkeziManager = new SormMerkeziManager();
        private StockInfoManager stockInfoManager = new StockInfoManager();

        public string orderStatus { get; set; }
        // GET: Order
        public ActionResult Index()
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);  // HTTP 1.1.
            //Response.Cache.AppendCacheExtension("no-store, must-revalidate");
            //Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
            //Response.AppendHeader("Expires", "0"); // Proxies.
            return View();
        }
        //Seller
        [CustomAuthorize(Roles = "Admin,Kordinator,Satici")]
        public async Task<ActionResult> NewOrder(string status)
        {
            ViewBag.Status = status;
            List<LocationGroup> _locationGroups = new List<LocationGroup>();
            _locationGroups = await locationGroupManager.GetGroupsAsync();
            List<LocationSubGroup> _locationSubGroup = new List<LocationSubGroup>();
            _locationSubGroup = await locationSubGroupManager.GetSubGroup();
            List<LocationNames> _locationName = new List<LocationNames>();
            _locationName = await locationNameManager.GetLocationName();
            ViewBag.LocationGroup = _locationGroups;
            ViewBag.LocationSubGroup = _locationSubGroup;
            ViewBag.LocationNames = _locationName;
            return View();
        }
        [CustomAuthorize(Roles = "Admin,Kordinator,Satici")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewOrder(Orders data)
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
                data.CustomerName = TextHelpers.CapitalizeFirstLetter(data.CustomerName.Trim());
                data.CustomerSurname = TextHelpers.CapitalizeFirstLetter(data.CustomerSurname.Trim());
                data.CustomerFatherName = TextHelpers.CapitalizeFirstLetter(data.CustomerFatherName.Trim());
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
            List<LocationGroup> _locationGroups = new List<LocationGroup>();
            _locationGroups = await locationGroupManager.GetGroupsAsync();
            List<LocationSubGroup> _locationSubGroup = new List<LocationSubGroup>();
            _locationSubGroup = await locationSubGroupManager.GetSubGroup();
            List<LocationNames> _locationName = new List<LocationNames>();
            _locationName = await locationNameManager.GetLocationName();
            ViewBag.LocationGroup = _locationGroups;
            ViewBag.LocationSubGroup = _locationSubGroup;
            ViewBag.LocationNames = _locationName;
            return View(data);
        }

        //Cordinator and seller
        [CustomAuthorize(Roles = "Admin,Kordinator,Satici")]
        public ActionResult ActiveOrders()
        {
            List<Users> istifadeciler = new List<Users>();
            List<SORUMLULUK_MERKEZLERI> magazalar = new List<SORUMLULUK_MERKEZLERI>();
            if (User.IsInRole("Satici"))
            {
                istifadeciler = (from user in userManager.ListQueryable()
                                 join roleMapping in userRolesMappingManager.ListQueryable()
                                 on user.UserID equals roleMapping.UserID
                                 join role in userRoleManager.ListQueryable()
                                 on roleMapping.RoleID equals role.ID
                                 where role.RoleName == "Satici" && user.IsActive == true && user.StoreCode==CurrentSession.User.StoreCode
                                 select user
                               ).ToList();
                magazalar = sormMerkeziManager.GetData(CurrentSession.User.StoreCode);
            }
            else
            {
                istifadeciler = (from user in userManager.ListQueryable()
                                 join roleMapping in userRolesMappingManager.ListQueryable()
                                 on user.UserID equals roleMapping.UserID
                                 join role in userRoleManager.ListQueryable()
                                 on roleMapping.RoleID equals role.ID
                                 where role.RoleName == "Satici" && user.IsActive == true
                                 select user
                               ).ToList();
                magazalar = sormMerkeziManager.GetData();
            }
            var saticilar = istifadeciler.Select(s => new SelectListItem { Value = s.UserName, Text = s.UserDisplayName }).ToList();
            var magaza = magazalar.Select(x => new SelectListItem { Value = x.som_isim, Text = x.som_kod }).ToList();
            ViewBag.Seller = saticilar;
            ViewBag.Stores = magaza;
            //read cookies---------------------------------
            HttpCookie searchCookie = Request.Cookies["axtarisCookie"];
            OrderSearch axtaris = new OrderSearch();
            if (searchCookie != null && searchCookie.Values != null && searchCookie.Expires<DateTime.Now)
            {
                axtaris.firstDate=searchCookie["firstDate"];
                axtaris.lastDate=searchCookie["lastDate"];
                axtaris.sellerCode = searchCookie["sellerCode"];
                axtaris.storeCode=searchCookie["storeCode"];
                axtaris.deletedOrders=Convert.ToBoolean(searchCookie["deletedOrders"]);
                axtaris.activeOrders=Convert.ToBoolean(searchCookie["activeOrders"]);
                axtaris.status=Convert.ToInt16(searchCookie["status"]);
            }
            ViewBag.Axtaris = axtaris;
            //----------------------------------------------
            return View();
        }

        [CustomAuthorize(Roles = "Admin,Kordinator,Satici")]
        [WebMethod]
        public ActionResult GetActiveOrders()
        {
            //axtaris cookie------------------------------------------
            HttpCookie searchCookie = Request.Cookies["axtarisCookie"];
            if (searchCookie!=null && searchCookie.Values!=null)
            {
                OrderSearch axtaris = new OrderSearch();
                axtaris.firstDate = searchCookie["firstDate"];
                axtaris.lastDate = searchCookie["lastDate"];
                axtaris.sellerCode = searchCookie["sellerCode"];
                axtaris.storeCode = searchCookie["storeCode"];
                axtaris.deletedOrders = Convert.ToBoolean(searchCookie["deletedOrders"]);
                axtaris.activeOrders = Convert.ToBoolean(searchCookie["activeOrders"]);
                axtaris.status = Convert.ToInt16(searchCookie["status"]);

                return RedirectToAction("GetActiveOrdersWithParametr",axtaris);
            }
            //----------------------------------------------------------
            List<Orders> fakturalar = new List<Orders>();
            if (User.IsInRole("Satici"))
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
                string link = "?Sira=" + item.OrderId.ToString();
                string customer = item.CustomerSurname + " " + item.CustomerName + " " + item.CustomerFatherName;
                UserData[j] = new object[] { item.OrderId, item.CreateOn.ToString("MM/dd/yyyy"), customer, item.Tel1, item.SellerCode, item.OrderStore, orderStatus, link, orderAktivstatus };
                j++;
            }
            return Json(UserData, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(Roles = "Admin,Kordinator,Satici")]
        [WebMethod]
        public ActionResult GetActiveOrdersWithParametr(OrderSearch data)
        {
            try
            {
                IEnumerable<Orders> fakturalar = new List<Orders>();
                string sellerName = null;
                string storeCode = null;
                if (User.IsInRole("Satici"))
                {
                    sellerName = CurrentSession.User.UserName;
                    storeCode = CurrentSession.User.StoreCode;
                }
                fakturalar = orderManager.GetOrdersWithParametr(data, sellerName,storeCode);
                var UserData = new object[fakturalar.Count()];
                int j = 0;
                foreach (var item in fakturalar)
                {
                    string orderStatus = Statuses.OrderStatus(item.OrderStatus);
                    string orderAktivstatus = Statuses.OrderActiveStatus(item.IsActive);
                    string link = "?Sira=" + item.OrderId.ToString();
                    string customer = item.CustomerSurname + " " + item.CustomerName + " " + item.CustomerFatherName;

                    UserData[j] = new object[] { item.OrderId, item.CreateOn.ToString("MM/dd/yyyy"), customer, item.Tel1, item.SellerCode, item.OrderStore, orderStatus, link, orderAktivstatus };
                    j++;
                }

                //axtaris cookie
                HttpCookie searchCookie = new HttpCookie("axtarisCookie");
                searchCookie["firstDate"] = data.firstDate;
                searchCookie["lastDate"] = data.lastDate;
                searchCookie["sellerCode"] = data.sellerCode;
                searchCookie["storeCode"] = data.storeCode;
                searchCookie["deletedOrders"] = data.deletedOrders.ToString();
                searchCookie["activeOrders"] = data.activeOrders.ToString();
                searchCookie["status"] = data.status.ToString();
                searchCookie.Expires = DateTime.Now.AddMinutes(10);
                Response.Cookies.Add(searchCookie);

                return Json(UserData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return RedirectToAction("Home/Index");
            }

        }

        //All
        [CustomAuthorize(Roles = "Admin,Kordinator,Satici")]
        public async Task<ActionResult> EditOrder(int? Sira)
        {
            if (Sira == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Users> users = (from user in userManager.ListQueryable()
                                 join roleMapping in userRolesMappingManager.ListQueryable()
                                 on user.UserID equals roleMapping.UserID
                                 join role in userRoleManager.ListQueryable()
                                 on roleMapping.RoleID equals role.ID
                                 where role.RoleName == "Vizitor" && user.IsActive == true
                                 select user
                               ).ToList();
            var listvisitor = users.Select(s => new SelectListItem { Value = s.UserName, Text = s.UserDisplayName }).ToList<SelectListItem>();
            List<Users> designers = (from user in userManager.ListQueryable()
                                     join roleMapping in userRolesMappingManager.ListQueryable()
                                     on user.UserID equals roleMapping.UserID
                                     join role in userRoleManager.ListQueryable()
                                     on roleMapping.RoleID equals role.ID
                                     where role.RoleName == "Dizayner" && user.IsActive == true
                                     select user
                               ).ToList();
            var listdesigner = designers.Select(s => new SelectListItem { Value = s.UserName, Text = s.UserDisplayName }).ToList<SelectListItem>();
            List<Users> planners = (from user in userManager.ListQueryable()
                                    join roleMapping in userRolesMappingManager.ListQueryable()
                                    on user.UserID equals roleMapping.UserID
                                    join role in userRoleManager.ListQueryable()
                                    on roleMapping.RoleID equals role.ID
                                    where role.RoleName == "Planlamaci" && user.IsActive == true
                                    select user
                              ).ToList();
            var listplanner = planners.Select(s => new SelectListItem { Value = s.UserName, Text = s.UserDisplayName }).ToList<SelectListItem>();
            Orders order = orderManager.Find(x => x.OrderId == Sira);
            ViewBag.Visitor = listvisitor;
            ViewBag.Designer = listdesigner;
            ViewBag.Planner = listplanner;
            //ViewBag.OrderStatus = Statuses.ListOrderStatuses();

            ViewBag.Link = "/Order/VisitInfo?Sira=" + order.OrderId.ToString();
            if (order == null)
            {
                return HttpNotFound();
            }
            TimeSpan gunler = DateTime.Now - order.CreateOn;
            decimal ferq = gunler.Days;
            if (User.IsInRole("Satici"))
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
            List<LocationGroup> _locationGroups = new List<LocationGroup>();
            _locationGroups = await locationGroupManager.GetGroupsAsync();
            List<LocationSubGroup> _locationSubGroup = new List<LocationSubGroup>();
            _locationSubGroup = await locationSubGroupManager.GetSubGroup();
            List<LocationNames> _locationName = new List<LocationNames>();
            _locationName = await locationNameManager.GetLocationName();
            ViewBag.LocationGroup = _locationGroups;
            ViewBag.LocationSubGroup = _locationSubGroup;
            ViewBag.LocationNames = _locationName;
            ViewBag.VisitorStatuses = Statuses.ListVisitorStatuses();
            ViewBag.DesignerStatuses = Statuses.ListDesignerStatuses();
            ViewBag.PlannerStatuses = Statuses.ListPlannerStatuses();
            return View(order);
        }

        [CustomAuthorize(Roles = "Admin,Kordinator,Satici")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditOrder(Orders data, FormCollection form)
        {
            data.LastUpdate = DateTime.Now;
            data.UpdateUser = CurrentSession.User.UserName;
            if (User.IsInRole("Kordinator") || User.IsInRole("Admin"))
            {
                if (data.VisitorStatus < 4)
                {
                    if (data.IsVisitorAdded == true)
                    {
                        data.OrderStatus = 2;
                        data.PlannerStatus = 0;
                        data.DesignerStatus = 0;
                        data.VisitorStatus = 1;
                        BusinessLayerResult<Users> users = userManager.GetUserInformation(data.VisitorCode);
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
                        data.VisitorStatus = 0;
                        data.PlannerStatus = 0;
                        data.DesignerStatus = 0;
                        data.IsDesignerAdded = false;
                        data.IsPlannerAdded = false;
                        data.VisitorCode = "";
                        data.VisitorName = "";
                    }
                }
                if (data.PlannerStatus < 4 && data.PlannerStatus >0)
                {
                    if (data.VisitorStatus == 4)
                    {
                        data.OrderStatus = 5;
                    }
                    if (data.IsPlannerAdded == true)
                    {
                        data.PlannerStatus = 1;
                        data.DesignerStatus = 0;
                        BusinessLayerResult<Users> users = userManager.GetUserInformation(data.PlannerCode);
                        if (users.Errors.Count == 0)
                        {
                            data.PlannerName = users.Result.UserDisplayName;
                        }
                        else
                        {
                            data.PlannerName = "User Not Found";
                        }
                    }
                    else
                    {
                        data.PlannerStatus = 0;
                        data.OrderStatus = 4;
                        data.PlannerCode = "";
                        data.PlannerName = "";
                        data.DesignerStatus = 0;
                    }
                }
                //TODO: dizayner duzelis edilecek
                if (data.DesignerStatus < 4 && data.DesignerStatus>0)
                {
                    //string designerName = "";
                    if (data.PlannerStatus == 4)
                    {
                        //designerName = form["Designer"].ToString();
                        data.OrderStatus = 8;
                    }
                    if (data.IsDesignerAdded == true)
                    {
                        data.DesignerStatus = 1;
                        //data.DesignerCode = designerName;
                        BusinessLayerResult<Users> users = userManager.GetUserInformation(data.DesignerCode);
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
                        data.OrderStatus = 7;
                        data.DesignerCode = "";
                        data.DesignerName = "";
                    }
                }

            }
            else if (User.IsInRole("Satici"))
            {
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
            List<Users> users1 = (from user in userManager.ListQueryable()
                                  join roleMapping in userRolesMappingManager.ListQueryable()
                                  on user.UserID equals roleMapping.UserID
                                  join role in userRoleManager.ListQueryable()
                                  on roleMapping.RoleID equals role.ID
                                  where role.RoleName == "Vizitor" && user.IsActive == true
                                  select user
                          ).ToList();
            var listvisitor = users1.Select(s => new SelectListItem { Value = s.UserName, Text = s.UserDisplayName }).ToList<SelectListItem>();
            List<Users> designers1 = (from user in userManager.ListQueryable()
                                      join roleMapping in userRolesMappingManager.ListQueryable()
                                      on user.UserID equals roleMapping.UserID
                                      join role in userRoleManager.ListQueryable()
                                      on roleMapping.RoleID equals role.ID
                                      where role.RoleName == "Dizayner" && user.IsActive == true
                                      select user
                                ).ToList();
            var listdesigner = designers1.Select(s => new SelectListItem { Value = s.UserName, Text = s.UserDisplayName }).ToList<SelectListItem>();
            List<Users> planners = (from user in userManager.ListQueryable()
                                    join roleMapping in userRolesMappingManager.ListQueryable()
                                    on user.UserID equals roleMapping.UserID
                                    join role in userRoleManager.ListQueryable()
                                    on roleMapping.RoleID equals role.ID
                                    where role.RoleName == "Planlamaci" && user.IsActive == true
                                    select user
                          ).ToList();
            var listplanner = planners.Select(s => new SelectListItem { Value = s.UserName, Text = s.UserDisplayName }).ToList<SelectListItem>();
            ViewBag.Visitor = listvisitor;
            ViewBag.Designer = listdesigner;
            ViewBag.Planner = listplanner;
            ViewBag.Link = "/Order/VisitInfo?Sira=" + data.OrderId.ToString();

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
            List<LocationGroup> _locationGroups = new List<LocationGroup>();
            _locationGroups = await locationGroupManager.GetGroupsAsync();
            List<LocationSubGroup> _locationSubGroup = new List<LocationSubGroup>();
            _locationSubGroup = await locationSubGroupManager.GetSubGroup();
            List<LocationNames> _locationName = new List<LocationNames>();
            _locationName = await locationNameManager.GetLocationName();
            ViewBag.LocationGroup = _locationGroups;
            ViewBag.LocationSubGroup = _locationSubGroup;
            ViewBag.LocationNames = _locationName;
            ViewBag.VisitorStatuses = Statuses.ListVisitorStatuses();
            ViewBag.DesignerStatuses = Statuses.ListDesignerStatuses();
            ViewBag.PlannerStatuses = Statuses.ListPlannerStatuses();
            return View(data);
        }
        public ActionResult OrderInfo(int? Sira)
        {
            if (Sira == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Users> users = (from user in userManager.ListQueryable()
                                 join roleMapping in userRolesMappingManager.ListQueryable()
                                 on user.UserID equals roleMapping.UserID
                                 join role in userRoleManager.ListQueryable()
                                 on roleMapping.RoleID equals role.ID
                                 where role.RoleName == "Vizitor" && user.IsActive == true
                                 select user
                               ).ToList();
            var listvisitor = users.Select(s => new SelectListItem { Value = s.UserName, Text = s.UserDisplayName }).ToList<SelectListItem>();
            List<Users> designers = (from user in userManager.ListQueryable()
                                     join roleMapping in userRolesMappingManager.ListQueryable()
                                     on user.UserID equals roleMapping.UserID
                                     join role in userRoleManager.ListQueryable()
                                     on roleMapping.RoleID equals role.ID
                                     where role.RoleName == "Dizayner" && user.IsActive == true
                                     select user
                               ).ToList();
            var listdesigner = designers.Select(s => new SelectListItem { Value = s.UserName, Text = s.UserDisplayName }).ToList<SelectListItem>();
            Orders order = orderManager.Find(x => x.OrderId == Sira);
            if (order == null)
            {
                return HttpNotFound();
            }
            List<ChangeLog> changeLog = changeLogManager.List(x => x.PrimaryKeyValue == order.OrderId.ToString() && x.EntityName == "Orders").ToList();
            ViewBag.Visitor = listvisitor;
            ViewBag.Designer = listdesigner;
            ViewBag.ChangeLog = changeLog;
            return View(order);
        }
        //Visitor
        [CustomAuthorize(Roles = "Admin,Kordinator,Vizitor")]
        public ActionResult VisitorOrders(string status)
        {
            ViewBag.Status = status;
            return View();
        }

        [CustomAuthorize(Roles = "Admin,Kordinator,Vizitor")]
        [WebMethod]
        public ActionResult GetVisitorActiveOrders()
        {
            string userName = CurrentSession.User.UserName;
            List<Orders> fakturalar = new List<Orders>();
            fakturalar = orderManager.ListQueryable().Where(x => x.IsActive == true & x.VisitorCode == userName & x.VisitorStatus < 4).ToList();
            var UserData = new object[fakturalar.Count];
            int j = 0;
            foreach (var item in fakturalar)
            {
                string orderStatus = Statuses.VisitorOrderStatus(item.VisitorStatus);
                string link = "?Sira=" + item.OrderId.ToString();
                string customer = item.CustomerSurname + " " + item.CustomerName + " " + item.CustomerFatherName;
                UserData[j] = new object[] { item.OrderId, item.CreateOn.ToString("MM/dd/yyyy"), customer, item.Tel1, item.Location, orderStatus, link, item.VisitorStatus };
                j++;
            }
            return Json(UserData, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(Roles = "Admin,Kordinator,Vizitor")]
        [WebMethod]
        public ActionResult GetVisitorActiveOrdersWithParametr(OrderSearch data)
        {

            string userName = CurrentSession.User.UserName;
            IEnumerable<Orders> fakturalar = orderManager.GetVisitorOrdersWithParametr(data, userName);
            //DateTime startdate = Convert.ToDateTime(data.firstDate);
            //DateTime enddate = Convert.ToDateTime(data.lastDate + " 23:59:59");
            //if (data.deletedOrders == true)
            //{
            //    fakturalar = orderManager.ListQueryable().Where(x => x.CreateOn >= startdate & x.CreateOn <= enddate & x.VisitorCode == userName & x.IsActive == true).ToList();
            //}
            //else
            //{
            //    fakturalar = orderManager.ListQueryable().Where(x => x.IsActive == true & x.CreateOn >= startdate & x.CreateOn <= enddate & x.VisitorCode == userName & x.VisitorStatus < 4).ToList();
            //}
            var UserData = new object[fakturalar.Count()];
            int j = 0;
            foreach (var item in fakturalar)
            {
                string orderStatus = Statuses.VisitorOrderStatus(item.VisitorStatus);
                string link = "?Sira=" + item.OrderId.ToString();
                string customer = item.CustomerSurname + " " + item.CustomerName + " " + item.CustomerFatherName;
                UserData[j] = new object[] { item.OrderId, item.CreateOn.ToString("MM/dd/yyyy"), customer, item.Tel1, item.Location, orderStatus, link, item.VisitorStatus };
                j++;
            }
            return Json(UserData, JsonRequestBehavior.AllowGet);

        }

        //[CustomAuthorize(Roles = "Admin,Kordinator,Vizitor,Satici,")]
        //[EncryptedActionParameter]
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
            else
            {
                List<Visits> visits = visitManager.List(x => x.OrderId == Sira).ToList();
                OrderFileUpload uploadedFiles = new OrderFileUpload();
                OrderData data = new OrderData();
                List<VisitImages> visitimages = new List<VisitImages>();
                if (visits.Count > 0)
                {
                    Visits visitGuid = visits.FirstOrDefault(x => x.OrderId == Sira);
                    visitimages = imagesManager.List(x => x.VisitGuid == visitGuid.VisitGuid);
                }
                data.order = order;
                data.orderFiles = uploadedFiles;
                data.visitData = visits;
                data.visitImages = visitimages;
                data.production = productionManager.ListQueryable().Where(x => x.OrderId == Sira).ToList();
                data.orderStatus = Statuses.OrderStatus(order.OrderStatus);
                data.visitorStatus = Statuses.VisitorOrderStatus(order.VisitorStatus);
                data.designerStaus = Statuses.DesignerStatus(order.DesignerStatus);
                data.plannerStatus = Statuses.PlannerStatus(order.PlannerStatus);
                data.itemGroups = new SelectList(anagrupManager.listAnaqruplar(), "san_kod", "san_isim");
                data.stockInfo = stockInfoManager.ListQueryable().Where(x => x.OrderNo == Sira).ToList();
                return View(data);
            }
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SellerVisitEdit(OrderData data)
        {
            BusinessLayerResult<Visits> visitDataUpdate = visitManager.SellerUpdate(data.visitData, CurrentSession.User);
            return RedirectToAction("VisitInfo", "Order", new { Sira=data.order.OrderId } );
        }

        [CustomAuthorize(Roles = "Admin,Kordinator,Vizitor")]
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
                if (visits.Count > 0)
                {
                    Visits visitGuid = visits.FirstOrDefault(x => x.OrderId == Sira);
                    visitimages = imagesManager.List(x => x.VisitGuid == visitGuid.VisitGuid);
                }
                data.order = order;
                data.orderFiles = uploadedFiles;
                data.visitData = visits;
                data.visitImages = visitimages;
                data.itemGroups = new SelectList(anagrupManager.listAnaqruplar(), "san_kod", "san_isim");
                return View(data);
            }
        }

        [CustomAuthorize(Roles = "Admin,Kordinator,Vizitor")]
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
            data.itemGroups = new SelectList(anagrupManager.listAnaqruplar(), "san_kod", "san_isim");
            if (data.visitImages == null)
            {
                data.visitImages = new List<VisitImages>();
            }
            if (data.visitData != null)
            {
                foreach (var item in data.visitData)
                {
                    if (item.ProductCode != null)
                    {
                        var anagrupadi = anagrupManager.listAnaqruplar().Where(x => x.san_kod == item.ProductCode).Select(p => p.san_isim).Single();
                        item.ProductName = anagrupadi;
                    }
                    else if (data.visitData.Count == 1)
                    {
                        ModelState.AddModelError("", "Məhsul seçilməyib");
                        return View(data);
                    }
                }
            }
            List<VisitImages> visitImages = new List<VisitImages>();
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Visits> visitData = visitManager.VisitorInsert(data.visitData, data.order.OrderId, CurrentSession.User);
                if (visitData.Errors.Count > 0)
                {
                    visitData.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(data);
                }
                BusinessLayerResult<Visits> visitDataUpdate = visitManager.VisitorUpdate(data.visitData, data.order.OrderId, CurrentSession.User);
                if (visitDataUpdate.Errors.Count > 0)
                {
                    visitDataUpdate.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
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
                            string fileType = file.ContentType.ToLower();
                            if (CheckFileType.checkIfImage(fileType))
                            {
                                WebImage img = new WebImage(file.InputStream);
                                if (img.Width > 1280)
                                    img.Resize(1280, 800);
                                var InputFileName = Path.GetFileName(file.FileName);
                                var ServerSavePath = Path.Combine(Server.MapPath("~/UploadedFiles/" + visitData.Result.VisitGuid.ToString() + "/") + InputFileName);
                                var imagePath = Path.Combine("~/UploadedFiles/" + visitData.Result.VisitGuid.ToString() + "/" + InputFileName);
                                img.Save(ServerSavePath);
                                visitImages.Add(new VisitImages() { VisitGuid = visitData.Result.VisitGuid, ImageName = InputFileName.ToString(), ImagePath = imagePath.ToString(), ImageType = 1 });
                            }
                        }
                    }
                    BusinessLayerResult<VisitImages> imageData = imagesManager.SaveImages(visitImages);
                }
                BusinessLayerResult<Orders> order = orderManager.AcceptOrder(data.order, visitorstatus);
                TempData["msg"] = "1";
                return RedirectToAction("VisitorOrders");
            }
            else
            {
                if (data.visitData == null)
                {
                    data.visitData = new List<Visits>();
                }
            }
            return View(data);
        }

        [CustomAuthorize(Roles = "Admin,Kordinator,Vizitor")]

        public ActionResult AcceptOrder(int? Sira)
        {
            if (Sira == null)
            {
                return RedirectToAction("/Home/HasError");
            }
            if (User.IsInRole("Vizitor") || User.IsInRole("Admin")) /*(CurrentSession.User.IsVisitor == true || CurrentSession.User.IsAdmin == true)*/
            {
                Orders order = orderManager.Find(x => x.OrderId == Sira);
                if (order == null)
                {
                    return RedirectToAction("/Home/HasError");
                }
                else
                {
                    if (order.VisitorStatus >= 2)
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
        [CustomAuthorize(Roles = "Admin,Kordinator,Dizayner")]

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
                if (order.DesignerStatus >= 2)
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

        [CustomAuthorize(Roles = "Admin,Kordinator,Dizayner")]
        public ActionResult DesignerOrders(string status)
        {
            ViewBag.Status = status;
            return View();
        }

        [CustomAuthorize(Roles = "Admin,Kordinator,Dizayner")]
        [WebMethod]
        public ActionResult GetDesignerActiveOrders()
        {
            string userName = CurrentSession.User.UserName;
            List<Orders> fakturalar = new List<Orders>();
            fakturalar = orderManager.ListQueryable().Where(x => x.IsActive == true & x.DesignerCode == userName & x.VisitorStatus == 4 & x.DesignerStatus < 4).ToList();
            var UserData = new object[fakturalar.Count];
            int j = 0;
            foreach (var item in fakturalar)
            {
                string orderStatus = Statuses.DesignerStatus(item.DesignerStatus);
                string link = "?Sira=" + item.OrderId.ToString();
                string customer = item.CustomerSurname + " " + item.CustomerName + " " + item.CustomerFatherName;
                UserData[j] = new object[] { item.OrderId, item.CreateOn.ToString("MM/dd/yyyy"), customer, item.Tel1, item.Location, orderStatus, link, item.DesignerStatus };
                j++;
            }
            return Json(UserData, JsonRequestBehavior.AllowGet);

        }

        [CustomAuthorize(Roles = "Admin,Kordinator,Dizayner")]

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
                var visitGuid = visits.Where(x => x.OrderId == Sira).Select(m => m.VisitGuid).First();
                data.order = order;
                data.orderFiles = uploadedFiles;
                data.visitData = visits;
                data.visitImages = imagesManager.List(x => x.VisitGuid == visitGuid);
                return View(data);
            }
        }

        [CustomAuthorize(Roles = "Admin,Kordinator,Dizayner")]
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
            var visitGuid = data.visitData.Where(x => x.OrderId == data.order.OrderId).Select(m => m.VisitGuid).First();
            data.visitImages = imagesManager.List(x => x.VisitGuid == visitGuid);
            List<VisitImages> visitImages = new List<VisitImages>();

            BusinessLayerResult<Visits> visitData = visitManager.DesignerUpdate(data.visitData, data.order.OrderId, CurrentSession.User);
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
                        string fileType = file.ContentType.ToLower();
                        if (CheckFileType.checkIfImage(fileType))
                        {
                            WebImage img = new WebImage(file.InputStream);
                            if (img.Width > 1280)
                                img.Resize(1280, 800);
                            var InputFileName = Path.GetFileName(file.FileName);
                            var ServerSavePath = Path.Combine(Server.MapPath("~/UploadedFiles/" + visitData.Result.VisitGuid.ToString() + "/") + InputFileName);
                            var imagePath = Path.Combine("~/UploadedFiles/" + visitData.Result.VisitGuid.ToString() + "/" + InputFileName);
                            img.Save(ServerSavePath);
                            visitImages.Add(new VisitImages() { VisitGuid = visitData.Result.VisitGuid, ImageName = InputFileName.ToString(), ImagePath = imagePath.ToString(), ImageType = 2 });
                        }
                    }
                }
                BusinessLayerResult<VisitImages> imageData = imagesManager.SaveImages(visitImages);
            }
            BusinessLayerResult<Orders> order = orderManager.AcceptOrder(data.order, designerstatus);
            if (order.Errors.Count > 0)
            {
                visitData.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                TempData["msg"] = "Qeyd Yenilənmədi!";
                TempData["typ"] = "error";
                return View(data);
            }
            else
            {
                TempData["msg"] = "Qeyd Yeniləndi!";
                TempData["typ"] = "success";
                return RedirectToAction("DesignerOrders");
            }
        }

        [CustomAuthorize(Roles = "Admin,Kordinator,Dizayner")]
        [WebMethod]
        public ActionResult GetDesignerActiveOrdersWithParametr(OrderSearch data)
        {
            string userName = CurrentSession.User.UserName;
            //List<Orders> fakturalar = new List<Orders>();
            //DateTime startdate = Convert.ToDateTime(data.firstDate);
            //DateTime enddate = Convert.ToDateTime(data.lastDate + " 23:59:59");
            //if (data.deletedOrders == true)
            //{
            //    fakturalar = orderManager.ListQueryable().Where(x => x.CreateOn >= startdate & x.CreateOn <= enddate & x.DesignerCode == userName & x.IsActive == true).ToList();
            //}
            //else
            //{
            //    fakturalar = orderManager.ListQueryable().Where(x => x.IsActive == true & x.CreateOn >= startdate & x.CreateOn <= enddate & x.DesignerCode == userName & x.DesignerStatus < 4).ToList();
            //}
            IEnumerable<Orders> fakturalar = orderManager.GetDesignerOrdersWithParametr(data, userName);
            var UserData = new object[fakturalar.Count()];
            int j = 0;
            foreach (var item in fakturalar)
            {
                string orderStatus = Statuses.DesignerStatus(item.DesignerStatus);
                string link = "?Sira=" + item.OrderId.ToString();
                string customer = item.CustomerSurname + " " + item.CustomerName + " " + item.CustomerFatherName;
                UserData[j] = new object[] { item.OrderId, item.CreateOn.ToString("MM/dd/yyyy"), customer, item.Tel1, item.Location, orderStatus, link, item.DesignerStatus };
                j++;
            }
            return Json(UserData, JsonRequestBehavior.AllowGet);

        }

        //Cordinator
        [CustomAuthorize(Roles = "Admin,Kordinator")]

        public ActionResult CloseOrder(int? Sira)
        {
            if (User.IsInRole("Kordinator") || User.IsInRole("Admin")) /*(CurrentSession.User.IsCord == true || CurrentSession.User.IsAdmin == true)*/
            {
                if (Sira == null)
                {
                    return RedirectToAction("/Home/HasError");
                }
                List<Users> users = (from user in userManager.ListQueryable()
                                     join roleMapping in userRolesMappingManager.ListQueryable()
                                     on user.UserID equals roleMapping.UserID
                                     join role in userRoleManager.ListQueryable()
                                     on roleMapping.RoleID equals role.ID
                                     where role.RoleName == "Vizitor" && user.IsActive == true
                                     select user
                               ).ToList();
                var listvisitor = users.Select(s => new SelectListItem { Value = s.UserName, Text = s.UserDisplayName }).ToList<SelectListItem>();
                List<Users> designers = (from user in userManager.ListQueryable()
                                         join roleMapping in userRolesMappingManager.ListQueryable()
                                         on user.UserID equals roleMapping.UserID
                                         join role in userRoleManager.ListQueryable()
                                         on roleMapping.RoleID equals role.ID
                                         where role.RoleName == "Dizayner" && user.IsActive == true
                                         select user
                               ).ToList();
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

        [CustomAuthorize(Roles = "Admin,Kordinator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CloseOrder(Orders data)
        {
            if (User.IsInRole("Kordinator") || User.IsInRole("Admin")) //if (CurrentSession.User.IsCord == true || CurrentSession.User.IsAdmin == true)
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

        public ActionResult ProductionRecipe(int? visitId, int? orderId)
        {
            ProductionData prorecipe = new ProductionData();
            prorecipe.order = orderManager.Find(x => x.OrderId == orderId);
            if (prorecipe.order == null)
            {
                return RedirectToAction("Index", "Home");
            }
            prorecipe.visits = visitManager.Find(x => x.VisitID == visitId);
            if (prorecipe.visits == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<AdditionalCharges> charges = new List<AdditionalCharges>();
            charges = additionalChargesManager.List();
            prorecipe.itemGroups = new SelectList(charges, "charge_value", "charge_name");
            prorecipe.productionItems = productionManager.List(x => x.OrderId == orderId && x.VisitId == visitId);
            return View(prorecipe);
        }
        [WebMethod]
        public ActionResult GetProducts()
        {
            List<Products> product = new List<Products>();
            product = productManager.GetProducts();
            return Json(new { data = product });
            //Datatable serverside processing
            //var draw = Request.Form.GetValues("draw").FirstOrDefault();
            //var start = Request.Form.GetValues("start").FirstOrDefault();
            //var length = Request.Form.GetValues("length").FirstOrDefault();
            //var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            //var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            //var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();


            ////Paging Size (10,20,50,100)
            //int pageSize = length != null ? Convert.ToInt32(length) : 0;
            //int skip = start != null ? Convert.ToInt32(start) : 0;
            //int recordsTotal = 0;

            //// Getting all Customer data
            //var customerData = productManager.GetProducts();

            ////Sorting
            ////if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            ////{
            ////    customerData = customerData.OrderBy(sortColumn + " " + sortColumnDir);
            ////}
            ////Search
            //if (!string.IsNullOrEmpty(searchValue))
            //{
            //    customerData = customerData.Where(m => m.product_name == searchValue).ToList();
            //}

            ////total number of rows count
            //recordsTotal = customerData.Count();
            ////Paging
            //var data = customerData.Skip(skip).Take(pageSize).ToList();
            ////Returning Json Data
            //return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });


        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult SaveRecipe(SaveRecipe data)
        {
            bool status = false;
            if (data.recipe_Items.Count > 0)
            {
                foreach (var item in data.recipe_Items)
                {
                    if (item.ProductTotal == 0)
                    {
                        string[] errors = new string[1];
                        errors[0] = "Stok siyahısında toplamı 0 olan stok mövcuddur";
                        status = false;
                        return Json(new { status, errors });
                    }
                }
            }
            double faiz = 0;
            double cem = 0;
            double yekun = 0;
            if (data.recipe_Items.Count > 0)
            {
                faiz = data.recipe_Items[0].ProductCharges;
                foreach (var item in data.recipe_Items)
                {
                    cem += item.ProductTotal;
                }
                yekun = cem + ((cem * faiz) / 100);
            }

            BusinessLayerResult<Production> _productionUpdate = productionManager.UpdateData(data.recipe_Items, CurrentSession.User, cem, yekun);
            if (_productionUpdate.Errors.Count > 0)
            {
                string[] errors = new string[_productionUpdate.Errors.Count];
                for (int i = 0; i < _productionUpdate.Errors.Count; i++)
                {
                    errors[i] = _productionUpdate.Errors[i].Message;
                }
                status = false;
                return Json(new { status, errors });
            }
            BusinessLayerResult<Production> _productionInsert = productionManager.InsertData(data.recipe_Items, CurrentSession.User, cem, yekun);
            if (_productionInsert.Errors.Count > 0)
            {
                string[] errors = new string[_productionInsert.Errors.Count];
                for (int i = 0; i < _productionInsert.Errors.Count; i++)
                {
                    errors[i] = _productionInsert.Errors[i].Message;
                }
                status = false;
                return Json(new { status, errors });
            }
            BusinessLayerResult<Visits> _visit = visitManager.UpdatePrice(data.visit_No, yekun, CurrentSession.User);
            if (_visit.Errors.Count > 0)
            {
                string[] errors = new string[_visit.Errors.Count];
                for (int i = 0; i < _visit.Errors.Count; i++)
                {
                    errors[i] = _visit.Errors[i].Message;
                }
                status = false;
                return Json(new { status, errors });
            }
            productionManager.DeleteProdData(data.deleted_items);
            status = true;
            string link = "?Sira=" + data.order_No.ToString();
            return Json(new { status, link, Url = Url.Action("PlannerEdit", "Order") });

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult DeleteProduct(int? id)
        {
            return Json(new { });
        }
        [CustomAuthorize(Roles = "Admin,Kordinator")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult SaveToMikro(int? orderid, int? visitid)//TODO: mikro insert duzelisler et
        {
            bool status = false;
            string[] errors;
            if (orderid != null && visitid != null)
            {
                Visits _visit = visitManager.Find(x => x.VisitID == visitid && x.OrderId == orderid);
                Orders _order = orderManager.Find(x => x.OrderId == orderid);
                if (_visit != null && _order != null)
                {
                    if (_visit.VisitStatus == 0)
                    {
                        string stokKodu = _order.OrderId.ToString("D4") + "." + _visit.VisitID.ToString() + "-" + _order.CustomerSurname.Substring(0,1) + _order.CustomerName.Substring(0,1);
                        string stokAdi = _order.CustomerSurname +" "+ _order.CustomerName + " sifarisNo-" + _order.OrderId.ToString() + " vizitNo-" + _visit.VisitID.ToString();
                        STOKLAR stokdata = new STOKLAR();
                        stokdata.sto_kod = stokKodu;
                        stokdata.sto_isim = stokAdi;
                        BusinessLayerResult<STOKLAR> _stoklar = stoklarManager.InsertData(stokdata, CurrentSession.User);
                        if (_stoklar.Errors.Count > 0)
                        {
                            errors = new string[_stoklar.Errors.Count];
                            for (int i = 0; i < _stoklar.Errors.Count; i++)
                            {
                                errors[i] = _stoklar.Errors[i].Message;
                            }
                            status = false;
                            return Json(new { status, errors });
                        }
                        STOK_SATIS_FIYAT_LISTELERI satisqiymeti = new STOK_SATIS_FIYAT_LISTELERI();
                        satisqiymeti.sfiyat_stokkod = stokKodu;
                        satisqiymeti.sfiyat_fiyati = _visit.FinalPrice;
                        BusinessLayerResult<STOK_SATIS_FIYAT_LISTELERI> _fiyatliste = satisFiyatiManager.InsertData(satisqiymeti, CurrentSession.User);
                        if (_fiyatliste.Errors.Count > 0)
                        {
                            errors = new string[_fiyatliste.Errors.Count];
                            for (int i = 0; i < _fiyatliste.Errors.Count; i++)
                            {
                                errors[i] = _fiyatliste.Errors[i].Message;
                            }
                            status = false;
                            return Json(new { status, errors });
                        }
                        URUNLER urundata = new URUNLER();
                        urundata.uru_stok_kod = stokKodu;
                        BusinessLayerResult<URUNLER> _urunler = urunManager.InsertData(urundata, CurrentSession.User);
                        if (_urunler.Errors.Count > 0)
                        {
                            errors = new string[_urunler.Errors.Count];
                            for (int i = 0; i < _urunler.Errors.Count; i++)
                            {
                                errors[i] = _urunler.Errors[i].Message;
                            }
                            status = false;
                            return Json(new { status, errors });
                        }
                        string carikod = _order.CustomerSurname +"_"+_order.CustomerName + "_" + _order.OrderId.ToString();
                        CARI_HESAPLAR carihesaplar = new CARI_HESAPLAR();
                        carihesaplar.cari_kod = carikod;
                        carihesaplar.cari_unvan1 = _order.CustomerSurname;
                        carihesaplar.cari_unvan2 = _order.CustomerName;
                        carihesaplar.cari_CepTel = RemoveCharacters.TelephoneClean(_order.Tel1);
                        carihesaplar.cari_grup_kodu = "Müştəri";
                        BusinessLayerResult<CARI_HESAPLAR> _carihesaplar = cariManager.InsertData(carihesaplar, CurrentSession.User);
                        if (_carihesaplar.Errors.Count > 0)
                        {
                            errors = new string[_carihesaplar.Errors.Count];
                            for (int i = 0; i < _carihesaplar.Errors.Count; i++)
                            {
                                errors[i] = _carihesaplar.Errors[i].Message;
                            }
                            status = false;
                            return Json(new { status, errors });
                        }
                        BusinessLayerResult<SIPARISLER> _sifarisler = sifarisManager.InsertData(stokKodu, _visit.FinalPrice, carikod, CurrentSession.User);
                        if (_sifarisler.Errors.Count > 0)
                        {
                            errors = new string[_sifarisler.Errors.Count];
                            for (int i = 0; i < _sifarisler.Errors.Count; i++)
                            {
                                errors[i] = _sifarisler.Errors[i].Message;
                            }
                            status = false;
                            return Json(new { status, errors });
                        }
                        List<Production> production = new List<Production>();
                        production = productionManager.List(x => x.VisitId == visitid && x.OrderId == orderid);
                        if (production.Count > 0)
                        {
                            BusinessLayerResult<URUN_RECETELERI> _urunreceteleri = urunReceteleriManager.InsertData(production, stokKodu, CurrentSession.User);
                            if (_urunreceteleri.Errors.Count > 0)
                            {
                                errors = new string[_urunreceteleri.Errors.Count];
                                for (int i = 0; i < _urunreceteleri.Errors.Count; i++)
                                {
                                    errors[i] = _urunreceteleri.Errors[i].Message;
                                }
                                status = false;
                                return Json(new { status, errors });
                            }
                        }
                        else
                        {
                            errors = new string[1];
                            errors[0] = "Məhsul resepti mövcud deyil.";
                            status = false;
                            return Json(new { status, errors });
                        }
                        BusinessLayerResult<Visits> updateVisit = visitManager.VisitUpdateStatus(_visit, CurrentSession.User);
                        if (updateVisit.Errors.Count > 0)
                        {
                            errors = new string[updateVisit.Errors.Count];
                            for (int i = 0; i < updateVisit.Errors.Count; i++)
                            {
                                errors[i] = updateVisit.Errors[i].Message;
                            }
                            status = false;
                            return Json(new { status, errors });
                        }
                        StockInfo _stockInfo = new StockInfo();
                        _stockInfo.StockName = stokKodu;
                        _stockInfo.Price = _visit.FinalPrice;
                        _stockInfo.VisitNo = _visit.VisitID;
                        _stockInfo.OrderNo = _order.OrderId;
                        _stockInfo.PaymentStatus = 0;
                        _stockInfo.SendStatus = 0;
                        BusinessLayerResult<StockInfo> insertStockInfo = stockInfoManager.InsertData(_stockInfo);
                        if (insertStockInfo.Errors.Count>0)
                        {
                            errors = new string[insertStockInfo.Errors.Count];
                            for (int i = 0; i < insertStockInfo.Errors.Count; i++)
                            {
                                errors[i] = insertStockInfo.Errors[i].Message;
                            }
                            status = false;
                            return Json(new { status, errors });
                        }
                        status = true;
                        string link = "?Sira=" + orderid.ToString();
                        return Json(new { status, link, Url = Url.Action("VisitInfo", "Order") });
                    }
                    else
                    {
                        errors = new string[1];
                        errors[0] = "Məhsul resepti mövcud deyil.";
                        status = false;
                        return Json(new { status, errors });
                    }
                }
                else
                {
                    errors = new string[1];
                    errors[0] = "Vizit məlumatı mövcud deyil.";
                    status = false;
                    return Json(new { status, errors });
                }
            }
            errors = new string[1];
            errors[0] = "Sifariş id və ya vizit id düzgün deyil.";
            status = false;
            return Json(new { status, errors });

        }
        [CustomAuthorize(Roles = "Admin,Kordinator")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult FinishOrder(int? orderid)
        {
            bool status = false;
            string[] errors;
            if (orderid != null)
            {
                BusinessLayerResult<Orders> orderresult = orderManager.FinishOrder(Convert.ToInt32(orderid), CurrentSession.User);
                if (orderresult.Errors.Count > 0)
                {
                    errors = new string[orderresult.Errors.Count];
                    for (int i = 0; i < orderresult.Errors.Count; i++)
                    {
                        errors[i] = orderresult.Errors[i].Message;
                    }
                    status = false;
                    return Json(new { status, errors });
                }
                status = true;
                return Json(new { status, Url = Url.Action("ActiveOrders", "Order") });

            }
            errors = new string[1];
            errors[0] = "Sifariş id düzgün deyil.";
            status = false;
            return Json(new { status, errors });

        }
        [CustomAuthorize(Roles = "Admin,Kordinator")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult ActivateOrder(int? orderid,int? menuStatus)
        {
            bool status = false;
            string[] errors;
            if (orderid != null)
            {
                BusinessLayerResult<Orders> orderresult = orderManager.ActivateOrder(Convert.ToInt32(orderid), CurrentSession.User, Convert.ToInt32(menuStatus));
                if (orderresult.Errors.Count > 0)
                {
                    errors = new string[orderresult.Errors.Count];
                    for (int i = 0; i < orderresult.Errors.Count; i++)
                    {
                        errors[i] = orderresult.Errors[i].Message;
                    }
                    status = false;
                    return Json(new { status, errors });
                }
                status = true;
                return Json(new { status, Url = Url.Action("ActiveOrders", "Order") });

            }
            errors = new string[1];
            errors[0] = "Sifariş id düzgün deyil.";
            status = false;
            return Json(new { status, errors });

        }
        [CustomAuthorize(Roles = "Admin,Kordinator,Planlamaci")]
        public ActionResult PlannerOrders(string status)
        {
            ViewBag.Status = status;
            return View();
        }
        [CustomAuthorize(Roles = "Admin,Kordinator,Planlamaci")]
        [WebMethod]
        public ActionResult GetPlannerActiveOrders()
        {
            string userName = CurrentSession.User.UserName;
            List<Orders> fakturalar = new List<Orders>();
            fakturalar = orderManager.ListQueryable().Where(x => x.IsActive == true & x.PlannerCode == userName & x.VisitorStatus == 4 & x.PlannerStatus < 4).ToList();
            var UserData = new object[fakturalar.Count];
            int j = 0;
            foreach (var item in fakturalar)
            {
                string orderStatus = Statuses.PlannerStatus(item.PlannerStatus);
                string link = "?Sira=" + item.OrderId.ToString();
                string customer = item.CustomerSurname + " " + item.CustomerName + " " + item.CustomerFatherName;
                UserData[j] = new object[] { item.OrderId, item.CreateOn.ToString("MM/dd/yyyy"), customer, item.Tel1, item.Location, orderStatus, link, item.PlannerStatus };
                j++;
            }
            return Json(UserData, JsonRequestBehavior.AllowGet);

        }
        [CustomAuthorize(Roles = "Admin,Kordinator,Planlamaci")]
        [WebMethod]
        public ActionResult GetPlannerActiveOrdersWithParametr(OrderSearch data)
        {
            string userName = CurrentSession.User.UserName;
            IEnumerable<Orders> fakturalar = orderManager.GetPlannerOrdersWithParametr(data, userName);
            var UserData = new object[fakturalar.Count()];
            int j = 0;
            foreach (var item in fakturalar)
            {
                string orderStatus = Statuses.PlannerStatus(item.PlannerStatus);
                string link = "?Sira=" + item.OrderId.ToString();
                string customer = item.CustomerSurname + " " + item.CustomerName + " " + item.CustomerFatherName;
                UserData[j] = new object[] { item.OrderId, item.CreateOn.ToString("MM/dd/yyyy"), customer, item.Tel1, item.Location, orderStatus, link, item.PlannerStatus };
                j++;
            }
            return Json(UserData, JsonRequestBehavior.AllowGet);

        }

        [CustomAuthorize(Roles = "Admin,Kordinator,Planlamaci")]
        public ActionResult AcceptPlannerOrder(int? Sira)
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
                if (order.DesignerStatus >= 2)
                {
                    return RedirectToAction("PlannerOrders");
                }
                BusinessLayerResult<Orders> data = orderManager.AcceptOrder(order, 32);
                if (data.Errors.Count > 0)
                {
                    data.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    TempData["msg"] = "0";
                    return RedirectToAction("PlannerOrders");
                }
            }
            TempData["msg"] = "Siafriş Qəbul Edildi!";
            TempData["typ"] = "success";
            return RedirectToAction("PlannerOrders");
        }

        [CustomAuthorize(Roles = "Admin,Kordinator,Planlamaci")]

        public ActionResult PlannerEdit(int? Sira)
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
            else if (order.PlannerStatus < 2)
            {
                TempData["msg"] = "Sifariş Qəbul Edilməyib!";
                TempData["typ"] = "error";
                return RedirectToAction("PlannerOrders");
            }
            else
            {
                List<Visits> visits = visitManager.List(x => x.OrderId == Sira).ToList();
                OrderFileUpload uploadedFiles = new OrderFileUpload();
                CustomerVisitData data = new CustomerVisitData();
                var visitGuid = visits.Where(x => x.OrderId == Sira).Select(m => m.VisitGuid).First();
                data.order = order;
                data.orderFiles = uploadedFiles;
                data.visitData = visits;
                data.visitImages = imagesManager.List(x => x.VisitGuid == visitGuid);
                data.orderStatus = Statuses.OrderStatus(order.OrderStatus);
                data.visitorStatus = Statuses.VisitorOrderStatus(order.VisitorStatus);
                data.designerStaus = Statuses.DesignerStatus(order.DesignerStatus);
                data.plannerStatus = Statuses.PlannerStatus(order.PlannerStatus);
                return View(data);
            }
        }
        //TODO: Planlamaci ucun deyisiklik edilecek
        [CustomAuthorize(Roles = "Admin,Kordinator,Planlamaci")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlannerEdit(CustomerVisitData data, string designCommand)
        {
            int plannerstatus = 0;
            if (designCommand.Equals("Save"))
            {
                plannerstatus = 33;
            }
            else if (designCommand.Equals("Finish"))
            {
                plannerstatus = 34;
            }
            data.order = orderManager.Find(x => x.OrderId == data.order.OrderId);
            var visitGuid = data.visitData.Where(x => x.OrderId == data.order.OrderId).Select(m => m.VisitGuid).First();
            data.visitImages = imagesManager.List(x => x.VisitGuid == visitGuid);
            List<VisitImages> visitImages = new List<VisitImages>();

            BusinessLayerResult<Visits> visitData = visitManager.DesignerUpdate(data.visitData, data.order.OrderId, CurrentSession.User);
            if (visitData.Errors.Count > 0)
            {
                visitData.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                data.orderStatus = Statuses.OrderStatus(data.order.OrderStatus);
                data.visitorStatus = Statuses.VisitorOrderStatus(data.order.VisitorStatus);
                data.designerStaus = Statuses.DesignerStatus(data.order.DesignerStatus);
                data.plannerStatus = Statuses.PlannerStatus(data.order.PlannerStatus);
                return View(data);
            }
            BusinessLayerResult<Orders> order = orderManager.AcceptOrder(data.order, plannerstatus);
            if (order.Errors.Count>0)
            {
                visitData.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                TempData["msg"] = "Qeyd Yenilənmədi!";
                TempData["typ"] = "error";
                return View(data);
            }
            else
            {
                TempData["msg"] = "Qeyd Yeniləndi!";
                TempData["typ"] = "success";
                return RedirectToAction("PlannerOrders");
            }
        }
    }
}