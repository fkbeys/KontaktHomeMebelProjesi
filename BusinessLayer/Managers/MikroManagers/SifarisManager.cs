using BusinessLayer.QueryResult;
using Entities;
using Entities.Messages;
using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Managers.MikroManagers
{
    public class SifarisManager : ManagerBaseMikro<SIPARISLER>
    {
        public BusinessLayerResult<SIPARISLER> InsertData(string stokkod, double qiymet, string musteri, Users user)
        {
            BusinessLayerResult<SIPARISLER> _siparisler = new BusinessLayerResult<SIPARISLER>();
            _siparisler.Result = List(x => x.sip_stok_kod == stokkod && x.sip_musteri_kod == musteri).FirstOrDefault();
            if (_siparisler.Result == null)
            {
                var evraksira = List(x => x.sip_evrakno_seri == "").Select(c => c.sip_evrakno_sira).DefaultIfEmpty(0).Max();
                DateTime Tarix = DateTime.Now;
                _siparisler.Result = new SIPARISLER();
                _siparisler.Result.sip_Guid = Guid.NewGuid();
                _siparisler.Result.sip_DBCno = 0;
                _siparisler.Result.sip_SpecRECno = 0;
                _siparisler.Result.sip_iptal = false;
                _siparisler.Result.sip_fileid = 21;
                _siparisler.Result.sip_hidden = false;
                _siparisler.Result.sip_kilitli = false;
                _siparisler.Result.sip_degisti = false;
                _siparisler.Result.sip_checksum = 0;
                _siparisler.Result.sip_create_user = 1;
                _siparisler.Result.sip_create_date = Tarix;
                _siparisler.Result.sip_lastup_user = 1;
                _siparisler.Result.sip_lastup_date = Tarix;
                _siparisler.Result.sip_special1 = "";
                _siparisler.Result.sip_special2 = "";
                _siparisler.Result.sip_special3 = "";
                _siparisler.Result.sip_firmano = 0;
                _siparisler.Result.sip_subeno = 0;
                _siparisler.Result.sip_tarih = DateTime.Today;
                _siparisler.Result.sip_teslim_tarih = DateTime.Today;
                _siparisler.Result.sip_tip = 0;
                _siparisler.Result.sip_cins = 0;
                _siparisler.Result.sip_evrakno_seri = "";
                _siparisler.Result.sip_evrakno_sira = evraksira + 1;
                _siparisler.Result.sip_satirno = 0;
                _siparisler.Result.sip_belgeno = "";
                _siparisler.Result.sip_belge_tarih = DateTime.Today;
                _siparisler.Result.sip_satici_kod = "";
                _siparisler.Result.sip_musteri_kod = musteri;
                _siparisler.Result.sip_stok_kod = stokkod;
                _siparisler.Result.sip_b_fiyat = qiymet;
                _siparisler.Result.sip_miktar = 1;
                _siparisler.Result.sip_birim_pntr = 1;
                _siparisler.Result.sip_teslim_miktar = 0;
                _siparisler.Result.sip_tutar = qiymet;
                _siparisler.Result.sip_iskonto_1 = 0;
                _siparisler.Result.sip_iskonto_2 = 0;
                _siparisler.Result.sip_iskonto_3 = 0;
                _siparisler.Result.sip_iskonto_4 = 0;
                _siparisler.Result.sip_iskonto_5 = 0;
                _siparisler.Result.sip_iskonto_6 = 0;
                _siparisler.Result.sip_masraf_1 = 0;
                _siparisler.Result.sip_masraf_2 = 0;
                _siparisler.Result.sip_masraf_3 = 0;
                _siparisler.Result.sip_masraf_4 = 0;
                _siparisler.Result.sip_vergi_pntr = 0;
                _siparisler.Result.sip_vergi = 0;
                _siparisler.Result.sip_masvergi_pntr = 0;
                _siparisler.Result.sip_masvergi = 0;
                _siparisler.Result.sip_opno = 0;
                _siparisler.Result.sip_aciklama = "";
                _siparisler.Result.sip_aciklama2 = "";
                _siparisler.Result.sip_depono = 0;
                _siparisler.Result.sip_OnaylayanKulNo = 0;
                _siparisler.Result.sip_vergisiz_fl = false;
                _siparisler.Result.sip_kapat_fl = false;
                _siparisler.Result.sip_promosyon_fl = false;
                _siparisler.Result.sip_cari_sormerk = "";
                _siparisler.Result.sip_stok_sormerk = "";
                _siparisler.Result.sip_cari_grupno = 0;
                _siparisler.Result.sip_doviz_cinsi = 0;
                _siparisler.Result.sip_doviz_kuru = 1;
                _siparisler.Result.sip_alt_doviz_kuru = 1;
                _siparisler.Result.sip_adresno = 1;
                _siparisler.Result.sip_teslimturu = "";
                _siparisler.Result.sip_cagrilabilir_fl = false;
                _siparisler.Result.sip_prosip_uid = Guid.Empty;
                _siparisler.Result.sip_iskonto1 = 0;
                _siparisler.Result.sip_iskonto2 = 1;
                _siparisler.Result.sip_iskonto3 = 1;
                _siparisler.Result.sip_iskonto4 = 1;
                _siparisler.Result.sip_iskonto5 = 1;
                _siparisler.Result.sip_iskonto6 = 1;
                _siparisler.Result.sip_masraf1 = 1;
                _siparisler.Result.sip_masraf2 = 1;
                _siparisler.Result.sip_masraf3 = 1;
                _siparisler.Result.sip_masraf4 = 1;
                _siparisler.Result.sip_isk1 = false;
                _siparisler.Result.sip_isk2 = false;
                _siparisler.Result.sip_isk3 = false;
                _siparisler.Result.sip_isk4 = false;
                _siparisler.Result.sip_isk5 = false;
                _siparisler.Result.sip_isk6 = false;
                _siparisler.Result.sip_mas1 = false;
                _siparisler.Result.sip_mas2 = false;
                _siparisler.Result.sip_mas3 = false;
                _siparisler.Result.sip_mas4 = false;
                _siparisler.Result.sip_Exp_Imp_Kodu = "";
                _siparisler.Result.sip_kar_orani = 0;
                _siparisler.Result.sip_durumu = 0;
                _siparisler.Result.sip_stal_uid = Guid.Empty;
                _siparisler.Result.sip_planlananmiktar = 0;
                _siparisler.Result.sip_teklif_uid = Guid.Empty;
                _siparisler.Result.sip_parti_kodu = "";
                _siparisler.Result.sip_lot_no = 0;
                _siparisler.Result.sip_projekodu = "";
                _siparisler.Result.sip_fiyat_liste_no = 1;
                _siparisler.Result.sip_Otv_Pntr = 0;
                _siparisler.Result.sip_Otv_Vergi = 0;
                _siparisler.Result.sip_otvtutari = 0;
                _siparisler.Result.sip_OtvVergisiz_Fl = 0;
                _siparisler.Result.sip_paket_kod = "";
                _siparisler.Result.sip_Rez_uid = Guid.Empty;
                _siparisler.Result.sip_harekettipi = 0;
                _siparisler.Result.sip_yetkili_uid = Guid.Empty;
                _siparisler.Result.sip_kapatmanedenkod = "";
                _siparisler.Result.sip_gecerlilik_tarihi = Convert.ToDateTime("1899-12-30 00:00:00.000");
                _siparisler.Result.sip_onodeme_evrak_tip = 0;
                _siparisler.Result.sip_onodeme_evrak_seri = "";
                _siparisler.Result.sip_onodeme_evrak_sira = 0;
                _siparisler.Result.sip_rezervasyon_miktari = 0;
                _siparisler.Result.sip_rezerveden_teslim_edilen = 0;
                _siparisler.Result.sip_HareketGrupKodu1 = "";
                _siparisler.Result.sip_HareketGrupKodu2 = "";
                _siparisler.Result.sip_HareketGrupKodu3 = "";
                _siparisler.Result.sip_Olcu1 = 0;
                _siparisler.Result.sip_Olcu2 = 0;
                _siparisler.Result.sip_Olcu3 = 0;
                _siparisler.Result.sip_Olcu4 = 0;
                _siparisler.Result.sip_Olcu5 = 0;
                _siparisler.Result.sip_FormulMiktarNo = 0;
                _siparisler.Result.sip_FormulMiktar = 0;

                if (base.Insert(_siparisler.Result) == 0)
                {
                    _siparisler.AddError(ErrorMessageCode.DataInsertError, "Sifariş qeyd edilmədi");
                }
            }
            return _siparisler;
        }
    }
}
