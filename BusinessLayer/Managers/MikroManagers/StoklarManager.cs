﻿using BusinessLayer.QueryResult;
using DataAccessLayer;
using Entities;
using Entities.Messages;
using Entities.Model;
using Entities.Model.MikroModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Managers.MikroManagers
{
    public class StoklarManager 
    {                
        public BusinessLayerResult<STOKLAR> InsertData(STOKLAR data, Users user)
        {
            using (var contextmikro = new DatabaseContextMikro())
            {
                BusinessLayerResult<STOKLAR> _stoklar = new BusinessLayerResult<STOKLAR>();
                _stoklar.Result = contextmikro.STOKLAR.FirstOrDefault(x => x.sto_kod == data.sto_kod);
                Guid _guid = Guid.NewGuid();
                if (_stoklar.Result != null)
                {
                    _stoklar.AddError(ErrorMessageCode.DataAlreadyExists, "Xəta baş verdi. Daxil edilən stok mövcuddur.");
                }
                else
                {
                    DateTime tarix = DateTime.Today;
                    _stoklar.Result = data;
                    _stoklar.Result.sto_Guid = _guid;
                    _stoklar.Result.sto_DBCno = 0;
                    _stoklar.Result.sto_SpecRECno = 0;
                    _stoklar.Result.sto_iptal = false;
                    _stoklar.Result.sto_fileid = 13;
                    _stoklar.Result.sto_hidden = false;
                    _stoklar.Result.sto_kilitli = false;
                    _stoklar.Result.sto_degisti = false;
                    _stoklar.Result.sto_checksum = 0;
                    _stoklar.Result.sto_create_user = 1;
                    _stoklar.Result.sto_create_date = tarix;
                    _stoklar.Result.sto_lastup_user = 1;
                    _stoklar.Result.sto_lastup_date = tarix;
                    _stoklar.Result.sto_special1 = "";
                    _stoklar.Result.sto_special2 = "";
                    _stoklar.Result.sto_special3 = "";
                    _stoklar.Result.sto_kisa_ismi = "";
                    _stoklar.Result.sto_yabanci_isim = "";
                    _stoklar.Result.sto_sat_cari_kod = "";
                    _stoklar.Result.sto_cins = 4;
                    _stoklar.Result.sto_doviz_cinsi = 0;
                    _stoklar.Result.sto_detay_takip = 0;
                    _stoklar.Result.sto_birim1_ad = "";
                    _stoklar.Result.sto_birim1_katsayi = 1;
                    _stoklar.Result.sto_birim1_agirlik = 0;
                    _stoklar.Result.sto_birim1_en = 0;
                    _stoklar.Result.sto_birim1_boy = 0;
                    _stoklar.Result.sto_birim1_yukseklik = 0;
                    _stoklar.Result.sto_birim1_dara = 0;
                    _stoklar.Result.sto_birim2_ad = "";
                    _stoklar.Result.sto_birim2_katsayi = 0;
                    _stoklar.Result.sto_birim2_agirlik = 0;
                    _stoklar.Result.sto_birim2_en = 0;
                    _stoklar.Result.sto_birim2_boy = 0;
                    _stoklar.Result.sto_birim2_yukseklik = 0;
                    _stoklar.Result.sto_birim2_dara = 0;
                    _stoklar.Result.sto_birim3_ad = "";
                    _stoklar.Result.sto_birim3_katsayi = 0;
                    _stoklar.Result.sto_birim3_agirlik = 0;
                    _stoklar.Result.sto_birim3_en = 0;
                    _stoklar.Result.sto_birim3_boy = 0;
                    _stoklar.Result.sto_birim3_yukseklik = 0;
                    _stoklar.Result.sto_birim3_dara = 0;
                    _stoklar.Result.sto_birim4_ad = "";
                    _stoklar.Result.sto_birim4_katsayi = 0;
                    _stoklar.Result.sto_birim4_agirlik = 0;
                    _stoklar.Result.sto_birim4_en = 0;
                    _stoklar.Result.sto_birim4_boy = 0;
                    _stoklar.Result.sto_birim4_yukseklik = 0;
                    _stoklar.Result.sto_birim4_dara = 0;
                    _stoklar.Result.sto_muh_kod = "205";
                    _stoklar.Result.sto_muh_Iade_kod = "205";
                    _stoklar.Result.sto_muh_sat_muh_kod = "601";
                    _stoklar.Result.sto_muh_satIadmuhkod = "602";
                    _stoklar.Result.sto_muh_sat_isk_kod = "602";
                    _stoklar.Result.sto_muh_aIiskmuhkod = "205";
                    _stoklar.Result.sto_muh_satmalmuhkod = "701";
                    _stoklar.Result.sto_yurtdisi_satmuhk = "601";
                    _stoklar.Result.sto_ilavemasmuhkod = "205";
                    _stoklar.Result.sto_yatirimtesmuhkod = "";
                    _stoklar.Result.sto_depsatmuhkod = "";
                    _stoklar.Result.sto_depsatmalmuhkod = "";
                    _stoklar.Result.sto_bagortsatmuhkod = "";
                    _stoklar.Result.sto_bagortsatIadmuhkod = "";
                    _stoklar.Result.sto_bagortsatIskmuhkod = "";
                    _stoklar.Result.sto_satfiyfarkmuhkod = "";
                    _stoklar.Result.sto_yurtdisisatmalmuhkod = "701";
                    _stoklar.Result.sto_bagortsatmalmuhkod = "";
                    _stoklar.Result.sto_sifirbedsatmalmuhkod = "";
                    _stoklar.Result.sto_ihrackayitlisatismuhkod = "601";
                    _stoklar.Result.sto_ihrackayitlisatismaliyetimuhkod = "";
                    _stoklar.Result.sto_karorani = 0;
                    _stoklar.Result.sto_min_stok = 0;
                    _stoklar.Result.sto_siparis_stok = 0;
                    _stoklar.Result.sto_max_stok = 0;
                    _stoklar.Result.sto_ver_sip_birim = 0;
                    _stoklar.Result.sto_al_sip_birim = 0;
                    _stoklar.Result.sto_siparis_sure = 0;
                    _stoklar.Result.sto_perakende_vergi = 4;
                    _stoklar.Result.sto_toptan_vergi = 4;
                    _stoklar.Result.sto_yer_kod = "";
                    _stoklar.Result.sto_elk_etk_tipi = 0;
                    _stoklar.Result.sto_raf_etiketli = 1;
                    _stoklar.Result.sto_etiket_bas = 1;
                    _stoklar.Result.sto_satis_dursun = 0;
                    _stoklar.Result.sto_siparis_dursun = 0;
                    _stoklar.Result.sto_malkabul_dursun = 0;
                    _stoklar.Result.sto_malkabul_gun1 = false;
                    _stoklar.Result.sto_malkabul_gun2 = false;
                    _stoklar.Result.sto_malkabul_gun3 = false;
                    _stoklar.Result.sto_malkabul_gun4 = false;
                    _stoklar.Result.sto_malkabul_gun5 = false;
                    _stoklar.Result.sto_malkabul_gun6 = false;
                    _stoklar.Result.sto_malkabul_gun7 = false;
                    _stoklar.Result.sto_siparis_gun1 = false;
                    _stoklar.Result.sto_siparis_gun2 = false;
                    _stoklar.Result.sto_siparis_gun3 = false;
                    _stoklar.Result.sto_siparis_gun4 = false;
                    _stoklar.Result.sto_siparis_gun5 = false;
                    _stoklar.Result.sto_siparis_gun6 = false;
                    _stoklar.Result.sto_siparis_gun7 = false;
                    _stoklar.Result.sto_iskon_yapilamaz = false;
                    _stoklar.Result.sto_tasfiyede = false;
                    _stoklar.Result.sto_alt_grup_no = 0;
                    _stoklar.Result.sto_kategori_kodu = "";
                    _stoklar.Result.sto_urun_sorkod = "";
                    _stoklar.Result.sto_altgrup_kod = "";
                    _stoklar.Result.sto_anagrup_kod = "";
                    _stoklar.Result.sto_uretici_kodu = "";
                    _stoklar.Result.sto_sektor_kodu = "";
                    _stoklar.Result.sto_reyon_kodu = "";
                    _stoklar.Result.sto_muhgrup_kodu = "";
                    _stoklar.Result.sto_ambalaj_kodu = "";
                    _stoklar.Result.sto_marka_kodu = "";
                    _stoklar.Result.sto_beden_kodu = "";
                    _stoklar.Result.sto_renk_kodu = "";
                    _stoklar.Result.sto_model_kodu = "";
                    _stoklar.Result.sto_sezon_kodu = "";
                    _stoklar.Result.sto_hammadde_kodu = "";
                    _stoklar.Result.sto_prim_kodu = "";
                    _stoklar.Result.sto_kalkon_kodu = "";
                    _stoklar.Result.sto_paket_kodu = "";
                    _stoklar.Result.sto_pozisyonbayrak_kodu = "";
                    _stoklar.Result.sto_mkod_artik = "";
                    _stoklar.Result.sto_kasa_tarti_fl = false;
                    _stoklar.Result.sto_bedenli_takip = false;
                    _stoklar.Result.sto_renkDetayli = false;
                    _stoklar.Result.sto_miktarondalikli_fl = false;
                    _stoklar.Result.sto_pasif_fl = false;
                    _stoklar.Result.sto_eksiyedusebilir_fl = false;
                    _stoklar.Result.sto_GtipNo = "";
                    _stoklar.Result.sto_puan = 0;
                    _stoklar.Result.sto_komisyon_hzmkodu = "";
                    _stoklar.Result.sto_komisyon_orani = 0;
                    _stoklar.Result.sto_otvuygulama = 0;
                    _stoklar.Result.sto_otvtutar = 0;
                    _stoklar.Result.sto_otvliste = 0;
                    _stoklar.Result.sto_otvbirimi = 1;
                    _stoklar.Result.sto_prim_orani = 0;
                    _stoklar.Result.sto_garanti_sure = 0;
                    _stoklar.Result.sto_garanti_sure_tipi = 0;
                    _stoklar.Result.sto_iplik_Ne_no = 0;
                    _stoklar.Result.sto_standartmaliyet = 0;
                    _stoklar.Result.sto_kanban_kasa_miktari = 0;
                    _stoklar.Result.sto_oivuygulama = 0;
                    _stoklar.Result.sto_zraporu_stoku_fl = false;
                    _stoklar.Result.sto_maxiskonto_orani = 0;
                    _stoklar.Result.sto_detay_takibinde_depo_kontrolu_fl = false;
                    _stoklar.Result.sto_tamamlayici_kodu = "";
                    _stoklar.Result.sto_oto_barkod_acma_sekli = 0;
                    _stoklar.Result.sto_oto_barkod_kod_yapisi = "";
                    _stoklar.Result.sto_KasaIskontoOrani = 0;
                    _stoklar.Result.sto_KasaIskontoTutari = 0;
                    _stoklar.Result.sto_gelirpayi = 0;
                    _stoklar.Result.sto_oivtutar = 0;
                    _stoklar.Result.sto_oivturu = 0;
                    _stoklar.Result.sto_giderkodu = "";
                    _stoklar.Result.sto_oivvergipntr = 0;
                    _stoklar.Result.sto_Tevkifat_turu = 0;
                    _stoklar.Result.sto_SKT_fl = false;
                    _stoklar.Result.sto_terazi_SKT = 0;
                    _stoklar.Result.sto_RafOmru = 0;
                    _stoklar.Result.sto_KasadaTaksitlenebilir_fl = false;
                    _stoklar.Result.sto_ufrsfark_kod = "";
                    _stoklar.Result.sto_iade_ufrsfark_kod = "";
                    _stoklar.Result.sto_yurticisat_ufrsfark_kod = "";
                    _stoklar.Result.sto_satiade_ufrsfark_kod = "";
                    _stoklar.Result.sto_satisk_ufrsfark_kod = "";
                    _stoklar.Result.sto_alisk_ufrsfark_kod = "";
                    _stoklar.Result.sto_satmal_ufrsfark_kod = "";
                    _stoklar.Result.sto_yurtdisisat_ufrsfark_kod = "";
                    _stoklar.Result.sto_ilavemas_ufrsfark_kod = "";
                    _stoklar.Result.sto_yatirimtes_ufrsfark_kod = "";
                    _stoklar.Result.sto_depsat_ufrsfark_kod = "";
                    _stoklar.Result.sto_depsatmal_ufrsfark_kod = "";
                    _stoklar.Result.sto_bagortsat_ufrsfark_kod = "";
                    _stoklar.Result.sto_bagortsatiade_ufrsfark_kod = "";
                    _stoklar.Result.sto_bagortsatisk_ufrsfark_kod = "";
                    _stoklar.Result.sto_satfiyfark_ufrsfark_kod = "";
                    _stoklar.Result.sto_yurtdisisatmal_ufrsfark_kod = "";
                    _stoklar.Result.sto_bagortsatmal_ufrsfark_kod = "";
                    _stoklar.Result.sto_sifirbedsatmal_ufrsfark_kod = "";
                    _stoklar.Result.sto_uretimmaliyet_ufrsfark_kod = "";
                    _stoklar.Result.sto_uretimkapasite_ufrsfark_kod = "";
                    _stoklar.Result.sto_degerdusuklugu_ufrs_kod = "";
                    _stoklar.Result.sto_halrusumyudesi = 0;
                    _stoklar.Result.sto_webe_gonderilecek_fl = false;
                    _stoklar.Result.sto_min_stok_belirleme_gun = 0;
                    _stoklar.Result.sto_sip_stok_belirleme_gun = 0;
                    _stoklar.Result.sto_max_stok_belirleme_gun = 0;
                    _stoklar.Result.sto_sev_bel_opr_degerlendime_fl = false;
                    _stoklar.Result.sto_otv_tevkifat_turu = 0;
                    _stoklar.Result.sto_kay_plan_degerlendir = 0;
                    _stoklar.Result.sto_CRM_sistemine_aktar_fl = false;
                    _stoklar.Result.sto_plu_no = 10;
                    _stoklar.Result.sto_yerli_yabanci = 0;
                    _stoklar.Result.sto_mensei = "";
                    _stoklar.Result.sto_oto_parti_lot_kod_fl = false;
                    _stoklar.Result.sto_efat_sinif_kodu = "";
                    _stoklar.Result.sto_efat_sinif_listesi = "";
                    _stoklar.Result.sto_efat_sinif_versiyonu = "";
                    _stoklar.Result.sto_utssisteminegonderilsin_fl = false;
                    _stoklar.Result.sto_posetbeyannamekonusu_fl = false;
                    _stoklar.Result.sto_STT_oncesi_kaldirma = 0;
                    _stoklar.Result.sto_toplam_rafomru = 0;
                    _stoklar.Result.sto_fiyat_kasada_belirlenir_fl = false;
                    _stoklar.Result.sto_franchise_siparis_dursun = 0;
                    _stoklar.Result.sto_GEKAP = "";
                    _stoklar.Result.sto_GEKAP_birim = 0;
                    _stoklar.Result.sto_resim_url = "";

                    
                    contextmikro.STOKLAR.Add(_stoklar.Result);
                    contextmikro.SaveChanges();
                    //if (base.Insert(_stoklar.Result) == 0)
                    //{
                    //    _stoklar.AddError(ErrorMessageCode.DataAlreadyExists, "Xəta baş verdi. Stok karti yaradılmadı.");
                    //}
                }
                return _stoklar;

            }
               
        }
    }
}
