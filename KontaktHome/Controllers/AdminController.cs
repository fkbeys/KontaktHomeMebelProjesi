using BusinessLayer;
using BusinessLayer.QueryResult;
using Entities;
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
    [AuthAdmin]
    [OutputCache(NoStore = true, Duration = 0)]
    public class AdminController : Controller
    {
        private UserManager userManager = new UserManager();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [WebMethod]
        public ActionResult GetUsers()
        {
            string userStatus = "";
            List <Users> user = userManager.List();
            var UserData = new object[user.Count];
            int j = 0;
            foreach (var item in user)
            {
                if (item.IsActive == true)
                {
                    userStatus = "Aktiv";
                }
                else { userStatus = "Bağlı"; }
                UserData[j] = new object[] { j + 1, item.UserID, item.UserName, item.UserDisplayName, item.StoreCode, item.StoreName, userStatus};
                j++;
            }
            return Json(UserData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateUser()
        {
            Users user = new Users();
            user.myADUsers = GetADUsers();
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(Users data)
        {
            data.myADUsers= GetADUsers();
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
            if (user==null)
            {
                TempData["msg"] = "Istifadəçi tapılmadı!";
                TempData["typ"] = "error";
                return RedirectToAction("Index");
            }           
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(Users data)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Users> user = userManager.UpdateUser(data);
                if (user.Errors.Count>0)
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
            if (istifadeci.Errors.Count>0)
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
    }

 

}