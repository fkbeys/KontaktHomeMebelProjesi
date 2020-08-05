using DataAccessLayer;
using Entities.Model;
using Entities.Model.MikroModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Managers.MikroManagers
{
    public class ProductsManager
    {
        DatabaseContextMikro db = new DatabaseContextMikro();
        public List<Products> GetProducts()
        {
            List<Products> _products = new List<Products>();
            _products =db.Products.SqlQuery("SELECT TOP (100) PERCENT dbo.STOKLAR.sto_kod AS product_code, dbo.STOKLAR.sto_isim AS product_name, dbo.fn_Stok_Son_Giris_Qiymeti(dbo.STOKLAR.sto_kod) AS product_price, dbo.fn_EldekiMiktar(dbo.STOKLAR.sto_kod) AS product_quantity FROM dbo.STOKLAR WITH(NOLOCK) WHERE dbo.STOKLAR.sto_cins in ('1','5') ORDER BY product_code").ToList();
            return _products;
        }
    }
}
