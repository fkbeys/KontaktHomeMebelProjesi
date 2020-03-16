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
    public class UserManager:ManagerBase<Users>
    {
        public BusinessLayerResult<Users> GetUserInformation(string userName)
        {
            BusinessLayerResult<Users> users = new BusinessLayerResult<Users>();
            users.Result = Find(x => x.UserName == userName);
            if (users.Result==null)
            {
                users.AddError(ErrorMessageCode.UserCouldNotFind, "Xəta başverdi.Istifadəçi Tamamlanmadı.");
            }           
            return users;
        }
    }
}
