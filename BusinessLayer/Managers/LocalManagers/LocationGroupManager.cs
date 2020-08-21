using BusinessLayer.QueryResult;
using DataAccessLayer;
using Entities.Messages;
using Entities.Model.LocalModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Managers.LocalManagers
{
    public class LocationGroupManager
    {
        DatabaseContext db = new DatabaseContext();
        public BusinessLayerResult<LocationGroup> InsertData(LocationGroup data)
        {
            BusinessLayerResult<LocationGroup> _locationGroup = new BusinessLayerResult<LocationGroup>();
            _locationGroup.Result = db.LocationGroup.FirstOrDefault(x => x.Value.ToLower() == data.Value.ToLower());
            if (_locationGroup.Result == null)
            {
                _locationGroup.Result = data;
                db.LocationGroup.Add(_locationGroup.Result);
                if (db.SaveChanges() == 0)
                {
                    _locationGroup.AddError(ErrorMessageCode.DataInsertError, "Xəta başverdi. Qeyd qəbul edilmədi");
                }
            }
            else
            {
                _locationGroup.AddError(ErrorMessageCode.DataAlreadyExists, "Xəta başverdi. Qeyd mövcuddur");
            }
            return _locationGroup;

        }
        public async Task<List<LocationGroup>> GetGroups()
        {
            List<LocationGroup> _locationGroup = new List<LocationGroup>();
            _locationGroup = await db.LocationGroup.ToListAsync();
            return _locationGroup;
        }
        public BusinessLayerResult<LocationGroup> FindGroup(int? id)
        {
            BusinessLayerResult<LocationGroup> _locationGroup = new BusinessLayerResult<LocationGroup>();
            _locationGroup.Result = db.LocationGroup.FirstOrDefault(x => x.ID == id);
            if (_locationGroup == null)
            {
                _locationGroup.AddError(ErrorMessageCode.DataNotFound, "Xəta başverdi. Qeyd mövcud deyil");
            }
            
            return _locationGroup;
        }
        public BusinessLayerResult<LocationGroup> UpdateData(LocationGroup data)
        {
            BusinessLayerResult<LocationGroup> _locationGroup = new BusinessLayerResult<LocationGroup>();
            _locationGroup.Result = db.LocationGroup.FirstOrDefault(x => x.ID==data.ID);
            if (_locationGroup.Result != null)
            {
                _locationGroup.Result.Value=data.Value;                 
                db.Entry(_locationGroup.Result).State = EntityState.Modified;
                if (db.SaveChanges() == 0)
                {
                    _locationGroup.AddError(ErrorMessageCode.DataInsertError, "Xəta başverdi. Qeyd yenilənmədi");
                }
            }
            else
            {
                _locationGroup.AddError(ErrorMessageCode.DataAlreadyExists, "Xəta başverdi. Qeyd mövcud deyil");
            }
            return _locationGroup;

        }
    }
}
