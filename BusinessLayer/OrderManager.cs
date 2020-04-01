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
        public BusinessLayerResult<Orders> UpdateOrder(Orders data)
        {
            Orders getOrder = Find(x => x.OrderId == data.OrderId);           
            BusinessLayerResult<Orders> orders = new BusinessLayerResult<Orders>();
            if (getOrder!=null)
            {
                orders.Result = Find(x => x.OrderId == data.OrderId);
                orders.Result.LastUpdate = data.LastUpdate;
                orders.Result.UpdateUser = data.UpdateUser;
                orders.Result.Address = data.Address;
                orders.Result.CustomerFatherName = data.CustomerFatherName;
                orders.Result.CustomerName = data.CustomerName;
                orders.Result.CustomerSurname = data.CustomerSurname;
                orders.Result.Note = data.Note;
                orders.Result.OrderType1 = data.OrderType1;
                orders.Result.OrderType2 = data.OrderType2;
                orders.Result.OrderType3 = data.OrderType3;
                orders.Result.Price = data.Price;
                orders.Result.SellerCode = data.SellerCode;
                orders.Result.Tel1 = data.Tel1;
                orders.Result.Tel2 = data.Tel2;
                orders.Result.VisitDate = data.VisitDate;
                orders.Result.IsVisitorAdded = data.IsVisitorAdded;
                orders.Result.VisitorCode = data.VisitorCode;
                orders.Result.VisitorName = data.VisitorName;
                orders.Result.OrderStatus = data.OrderStatus;
                orders.Result.DesignerStatus = data.DesignerStatus;
                orders.Result.DesignerCode = data.DesignerCode;
                orders.Result.DesignerName = data.DesignerName;
                orders.Result.IsDesignerAdded = data.IsDesignerAdded;
                orders.Result.VisitorStatus = data.VisitorStatus;

                if (base.Update(orders.Result)==0)
                {
                    orders.AddError(ErrorMessageCode.DataUpdateError, "Xəta başverdi. Qeyd Yenilənmədi");
                }
            }
            else
            {
                orders.AddError(ErrorMessageCode.DataNotFound, "Xəta başverdi. Seçilən Qeyd tapılmadı");
            }
            return orders;         
           
        }
        public BusinessLayerResult<Orders> AcceptOrder(Orders data,int? status)
        {
            Orders getOrder = Find(x => x.OrderId == data.OrderId);
            BusinessLayerResult<Orders> orders = new BusinessLayerResult<Orders>();
            if (getOrder != null)
            {
                orders.Result = Find(x => x.OrderId == data.OrderId);               
                if (status==1)
                {                   
                    orders.Result.VisitorStatus = 2;
                }
                else if (status==2)
                {
                    orders.Result.DesignerStatus = 2;
                }
                
                if (base.Update(orders.Result) == 0)
                {
                    orders.AddError(ErrorMessageCode.DataUpdateError, "Xəta başverdi. Qeyd Yenilənmədi");
                }
            }
            else
            {
                orders.AddError(ErrorMessageCode.DataNotFound, "Xəta başverdi. Seçilən Qeyd tapılmadı");
            }
            return orders;

        }
        public BusinessLayerResult<Orders> CloseOrder(Orders data)
        {
            Orders getOrder = Find(x => x.OrderId == data.OrderId);
            BusinessLayerResult<Orders> orders = new BusinessLayerResult<Orders>();
            if (getOrder != null)
            {
                orders.Result = Find(x => x.OrderId == data.OrderId);
                orders.Result.LastUpdate = data.LastUpdate;
                orders.Result.UpdateUser = data.UpdateUser;
                orders.Result.IsActive = false;
                orders.Result.CloseReason = data.CloseReason;

                if (base.Update(orders.Result) == 0)
                {
                    orders.AddError(ErrorMessageCode.DataUpdateError, "Xəta başverdi. Sifariş bağlanmadı");
                }
            }
            else
            {
                orders.AddError(ErrorMessageCode.DataNotFound, "Xəta başverdi. Seçilən Qeyd tapılmadı");
            }
            return orders;

        }
    }
}
