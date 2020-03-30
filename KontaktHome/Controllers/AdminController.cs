using BusinessLayer;
using BusinessLayer.QueryResult;
using Entities;
using KontaktHome.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace KontaktHome.Controllers
{
    [Exc]
    [Auth]
    [AuthAdmin]
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
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(Users data)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Users> newUser = userManager.InsertUser(data);
                if (newUser.Errors.Count > 0)
                {
                    newUser.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    //TempData["msg"] = "0";
                    return View(data);
                }
                TempData["msg"] = "1";
                return RedirectToAction("Index");
            }
            //TempData["msg"] = "0";
            return View(data);          
        }
        public ActionResult EditUser(int userid)
        {
            Users user = userManager.Find(x => x.UserID == userid);
            if (user==null)
            {
                TempData["msg"] = "2";
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
                TempData["msg"] = "3";
                return RedirectToAction("Index");
            }
            return View(data);
        }
    }

 

}