﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model.MikroModels
{
    [Table("STOKLAR")]
    public class STOKLAR_old
    {
        public System.Guid sto_Guid { get; set; }
        public short sto_DBCno { get; set; }
        public Nullable<int> sto_SpecRECno { get; set; }
        public Nullable<bool> sto_iptal { get; set; }
        public Nullable<short> sto_fileid { get; set; }
        public Nullable<bool> sto_hidden { get; set; }
        public Nullable<bool> sto_kilitli { get; set; }
        public Nullable<bool> sto_degisti { get; set; }
        public Nullable<int> sto_checksum { get; set; }
        public Nullable<short> sto_create_user { get; set; }
        public System.DateTime sto_create_date { get; set; }
        public Nullable<short> sto_lastup_user { get; set; }
        public Nullable<System.DateTime> sto_lastup_date { get; set; }
        public string sto_special1 { get; set; }
        public string sto_special2 { get; set; }
        public string sto_special3 { get; set; }
        [Key]
        public string sto_kod { get; set; }
        public string sto_isim { get; set; }
        public string sto_kisa_ismi { get; set; }
        public string sto_yabanci_isim { get; set; }
        public string sto_sat_cari_kod { get; set; }
        public Nullable<byte> sto_cins { get; set; }
        public Nullable<byte> sto_doviz_cinsi { get; set; }
        public Nullable<byte> sto_detay_takip { get; set; }
        public string sto_birim1_ad { get; set; }
        public Nullable<double> sto_birim1_katsayi { get; set; }
        public Nullable<double> sto_birim1_agirlik { get; set; }
        public Nullable<double> sto_birim1_en { get; set; }
        public Nullable<double> sto_birim1_boy { get; set; }
        public Nullable<double> sto_birim1_yukseklik { get; set; }
        public Nullable<double> sto_birim1_dara { get; set; }
        public string sto_birim2_ad { get; set; }
        public Nullable<double> sto_birim2_katsayi { get; set; }
        public Nullable<double> sto_birim2_agirlik { get; set; }
        public Nullable<double> sto_birim2_en { get; set; }
        public Nullable<double> sto_birim2_boy { get; set; }
        public Nullable<double> sto_birim2_yukseklik { get; set; }
        public Nullable<double> sto_birim2_dara { get; set; }
        public string sto_birim3_ad { get; set; }
        public Nullable<double> sto_birim3_katsayi { get; set; }
        public Nullable<double> sto_birim3_agirlik { get; set; }
        public Nullable<double> sto_birim3_en { get; set; }
        public Nullable<double> sto_birim3_boy { get; set; }
        public Nullable<double> sto_birim3_yukseklik { get; set; }
        public Nullable<double> sto_birim3_dara { get; set; }
        public string sto_birim4_ad { get; set; }
        public Nullable<double> sto_birim4_katsayi { get; set; }
        public Nullable<double> sto_birim4_agirlik { get; set; }
        public Nullable<double> sto_birim4_en { get; set; }
        public Nullable<double> sto_birim4_boy { get; set; }
        public Nullable<double> sto_birim4_yukseklik { get; set; }
        public Nullable<double> sto_birim4_dara { get; set; }
        public string sto_muh_kod { get; set; }
        public string sto_muh_Iade_kod { get; set; }
        public string sto_muh_sat_muh_kod { get; set; }
        public string sto_muh_satIadmuhkod { get; set; }
        public string sto_muh_sat_isk_kod { get; set; }
        public string sto_muh_aIiskmuhkod { get; set; }
        public string sto_muh_satmalmuhkod { get; set; }
        public string sto_yurtdisi_satmuhk { get; set; }
        public string sto_ilavemasmuhkod { get; set; }
        public string sto_yatirimtesmuhkod { get; set; }
        public string sto_depsatmuhkod { get; set; }
        public string sto_depsatmalmuhkod { get; set; }
        public string sto_bagortsatmuhkod { get; set; }
        public string sto_bagortsatIadmuhkod { get; set; }
        public string sto_bagortsatIskmuhkod { get; set; }
        public string sto_satfiyfarkmuhkod { get; set; }
        public string sto_yurtdisisatmalmuhkod { get; set; }
        public string sto_bagortsatmalmuhkod { get; set; }
        public string sto_sifirbedsatmalmuhkod { get; set; }
        public string sto_ihrackayitlisatismuhkod { get; set; }
        public string sto_ihrackayitlisatismaliyetimuhkod { get; set; }
        public Nullable<double> sto_karorani { get; set; }
        public Nullable<double> sto_min_stok { get; set; }
        public Nullable<double> sto_siparis_stok { get; set; }
        public Nullable<double> sto_max_stok { get; set; }
        public Nullable<byte> sto_ver_sip_birim { get; set; }
        public Nullable<byte> sto_al_sip_birim { get; set; }
        public Nullable<short> sto_siparis_sure { get; set; }
        public Nullable<byte> sto_perakende_vergi { get; set; }
        public Nullable<byte> sto_toptan_vergi { get; set; }
        public string sto_yer_kod { get; set; }
        public Nullable<byte> sto_elk_etk_tipi { get; set; }
        public Nullable<byte> sto_raf_etiketli { get; set; }
        public Nullable<byte> sto_etiket_bas { get; set; }
        public Nullable<byte> sto_satis_dursun { get; set; }
        public Nullable<byte> sto_siparis_dursun { get; set; }
        public Nullable<byte> sto_malkabul_dursun { get; set; }
        public Nullable<bool> sto_malkabul_gun1 { get; set; }
        public Nullable<bool> sto_malkabul_gun2 { get; set; }
        public Nullable<bool> sto_malkabul_gun3 { get; set; }
        public Nullable<bool> sto_malkabul_gun4 { get; set; }
        public Nullable<bool> sto_malkabul_gun5 { get; set; }
        public Nullable<bool> sto_malkabul_gun6 { get; set; }
        public Nullable<bool> sto_malkabul_gun7 { get; set; }
        public Nullable<bool> sto_siparis_gun1 { get; set; }
        public Nullable<bool> sto_siparis_gun2 { get; set; }
        public Nullable<bool> sto_siparis_gun3 { get; set; }
        public Nullable<bool> sto_siparis_gun4 { get; set; }
        public Nullable<bool> sto_siparis_gun5 { get; set; }
        public Nullable<bool> sto_siparis_gun6 { get; set; }
        public Nullable<bool> sto_siparis_gun7 { get; set; }
        public Nullable<bool> sto_iskon_yapilamaz { get; set; }
        public Nullable<bool> sto_tasfiyede { get; set; }
        public Nullable<short> sto_alt_grup_no { get; set; }
        public string sto_kategori_kodu { get; set; }
        public string sto_urun_sorkod { get; set; }
        public string sto_altgrup_kod { get; set; }
        public string sto_anagrup_kod { get; set; }
        public string sto_uretici_kodu { get; set; }
        public string sto_sektor_kodu { get; set; }
        public string sto_reyon_kodu { get; set; }
        public string sto_muhgrup_kodu { get; set; }
        public string sto_ambalaj_kodu { get; set; }
        public string sto_marka_kodu { get; set; }
        public string sto_beden_kodu { get; set; }
        public string sto_renk_kodu { get; set; }
        public string sto_model_kodu { get; set; }
        public string sto_sezon_kodu { get; set; }
        public string sto_hammadde_kodu { get; set; }
        public string sto_prim_kodu { get; set; }
        public string sto_kalkon_kodu { get; set; }
        public string sto_paket_kodu { get; set; }
        public string sto_pozisyonbayrak_kodu { get; set; }
        public string sto_mkod_artik { get; set; }
        public Nullable<bool> sto_kasa_tarti_fl { get; set; }
        public Nullable<bool> sto_bedenli_takip { get; set; }
        public Nullable<bool> sto_renkDetayli { get; set; }
        public Nullable<bool> sto_miktarondalikli_fl { get; set; }
        public Nullable<bool> sto_pasif_fl { get; set; }
        public Nullable<bool> sto_eksiyedusebilir_fl { get; set; }
        public string sto_GtipNo { get; set; }
        public Nullable<double> sto_puan { get; set; }
        public string sto_komisyon_hzmkodu { get; set; }
        public Nullable<double> sto_komisyon_orani { get; set; }
        public Nullable<byte> sto_otvuygulama { get; set; }
        public Nullable<double> sto_otvtutar { get; set; }
        public Nullable<byte> sto_otvliste { get; set; }
        public Nullable<byte> sto_otvbirimi { get; set; }
        public Nullable<double> sto_prim_orani { get; set; }
        public Nullable<short> sto_garanti_sure { get; set; }
        public Nullable<byte> sto_garanti_sure_tipi { get; set; }
        public Nullable<double> sto_iplik_Ne_no { get; set; }
        public Nullable<double> sto_standartmaliyet { get; set; }
        public Nullable<double> sto_kanban_kasa_miktari { get; set; }
        public Nullable<byte> sto_oivuygulama { get; set; }
        public Nullable<bool> sto_zraporu_stoku_fl { get; set; }
        public Nullable<double> sto_maxiskonto_orani { get; set; }
        public Nullable<bool> sto_detay_takibinde_depo_kontrolu_fl { get; set; }
        public string sto_tamamlayici_kodu { get; set; }
        public Nullable<byte> sto_oto_barkod_acma_sekli { get; set; }
        public string sto_oto_barkod_kod_yapisi { get; set; }
        public Nullable<double> sto_KasaIskontoOrani { get; set; }
        public Nullable<double> sto_KasaIskontoTutari { get; set; }
        public Nullable<double> sto_gelirpayi { get; set; }
        public Nullable<double> sto_oivtutar { get; set; }
        public Nullable<byte> sto_oivturu { get; set; }
        public string sto_giderkodu { get; set; }
        public Nullable<byte> sto_oivvergipntr { get; set; }
        public Nullable<byte> sto_Tevkifat_turu { get; set; }
        public Nullable<bool> sto_SKT_fl { get; set; }
        public Nullable<short> sto_terazi_SKT { get; set; }
        public Nullable<short> sto_RafOmru { get; set; }
        public Nullable<bool> sto_KasadaTaksitlenebilir_fl { get; set; }
        public string sto_ufrsfark_kod { get; set; }
        public string sto_iade_ufrsfark_kod { get; set; }
        public string sto_yurticisat_ufrsfark_kod { get; set; }
        public string sto_satiade_ufrsfark_kod { get; set; }
        public string sto_satisk_ufrsfark_kod { get; set; }
        public string sto_alisk_ufrsfark_kod { get; set; }
        public string sto_satmal_ufrsfark_kod { get; set; }
        public string sto_yurtdisisat_ufrsfark_kod { get; set; }
        public string sto_ilavemas_ufrsfark_kod { get; set; }
        public string sto_yatirimtes_ufrsfark_kod { get; set; }
        public string sto_depsat_ufrsfark_kod { get; set; }
        public string sto_depsatmal_ufrsfark_kod { get; set; }
        public string sto_bagortsat_ufrsfark_kod { get; set; }
        public string sto_bagortsatiade_ufrsfark_kod { get; set; }
        public string sto_bagortsatisk_ufrsfark_kod { get; set; }
        public string sto_satfiyfark_ufrsfark_kod { get; set; }
        public string sto_yurtdisisatmal_ufrsfark_kod { get; set; }
        public string sto_bagortsatmal_ufrsfark_kod { get; set; }
        public string sto_sifirbedsatmal_ufrsfark_kod { get; set; }
        public string sto_uretimmaliyet_ufrsfark_kod { get; set; }
        public string sto_uretimkapasite_ufrsfark_kod { get; set; }
        public string sto_degerdusuklugu_ufrs_kod { get; set; }
        public Nullable<double> sto_halrusumyudesi { get; set; }
        public Nullable<bool> sto_webe_gonderilecek_fl { get; set; }
        public Nullable<short> sto_min_stok_belirleme_gun { get; set; }
        public Nullable<short> sto_sip_stok_belirleme_gun { get; set; }
        public Nullable<short> sto_max_stok_belirleme_gun { get; set; }
        public Nullable<bool> sto_sev_bel_opr_degerlendime_fl { get; set; }
        public Nullable<byte> sto_otv_tevkifat_turu { get; set; }
        public Nullable<byte> sto_kay_plan_degerlendir { get; set; }
        public Nullable<bool> sto_CRM_sistemine_aktar_fl { get; set; }
        public int sto_plu_no { get; set; }
        public Nullable<byte> sto_yerli_yabanci { get; set; }
        public string sto_mensei { get; set; }
        public Nullable<bool> sto_oto_parti_lot_kod_fl { get; set; }
        public string sto_efat_sinif_kodu { get; set; }
        public string sto_efat_sinif_listesi { get; set; }
        public string sto_efat_sinif_versiyonu { get; set; }
        public Nullable<bool> sto_utssisteminegonderilsin_fl { get; set; }
        public Nullable<bool> sto_posetbeyannamekonusu_fl { get; set; }
        public Nullable<short> sto_STT_oncesi_kaldirma { get; set; }
        public Nullable<short> sto_toplam_rafomru { get; set; }
        public Nullable<bool> sto_fiyat_kasada_belirlenir_fl { get; set; }
        public Nullable<byte> sto_franchise_siparis_dursun { get; set; }
        public string sto_GEKAP { get; set; }
        public Nullable<byte> sto_GEKAP_birim { get; set; }
        public string sto_resim_url { get; set; }
    }
}
