using BusinessLayer;
using BusinessLayer.QueryResult;
using Entities;
using KontaktHome.Filters;
using KontaktHome.Models;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Web.Mvc;

namespace KontaktHome.Controllers
{
    [Exc]
    [OutputCache(NoStore = true, Duration = 0)]
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
                if (ConfigurationManager.AppSettings["LoginMode"] == "Local")
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
                            CurrentSession.Set<Users>("login", res.Result); // Session'a kullanıcı bilgi saklama..
                            return RedirectToAction("Index");   // yönlendirme..
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
            return RedirectToAction("Login");
        }
    }
}