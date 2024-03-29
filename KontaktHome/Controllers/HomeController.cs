﻿using BusinessLayer;
using BusinessLayer.QueryResult;
using Entities;
using Entities.Model;
using KontaktHome.Filters;
using KontaktHome.Models;
using System;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace KontaktHome.Controllers
{
    [Exc]
    [OutputCache(NoStore = true, Duration = 0)]
    public class HomeController : Controller
    {
        private UserManager userManager = new UserManager();
        private OrderManager orderManager = new OrderManager();
        private VisitManager visitManager = new VisitManager();

        // GET: Home
        [Auth]
        public ActionResult Index()
        {
            HttpCookie searchCookie = Request.Cookies["axtarisCookie"];
            if (searchCookie != null && searchCookie.Values != null)
            {
                searchCookie.Value = "";
                searchCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(searchCookie);
            }           
           
            int[] orderstatuses = new int[] { 2, 3, 4, 5, 6 };
            Widgets widgets = new Widgets();
            widgets.WaitingOrders = orderManager.ListQueryable().Where(x => x.IsActive == true && x.OrderStatus == 1).Count();
            widgets.ProcessingOrders = orderManager.ListQueryable().Where(x => x.IsActive == true && orderstatuses.Contains(x.OrderStatus)).Count();
            widgets.SaleWaitingOrders = orderManager.ListQueryable().Where(x => x.IsActive == true && x.OrderStatus >6 && x.OrderStatus<11).Count();
            widgets.ProductionOrders = visitManager.ListQueryable().Where(x => x.VisitStatus == 1 && x.IsDeclined == false).Count();
            return View(widgets);
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
                if (ConfigurationManager.AppSettings["LoginMode"] == "Local")
                {
                    BusinessLayerResult<Users> res = userManager.LoginUser(model);

                    if (res.Errors.Count > 0)
                    {
                        res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

                        return View(model);
                    }

                    CurrentSession.Set<Users>("login", res.Result);
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    return RedirectToAction("Index");
                }
                if (ConfigurationManager.AppSettings["LoginMode"] == "Domain")
                {
                    using (PrincipalContext domainconnect = new PrincipalContext(ContextType.Domain, ConfigurationManager.AppSettings["DomainName"], ConfigurationManager.AppSettings["DomainDc"], ConfigurationManager.AppSettings["UserName"], ConfigurationManager.AppSettings["Password"]))
                    {
                        if (domainconnect.ValidateCredentials(model.Username, model.Password) == true)
                        {
                            BusinessLayerResult<Users> res = userManager.CheckUser(model.Username);
                            if (res.Errors.Count > 0)
                            {
                                res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                                return View(model);
                            }
                            CurrentSession.Set<Users>("login", res.Result);
                            FormsAuthentication.SetAuthCookie(model.Username, false);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Istifadəçi adı və ya şifrə yanlışdır");
                            return View(model);
                        }

                    }
                }
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
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}