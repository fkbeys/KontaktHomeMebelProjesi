using Entities;
using System.Data.Entity;

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
    }
}
