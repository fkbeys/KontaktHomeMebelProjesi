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
    public class VisitManager : ManagerBase<Visits>
    {
        public BusinessLayerResult<Visits> VisitorInsert(List<Visits> data, int orderId, Users user)
        {
            BusinessLayerResult<Visits> _visits = new BusinessLayerResult<Visits>();
            Guid _guid = Guid.NewGuid();
            foreach (var item in data)
            {
                if (item.VisitID > 0)
                {
                    _guid = item.VisitGuid;
                    break;
                }
            }
            DateTime tarix = DateTime.Now;
            foreach (var item in data)
            {
                if (item.ProductCode != null)
                {
                    if (item.VisitID == 0)
                    {
                        item.CreateOn = tarix;
                        item.CreateUser = user.UserName;
                        item.LastUpdate = tarix;
                        item.UpdateUser = user.UserName;
                        item.VisitGuid = _guid;
                        item.OrderId = orderId;
                        item.Price = 0;
                        item.FinalPrice = 0;
                        item.VisitStatus = 0;
                        if (base.Insert(item) == 0)
                        {
                            _visits.AddError(ErrorMessageCode.DataInsertError, "Xəta başverdi.Qeyd Tamamlanmadı.");
                        }
                    }
                }
            }
            _visits.Result = new Visits();
            _visits.Result.VisitGuid = _guid;
            return _visits;
        }
        public BusinessLayerResult<Visits> VisitorUpdate(List<Visits> data, int orderId, Users user)
        {
            BusinessLayerResult<Visits> visitdata = new BusinessLayerResult<Visits>();
            foreach (var item in data)
            {
                if (item.ProductCode != null)
                {
                    if (item.VisitID > 0)
                    {
                        visitdata.Result = Find(x => x.VisitID == item.VisitID);
                        if (visitdata.Result != null)
                        {
                            visitdata.Result.UpdateUser = item.UpdateUser;
                            visitdata.Result.LastUpdate = DateTime.Now;
                            visitdata.Result.DWidth = item.DWidth;
                            visitdata.Result.DLenght = item.DLenght;
                            visitdata.Result.DHeight = item.DHeight;
                            visitdata.Result.Accessory = item.Accessory;
                            visitdata.Result.Mirror = item.Mirror;
                            visitdata.Result.Note = item.Note;
                            visitdata.Result.ProductCode = item.ProductCode;
                            visitdata.Result.ProductName = item.ProductName;
                            visitdata.Result.PanelType = item.PanelType;
                            visitdata.Result.PanelColour = item.PanelColour;
                            visitdata.Result.MaterialType = item.MaterialType;
                            visitdata.Result.MaterialColour = item.MaterialColour;
                            visitdata.Result.DoorType = item.DoorType;
                            visitdata.Result.DoorColour = item.DoorColour;
                        }
                        if (base.Update(visitdata.Result) == 0)
                        {
                            visitdata.AddError(ErrorMessageCode.DataUpdateError, "Xəta başverdi. Qeyd Yenilənmədi");
                        }
                    }
                }
            }
            return visitdata;
        }
        public BusinessLayerResult<Visits> VisitUpdateStatus(Visits data, Users user)
        {
            BusinessLayerResult<Visits> visitdata = new BusinessLayerResult<Visits>();
            if (data.ProductCode != null)
            {
                if (data.VisitID > 0)
                {
                    visitdata.Result = data;
                    visitdata.Result.UpdateUser = data.UpdateUser;
                    visitdata.Result.LastUpdate = DateTime.Now;
                    visitdata.Result.VisitStatus = 1;
                    if (base.Update(visitdata.Result) == 0)
                    {
                        visitdata.AddError(ErrorMessageCode.DataUpdateError, "Xəta başverdi. Qeyd Yenilənmədi");
                    }
                }
            }
            return visitdata;
        }
        //public BusinessLayerResult<Visits> SaveVisit(List<Visits> data, int orderId, Users user)
        //{
        //    BusinessLayerResult<Visits> visitdata = new BusinessLayerResult<Visits>();
        //    visitdata.Result = new Visits();
        //    Guid guid = Guid.NewGuid();
        //    bool hasData = false;

        //    foreach (var item in data)
        //    {
        //        if (item.VisitID > 0)
        //        {
        //            guid = item.VisitGuid;
        //            hasData = true;
        //            break;
        //        }
        //    }
        //    if (hasData == false)
        //    {
        //        DateTime tarix = DateTime.Now;
        //        foreach (var item in data)
        //        {
        //            if (item.ProductCode != null)
        //            {
        //                item.CreateOn = tarix;
        //                item.CreateUser = user.UserName;
        //                item.LastUpdate = tarix;
        //                item.UpdateUser = user.UserName;
        //                item.VisitGuid = guid;
        //                item.OrderId = orderId;
        //                item.Price = 0;
        //                item.FinalPrice = 0;
        //                if (base.Insert(item) == 0)
        //                {
        //                    visitdata.AddError(ErrorMessageCode.DataInsertError, "Xəta başverdi.Qeyd Tamamlanmadı.");
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        DateTime tarix = DateTime.Now;
        //        foreach (var item in data)
        //        {
        //            if (item.ProductCode != null)
        //            {
        //                if (item.VisitID == 0)
        //                {
        //                    item.CreateOn = tarix;
        //                    item.CreateUser = user.UserName;
        //                    item.LastUpdate = tarix;
        //                    item.UpdateUser = user.UserName;
        //                    item.VisitGuid = guid;
        //                    item.OrderId = orderId;
        //                    if (base.Insert(item) == 0)
        //                    {
        //                        visitdata.AddError(ErrorMessageCode.DataInsertError, "Xəta başverdi.Qeyd Tamamlanmadı.");
        //                    }
        //                }
        //                else
        //                {
        //                    item.LastUpdate = tarix;
        //                    item.UpdateUser = user.UserName;
        //                    visitdata.Result = Find(x => x.VisitID == item.VisitID);
        //                    if (item.IsDeclined == false)
        //                    {
        //                        item.FinalPrice = item.Price;
        //                    }
        //                    if (visitdata.Result != null)
        //                    {
        //                        visitdata.Result.VisitGuid = item.VisitGuid;
        //                        visitdata.Result.UpdateUser = item.UpdateUser;
        //                        visitdata.Result.ProductName = item.ProductName;
        //                        visitdata.Result.ProductCode = item.ProductCode;
        //                        visitdata.Result.PanelType = item.PanelType;
        //                        visitdata.Result.PanelColour = item.PanelColour;
        //                        visitdata.Result.OrderId = item.OrderId;
        //                        visitdata.Result.Note = item.Note;
        //                        visitdata.Result.Mirror = item.Mirror;
        //                        visitdata.Result.MaterialType = item.MaterialType;
        //                        visitdata.Result.MaterialColour = item.MaterialColour;
        //                        visitdata.Result.LastUpdate = item.LastUpdate;
        //                        visitdata.Result.DWidth = item.DWidth;
        //                        visitdata.Result.DoorType = item.DoorType;
        //                        visitdata.Result.DoorColour = item.DoorColour;
        //                        visitdata.Result.DLenght = item.DLenght;
        //                        visitdata.Result.DHeight = item.DHeight;
        //                        visitdata.Result.CreateUser = item.CreateUser;
        //                        visitdata.Result.CreateOn = item.CreateOn;
        //                        visitdata.Result.Accessory = item.Accessory;
        //                        visitdata.Result.Price = item.Price;
        //                        visitdata.Result.IsDeclined = item.IsDeclined;
        //                        visitdata.Result.DeclineReason = item.DeclineReason;
        //                        visitdata.Result.FinalPrice = item.FinalPrice;
        //                    }
        //                    if (base.Update(visitdata.Result) == 0)
        //                    {
        //                        visitdata.AddError(ErrorMessageCode.DataUpdateError, "Xəta başverdi. Qeyd Yenilənmədi");
        //                    }
        //                }
        //            }

        //        }
        //    }
        //    visitdata.Result.VisitGuid = guid;
        //    return visitdata;
        //}
        public BusinessLayerResult<Visits> UpdatePrice(int visitid, double price, Users user)
        {

            BusinessLayerResult<Visits> _visit = new BusinessLayerResult<Visits>();
            _visit.Result = Find(x => x.VisitID == visitid);
            if (_visit.Result != null)
            {
                _visit.Result.Price = price;
                _visit.Result.FinalPrice = price;
                _visit.Result.LastUpdate = DateTime.Now;
                _visit.Result.UpdateUser = user.UserName;

                if (base.Update(_visit.Result) == 0)
                {
                    _visit.AddError(ErrorMessageCode.DataUpdateError, "Xəta başverdi. Qeyd Yenilənmədi");
                }
            }
            else
            {
                _visit.AddError(ErrorMessageCode.DataNotFound, "Xəta başverdi. Qeyd Tapılmadı");
            }
            return _visit;
        }
        public BusinessLayerResult<Visits> DesignerUpdate(List<Visits> data, int orderId, Users user)
        {
            BusinessLayerResult<Visits> _visits = new BusinessLayerResult<Visits>();
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    _visits.Result = Find(x => x.VisitID == item.VisitID && x.OrderId == item.OrderId);
                    if (_visits.Result != null)
                    {
                        _visits.Result.Price = item.Price;
                        _visits.Result.FinalPrice = item.FinalPrice;
                        _visits.Result.IsDeclined = item.IsDeclined;
                        _visits.Result.DeclineReason = item.DeclineReason;
                        _visits.Result.LastUpdate = DateTime.Now;
                        _visits.Result.UpdateUser = user.UserName;

                        if (base.Update(_visits.Result) == 0)
                        {
                            _visits.AddError(ErrorMessageCode.DataUpdateError, "Xəta baş verdi.Məlumatlar yenilənmədi.");
                        }
                    }
                    else
                    {
                        _visits.AddError(ErrorMessageCode.DataNotFound, "Xəta baş verdi.Məlumatlar tapIlmadı.");
                    }
                }
            }
            else
            {
                _visits.AddError(ErrorMessageCode.DataNotFound, "Xəta baş verdi.Məlumatlar mövcud deyil.");
            }
            return _visits;
        }      
    }
}
