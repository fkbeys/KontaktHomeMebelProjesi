using BusinessLayer.QueryResult;
using Entities.Messages;
using Entities.Model.LocalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Managers
{
    public class StockInfoManager:ManagerBase<StockInfo>
    {
        public BusinessLayerResult<StockInfo> InsertData(StockInfo data)
        {
            BusinessLayerResult<StockInfo> _stockinfo = new BusinessLayerResult<StockInfo>();
            _stockinfo.Result = data;
            if (base.Insert(_stockinfo.Result)==0)
            {
                _stockinfo.AddError(ErrorMessageCode.DataInsertError, "Xəta baş verdi. Məlumat qeyd edilmədi");
            }
            return _stockinfo;
        }
    }
}
