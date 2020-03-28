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
        public BusinessLayerResult<Visits> SaveVisit(List<Visits> data,int orderId,Users user)
        {
            BusinessLayerResult<Visits> visitdata = new BusinessLayerResult<Visits>();
            visitdata.Result = new Visits();
            Guid guid=Guid.NewGuid();           
            bool hasData = false;
            
            foreach (var item in data)
            {
                if (item.VisitID>0)
                {
                    guid = item.VisitGuid;
                    hasData = true;
                    break;
                }
                
            }

            if (hasData == false)
            {
                DateTime tarix = DateTime.Now;                
                foreach (var item in data)
                {
                    item.CreateOn = tarix;
                    item.CreateUser = user.UserName;
                    item.LastUpdate = tarix;
                    item.UpdateUser = user.UserName;
                    item.VisitGuid = guid;
                    item.OrderId = orderId;
                    if (base.Insert(item) == 0)
                    {
                        visitdata.AddError(ErrorMessageCode.DataInsertError, "Xəta başverdi.Qeyd Tamamlanmadı.");
                    }
                }
            }
            else
            {
                DateTime tarix = DateTime.Now;
                foreach (var item in data)
                {
                    if (item.VisitID==0)
                    {
                        item.CreateOn = tarix;
                        item.CreateUser = user.UserName;
                        item.LastUpdate = tarix;
                        item.UpdateUser = user.UserName;
                        item.VisitGuid = guid;
                        item.OrderId = orderId;
                        if (base.Insert(item) == 0)
                        {
                            visitdata.AddError(ErrorMessageCode.DataInsertError, "Xəta başverdi.Qeyd Tamamlanmadı.");
                        }
                    }
                    else
                    {
                        item.LastUpdate = tarix;
                        item.UpdateUser = user.UserName;
                        visitdata.Result = Find(x => x.VisitID == item.VisitID);
                        if (visitdata.Result!=null)
                        {
                            visitdata.Result.VisitGuid = item.VisitGuid;
                            visitdata.Result.UpdateUser = item.UpdateUser;
                            visitdata.Result.ProductName = item.ProductName;
                            visitdata.Result.ProductCode = item.ProductCode;
                            visitdata.Result.PanelType = item.PanelType;
                            visitdata.Result.PanelColour = item.PanelColour;
                            visitdata.Result.OrderId = item.OrderId;
                            visitdata.Result.Note = item.Note;
                            visitdata.Result.Mirror = item.Mirror;
                            visitdata.Result.MaterialType = item.MaterialType;
                            visitdata.Result.MaterialColour = item.MaterialColour;
                            visitdata.Result.LastUpdate = item.LastUpdate;
                            visitdata.Result.DWidth = item.DWidth;
                            visitdata.Result.DoorType = item.DoorType;
                            visitdata.Result.DoorColour = item.DoorColour;
                            visitdata.Result.DLenght = item.DLenght;
                            visitdata.Result.DHeight = item.DHeight;
                            visitdata.Result.CreateUser = item.CreateUser;
                            visitdata.Result.CreateOn = item.CreateOn;
                            visitdata.Result.Accessory = item.Accessory;
                            
                        }
                       
                       
                        if (base.Update(visitdata.Result)== 0)
                        {
                            visitdata.AddError(ErrorMessageCode.DataUpdateError, "Xəta başverdi. Qeyd Yenilənmədi");
                        }
                    }
                }
            }
            visitdata.Result.VisitGuid = guid;
            return visitdata;
        }
    }
}
