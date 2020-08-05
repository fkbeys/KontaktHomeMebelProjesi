using BusinessLayer.QueryResult;
using Entities;
using Entities.Messages;
using Entities.Model;
using Entities.Model.LocalModels;
using Entities.Model.MikroModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Managers.MikroManagers
{
    public class UrunReceteleriManager:ManagerBaseMikro<URUN_RECETELERI>
    {
        public BusinessLayerResult<URUN_RECETELERI> InsertData(List<Production> data,string productCode, Users user)
        {
            BusinessLayerResult<URUN_RECETELERI> _urunreceteleri = new BusinessLayerResult<URUN_RECETELERI>();
            List<URUN_RECETELERI> _recete = List(x => x.rec_anakod == productCode);
            if (_recete.Count>0)
            {
                _urunreceteleri.AddError(ErrorMessageCode.DataAlreadyExists, "Xəta baş verdi. Məhsul istehsal resepti mövcuddur. Qeyd qəbul edilmədi.");
            }
            else
            {
                if (data.Count>0)
                {                                       
                    for (int i = 0; i < data.Count; i++)                   
                    {
                        Guid _guid = Guid.NewGuid();
                        DateTime tarix = DateTime.Now;
                        _urunreceteleri.Result = new URUN_RECETELERI();
                        _urunreceteleri.Result.rec_Guid = _guid;
                        _urunreceteleri.Result.rec_DBCno = 0;
                        _urunreceteleri.Result.rec_SpecRECno = 0;
                        _urunreceteleri.Result.rec_iptal = false;
                        _urunreceteleri.Result.rec_fileid = 18;
                        _urunreceteleri.Result.rec_hidden = false;
                        _urunreceteleri.Result.rec_kilitli = false;
                        _urunreceteleri.Result.rec_degisti = false;
                        _urunreceteleri.Result.rec_checksum = 0;
                        _urunreceteleri.Result.rec_create_user = 1;
                        _urunreceteleri.Result.rec_create_date = tarix;
                        _urunreceteleri.Result.rec_lastup_user = 1;
                        _urunreceteleri.Result.rec_lastup_date = tarix;
                        _urunreceteleri.Result.rec_special1 = "";
                        _urunreceteleri.Result.rec_special2 = "";
                        _urunreceteleri.Result.rec_special3 = "";
                        _urunreceteleri.Result.rec_anatipi = 0;
                        _urunreceteleri.Result.rec_anakod = productCode;
                        _urunreceteleri.Result.rec_tanimkod = "";
                        _urunreceteleri.Result.rec_cinsi = 0;
                        _urunreceteleri.Result.rec_tarih = DateTime.Today;
                        _urunreceteleri.Result.rec_aciklama = "";
                        _urunreceteleri.Result.rec_anabirim = 1;
                        _urunreceteleri.Result.rec_anamiktar = 1;
                        _urunreceteleri.Result.rec_tuketim_tur =0;
                        _urunreceteleri.Result.rec_tuketim_kod = data[i].ProductCode;
                        _urunreceteleri.Result.rec_tuketim_tanim_kodu = "";
                        _urunreceteleri.Result.rec_tuketim_recete_cinsi = 0;
                        _urunreceteleri.Result.rec_tuketim_miktar = data[i].ProductQuantity;
                        _urunreceteleri.Result.rec_tuketim_birim = 1;
                        _urunreceteleri.Result.rec_uretim_tuketim = 0;
                        _urunreceteleri.Result.rec_satirno = i;
                        _urunreceteleri.Result.rec_satir_acik = "";
                        _urunreceteleri.Result.rec_depono = 1;
                        _urunreceteleri.Result.rec_fireyuzde = 0;
                        _urunreceteleri.Result.rec_baslama_tarihi = Convert.ToDateTime("1899-12-30 00:00:00.000");
                        _urunreceteleri.Result.rec_bitis_tarihi = Convert.ToDateTime("1899-12-30 00:00:00.000");
                        _urunreceteleri.Result.rec_alt_tukkod1 = "";
                        _urunreceteleri.Result.rec_alt_1_katsayi = 0;
                        _urunreceteleri.Result.rec_alt_tukkod2 = "";
                        _urunreceteleri.Result.rec_alt_2_katsayi = 0;
                        _urunreceteleri.Result.rec_alt_tukkod3 = "";
                        _urunreceteleri.Result.rec_alt_3_katsayi = 0;
                        _urunreceteleri.Result.rec_alt_tukkod4 = "";
                        _urunreceteleri.Result.rec_alt_4_katsayi = 0;
                        _urunreceteleri.Result.rec_alt_tukkod5 = "";
                        _urunreceteleri.Result.rec_alt_5_katsayi = 0;
                        _urunreceteleri.Result.rec_safha_no = 0;
                        _urunreceteleri.Result.rec_safha_turu = 0;
                        _urunreceteleri.Result.rec_ana_renk_no = 0;
                        _urunreceteleri.Result.rec_ana_beden_no = 0;
                        _urunreceteleri.Result.rec_tuketim_renk_no = 0;
                        _urunreceteleri.Result.rec_tuketim_beden_no = 0;
                        _urunreceteleri.Result.rec_PlanlamaTipi = 0;
                        _urunreceteleri.Result.rec_eklenme_sarti = 0;
                        _urunreceteleri.Result.rec_miktar_fonksiyon_adi = "";

                        if (base.Insert(_urunreceteleri.Result)==0)
                        {
                            _urunreceteleri.AddError(ErrorMessageCode.DataInsertError, "Xəəta baş verdi. Məhsul istehsal resepti qeyd edilmədi.");
                        }
                    }
                }
            }
            return _urunreceteleri;
        }
    }
}
