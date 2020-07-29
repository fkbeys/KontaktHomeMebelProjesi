using Entities;
using Entities.Model.MikroModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DatabaseContextMikro:DbContext
    {
        public DbSet<STOK_ANA_GRUPLARI> StokAnaGruplar { get; set; }
        public DbSet<Products> Products { get; set; }
    }
}
