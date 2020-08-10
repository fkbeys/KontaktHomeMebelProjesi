using BusinessLayer.QueryResult;
using DataAccessLayer;
using Entities;
using Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class OrderManager : ManagerBase<Orders>
    {       
        public BusinessLayerResult<Orders> SaveOrder(Orders data)
        {
            BusinessLayerResult<Orders> orders = new BusinessLayerResult<Orders>();
            orders.Result = data;
            if (base.Insert(orders.Result) == 0)
            {
                orders.AddError(ErrorMessageCode.UserCouldNotInserted, "Xəta başverdi.Qeyd Tamamlanmadı.");
            }
            return orders;
        }
        public BusinessLayerResult<Orders> UpdateOrder(Orders data)
        {            
            Orders getOrder = Find(x => x.OrderId == data.OrderId);
            BusinessLayerResult<Orders> orders = new BusinessLayerResult<Orders>();
            if (getOrder != null)
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
                orders.Result.Location = data.Location;
                orders.Result.CustomerWillAnswer = data.CustomerWillAnswer;
                orders.Result.IsCompleted = data.IsCompleted;
                orders.Result.InvoiceNo = data.InvoiceNo;
                if (data.IsCompleted == true)
                {
                    orders.Result.IsActive = false;
                    orders.Result.OrderStatus = 8;
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
        public BusinessLayerResult<Orders> AcceptOrder(Orders data, int? status)
        {
            Orders getOrder = Find(x => x.OrderId == data.OrderId);
            BusinessLayerResult<Orders> orders = new BusinessLayerResult<Orders>();
            if (getOrder != null)
            {
                // 1* vizitor, 2* designer
                orders.Result = Find(x => x.OrderId == data.OrderId);
                if (status == 12)
                {
                    orders.Result.VisitorStatus = 2;
                    orders.Result.OrderStatus = 3;
                }
                else if (status == 13)
                {
                    orders.Result.VisitorStatus = 3;
                }
                else if (status == 14)
                {
                    orders.Result.VisitorStatus = 4;
                    orders.Result.OrderStatus = 4;
                }
                else if (status == 22)
                {
                    orders.Result.DesignerStatus = 2;
                    orders.Result.OrderStatus = 6;
                }
                else if (status == 23)
                {
                    orders.Result.DesignerStatus = 3;
                }
                else if (status == 24)
                {
                    orders.Result.DesignerStatus = 4;
                    orders.Result.OrderStatus = 7;
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
                orders.Result = getOrder;
                orders.Result.LastUpdate = data.LastUpdate;
                orders.Result.UpdateUser = data.UpdateUser;
                orders.Result.IsActive = false;
                orders.Result.CloseReason = data.CloseReason;
                orders.Result.OrderStatus = data.OrderStatus;
                orders.Result.IsCompleted = data.IsCompleted;
              
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
        public BusinessLayerResult<Orders> FinishOrder(int orderid,Users user)
        {
            BusinessLayerResult<Orders> _orders = new BusinessLayerResult<Orders>();
            _orders.Result = Find(x => x.OrderId == orderid);
            if (_orders.Result!=null)
            {
                _orders.Result.LastUpdate = DateTime.Now;
                _orders.Result.IsActive = false;
                _orders.Result.OrderStatus = 8;
                _orders.Result.UpdateUser = user.UserName;
                if (base.Update(_orders.Result)==0)
                {
                    _orders.AddError(ErrorMessageCode.DataUpdateError, "Sifariş tamamlanmad;");
                }

            }
            else
            {
                _orders.AddError(ErrorMessageCode.DataNotFound, "Sifariş tapılmadı");
            }
            return _orders;
        }
        public BusinessLayerResult<Orders> ActivateOrder(int orderid, Users user)
        {
            BusinessLayerResult<Orders> _orders = new BusinessLayerResult<Orders>();
            _orders.Result = Find(x => x.OrderId == orderid);
            if (_orders.Result != null)
            {
                _orders.Result.LastUpdate = DateTime.Now;
                _orders.Result.IsActive = true;               
                _orders.Result.UpdateUser = user.UserName;
                if (base.Update(_orders.Result) == 0)
                {
                    _orders.AddError(ErrorMessageCode.DataUpdateError, "Sifariş aktiv edilmədi");
                }

            }
            else
            {
                _orders.AddError(ErrorMessageCode.DataNotFound, "Sifariş tapılmadı");
            }
            return _orders;
        }
        public IEnumerable<Orders> GetOrdersWithParametr(OrderSearch data, string sellerName)
        {
            DateTime startdate = Convert.ToDateTime(data.firstDate);
            DateTime enddate = Convert.ToDateTime(data.lastDate + " 23:59:59");
            IEnumerable<Orders> orders = new List<Orders>();
            if (sellerName == null)
            {
                if (data.activeOrders == true && data.deletedOrders == false)
                {
                    orders = ListQueryable().Where(x => x.CreateOn >= startdate && x.CreateOn <= enddate && x.IsActive == true).ToList();
                }
                if (data.deletedOrders == true && data.activeOrders == false)
                {
                    orders = ListQueryable().Where(x => x.CreateOn >= startdate && x.CreateOn <= enddate && x.IsActive == false).ToList();
                }
                if (data.deletedOrders == true && data.activeOrders == true)
                {
                    orders = ListQueryable().Where(x => x.CreateOn >= startdate && x.CreateOn <= enddate).ToList();
                }
                if (data.sellerCode != null)
                {
                    orders = orders.Where(x => x.SellerCode == data.sellerCode).ToList();
                }
                if (data.storeCode != null)
                {
                    orders = orders.Where(x => x.OrderStore == data.storeCode);
                }
            }
            else
            {
                if (data.activeOrders == true && data.deletedOrders == false)
                {
                    orders = ListQueryable().Where(x => x.CreateOn >= startdate && x.CreateOn <= enddate && x.IsActive == true && x.SellerCode == sellerName).ToList();
                }
                if (data.deletedOrders == true && data.activeOrders == false)
                {
                    orders = ListQueryable().Where(x => x.CreateOn >= startdate && x.CreateOn <= enddate && x.IsActive == false && x.SellerCode == sellerName).ToList();
                }
                if (data.deletedOrders == true && data.activeOrders == true)
                {
                    orders = ListQueryable().Where(x => x.CreateOn >= startdate && x.CreateOn <= enddate && x.SellerCode == sellerName).ToList();
                }
                if (data.sellerCode != null)
                {
                    orders = orders.Where(x => x.SellerCode == data.sellerCode).ToList();
                }
                if (data.storeCode != null)
                {
                    orders = orders.Where(x => x.OrderStore == data.storeCode);
                }
            }

            return orders;
        }
        public IEnumerable<Orders> GetVisitorOrdersWithParametr(OrderSearch data, string visitorName)
        {
            DateTime startdate = Convert.ToDateTime(data.firstDate);
            DateTime enddate = Convert.ToDateTime(data.lastDate + " 23:59:59");
            IEnumerable<Orders> orders = new List<Orders>();

            if (data.activeOrders == true && data.deletedOrders == false)
            {
                orders = ListQueryable().Where(x => x.CreateOn >= startdate & x.CreateOn <= enddate & x.VisitorCode == visitorName & x.IsActive == true).ToList();
            }
            if (data.deletedOrders == true && data.activeOrders == false)
            {
                orders = ListQueryable().Where(x => x.CreateOn >= startdate & x.CreateOn <= enddate & x.VisitorCode == visitorName & x.IsActive == false).ToList();
            }
            if (data.deletedOrders == true && data.activeOrders == true)
            {
                orders = ListQueryable().Where(x => x.CreateOn >= startdate & x.CreateOn <= enddate & x.VisitorCode == visitorName).ToList();
            }
            return orders;
        }
        public IEnumerable<Orders> GetDesignerOrdersWithParametr(OrderSearch data, string designerName)
        {
            DateTime startdate = Convert.ToDateTime(data.firstDate);
            DateTime enddate = Convert.ToDateTime(data.lastDate + " 23:59:59");
            IEnumerable<Orders> orders = new List<Orders>();

            if (data.activeOrders == true && data.deletedOrders == false)
            {
                orders = ListQueryable().Where(x => x.CreateOn >= startdate & x.CreateOn <= enddate & x.DesignerCode == designerName & x.IsActive == true).ToList();
            }
            if (data.deletedOrders == true && data.activeOrders == false)
            {
                orders = ListQueryable().Where(x => x.CreateOn >= startdate & x.CreateOn <= enddate & x.DesignerCode == designerName & x.IsActive == false).ToList();
            }
            if (data.deletedOrders == true && data.activeOrders == true)
            {
                orders = ListQueryable().Where(x => x.CreateOn >= startdate & x.CreateOn <= enddate & x.DesignerCode == designerName).ToList();
            }
            return orders;
        }
    }
}
