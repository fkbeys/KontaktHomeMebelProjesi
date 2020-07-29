using BusinessLayer.QueryResult;
using Entities.Messages;
using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Managers
{
    public class UserRolesMappingManager : ManagerBase<UserRolesMapping>
    {
        public BusinessLayerResult<UserRolesMapping> InsertUserRoles(UserRolesMapping data)
        {
            BusinessLayerResult<UserRolesMapping> userRoles = new BusinessLayerResult<UserRolesMapping>();
            userRoles.Result = Find(x => x.RoleID == data.RoleID && x.UserID == data.UserID);
            if (userRoles.Result != null)
            {
                userRoles.AddError(ErrorMessageCode.RoleAlreadyAssigned, "İstifadəçi səlahiyyəti mövcuddur");
            }
            else
            {
                userRoles.Result = data;
                if (base.Insert(userRoles.Result) == 0)
                {
                    userRoles.AddError(ErrorMessageCode.RoleCouldnotInserted, "Xəta baş verdi.İstifadəçi səlahiyyəti qeyd edilmədi");
                }
            }
            return userRoles;
        }
        public BusinessLayerResult<UserRolesMapping> DeleteUserRole(int data)
        {
            BusinessLayerResult<UserRolesMapping> userRoles = new BusinessLayerResult<UserRolesMapping>();
            userRoles.Result = Find(x => x.ID == data);
            if (userRoles.Result!=null)
            {
                if (Delete(userRoles.Result) == 0)
                {
                    userRoles.AddError(ErrorMessageCode.RoleCouldnotInserted, "Xəta baş verdi.İstifadəçi səlahiyyəti silinmədi");
                }
            }
            else
            {
                userRoles.AddError(ErrorMessageCode.RoleCouldnotInserted, "Xəta baş verdi.İstifadəçi səlahiyyəti silinmədi");
            }
            return userRoles;
        }
    }
}
