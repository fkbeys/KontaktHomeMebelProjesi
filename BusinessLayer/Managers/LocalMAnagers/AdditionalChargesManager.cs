using BusinessLayer.QueryResult;
using Entities.Messages;
using Entities.Model.LocalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Managers.LocalManagers
{
    public class AdditionalChargesManager:ManagerBase<AdditionalCharges>
    {
        public BusinessLayerResult<AdditionalCharges> InsertData(AdditionalCharges data)
        {
            BusinessLayerResult<AdditionalCharges> _charges = new BusinessLayerResult<AdditionalCharges>();
            _charges.Result = Find(x => x.charge_name == data.charge_name);
            if (_charges.Result==null)
            {
                _charges.Result = data;
                if (base.Insert(_charges.Result)==0)
                {
                    _charges.AddError(ErrorMessageCode.DataInsertError, "Xəta baş verdi. Qeyd qəbul edilmədi");
                }
            }
            else
            {
                _charges.AddError(ErrorMessageCode.DataAlreadyExists, "Xəta baş verdi. Qeyd mövcuddur");
            }
            return _charges;
        }
        public BusinessLayerResult<AdditionalCharges> UpdateData(AdditionalCharges data)
        {
            BusinessLayerResult<AdditionalCharges> _charges = new BusinessLayerResult<AdditionalCharges>();
            _charges.Result = Find(x => x.ID == data.ID);
            if (_charges.Result!=null)
            {
                _charges.Result.charge_name=data.charge_name;
                _charges.Result.charge_value = data.charge_value;
               
                if (base.Update(_charges.Result)==0)
                {
                    _charges.AddError(ErrorMessageCode.DataUpdateError, "Xəta baş verdi. Qeyd yenilənmədi.");
                } 
               
            }
            else
            {
                _charges.AddError(ErrorMessageCode.DataNotFound, "Xəta baş verdi. Qeyd mövcud deyil");
            }
            return _charges;
        }
        public BusinessLayerResult<AdditionalCharges> DeleteData(int? id)
        {
            BusinessLayerResult<AdditionalCharges> _charges = new BusinessLayerResult<AdditionalCharges>();
            _charges.Result = Find(x => x.ID == id);
            if (_charges.Result!=null)
            {
                if (base.Delete(_charges.Result)==0)
                {
                    _charges.AddError(ErrorMessageCode.DataUpdateError, "Xəta başverdi. Qeyd silinmədi");
                }
            }
            else
            {
                _charges.AddError(ErrorMessageCode.DataNotFound, "Xəta başverdi. Qeyd mövcud deyil");
            }
            return _charges;
        }
    }
}
