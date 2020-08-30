using BusinessLayer;
using BusinessLayer.Managers;
using BusinessLayer.Managers.LocalManagers;
using BusinessLayer.Managers.MikroManagers;
using BusinessLayer.QueryResult;
using Entities;
using Entities.Helper;
using Entities.Model;
using Entities.Model.LocalModels;
using KontaktHome.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Dynamic;
using System.Linq;
using System.Runtime.Remoting;
using System.Threading.Tasks;
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
        private AdditionalChargesManager chargesManager = new AdditionalChargesManager();
        private LocationGroupManager locationGroupManager = new LocationGroupManager();
        private LocationSubGroupManager locationSubGroupManager = new LocationSubGroupManager();
        private LocationNameManager locationNameManager = new LocationNameManager();
        private SormMerkeziManager sormMerkeziManager = new SormMerkeziManager();
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
                var userroles = (from users in userManager.ListQueryable()
                                 join roleMapping in userRoleMappingManager.ListQueryable()
                                 on users.UserID equals roleMapping.UserID
                                 join role in userRolesManager.ListQueryable()
                                 on roleMapping.RoleID equals role.ID
                                 where users.UserID == item.UserID
                                 select new
                                 {                                     
                                     RoleName = role.RoleName
                                 });
                if (item.IsActive == true)
                {
                    userStatus = "Aktiv";
                }
                else { userStatus = "Bağlı"; }
                string userRoleItem = "";
                foreach (var userrole in userroles)
                {
                    userRoleItem = userRoleItem + " " + userrole.RoleName;
                }
                UserData[j] = new object[] { item.UserID, item.UserName, item.UserDisplayName, item.StoreCode, item.StoreName, userStatus, userRoleItem };
                j++;
            }
            return Json(UserData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateUser()
        {
            Users user = new Users();
            user.myADUsers = GetADUsers();
            List<SORUMLULUK_MERKEZLERI> magazalar = sormMerkeziManager.GetData();
            var magaza = magazalar.Select(x => new SelectListItem { Value = x.som_kod, Text = x.som_isim }).ToList();
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
            List<SORUMLULUK_MERKEZLERI> magazalar = sormMerkeziManager.GetData();
            var magaza = magazalar.Select(x => new SelectListItem { Value = x.som_kod, Text = x.som_isim }).ToList();
            ViewBag.Stores = magaza;
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
            List<SORUMLULUK_MERKEZLERI> magazalar = sormMerkeziManager.GetData();
            var magaza = magazalar.Select(x => new SelectListItem { Value = x.som_isim, Text = x.som_kod }).ToList();          
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
            List<SORUMLULUK_MERKEZLERI> magazalar = sormMerkeziManager.GetData();
            var magaza = magazalar.Select(x => new SelectListItem { Value = x.som_isim, Text = x.som_kod }).ToList();
            ViewBag.Stores = magaza;
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
            Users userinformation = userManager.Find(x => x.UserID == UserID);
            UserRolesMapping roleMapping = new UserRolesMapping();
            roleMapping.UserID = UserID;
            List<UserRoles> userRoles = userRolesManager.List();
            var listroles = userRoles.Select(s => new SelectListItem { Value = s.ID.ToString(), Text = s.RoleName }).ToList<SelectListItem>();
            ViewBag.Roles = listroles;
            ViewBag.UserName = userinformation.UserName;
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
            Users userinformation = userManager.Find(x => x.UserID == model.UserID);
            ViewBag.UserName = userinformation.UserName;
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
                string status = "";
                if (item.IsActive == true)
                {
                    status = "Aktiv";
                }
                else
                {
                    status = "Dekativ";
                }
                UserData[j] = new object[] { item.StoreID, item.StoreCode, item.StoreName, status };
                j++;
            }
            return Json(UserData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeaktivateStore(int? storeid)
        {
            BusinessLayerResult<Stores> store = storesManager.DeactivateActivateStore(storeid, false);
            if (store.Errors.Count > 0)
            {
                string storeerror = "";
                store.Errors.ForEach(x => storeerror = x.Message);
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
            BusinessLayerResult<Stores> store = storesManager.DeactivateActivateStore(storeid, true);
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

        public ActionResult Charges()
        {
            List<AdditionalCharges> _charges = chargesManager.List();
            ViewBag.Charges = _charges;
            return View();
        }

        [HttpPost]
        public ActionResult SaveCharges(AdditionalCharges charges)
        {
            bool status = false;
            string[] errors;
            if (ModelState.IsValid)
            {
                BusinessLayerResult<AdditionalCharges> _charges = chargesManager.InsertData(charges);
                if (_charges.Errors.Count > 0)
                {
                    errors = new string[_charges.Errors.Count];
                    for (int i = 0; i < _charges.Errors.Count; i++)
                    {
                        errors[i] = _charges.Errors[i].Message;
                    }
                    status = false;
                    return Json(new { status, errors });
                }
                status = true;
                return Json(new { status, Url = Url.Action("Charges", "Admin") });
            }
            errors = new string[1];
            errors[0] = "Daxil edilən məlumatlar düzgün deyil.";
            status = false;
            return Json(new { status, errors });
        }
        public ActionResult EditCharges(int? id)
        {
            if (id != null)
            {
                AdditionalCharges _charges = chargesManager.Find(x => x.ID == id);
                return View(_charges);
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCharges(AdditionalCharges data)
        {
            bool status = false;
            string[] errors;
            if (ModelState.IsValid)
            {
                BusinessLayerResult<AdditionalCharges> _charges = chargesManager.UpdateData(data);
                if (_charges.Errors.Count > 0)
                {
                    errors = new string[_charges.Errors.Count];
                    for (int i = 0; i < _charges.Errors.Count; i++)
                    {
                        errors[i] = _charges.Errors[i].Message;
                    }
                    status = false;
                    return Json(new { status, errors, Url = Url.Action("Charges", "Admin") });
                }
                status = true;
                return Json(new { status, Url = Url.Action("Charges", "Admin") });
            }
            errors = new string[1];
            errors[0] = "Daxil edilən məlumatlar düzgün deyil.";
            status = false;
            return Json(new { status, errors, Url = Url.Action("Charges", "Admin") });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCharge(int? id)
        {
            bool status = false;
            string[] errors;
            if (id != null)
            {
                BusinessLayerResult<AdditionalCharges> _charges = chargesManager.DeleteData(id);
                if (_charges.Errors.Count > 0)
                {
                    errors = new string[_charges.Errors.Count];
                    for (int i = 0; i < _charges.Errors.Count; i++)
                    {
                        errors[i] = _charges.Errors[i].Message;
                    }
                    status = false;
                    return Json(new { status, errors, Url = Url.Action("Charges", "Admin") });
                }
                status = true;
                return Json(new { status, Url = Url.Action("Charges", "Admin") });
            }
            errors = new string[1];
            errors[0] = "Qeyd düzgün deyil.";
            status = false;
            return Json(new { status, errors, Url = Url.Action("Charges", "Admin") });
        }

        public ActionResult Locations()
        {
            return View();
        }
        [WebMethod]
        public async Task<ActionResult> GetLocationGroups()
        {
            List<LocationGroup> _locationGroups = new List<LocationGroup>();
            _locationGroups = await locationGroupManager.GetGroupsAsync();

            var GroupData = new object[_locationGroups.Count()];
            int j = 0;
            foreach (var item in _locationGroups)
            {
                GroupData[j] = new object[] { item.ID, item.Value };
                j++;
            }
            return Json(GroupData, JsonRequestBehavior.AllowGet);
        }
        [WebMethod]
        public async Task<ActionResult> GetLocationSubGroups()
        {
            List<LocationSubGroupList> _locationSubGroupList = new List<LocationSubGroupList>();
            _locationSubGroupList = await locationSubGroupManager.GetSubGroups();

            var GroupData = new object[_locationSubGroupList.Count()];
            int j = 0;
            foreach (var item in _locationSubGroupList)
            {
                GroupData[j] = new object[] { item.id, item.groupName, item.subGroupName };
                j++;
            }
            return Json(GroupData, JsonRequestBehavior.AllowGet);
        }
        [WebMethod]
        public async Task<ActionResult> GetLocationNames()
        {
            List<LocationNameList> _locationNameList = new List<LocationNameList>();
            _locationNameList = await locationNameManager.GetLocationNames();

            var GroupData = new object[_locationNameList.Count()];
            int j = 0;
            foreach (var item in _locationNameList)
            {
                GroupData[j] = new object[] { item.id, item.groupname, item.subgroupname, item.locationname };
                j++;
            }
            return Json(GroupData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateEditLocationGroup(int? id)
        {
            if (id == 0)
            {
                return View();
            }
            else
            {
                BusinessLayerResult<LocationGroup> _locationGroup = locationGroupManager.FindGroup(id);
                if (_locationGroup.Errors.Count > 0)
                {
                    return View();
                }
                return View(_locationGroup.Result);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEditLocationGroup(LocationGroup data)
        {
            if (data.ID == 0)
            {
                bool status = false;
                string[] errors;
                if (data.Value != null)
                {
                    //data.Value = TextHelpers.CapitalizeFirstLetter(data.Value);
                    BusinessLayerResult<LocationGroup> _locationGroup = locationGroupManager.InsertData(data);
                    if (_locationGroup.Errors.Count > 0)
                    {
                        errors = new string[_locationGroup.Errors.Count];
                        for (int i = 0; i < _locationGroup.Errors.Count; i++)
                        {
                            errors[i] = _locationGroup.Errors[i].Message;
                        }
                        status = false;
                        return Json(new { status, errors, Url = Url.Action("Locations", "Admin") });
                    }
                    status = true;
                    return Json(new { status, Url = Url.Action("Locations", "Admin") });
                }
                errors = new string[1];
                errors[0] = "Daxil edilən məlumatlar düzgün deyil.";
                status = false;
                return Json(new { status, errors, Url = Url.Action("Locations", "Admin") });
            }
            else
            {
                bool status = false;
                string[] errors;
                if (data.Value != null)
                {
                    data.Value = TextHelpers.CapitalizeFirstLetter(data.Value);
                    BusinessLayerResult<LocationGroup> _locationGroup = locationGroupManager.UpdateData(data);
                    if (_locationGroup.Errors.Count > 0)
                    {
                        errors = new string[_locationGroup.Errors.Count];
                        for (int i = 0; i < _locationGroup.Errors.Count; i++)
                        {
                            errors[i] = _locationGroup.Errors[i].Message;
                        }
                        status = false;
                        return Json(new { status, errors, Url = Url.Action("Locations", "Admin") });
                    }
                    status = true;
                    return Json(new { status, Url = Url.Action("Locations", "Admin") });
                }
                errors = new string[1];
                errors[0] = "Daxil edilən məlumatlar düzgün deyil.";
                status = false;
                return Json(new { status, errors, Url = Url.Action("Locations", "Admin") });
            }
        }
        public ActionResult CreateEditLocationSubGroup(int? id)
        {

            List<LocationGroup> _locationGroup = locationGroupManager.GetGroups();
            var grouplist = _locationGroup.Select(x => new SelectListItem() { Value = x.ID.ToString(), Text = x.Value }).ToList();
            ViewBag.Groups = grouplist;
            if (id == 0)
            {
                return View();
            }
            else
            {
                BusinessLayerResult<LocationSubGroup> _locationSubGroup = locationSubGroupManager.FindGroup(id);
                if (_locationSubGroup.Errors.Count > 0)
                {
                    return View();
                }
                return View(_locationSubGroup.Result);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEditLocationSubGroup(LocationSubGroup data)
        {
            if (data.ID == 0)
            {
                bool status = false;
                string[] errors;
                if (data.Value != null && data.GroupID != 0)
                {
                    //data.Value = TextHelpers.CapitalizeFirstLetter(data.Value);
                    BusinessLayerResult<LocationSubGroup> _locationsubGroup = locationSubGroupManager.InsertData(data);
                    if (_locationsubGroup.Errors.Count > 0)
                    {
                        errors = new string[_locationsubGroup.Errors.Count];
                        for (int i = 0; i < _locationsubGroup.Errors.Count; i++)
                        {
                            errors[i] = _locationsubGroup.Errors[i].Message;
                        }
                        status = false;
                        return Json(new { status, errors, Url = Url.Action("Locations", "Admin") });
                    }
                    status = true;
                    return Json(new { status, Url = Url.Action("Locations", "Admin") });
                }
                errors = new string[1];
                errors[0] = "Daxil edilən məlumatlar düzgün deyil.";
                status = false;
                return Json(new { status, errors, Url = Url.Action("Locations", "Admin") });
            }
            else
            {
                bool status = false;
                string[] errors;
                if (data.Value != null && data.GroupID != 0)
                {
                    data.Value = TextHelpers.CapitalizeFirstLetter(data.Value);
                    BusinessLayerResult<LocationSubGroup> _locationsubGroup = locationSubGroupManager.UpdateDate(data);
                    if (_locationsubGroup.Errors.Count > 0)
                    {
                        errors = new string[_locationsubGroup.Errors.Count];
                        for (int i = 0; i < _locationsubGroup.Errors.Count; i++)
                        {
                            errors[i] = _locationsubGroup.Errors[i].Message;
                        }
                        status = false;
                        return Json(new { status, errors, Url = Url.Action("Locations", "Admin") });
                    }
                    status = true;
                    return Json(new { status, Url = Url.Action("Locations", "Admin") });
                }
                errors = new string[1];
                errors[0] = "Daxil edilən məlumatlar düzgün deyil.";
                status = false;
                return Json(new { status, errors, Url = Url.Action("Locations", "Admin") });
            }
        }
        public ActionResult CreateEditLocationName(int? id)
        {

            List<LocationGroup> _locationGroup = locationGroupManager.GetGroups();
            var grouplist = _locationGroup.Select(x => new SelectListItem() { Value = x.ID.ToString(), Text = x.Value }).ToList();
            ViewBag.Groups = grouplist;
            if (id == 0)
            {

                ViewBag.SubGroups = new SelectList(Enumerable.Empty<SelectListItem>());
                return View();
            }
            else
            {
                BusinessLayerResult<LocationNames> _locationNames = locationNameManager.FindName(id);
                if (_locationNames.Errors.Count > 0)
                {
                    return View();
                }
                List<LocationSubGroup> _locationSubGroupList = locationSubGroupManager.GetSubGroup(_locationNames.Result.GroupID);
                var subgrouplist = _locationSubGroupList.Select(x => new SelectListItem() { Value = x.ID.ToString(), Text = x.Value }).ToList();
                ViewBag.SubGroups = subgrouplist;
                return View(_locationNames.Result);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEditLocationName(LocationNames data)
        {
            if (data.ID == 0)
            {
                bool status = false;
                string[] errors;
                if (data.Value != null && data.GroupID != 0 && data.SubGroupID != 0)
                {
                    //data.Value = TextHelpers.CapitalizeFirstLetter(data.Value);
                    BusinessLayerResult<LocationNames> _locationNames = locationNameManager.InsertData(data);
                    if (_locationNames.Errors.Count > 0)
                    {
                        errors = new string[_locationNames.Errors.Count];
                        for (int i = 0; i < _locationNames.Errors.Count; i++)
                        {
                            errors[i] = _locationNames.Errors[i].Message;
                        }
                        status = false;
                        return Json(new { status, errors, Url = Url.Action("Locations", "Admin") });
                    }
                    status = true;
                    return Json(new { status, Url = Url.Action("Locations", "Admin") });
                }
                errors = new string[1];
                errors[0] = "Daxil edilən məlumatlar düzgün deyil.";
                status = false;
                return Json(new { status, errors, Url = Url.Action("Locations", "Admin") });
            }
            else
            {
                bool status = false;
                string[] errors;
                if (data.Value != null && data.GroupID != 0 && data.SubGroupID != 0)
                {
                    data.Value = TextHelpers.CapitalizeFirstLetter(data.Value);
                    BusinessLayerResult<LocationNames> _locationNames = locationNameManager.UpdateData(data);
                    if (_locationNames.Errors.Count > 0)
                    {
                        errors = new string[_locationNames.Errors.Count];
                        for (int i = 0; i < _locationNames.Errors.Count; i++)
                        {
                            errors[i] = _locationNames.Errors[i].Message;
                        }
                        status = false;
                        return Json(new { status, errors, Url = Url.Action("Locations", "Admin") });
                    }
                    status = true;
                    return Json(new { status, Url = Url.Action("Locations", "Admin") });
                }
                errors = new string[1];
                errors[0] = "Daxil edilən məlumatlar düzgün deyil.";
                status = false;
                return Json(new { status, errors, Url = Url.Action("Locations", "Admin") });
            }
        }
        [WebMethod]
        public ActionResult GetLocationSubGroup(int? id)
        {
            List<LocationSubGroup> _locationSubGroupList = new List<LocationSubGroup>();
            _locationSubGroupList = locationSubGroupManager.GetSubGroup(id);

            return Json(_locationSubGroupList.Select(x => new
            {
                ID = x.ID,
                Value = x.Value
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteLocation(int id, int loctype)
        {
            bool status = false;
            string[] errors;
            if (loctype == 3 && id > 0)
            {
                BusinessLayerResult<LocationNames> _locationNames = locationNameManager.DeleteData(id);
                if (_locationNames.Errors.Count > 0)
                {
                    errors = new string[_locationNames.Errors.Count];
                    for (int i = 0; i < _locationNames.Errors.Count; i++)
                    {
                        errors[i] = _locationNames.Errors[i].Message;
                    }
                    status = false;
                    return Json(new { status, errors, Url = Url.Action("Locations", "Admin") });
                }
                status = true;
                return Json(new { status, Url = Url.Action("Locations", "Admin") });
            }
            else if (loctype == 2 && id > 0)
            {
                BusinessLayerResult<LocationSubGroup> _locationSubGroup = locationSubGroupManager.DeleteData(id);
                if (_locationSubGroup.Errors.Count > 0)
                {
                    errors = new string[_locationSubGroup.Errors.Count];
                    for (int i = 0; i < _locationSubGroup.Errors.Count; i++)
                    {
                        errors[i] = _locationSubGroup.Errors[i].Message;
                    }
                    status = false;
                    return Json(new { status, errors, Url = Url.Action("Locations", "Admin") });
                }
                status = true;
                return Json(new { status, Url = Url.Action("Locations", "Admin") });
            }
            else if (loctype == 1 && id > 0)
            {
                BusinessLayerResult<LocationGroup> _locationGroup = locationGroupManager.DeleteData(id);
                if (_locationGroup.Errors.Count > 0)
                {
                    errors = new string[_locationGroup.Errors.Count];
                    for (int i = 0; i < _locationGroup.Errors.Count; i++)
                    {
                        errors[i] = _locationGroup.Errors[i].Message;
                    }
                    status = false;
                    return Json(new { status, errors, Url = Url.Action("Locations", "Admin") });
                }
                status = true;
                return Json(new { status, Url = Url.Action("Locations", "Admin") });
            }
            errors = new string[1];
            errors[0] = "Daxil edilən məlumatlar düzgün deyil.";
            status = false;
            return Json(new { status, errors, Url = Url.Action("Locations", "Admin") });
        }
        public ActionResult EditStore(int? id)
        {
            Stores _stores = new Stores();
            _stores = storesManager.Find(x => x.StoreID == id);
            if (_stores!=null)
            {
                return View(_stores);
            }
            TempData["msg"] = "Mağaza kodu mövcud deyil!";
            TempData["typ"] = "error";
            return RedirectToAction("CreateStore");         
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStore(Stores data)
        {
            bool status = false;
            string[] errors;
            if (ModelState.IsValid)            
            {              
                BusinessLayerResult<Stores> _stores = storesManager.UpdateStore(data);
                if (_stores.Errors.Count > 0)
                {
                    errors = new string[_stores.Errors.Count];
                    for (int i = 0; i < _stores.Errors.Count; i++)
                    {
                        errors[i] = _stores.Errors[i].Message;
                    }
                    status = false;
                    return Json(new { status, errors, Url = Url.Action("CreateStore", "Admin") });
                }
                status = true;
                return Json(new { status, Url = Url.Action("CreateStore", "Admin") });
            }
            errors = new string[1];
            errors[0] = "Daxil edilən məlumatlar düzgün deyil.";
            status = false;
            return Json(new { status, errors, Url = Url.Action("CreateStore", "Admin") });
        }
    }
}