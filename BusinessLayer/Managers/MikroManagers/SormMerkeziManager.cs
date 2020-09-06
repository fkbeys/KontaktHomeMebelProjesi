using DataAccessLayer;
using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BusinessLayer.Managers.MikroManagers
{
    public class SormMerkeziManager
    {
        DatabaseContextMikro db = new DatabaseContextMikro();
        public List<SORUMLULUK_MERKEZLERI> GetData()
        {
            List<SORUMLULUK_MERKEZLERI> _sormmerkezi = db.SORUMLULUK_MERKEZLERI.ToList();
            return _sormmerkezi;
        }
        public List<SORUMLULUK_MERKEZLERI> GetData(string storeCode)
        {
            List<SORUMLULUK_MERKEZLERI> _sormmerkezi = db.SORUMLULUK_MERKEZLERI.ToList().Where(x => x.som_kod == storeCode).ToList();
            return _sormmerkezi;
        }
        public List<SelectListItem> listMagaza()
        {
            List<SORUMLULUK_MERKEZLERI> magazalar = GetData();
            var magaza = magazalar.Select(x => new SelectListItem { Value = x.som_kod, Text = x.som_isim }).ToList();
            return magaza;
        }
    }
}
