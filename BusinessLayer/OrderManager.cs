using BusinessLayer.QueryResult;
using DataAccessLayer;
using Entities;
using Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class OrderManager:ManagerBase<Orders>
    {
        public BusinessLayerResult<Orders> SaveOrder(Orders data)
        {
            BusinessLayerResult<Orders> orders = new BusinessLayerResult<Orders>();
            orders.Result = data;
            if (base.Insert(orders.Result)==0)
            {
                orders.AddError(ErrorMessageCode.UserCouldNotInserted, "Xəta başverdi.Qeyd Tamamlanmadı.");
            }
            return orders;
        }
    }
}
