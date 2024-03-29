﻿using BusinessLayer.QueryResult;
using Entities;
using Entities.Helper;
using Entities.Messages;
using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Managers.MikroManagers
{
    public class CariManager:ManagerBaseMikro<CARI_HESAPLAR>
    {
        public BusinessLayerResult<CARI_HESAPLAR> InsertData(CARI_HESAPLAR data, Users user)
        {
            BusinessLayerResult<CARI_HESAPLAR> _carihesaplar = new BusinessLayerResult<CARI_HESAPLAR>();
            _carihesaplar.Result = Find(x => x.cari_kod == data.cari_kod);
            if (_carihesaplar.Result==null)
            {
                _carihesaplar.Result = data;
                _carihesaplar.Result.cari_create_date = DateTime.Now;
                _carihesaplar.Result.cari_lastup_date = DateTime.Now;
                _carihesaplar.Result.cari_Guid = Guid.NewGuid();
                _carihesaplar.Result.cari_DBCno = 0;
                _carihesaplar.Result.cari_SpecRECno = 0;
                _carihesaplar.Result.cari_iptal = false;
                _carihesaplar.Result.cari_fileid = 31;
                _carihesaplar.Result.cari_hidden = false;
                _carihesaplar.Result.cari_kilitli = false;
                _carihesaplar.Result.cari_degisti = false;
                _carihesaplar.Result.cari_checksum = 0;
                _carihesaplar.Result.cari_create_user = 1;//mikro istifadecisi
                _carihesaplar.Result.cari_lastup_user = 1;//mikro istifadecisi
                _carihesaplar.Result.cari_special1 = "";
                _carihesaplar.Result.cari_special2 = "";
                _carihesaplar.Result.cari_special3 = "";
                _carihesaplar.Result.cari_kod = data.cari_kod;
                _carihesaplar.Result.cari_unvan1 = data.cari_unvan1;
                _carihesaplar.Result.cari_unvan2 = data.cari_unvan2;
                _carihesaplar.Result.cari_hareket_tipi = 1;
                _carihesaplar.Result.cari_baglanti_tipi = 0;
                _carihesaplar.Result.cari_stok_alim_cinsi = 2;
                _carihesaplar.Result.cari_stok_satim_cinsi = 2;
                _carihesaplar.Result.cari_muh_kod = "";
                _carihesaplar.Result.cari_muh_kod1 = "";
                _carihesaplar.Result.cari_muh_kod2 = "";
                _carihesaplar.Result.cari_doviz_cinsi = 0;
                _carihesaplar.Result.cari_doviz_cinsi1 = 255;
                _carihesaplar.Result.cari_doviz_cinsi2 = 255;
                _carihesaplar.Result.cari_vade_fark_yuz = 25;
                _carihesaplar.Result.cari_vade_fark_yuz1 = 0;
                _carihesaplar.Result.cari_vade_fark_yuz2 = 0;
                _carihesaplar.Result.cari_KurHesapSekli = 1;
                _carihesaplar.Result.cari_vdaire_adi = "";
                _carihesaplar.Result.cari_vdaire_no = "";
                _carihesaplar.Result.cari_sicil_no = "";
                _carihesaplar.Result.cari_VergiKimlikNo = "";
                _carihesaplar.Result.cari_satis_fk = 1;
                _carihesaplar.Result.cari_odeme_cinsi = 0;
                _carihesaplar.Result.cari_odeme_gunu = 0;
                _carihesaplar.Result.cari_odemeplan_no = 0;
                _carihesaplar.Result.cari_opsiyon_gun = 0;
                _carihesaplar.Result.cari_cariodemetercihi = 0;
                _carihesaplar.Result.cari_fatura_adres_no = 1;
                _carihesaplar.Result.cari_sevk_adres_no = 1;
                _carihesaplar.Result.cari_EftHesapNum = 1;
                _carihesaplar.Result.cari_Ana_cari_kodu = "";
                _carihesaplar.Result.cari_satis_isk_kod = "";
                _carihesaplar.Result.cari_sektor_kodu = data.cari_sektor_kodu;
                _carihesaplar.Result.cari_bolge_kodu = data.cari_bolge_kodu;
                _carihesaplar.Result.cari_grup_kodu = data.cari_grup_kodu;
                _carihesaplar.Result.cari_temsilci_kodu = data.cari_temsilci_kodu;
                _carihesaplar.Result.cari_muhartikeli = "";
                _carihesaplar.Result.cari_firma_acik_kapal = false;
                _carihesaplar.Result.cari_BUV_tabi_fl = false;
                _carihesaplar.Result.cari_cari_kilitli_flg = false;
                _carihesaplar.Result.cari_etiket_bas_fl = false;
                _carihesaplar.Result.cari_Detay_incele_flg = false;
                _carihesaplar.Result.cari_efatura_fl = false;
                _carihesaplar.Result.cari_POS_ongpesyuzde = 0;
                _carihesaplar.Result.cari_POS_ongtaksayi = 0;
                _carihesaplar.Result.cari_POS_ongIskOran = 0;
                _carihesaplar.Result.cari_kaydagiristarihi = DateTime.Today;
                _carihesaplar.Result.cari_KabEdFCekTutar = 0;
                _carihesaplar.Result.cari_hal_caritip = 0;
                _carihesaplar.Result.cari_HalKomYuzdesi = 0;
                _carihesaplar.Result.cari_TeslimSuresi = 0;
                _carihesaplar.Result.cari_CepTel = RemoveCharacters.TelephoneClean(data.cari_CepTel);
                _carihesaplar.Result.cari_VarsayilanCikisDepo = 0;
                _carihesaplar.Result.cari_VarsayilanCikisDepo = 0;
                _carihesaplar.Result.cari_Portal_Enabled = false;
                _carihesaplar.Result.cari_Portal_PW = "";
                _carihesaplar.Result.cari_BagliOrtaklisa_Firma = 0;
                _carihesaplar.Result.cari_kampanyakodu = "";
                _carihesaplar.Result.cari_b_bakiye_degerlendirilmesin_fl = false;
                _carihesaplar.Result.cari_a_bakiye_degerlendirilmesin_fl = false;
                _carihesaplar.Result.cari_b_irsbakiye_degerlendirilmesin_fl = false;
                _carihesaplar.Result.cari_a_irsbakiye_degerlendirilmesin_fl = false;
                _carihesaplar.Result.cari_AvmBilgileri1TebligatSekli = 0;
                _carihesaplar.Result.cari_AvmBilgileri2TebligatSekli = 0;
                _carihesaplar.Result.cari_AvmBilgileri3TebligatSekli = 0;
                _carihesaplar.Result.cari_AvmBilgileri4TebligatSekli = 0;
                _carihesaplar.Result.cari_AvmBilgileri5TebligatSekli = 0;
                _carihesaplar.Result.cari_AvmBilgileri6TebligatSekli = 0;
                _carihesaplar.Result.cari_AvmBilgileri7TebligatSekli = 0;
                _carihesaplar.Result.cari_AvmBilgileri8TebligatSekli = 0;
                _carihesaplar.Result.cari_AvmBilgileri9TebligatSekli = 0;
                _carihesaplar.Result.cari_AvmBilgileri10TebligatSekli = 0;
                _carihesaplar.Result.cari_KrediRiskTakibiVar_flg = false;
                _carihesaplar.Result.cari_odeme_sekli = 0;
                _carihesaplar.Result.cari_TeminatMekAlacakMuhKodu = "910";
                _carihesaplar.Result.cari_TeminatMekAlacakMuhKodu1 = "";
                _carihesaplar.Result.cari_TeminatMekAlacakMuhKodu2 = "";
                _carihesaplar.Result.cari_TeminatMekBorcMuhKodu = "912";
                _carihesaplar.Result.cari_TeminatMekBorcMuhKodu1 = "";
                _carihesaplar.Result.cari_TeminatMekBorcMuhKodu2 = "";
                _carihesaplar.Result.cari_VerilenDepozitoTeminatMuhKodu = "226";
                _carihesaplar.Result.cari_VerilenDepozitoTeminatMuhKodu1 = "";
                _carihesaplar.Result.cari_VerilenDepozitoTeminatMuhKodu2 = "";
                _carihesaplar.Result.cari_AlinanDepozitoTeminatMuhKodu = "326";
                _carihesaplar.Result.cari_AlinanDepozitoTeminatMuhKodu1 = "";
                _carihesaplar.Result.cari_AlinanDepozitoTeminatMuhKodu2 = "";
                _carihesaplar.Result.cari_def_efatura_cinsi = 0;
                _carihesaplar.Result.cari_otv_tevkifatina_tabii_fl = false;
                _carihesaplar.Result.cari_efatura_baslangic_tarihi = Convert.ToDateTime("1899-12-31 00:00:00");
                _carihesaplar.Result.cari_gonderionayi_sms = false;
                _carihesaplar.Result.cari_gonderionayi_email = false;
                _carihesaplar.Result.cari_eirsaliye_fl = false;
                _carihesaplar.Result.cari_eirsaliye_baslangic_tarihi = Convert.ToDateTime("1899-12-31 00:00:00");
                _carihesaplar.Result.cari_CRM_sistemine_aktar_fl = false;
                _carihesaplar.Result.cari_kisi_kimlik_bilgisi_aciklama_turu = 0;
                _carihesaplar.Result.cari_kamu_kurumu_fl = false;
                _carihesaplar.Result.cari_Perakende_fl = false;
                _carihesaplar.Result.cari_wwwadresi = "";
                _carihesaplar.Result.cari_EMail = "";
                _carihesaplar.Result.cari_VarsayilanGirisDepo = 0;
                _carihesaplar.Result.cari_b_sipbakiye_degerlendirilmesin_fl = false;
                _carihesaplar.Result.cari_a_sipbakiye_degerlendirilmesin_fl = false;
                _carihesaplar.Result.cari_AvmBilgileri1KiraKodu = "";
                _carihesaplar.Result.cari_AvmBilgileri2KiraKodu = "";
                _carihesaplar.Result.cari_AvmBilgileri3KiraKodu = "";
                _carihesaplar.Result.cari_AvmBilgileri4KiraKodu = "";
                _carihesaplar.Result.cari_AvmBilgileri5KiraKodu = "";
                _carihesaplar.Result.cari_AvmBilgileri6KiraKodu = "";
                _carihesaplar.Result.cari_AvmBilgileri7KiraKodu = "";
                _carihesaplar.Result.cari_AvmBilgileri8KiraKodu = "";
                _carihesaplar.Result.cari_AvmBilgileri9KiraKodu = "";
                _carihesaplar.Result.cari_AvmBilgileri10KiraKodu = "";
                _carihesaplar.Result.cari_ufrs_fark_muh_kod = "";
                _carihesaplar.Result.cari_ufrs_fark_muh_kod1 = "";
                _carihesaplar.Result.cari_ufrs_fark_muh_kod2 = "";
                _carihesaplar.Result.cari_KEP_adresi = "";
                _carihesaplar.Result.cari_mutabakat_mail_adresi = "";
                _carihesaplar.Result.cari_mersis_no = data.cari_mersis_no;
                _carihesaplar.Result.cari_istasyon_cari_kodu = "";
                _carihesaplar.Result.cari_vergidairekodu = "";
                _carihesaplar.Result.cari_efatura_xslt_dosya = "";
                _carihesaplar.Result.cari_uts_kurum_no = "";
                _carihesaplar.Result.cari_earsiv_xslt_dosya = "";
                _carihesaplar.Result.cari_banka_tcmb_ilkod1 = "";
                _carihesaplar.Result.cari_banka_swiftkodu1 = "";
                _carihesaplar.Result.cari_banka_tcmb_subekod1 = "";
                _carihesaplar.Result.cari_banka_tcmb_kod1 = "";
                _carihesaplar.Result.cari_banka_hesapno1 = "";
                _carihesaplar.Result.cari_banka_tcmb_ilkod2 = "";
                _carihesaplar.Result.cari_banka_swiftkodu2 = "";
                _carihesaplar.Result.cari_banka_tcmb_subekod2 = "";
                _carihesaplar.Result.cari_banka_tcmb_kod2 = "";
                _carihesaplar.Result.cari_banka_hesapno2 = "";
                _carihesaplar.Result.cari_banka_tcmb_ilkod3 = "";
                _carihesaplar.Result.cari_banka_swiftkodu3 = "";
                _carihesaplar.Result.cari_banka_tcmb_subekod3 = "";
                _carihesaplar.Result.cari_banka_tcmb_kod3 = "";
                _carihesaplar.Result.cari_banka_hesapno3 = "";
                _carihesaplar.Result.cari_banka_tcmb_ilkod4 = "";
                _carihesaplar.Result.cari_banka_swiftkodu4 = "";
                _carihesaplar.Result.cari_banka_tcmb_subekod4 = "";
                _carihesaplar.Result.cari_banka_tcmb_kod4 = "";
                _carihesaplar.Result.cari_banka_hesapno4 = "";
                _carihesaplar.Result.cari_banka_tcmb_ilkod5 = "";
                _carihesaplar.Result.cari_banka_swiftkodu5 = "";
                _carihesaplar.Result.cari_banka_tcmb_subekod5 = "";
                _carihesaplar.Result.cari_banka_tcmb_kod5 = "";
                _carihesaplar.Result.cari_banka_hesapno5 = "";
                _carihesaplar.Result.cari_banka_tcmb_ilkod6 = "";
                _carihesaplar.Result.cari_banka_swiftkodu6 = "";
                _carihesaplar.Result.cari_banka_tcmb_subekod6 = "";
                _carihesaplar.Result.cari_banka_tcmb_kod6 = "";
                _carihesaplar.Result.cari_banka_hesapno6 = "";
                _carihesaplar.Result.cari_banka_tcmb_ilkod7 = "";
                _carihesaplar.Result.cari_banka_swiftkodu7 = "";
                _carihesaplar.Result.cari_banka_tcmb_subekod7 = "";
                _carihesaplar.Result.cari_banka_tcmb_kod7 = "";
                _carihesaplar.Result.cari_banka_hesapno7 = "";
                _carihesaplar.Result.cari_banka_tcmb_ilkod8 = "";
                _carihesaplar.Result.cari_banka_swiftkodu8 = "";
                _carihesaplar.Result.cari_banka_tcmb_subekod8 = "";
                _carihesaplar.Result.cari_banka_tcmb_kod8 = "";
                _carihesaplar.Result.cari_banka_hesapno8 = "";
                _carihesaplar.Result.cari_banka_tcmb_ilkod9 = "";
                _carihesaplar.Result.cari_banka_swiftkodu9 = "";
                _carihesaplar.Result.cari_banka_tcmb_subekod9 = "";
                _carihesaplar.Result.cari_banka_tcmb_kod9 = "";
                _carihesaplar.Result.cari_banka_hesapno9 = "";
                _carihesaplar.Result.cari_banka_tcmb_ilkod10 = "";
                _carihesaplar.Result.cari_banka_swiftkodu10 = "";
                _carihesaplar.Result.cari_banka_tcmb_subekod10 = "";
                _carihesaplar.Result.cari_banka_tcmb_kod10 = "";
                _carihesaplar.Result.cari_banka_hesapno10 = "";

                if (base.Insert(_carihesaplar.Result) == 0)
                {
                    _carihesaplar.AddError(ErrorMessageCode.DataInsertError, "Xəta başverdi. Qeyd qebul edilmədi");
                }
            }
            return _carihesaplar;
        }
    }
}
