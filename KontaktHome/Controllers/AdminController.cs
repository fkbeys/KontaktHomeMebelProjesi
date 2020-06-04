using BusinessLayer;
using BusinessLayer.Managers;
using BusinessLayer.QueryResult;
using Entities;
using Entities.Model;
using KontaktHome.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace KontaktHome.Controllers
{
    [Exc]
    [Auth]
    //[AuthAdmin]
    [CustomAuthorize(Roles = "Admin")]
    [OutputCache(NoStore = true, Duration = 0)]
    public class AdminController : Controller
    {
        private UserManager userManager = new UserManager();
        private UserRolesManager userRolesManager = new UserRolesManager();
        private UserRolesMappingManager userRoleMappingManager = new UserRolesMappingManager();
        private StoresManager storesManager = new StoresManager();
        public ActionResult Index()
        {
            return View();
        }
        [WebMethod]
        public ActionResult GetUsers()
        {
            string userStatus = "";
            List<Users> user = userManager.List();
            var UserData = new object[user.Count];
            int j = 0;
            foreach (var item in user)
            {
                if (item.IsActive == true)
                {
                    userStatus = "Aktiv";
                }
                else { userStatus = "Bağlı"; }
                UserData[j] = new object[] { j + 1, item.UserID, item.UserName, item.UserDisplayName, item.StoreCode, item.StoreName, userStatus };
                j++;
            }
            return Json(UserData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateUser()
        {
            Users user = new Users();
            user.myADUsers = GetADUsers();
            List<Stores> magazalar = storesManager.List();
            var magaza = magazalar.Select(x => new SelectListItem { Value = x.StoreCode, Text = x.StoreName }).ToList();
            ViewBag.Stores = magaza;
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(Users data)
        {
            data.myADUsers = GetADUsers();
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Users> newUser = userManager.InsertUser(data);
                if (newUser.Errors.Count > 0)
                {
                    newUser.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    //TempData["msg"] = "0";
                    return View(data);
                }
                TempData["msg"] = "Istifadəçi qeyd edildi!";
                TempData["typ"] = "success";
                return RedirectToAction("Index");
            }
            //TempData["msg"] = "0";
            return View(data);
        }
        public ActionResult EditUser(int? userid)
        {
            Users user = userManager.Find(x => x.UserID == userid);
            if (user == null)
            {
                TempData["msg"] = "Istifadəçi tapılmadı!";
                TempData["typ"] = "error";
                return RedirectToAction("Index");
            }
            List<Stores> magazalar = storesManager.List();
            var magaza = magazalar.Select(x => new SelectListItem { Value = x.StoreCode, Text = x.StoreName }).ToList();
            ViewBag.Stores = magaza;
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(Users data)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Users> user = userManager.UpdateUser(data);
                if (user.Errors.Count > 0)
                {
                    user.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(data);
                }
                TempData["msg"] = "Istifadəçi yeniləndi!";
                TempData["typ"] = "success";
                return RedirectToAction("Index");
            }
            return View(data);
        }
        public ActionResult DeleteUser(int? userid)
        {
            Users user = userManager.Find(x => x.UserID == userid);
            if (user == null)
            {
                TempData["msg"] = "Istifadəçi tapılmadı!";
                TempData["typ"] = "error";
                return RedirectToAction("Index");
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(Users data)
        {
            BusinessLayerResult<Users> istifadeci = userManager.DeleteUser(data.UserID);
            if (istifadeci.Errors.Count > 0)
            {
                istifadeci.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                return View(data);
            }
            TempData["msg"] = "Istifadəçi silindi!";
            TempData["typ"] = "success";
            return RedirectToAction("Index");
        }
        public IEnumerable<SelectListItem> GetADUsers()
        {
            List<SelectListItem> _users = new List<SelectListItem>();
            PrincipalContext domainconnect = new PrincipalContext(ContextType.Domain, ConfigurationManager.AppSettings["DomainName"], ConfigurationManager.AppSettings["DomainDc"], ConfigurationManager.AppSettings["UserName"], ConfigurationManager.AppSettings["Password"]);
            GroupPrincipal adGroup = GroupPrincipal.FindByIdentity(domainconnect, IdentityType.Name, ConfigurationManager.AppSettings["GroupName"]);
            if (adGroup != null)
            {
                foreach (Principal p in adGroup.GetMembers(true))
                {
                    _users.Add(new SelectListItem { Text = p.Name, Value = p.SamAccountName });
                }
            }
            IEnumerable<SelectListItem> myADUsers = _users;
            return myADUsers;
        }
        public ActionResult UserRoles(int UserID)
        {
            UserRolesMapping roleMapping = new UserRolesMapping();
            roleMapping.UserID = UserID;
            List<UserRoles> userRoles = userRolesManager.List();
            var listroles = userRoles.Select(s => new SelectListItem { Value = s.ID.ToString(), Text = s.RoleName }).ToList<SelectListItem>();
            ViewBag.Roles = listroles;
            return View(roleMapping);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserRoles(UserRolesMapping model)
        {
            List<UserRoles> userRoles = userRolesManager.List();
            var listroles = userRoles.Select(s => new SelectListItem { Value = s.ID.ToString(), Text = s.RoleName }).ToList<SelectListItem>();
            ViewBag.Roles = listroles;
            BusinessLayerResult<UserRolesMapping> rolesMapping = userRoleMappingManager.InsertUserRoles(model);
            if (rolesMapping.Errors.Count > 0)
            {
                rolesMapping.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                return View(model);
            }
            else
            {
                TempData["msg"] = "İstifadəçi səlahiyyəti əlavə edildi!";
                TempData["typ"] = "success";
            }
            return View(model);
        }
        [WebMethod]
        public ActionResult GetUserRoles(int userid)
        {
            var userroles = (from user in userManager.ListQueryable()
                             join roleMapping in userRoleMappingManager.ListQueryable()
                             on user.UserID equals roleMapping.UserID
                             join role in userRolesManager.ListQueryable()
                             on roleMapping.RoleID equals role.ID
                             where user.UserID == userid
                             select new
                             {
                                 UserName = user.UserName,
                                 UserFullName = user.UserDisplayName,
                                 RoleId = roleMapping.ID,
                                 RoleName = role.RoleName
                             });
            var UserData = new object[userroles.Count()];
            int j = 0;
            foreach (var item in userroles)
            {
                UserData[j] = new object[] { j + 1, item.UserName, item.UserFullName, item.RoleId, item.RoleName };
                j++;
            }
            return Json(UserData, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUserRole(string roleMappingID)
        {
            if (roleMappingID != null)
            {
                int id = Convert.ToInt32(roleMappingID);
                BusinessLayerResult<UserRolesMapping> roles = userRoleMappingManager.DeleteUserRole(id);
                if (roles.Errors.Count > 0)
                {
                    TempData["msg"] = "Istifadəçi səlahiyyəti silinmədi";
                    TempData["typ"] = "error";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["msg"] = "Istifadəçi səlahiyyəti silindi!";
                    TempData["typ"] = "success";
                    return RedirectToAction("UserRoles", new { roles.Result.UserID });
                }


            }
            return RedirectToAction("Index");
        }
        public ActionResult CreateStore()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStore(Stores data)
        {
            if (ModelState.IsValid)
            {
                data.IsActive = true;
                BusinessLayerResult<Stores> newStore = storesManager.InsertStore(data);
                if (newStore.Errors.Count > 0)
                {
                    newStore.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(data);
                }
                TempData["msg"] = "Mağaza qeyd edildi!";
                TempData["typ"] = "success";
                return RedirectToAction("CreateStore");
            }
            return View(data);
        }
        [WebMethod]
        public ActionResult GetStores()
        {
            List<Stores> magazalar = storesManager.List();
            var UserData = new object[magazalar.Count()];
            int j = 0;
            foreach (var item in magazalar)
            {
                UserData[j] = new object[] { item.StoreID, item.StoreCode, item.StoreName,item.IsActive };
                j++;
            }
            return Json(UserData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeaktivateStore(int? storeid)
        {
            BusinessLayerResult<Stores> store = storesManager.DeactivateActivateStore(storeid,false);
            if (store.Errors.Count > 0)
            {
                string storeerror = "";
                store.Errors.ForEach(x => storeerror=x.Message);
                TempData["msg"] = storeerror;
                TempData["typ"] = "error";
                return RedirectToAction("CreateStore");
            }
            TempData["msg"] = "Seçilən Mağaza deaktiv olundu!";
            TempData["typ"] = "success";
            return RedirectToAction("CreateStore");

        }
        public ActionResult ActivateStore(int? storeid)
        {
            BusinessLayerResult<Stores> store = storesManager.DeactivateActivateStore(storeid,true);
            if (store.Errors.Count > 0)
            {
                string storeerror = "";
                store.Errors.ForEach(x => storeerror = x.Message);
                TempData["msg"] = storeerror;
                TempData["typ"] = "error";
                return RedirectToAction("CreateStore");
            }
            TempData["msg"] = "Seçilən Mağaza aktiv olundu!";
            TempData["typ"] = "success";
            return RedirectToAction("CreateStore");

        }
    }



}