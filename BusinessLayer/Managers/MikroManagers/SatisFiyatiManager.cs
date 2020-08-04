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
    public class SatisFiyatiManager:ManagerBaseMikro<STOK_SATIS_FIYAT_LISTELERI>
    {
        public BusinessLayerResult<STOK_SATIS_FIYAT_LISTELERI> InsertData(STOK_SATIS_FIYAT_LISTELERI data, Users user)
        {
            BusinessLayerResult<STOK_SATIS_FIYAT_LISTELERI> _satisfiyati = new BusinessLayerResult<STOK_SATIS_FIYAT_LISTELERI>();
            _satisfiyati.Result = Find(x => x.sfiyat_stokkod == data.sfiyat_stokkod && x.sfiyat_listesirano == 1);
            if (_satisfiyati.Result!=null)
            {
                _satisfiyati.Result.sfiyat_fiyati = data.sfiyat_fiyati;
                _satisfiyati.Result.sfiyat_lastup_date = DateTime.Now;
                _satisfiyati.Result.sfiyat_lastup_user = 1;
                if (base.Update(_satisfiyati.Result)==0)
                {
                    _satisfiyati.AddError(ErrorMessageCode.DataUpdateError, "Xəta baş verdi. Qiymət yenilənmədi");
                }
            }
            else
            {
                Guid _guid = Guid.NewGuid();
                DateTime tarix = DateTime.Now;
                _satisfiyati.Result = data;
                _satisfiyati.Result.sfiyat_Guid = _guid;
                _satisfiyati.Result.sfiyat_DBCno = 0;
                _satisfiyati.Result.sfiyat_SpecRECno = 0;
                _satisfiyati.Result.sfiyat_iptal = false;
                _satisfiyati.Result.sfiyat_fileid = 228;
                _satisfiyati.Result.sfiyat_hidden = false;
                _satisfiyati.Result.sfiyat_kilitli = false;
                _satisfiyati.Result.sfiyat_degisti = false;
                _satisfiyati.Result.sfiyat_checksum = 0;
                _satisfiyati.Result.sfiyat_create_user =1;
                _satisfiyati.Result.sfiyat_create_date = tarix;
                _satisfiyati.Result.sfiyat_lastup_user = 1;
                _satisfiyati.Result.sfiyat_lastup_date = tarix;
                _satisfiyati.Result.sfiyat_special1 = "";
                _satisfiyati.Result.sfiyat_special2 = "";
                _satisfiyati.Result.sfiyat_special3 = "";               
                _satisfiyati.Result.sfiyat_listesirano = 1;
                _satisfiyati.Result.sfiyat_deposirano = 0;
                _satisfiyati.Result.sfiyat_odemeplan = 0;
                _satisfiyati.Result.sfiyat_birim_pntr = 1;
                _satisfiyati.Result.sfiyat_fiyati = 5;
                _satisfiyati.Result.sfiyat_doviz = 0;
                _satisfiyati.Result.sfiyat_iskontokod = "";
                _satisfiyati.Result.sfiyat_deg_nedeni = 0;
                _satisfiyati.Result.sfiyat_primyuzdesi = 0;
                _satisfiyati.Result.sfiyat_kampanyakod = "";

                if (base.Insert(_satisfiyati.Result)==0)
                {
                    _satisfiyati.AddError(ErrorMessageCode.DataUpdateError, "Xəta baş verdi. Qiymət qeyd edilmədi");
                }
            }
            return _satisfiyati;
        }
    }
}
