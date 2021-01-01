using BusinessLayer.QueryResult;
using Entities;
using Entities.Messages;
using Entities.Model.LocalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Providers.Entities;

namespace BusinessLayer.Managers.LocalManagers
{
    public class ProductionManager:ManagerBase<Production>
    {
        public BusinessLayerResult<Production> InsertData(List<Production> data,Users user,double cem,double yekun)
        {

            BusinessLayerResult<Production> _production = new BusinessLayerResult<Production>();
            foreach (var item in data)
            {
                if (item.Id==0)
                {
                    item.CreateOn = DateTime.Now;
                    item.CreateUser = user.UserName;
                    item.LastupDate = DateTime.Now;
                    item.LastupUser = user.UserName;
                    item.DocSum = cem;
                    item.DocTotal = yekun;

                    if (base.Insert(item) == 0)
                    {
                        _production.AddError(ErrorMessageCode.DataInsertError, "Xəta başverdi.Qeyd Tamamlanmadı.");
                    }
                }
            }
            return _production;
        }
        public BusinessLayerResult<Production> UpdateData(List<Production> data, Users user, double cem, double yekun)
        {
            BusinessLayerResult<Production> _productionresult = new BusinessLayerResult<Production>();
            Production _production = new Production();
            foreach (var item in data)
            {
                if (item.Id!=0)
                {
                    _production = Find(x => x.Id == item.Id);
                    if (_production!=null)
                    {
                        _production.LastupDate = DateTime.Now;
                        _production.LastupUser = user.UserName;
                        _production.ProductCode = item.ProductCode;
                        _production.ProductName = item.ProductName;
                        _production.ProductPrice = item.ProductPrice;
                        _production.ProductQuantity = item.ProductQuantity;
                        _production.ProductTotal = item.ProductTotal;
                        _production.VisitId = item.VisitId;
                        _production.OrderId = item.OrderId;
                        _production.DocSum = cem;
                        _production.DocTotal = yekun;
                        _production.ProductType = item.ProductType;
                        if (base.Update(_production) == 0)
                        {
                            _productionresult.AddError(ErrorMessageCode.DataUpdateError, "Xəta baş verdi. Məlumat yenilənmədi");
                        }
                    }
                    else
                    {
                        _productionresult.AddError(ErrorMessageCode.DataNotFound, "Xəta baş verdi. Məlumat tapılmadı");
                    }
                }
            }
            return _productionresult;
        }

        public void DeleteProdData(int[] prodId)
        {
            Production _production = new Production();
            if (prodId!=null)
            {
                foreach (var item in prodId)
                {
                    _production = Find(x => x.Id == item);
                    if (_production != null)
                    {
                        base.Delete(_production);
                    }
                }
            }
        }


        public void DeleteData(int visitid)
        {
            List<Production> _production = ListQueryable().Where(x => x.VisitId == visitid).ToList();
            if (_production.Count>0)
            {
                foreach (var item in _production)
                {
                    base.Delete(item);
                }
            }
        }
    }
}
