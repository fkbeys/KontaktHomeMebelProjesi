using BusinessLayer;
using BusinessLayer.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace KontaktHome.Models
{
    public class UsersRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            UserRolesManager userRoleManager = new UserRolesManager();
            UserRolesMappingManager userRolesMappingManager = new UserRolesMappingManager();
            UserManager userManager = new UserManager();
            var userroles = (from user in userManager.ListQueryable()
                             join roleMapping in userRolesMappingManager.ListQueryable()
                             on user.UserID equals roleMapping.UserID
                             join role in userRoleManager.ListQueryable()
                             on roleMapping.RoleID equals role.ID
                             where user.UserName == username
                             select role.RoleName).ToArray();

            return userroles;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}