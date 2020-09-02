using Entities;
using Entities.Model;
using Entities.Model.MikroModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class AnaGrupManager:ManagerBaseMikro<STOK_ANA_GRUPLARI>
    {
        public List<STOK_ANA_GRUPLARI> listAnaqruplar()
        {
            List<STOK_ANA_GRUPLARI> anagruplar = new List<STOK_ANA_GRUPLARI>();
            anagruplar.Add(new STOK_ANA_GRUPLARI() { san_kod = "Metbex", san_isim = "Mətbəx" });
            anagruplar.Add(new STOK_ANA_GRUPLARI() { san_kod = "Ofis", san_isim = "Ofis Mebeli" });
            anagruplar.Add(new STOK_ANA_GRUPLARI() { san_kod = "Ev", san_isim = "Ev Mebeli" });
            return anagruplar;
        }
    }
}
