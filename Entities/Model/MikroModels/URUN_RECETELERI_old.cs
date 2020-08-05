﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model.MikroModels
{
    [Table("URUN_RECETELERI")]
    public class URUN_RECETELERI_old
    {
        [Key]
        public System.Guid rec_Guid { get; set; }
        public short rec_DBCno { get; set; }
        public Nullable<int> rec_SpecRECno { get; set; }
        public Nullable<bool> rec_iptal { get; set; }
        public Nullable<short> rec_fileid { get; set; }
        public Nullable<bool> rec_hidden { get; set; }
        public Nullable<bool> rec_kilitli { get; set; }
        public Nullable<bool> rec_degisti { get; set; }
        public Nullable<int> rec_checksum { get; set; }
        public Nullable<short> rec_create_user { get; set; }
        public System.DateTime rec_create_date { get; set; }
        public Nullable<short> rec_lastup_user { get; set; }
        public Nullable<System.DateTime> rec_lastup_date { get; set; }
        public string rec_special1 { get; set; }
        public string rec_special2 { get; set; }
        public string rec_special3 { get; set; }
        public Nullable<byte> rec_anatipi { get; set; }
        public string rec_anakod { get; set; }
        public string rec_tanimkod { get; set; }
        public Nullable<byte> rec_cinsi { get; set; }
        public Nullable<System.DateTime> rec_tarih { get; set; }
        public string rec_aciklama { get; set; }
        public Nullable<byte> rec_anabirim { get; set; }
        public Nullable<double> rec_anamiktar { get; set; }
        public Nullable<byte> rec_tuketim_tur { get; set; }
        public string rec_tuketim_kod { get; set; }
        public string rec_tuketim_tanim_kodu { get; set; }
        public Nullable<byte> rec_tuketim_recete_cinsi { get; set; }
        public Nullable<double> rec_tuketim_miktar { get; set; }
        public Nullable<byte> rec_tuketim_birim { get; set; }
        public Nullable<byte> rec_uretim_tuketim { get; set; }
        public Nullable<int> rec_satirno { get; set; }
        public string rec_satir_acik { get; set; }
        public Nullable<int> rec_depono { get; set; }
        public Nullable<double> rec_fireyuzde { get; set; }
        public Nullable<System.DateTime> rec_baslama_tarihi { get; set; }
        public Nullable<System.DateTime> rec_bitis_tarihi { get; set; }
        public string rec_alt_tukkod1 { get; set; }
        public Nullable<double> rec_alt_1_katsayi { get; set; }
        public string rec_alt_tukkod2 { get; set; }
        public Nullable<double> rec_alt_2_katsayi { get; set; }
        public string rec_alt_tukkod3 { get; set; }
        public Nullable<double> rec_alt_3_katsayi { get; set; }
        public string rec_alt_tukkod4 { get; set; }
        public Nullable<double> rec_alt_4_katsayi { get; set; }
        public string rec_alt_tukkod5 { get; set; }
        public Nullable<double> rec_alt_5_katsayi { get; set; }
        public Nullable<short> rec_safha_no { get; set; }
        public Nullable<byte> rec_safha_turu { get; set; }
        public Nullable<byte> rec_ana_renk_no { get; set; }
        public Nullable<byte> rec_ana_beden_no { get; set; }
        public Nullable<byte> rec_tuketim_renk_no { get; set; }
        public Nullable<byte> rec_tuketim_beden_no { get; set; }
        public Nullable<byte> rec_PlanlamaTipi { get; set; }
        public Nullable<byte> rec_eklenme_sarti { get; set; }
        public string rec_miktar_fonksiyon_adi { get; set; }
    }
}
