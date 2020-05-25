using BusinessLayer.QueryResult;
using Entities;
using Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class StoresManager:ManagerBase<Stores>
    {
        public BusinessLayerResult<Stores> InsertStore(Stores data)
        {
            BusinessLayerResult<Stores> stores = new BusinessLayerResult<Stores>();
            stores.Result = Find(x => x.StoreCode == data.StoreCode);
            if (stores.Result != null)
            {
                stores.AddError(ErrorMessageCode.DataAlreadyExists, "Mağaza kodu mövcuddur.");
            }
            else
            {
                stores.Result = data;
                if (base.Insert(stores.Result)==0)
                {
                    stores.AddError(ErrorMessageCode.DataInsertError, "Mağaza kodu mövcuddur.");
                }
            }
            return stores;
        }
        public BusinessLayerResult<Stores> DeleteStore(int? storeid)
        {
            BusinessLayerResult<Stores> stores = new BusinessLayerResult<Stores>();
            if (storeid==null)
            {
                stores.AddError(ErrorMessageCode.DataNotFound, "Seçilən Mağaza kodu tapılmadı.");
            }
            else
            {
                stores.Result = Find(x => x.StoreID == storeid);
                if (stores.Result != null)
                {
                    if (Delete(stores.Result) == 0)
                    {
                        stores.AddError(ErrorMessageCode.UserCouldNotRemove, "Seçilən Mağaza silinmədi.");
                        return stores;
                    }
                }
                else
                {
                    stores.AddError(ErrorMessageCode.UserCouldNotFind, "Seçilən Mağaza kodu tapılmadı.");
                }

            }          
            return stores;
        }
    }
}
