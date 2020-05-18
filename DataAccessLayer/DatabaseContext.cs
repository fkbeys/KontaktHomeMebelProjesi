using Entities;
using Entities.Model;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<STOK_ANA_GRUPLARI> StokAnaGruplar { get; set; }
        public DbSet<Visits> Visits { get; set; }
        public DbSet<VisitImages> VisitImages { get; set; }
        public DbSet<Stores> Stores { get; set; }
        public DbSet<ChangeLog> ChangeLogs { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<UserRolesMapping> UserRolesMappings { get; set; }

        public override int SaveChanges()
        {
            var modifiedEntities = ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Modified).ToList();
            var now = DateTime.UtcNow;
            string userName = "";
            foreach (var change in modifiedEntities)
            {
                var entityName = change.Entity.GetType().Name;
                var primaryKey = GetPrimaryKeyValue(change);

                foreach (var prop in change.OriginalValues.PropertyNames)
                {
                    if (prop.ToString() == "UpdateUser")
                    {
                        userName= change.CurrentValues[prop]?.ToString();
                    }
                    if (prop.ToString() != "LastUpdate" && prop.ToString() != "UpdateUser")
                    {
                        var originalValue = change.OriginalValues[prop]?.ToString();
                        var currentValue = change.CurrentValues[prop]?.ToString();
                        if (originalValue != currentValue)
                        {
                            ChangeLog log = new ChangeLog()
                            {
                                EntityName = entityName,
                                PrimaryKeyValue = primaryKey.ToString(),
                                PropertyName = prop,
                                OldValue = originalValue,
                                NewValue = currentValue,
                                DateChanged = now,
                                ChangedUser=userName
                            };
                            ChangeLogs.Add(log);
                        }
                    }
                }
            }
            return base.SaveChanges();
        }
        object GetPrimaryKeyValue(DbEntityEntry entry)
        {
            var objectStateEntry = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            return objectStateEntry.EntityKey.EntityKeyValues[0].Value;
        }
    }
}
