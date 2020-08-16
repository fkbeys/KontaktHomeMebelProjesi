using Entities;
using Entities.Model;
using Entities.Model.LocalModels;
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
        public DbSet<Visits> Visits { get; set; }
        public DbSet<VisitImages> VisitImages { get; set; }
        public DbSet<Stores> Stores { get; set; }
        public DbSet<ChangeLog> ChangeLogs { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<UserRolesMapping> UserRolesMappings { get; set; }
        public DbSet<AdditionalCharges> AdditionalCharges { get; set; }
        public DbSet<Production> Production { get; set; }
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
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // one-to-many relationship
            modelBuilder.Entity<UserRolesMapping>().HasRequired<Users>(s=>s.User).WithMany(g=>g.UserRolesMappings).HasForeignKey<int>(s => s.UserID);
            modelBuilder.Entity<UserRolesMapping>().HasRequired<UserRoles>(s => s.UserRole).WithMany(g => g.UserRolesMappings).HasForeignKey<int>(s => s.RoleID);
        }

        public DatabaseContext()
        {
            Database.SetInitializer(new CustomInitializer());
        }
        
    }
}
