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
        public BusinessLayerResult<Users> LoginUser(LoginViewModel data)
        {
            BusinessLayerResult<Users> res = new BusinessLayerResult<Users>();

            res.Result = Find(x => x.UserName == data.Username);
            if (res.Result != null)
            {
                //var keyNew = res.Result.Code;
                //var password = PassHelper.EncodePassword(data.Password, keyNew);
                res.Result = Find(x => x.UserName == data.Username && x.UserPassword == data.Password);

                if (res.Result != null)
                {
                    if (!res.Result.IsActive)
                    {
                        res.AddError(ErrorMessageCode.UserIsNotActive, "İstifadəçi aktiv edilməyib.");
                    }
                }
                else
                {
                    res.AddError(ErrorMessageCode.UsernameOrPassWrong, "İstifadəçi adı və ya şifrə düzgün deyil.");
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UsernameOrPassWrong, "İstifadəçi adı və ya şifrə düzgün deyil.");
            }
            return res;
        }
    }
}
