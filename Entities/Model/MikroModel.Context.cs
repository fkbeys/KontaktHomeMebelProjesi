﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entities.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<STOK_ANA_GRUPLARI> STOK_ANA_GRUPLARI { get; set; }
        public virtual DbSet<STOK_SATIS_FIYAT_LISTELERI> STOK_SATIS_FIYAT_LISTELERI { get; set; }
        public virtual DbSet<STOKLAR> STOKLAR { get; set; }
        public virtual DbSet<URUN_RECETELERI> URUN_RECETELERI { get; set; }
        public virtual DbSet<URUNLER> URUNLER { get; set; }
        public virtual DbSet<Products> Products { get; set; }
    }
}
