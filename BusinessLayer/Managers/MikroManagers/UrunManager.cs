using BusinessLayer.QueryResult;
using Entities;
using Entities.Messages;
using Entities.Model;
using Entities.Model.MikroModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Managers.MikroManagers
{
    public class UrunManager:ManagerBaseMikro<URUNLER>
    {
        public BusinessLayerResult<URUNLER> InsertData(URUNLER data, Users user)
        {
            BusinessLayerResult<URUNLER> _urunler = new BusinessLayerResult<URUNLER>();
            _urunler.Result = Find(x => x.uru_stok_kod == data.uru_stok_kod);
            if (_urunler.Result!=null)
            {
                //_urunler.AddError(ErrorMessageCode.DataAlreadyExists, "xəta baş verdi. Məhsul mövcuddur");
            }
            else
            {
                Guid _guid = Guid.NewGuid();
                DateTime tarix = DateTime.Now;
                _urunler.Result = data;
                _urunler.Result.uru_Guid = _guid;
                _urunler.Result.uru_DBCno = 0;
                _urunler.Result.uru_SpecRECno = 0;
                _urunler.Result.uru_iptal = false;
                _urunler.Result.uru_fileid = 63;
                _urunler.Result.uru_hidden =false;
                _urunler.Result.uru_kilitli = false;
                _urunler.Result.uru_degisti = false;
                _urunler.Result.uru_checksum =0;
                _urunler.Result.uru_create_user = 1;
                _urunler.Result.uru_create_date = tarix;
                _urunler.Result.uru_lastup_user =1;
                _urunler.Result.uru_lastup_date = tarix;
                _urunler.Result.uru_special1 = "";
                _urunler.Result.uru_special2 = "";
                _urunler.Result.uru_special3 = "";               
                _urunler.Result.uru_gider1 = 1;
                _urunler.Result.uru_gider2 = 1;
                _urunler.Result.uru_gider3 = 1;
                _urunler.Result.uru_gider4 = 1;
                _urunler.Result.uru_gider5 = 1;
                _urunler.Result.uru_tip = 0;
                _urunler.Result.uru_max_fire_yuz = 0;
                _urunler.Result.uru_min_fire_yuz = 0;
                _urunler.Result.uru_fasonmaliyet = 0;
                _urunler.Result.uru_partimiktari = 0;
                _urunler.Result.uru_isemriID = " ";
                _urunler.Result.uru_uretimortakcarpani = 1;
                _urunler.Result.uru_uretimsekli = 0;
                _urunler.Result.uru_formdosyaadi = "";
                _urunler.Result.uru_excdosyaadi = "";
                _urunler.Result.uru_ongorulendepo = 0;
                _urunler.Result.uru_gider6 = 1;
                _urunler.Result.uru_gider7 = 1;
                _urunler.Result.uru_gider8 = 1;
                _urunler.Result.uru_gider9 = 1;
                _urunler.Result.uru_gider10 = 1;
                _urunler.Result.uru_ozel_tipi = 0;
                _urunler.Result.uru_sabitmlyt_anauruncarpan = 0;
                _urunler.Result.uru_masterrecete = "";
                _urunler.Result.uru_SorumlulukMerkezi = "";
                _urunler.Result.uru_varsayilanismerkezi = "";
                _urunler.Result.uru_Muhgrup_kodu = "";
                _urunler.Result.uru_masterkalip_kodu = "";
                _urunler.Result.uru_recete_ismi1 = "";
                _urunler.Result.uru_recete_ismi2 = "";
                _urunler.Result.uru_recete_ismi3 = "";
                _urunler.Result.uru_recete_ismi4 = "";
                _urunler.Result.uru_recete_ismi5 = "";
                _urunler.Result.uru_varsayilan_recete_tanim_kodu = "";
                _urunler.Result.uru_varsayilan_recete_cins = 0;

                if (base.Insert(_urunler.Result)==0)
                {
                    _urunler.AddError(ErrorMessageCode.DataInsertError, "Xəta baş verdi. Məhsul qeyd edilmədi.");
                }


            }
            return _urunler;
        }
    }
}
