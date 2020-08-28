using DataAccessLayer;
using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
