using Entities;
using Entities.Model;
using Entities.Model.LocalModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CustomInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            Users user = new Users()
            {
                UserDisplayName = "Administrator",
                UserName = "administrator",
                UserPassword = "654321",
                IsActive = true,
                EditDate = 1
            };
            context.Users.Add(user);
            context.SaveChanges();


            List<UserRoles> userRole = new List<UserRoles>();
            userRole.Add(new UserRoles() { RoleName = "Admin" });
            userRole.Add(new UserRoles() { RoleName = "Kordinator" });
            userRole.Add(new UserRoles() { RoleName = "Vizitor" });
            userRole.Add(new UserRoles() { RoleName = "Satici" });
            userRole.Add(new UserRoles() { RoleName = "Dizayner" });
            context.UserRoles.AddRange(userRole);
            context.SaveChanges();

            UserRoles userRoleId = context.UserRoles.FirstOrDefault(x => x.RoleName == "Admin");
            Users userId = context.Users.FirstOrDefault(x => x.UserName == "administrator");

            UserRolesMapping roleMapping = new UserRolesMapping()
            {
                RoleID = userRoleId.ID,
                UserID = userId.UserID
            };

            context.UserRolesMappings.Add(roleMapping);

            AdditionalCharges charges = new AdditionalCharges() { charge_name = "Custom", charge_value = 0 };
            context.AdditionalCharges.Add(charges);
            context.SaveChanges();

        }
    }
}
