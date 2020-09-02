using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BusinessLayer.Managers
{
    public class UserRolesManager:ManagerBase<UserRoles>
    {
        public List<SelectListItem> listUserRoles()
        {
            List<UserRoles> userRoles = List();
            var listroles = userRoles.Select(s => new SelectListItem { Value = s.ID.ToString(), Text = s.RoleName }).ToList<SelectListItem>();
            return listroles;
        }
    }
}
