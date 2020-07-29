using Entities.Model.MikroModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Managers.MikroManagers
{
    public class ProductsManager:ManagerBaseMikro<Products>
    {
        public List<Products> GetProducts()
        {
            List<Products> _products = new List<Products>();
            _products = GetWithRawSql("SELECT TOP (100) PERCENT dbo.STOKLAR.sto_kod AS product_code, dbo.STOKLAR.sto_isim AS product_name, ISNULL(dbo.STOK_SATIS_FIYATLARI_F1_D0_VIEW.sfiyat_fiyati, 0) AS product_price, dbo.fn_EldekiMiktar(dbo.STOKLAR.sto_kod) AS product_quantity FROM dbo.STOKLAR WITH(NOLOCK) LEFT OUTER JOIN dbo.STOK_SATIS_FIYATLARI_F1_D0_VIEW ON dbo.STOK_SATIS_FIYATLARI_F1_D0_VIEW.sfiyat_stokkod = dbo.STOKLAR.sto_kod AND dbo.STOK_SATIS_FIYATLARI_F1_D0_VIEW.sfiyat_satirno = 1 ORDER BY product_code").ToList();
            return _products;
        }
    }
}
