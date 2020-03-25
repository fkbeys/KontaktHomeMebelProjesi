using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<STOK_ANA_GRUPLARI> StokAnaGruplar { get; set; }
        public DbSet<Visits> Visits { get; set; }
        public DbSet<VisitImages> VisitImages { get; set; }
    }
}
