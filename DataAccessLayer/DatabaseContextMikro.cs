﻿using Entities;
using Entities.Model;
using Entities.Model.MikroModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DatabaseContextMikro:DbContext
    {
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    throw new UnintentionalCodeFirstException();
        //}
        public virtual DbSet<STOK_ANA_GRUPLARI> StokAnaGruplar { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<STOKLAR> STOKLAR { get; set; }
        public virtual DbSet<STOK_SATIS_FIYAT_LISTELERI> STOK_SATIS_FIYAT_LISTELERI { get; set; }
        public virtual DbSet<URUNLER> URUNLER { get; set; }
        public virtual DbSet<URUN_RECETELERI> URUN_RECETELERI { get; set; }
        public virtual DbSet<CARI_HESAPLAR> CARI_HESAPLAR { get; set; }
        public virtual DbSet<SIPARISLER> SIPARISLER { get; set; }
        public virtual DbSet<SORUMLULUK_MERKEZLERI> SORUMLULUK_MERKEZLERI { get; set; }
        
    }
}
