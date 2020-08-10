﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model.MikroModels
{
    [Table("STOK_SATIS_FIYAT_LISTELERI")]
    public class STOK_SATIS_FIYAT_LISTELERI_old
    {
        [Key]
        public System.Guid sfiyat_Guid { get; set; }
        public short sfiyat_DBCno { get; set; }
        public Nullable<int> sfiyat_SpecRECno { get; set; }
        public Nullable<bool> sfiyat_iptal { get; set; }
        public Nullable<short> sfiyat_fileid { get; set; }
        public Nullable<bool> sfiyat_hidden { get; set; }
        public Nullable<bool> sfiyat_kilitli { get; set; }
        public Nullable<bool> sfiyat_degisti { get; set; }
        public Nullable<int> sfiyat_checksum { get; set; }
        public Nullable<short> sfiyat_create_user { get; set; }
        public System.DateTime sfiyat_create_date { get; set; }
        public Nullable<short> sfiyat_lastup_user { get; set; }
        public Nullable<System.DateTime> sfiyat_lastup_date { get; set; }
        public string sfiyat_special1 { get; set; }
        public string sfiyat_special2 { get; set; }
        public string sfiyat_special3 { get; set; }
        public string sfiyat_stokkod { get; set; }
        public Nullable<int> sfiyat_listesirano { get; set; }
        public Nullable<int> sfiyat_deposirano { get; set; }
        public Nullable<int> sfiyat_odemeplan { get; set; }
        public Nullable<byte> sfiyat_birim_pntr { get; set; }
        public Nullable<double> sfiyat_fiyati { get; set; }
        public Nullable<byte> sfiyat_doviz { get; set; }
        public string sfiyat_iskontokod { get; set; }
        public Nullable<byte> sfiyat_deg_nedeni { get; set; }
        public Nullable<double> sfiyat_primyuzdesi { get; set; }
        public string sfiyat_kampanyakod { get; set; }
    }
}