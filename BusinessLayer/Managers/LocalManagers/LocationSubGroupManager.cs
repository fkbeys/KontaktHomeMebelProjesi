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
using System.Web.UI.WebControls;

namespace BusinessLayer.Managers.LocalManagers
{
    public class LocationSubGroupManager
    {
        DatabaseContext db = new DatabaseContext();
        public BusinessLayerResult<LocationSubGroup> InsertData(LocationSubGroup data)
        {
            BusinessLayerResult<LocationSubGroup> _locationsubGroup = new BusinessLayerResult<LocationSubGroup>();
            _locationsubGroup.Result =db.LocationSubGroup.FirstOrDefault(x => x.Value.ToLower() == data.Value.ToLower() && x.GroupID==data.GroupID);
            if (_locationsubGroup.Result == null)
            {
                _locationsubGroup.Result = data;
                db.LocationSubGroup.Add(_locationsubGroup.Result);
                if (db.SaveChanges() == 0)
                {
                    _locationsubGroup.AddError(ErrorMessageCode.DataInsertError, "Xəta başverdi. Qeyd qəbul edilmədi");
                }
            }
            else
            {
                _locationsubGroup.AddError(ErrorMessageCode.DataAlreadyExists, "Xəta başverdi. Qeyd mövcuddur");
            }
            return _locationsubGroup;
        }        
        public async Task<List<LocationSubGroupList>> GetSubGroups()
        {           
            var subgroups = await (from a in db.LocationSubGroup
                                 join c in db.LocationGroup on a.GroupID equals c.ID                                
                                 select new 
                                 {
                                     id = a.ID,
                                     subGroupName = a.Value,
                                     groupName = c.Value
                                 }).ToListAsync();

            List<LocationSubGroupList> _subgrouplist = new List<LocationSubGroupList>();
            foreach (var item in subgroups)
            {
                _subgrouplist.Add(new LocationSubGroupList() { id = item.id, subGroupName = item.subGroupName, groupName = item.groupName });
            }
            return _subgrouplist;
           
        }
        public async Task<List<LocationSubGroup>> GetSubGroup()
        {
            List<LocationSubGroup> _subgrouplist = await db.LocationSubGroup.ToListAsync();
            return _subgrouplist;
        }
        public List<LocationSubGroup> GetSubGroup(int? id)
        {
            List<LocationSubGroup> _locationSubgroup =db.LocationSubGroup.Where(x => x.GroupID == id).ToList();          
            return _locationSubgroup;
        }
        public BusinessLayerResult<LocationSubGroup> UpdateDate(LocationSubGroup data)
        {
            BusinessLayerResult<LocationSubGroup> _locationSubGroup = new BusinessLayerResult<LocationSubGroup>();
            _locationSubGroup.Result = db.LocationSubGroup.FirstOrDefault(x => x.ID == data.ID);
            if (_locationSubGroup.Result!=null)
            {
                _locationSubGroup.Result.Value = data.Value;
                _locationSubGroup.Result.GroupID = data.GroupID;
                db.Entry(_locationSubGroup.Result).State = EntityState.Modified;
                if (db.SaveChanges()==0)
                {
                    _locationSubGroup.AddError(ErrorMessageCode.DataInsertError, "Xəta başverdi. Qeyd yenilənmədi");
                }

            }
            else
            {
                _locationSubGroup.AddError(ErrorMessageCode.DataAlreadyExists, "Xəta başverdi. Qeyd mövcud deyil");
            }
            return _locationSubGroup;
        }
        public BusinessLayerResult<LocationSubGroup> FindGroup(int? id)
        {
            BusinessLayerResult<LocationSubGroup> _locationSubGroup = new BusinessLayerResult<LocationSubGroup>();
            _locationSubGroup.Result = db.LocationSubGroup.FirstOrDefault(x => x.ID == id);
            if (_locationSubGroup == null)
            {
                _locationSubGroup.AddError(ErrorMessageCode.DataNotFound, "Xəta başverdi. Qeyd mövcud deyil");
            }

            return _locationSubGroup;
        }
        public BusinessLayerResult<LocationSubGroup> DeleteData(int id)
        {
            BusinessLayerResult<LocationSubGroup> _locationSubGroup = new BusinessLayerResult<LocationSubGroup>();
            _locationSubGroup.Result = db.LocationSubGroup.FirstOrDefault(x => x.ID == id);
            if (_locationSubGroup.Result != null)
            {
                db.LocationSubGroup.Remove(_locationSubGroup.Result);
                if (db.SaveChanges() == 0)
                {
                    _locationSubGroup.AddError(ErrorMessageCode.DataUpdateError, "Xəta başverdi. Qeyd silinmədi");
                }
            }
            else
            {
                _locationSubGroup.AddError(ErrorMessageCode.DataNotFound, "Xəta baş verdi. Qeyd möcud deyil.");
            }
            return _locationSubGroup;
        }
    }
}
