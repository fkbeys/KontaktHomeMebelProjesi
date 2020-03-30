using BusinessLayer;
using BusinessLayer.QueryResult;
using Entities;
using KontaktHome.Filters;
using KontaktHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KontaktHome.Controllers
{
    [Exc]
    public class HomeController : Controller
    {
        private UserManager userManager = new UserManager();
        // GET: Home
        [Auth]
        public ActionResult Index()
        {            
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Users> res = userManager.LoginUser(model);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

                    return View(model);
                }

                CurrentSession.Set<Users>("login", res.Result); // Session'a kullanıcı bilgi saklama..
                return RedirectToAction("Index");   // yönlendirme..
            }
            return View(model);
        }
        public ActionResult HasError()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}