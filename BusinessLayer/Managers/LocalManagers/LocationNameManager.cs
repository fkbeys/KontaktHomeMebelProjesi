using BusinessLayer.QueryResult;
using DataAccessLayer;
using Entities.Messages;
using Entities.Model.LocalModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Managers.LocalManagers
{
    public class LocationNameManager
    {
        DatabaseContext db = new DatabaseContext();
        public BusinessLayerResult<LocationNames> InsertData(LocationNames data)
        {
            BusinessLayerResult<LocationNames> _locationNames = new BusinessLayerResult<LocationNames>();
            _locationNames.Result = db.LocationName.FirstOrDefault(x => x.Value.ToLower() == data.Value.ToLower() && x.SubGroupID == data.SubGroupID && x.GroupID == data.GroupID);
            if (_locationNames.Result==null)
            {
                _locationNames.Result = data;
                db.LocationName.Add(_locationNames.Result);
                if (db.SaveChanges() == 0)
                {
                    _locationNames.AddError(ErrorMessageCode.DataInsertError, "Xətabaşverdi. Qeyd qəbul edilmədi");
                }                
            }
            else
            {
                _locationNames.AddError(ErrorMessageCode.DataAlreadyExists, "Xəta başverdi. Qeyd mövcuddur");
            }
            return _locationNames;
        }
        public async Task<List<LocationNameList>> GetLocationNames()
        {            
            var locationnames = await (from a in db.LocationName
                                       from c in db.LocationGroup.Where(x => x.ID == a.GroupID).DefaultIfEmpty()
                                       from j in db.LocationSubGroup.Where(x => x.ID == a.SubGroupID)
                                       select new
                                       {
                                           id = a.ID,
                                           subGroupName = j.Value,
                                           groupName = c.Value,
                                           locationName = a.Value
                                       }).ToListAsync();

            List<LocationNameList> _locationNames = new List<LocationNameList>();
            foreach (var item in locationnames)
            {
                _locationNames.Add(new LocationNameList() { id = item.id, subgroupname = item.subGroupName, groupname = item.groupName,locationname=item.locationName });
            }
            return _locationNames;

        }
        public async Task<List<LocationNames>> GetLocationName()
        {
            List<LocationNames> _locationNames = await db.LocationName.ToListAsync();        
            return _locationNames;
        }
        public BusinessLayerResult<LocationNames> FindName(int? id)
        {
            BusinessLayerResult<LocationNames> _locationNames = new BusinessLayerResult<LocationNames>();
            _locationNames.Result = db.LocationName.FirstOrDefault(x => x.ID == id);
            if (_locationNames == null)
            {
                _locationNames.AddError(ErrorMessageCode.DataNotFound, "Xəta başverdi. Qeyd mövcud deyil");
            }

            return _locationNames;
        }
        public BusinessLayerResult<LocationNames> UpdateData(LocationNames data)
        {
            BusinessLayerResult<LocationNames> _locationNames = new BusinessLayerResult<LocationNames>();
            _locationNames.Result = db.LocationName.FirstOrDefault(x => x.ID == data.ID);
            if (_locationNames.Result != null)
            {
                _locationNames.Result.Value = data.Value;
                _locationNames.Result.SubGroupID = data.SubGroupID;
                _locationNames.Result.GroupID = data.GroupID;
                db.Entry(_locationNames.Result).State = EntityState.Modified;
                if (db.SaveChanges() == 0)
                {
                    _locationNames.AddError(ErrorMessageCode.DataInsertError, "Xəta başverdi. Qeyd yenilənmədi");
                }
            }
            else
            {
                _locationNames.AddError(ErrorMessageCode.DataAlreadyExists, "Xəta başverdi. Qeyd mövcud deyil");
            }
            return _locationNames;

        }
        public BusinessLayerResult<LocationNames> DeleteData(int id)
        {
            BusinessLayerResult<LocationNames> _locationNames = new BusinessLayerResult<LocationNames>();
            _locationNames.Result = db.LocationName.FirstOrDefault(x => x.ID == id);
            if (_locationNames.Result!=null)
            {
                db.LocationName.Remove(_locationNames.Result);
                if (db.SaveChanges()==0)
                {
                    _locationNames.AddError(ErrorMessageCode.DataUpdateError, "Xəta başverdi. Qeyd silinmədi");
                }
            }
            else
            {
                _locationNames.AddError(ErrorMessageCode.DataNotFound, "Xəta baş verdi. Qeyd möcud deyil.");
            }
            return _locationNames;
        }
    }
}
