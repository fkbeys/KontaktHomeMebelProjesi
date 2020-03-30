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
        public BusinessLayerResult<Users> InsertUser(Users data)
        {
            BusinessLayerResult<Users> newuser = new BusinessLayerResult<Users>();
            newuser.Result = Find(x => x.UserName == data.UserName);
            if (newuser.Result!=null)
            {
                newuser.AddError(ErrorMessageCode.UsernameAlreadyExists, "İstifadəçi kodu mövcuddur.");
            }
            else
            {
                newuser.Result = data;
                newuser.Result.IsActive = true;
                if (base.Insert(newuser.Result)==0)
                {
                    newuser.AddError(ErrorMessageCode.UserCouldNotInserted, "Xəta baş verdi. İstifadəçi qeyd edilmədi.");
                }
            }
            return newuser;
        }
        public BusinessLayerResult<Users> UpdateUser(Users data)
        {
            BusinessLayerResult<Users> user = new BusinessLayerResult<Users>();
            user.Result = Find(x => x.UserID == data.UserID);
            if (user.Result!=null)
            {
                user.Result.UserPassword = data.UserPassword;
                user.Result.UserDisplayName = data.UserDisplayName;
                user.Result.IsActive = data.IsActive;
                user.Result.IsAdmin = data.IsAdmin;
                user.Result.IsCord = data.IsCord;
                user.Result.IsSeller = data.IsSeller;
                user.Result.IsVisitor = data.IsVisitor;
                user.Result.StoreCode = data.StoreCode;
                user.Result.StoreName = data.StoreName;
                user.Result.IsDesigner = data.IsDesigner;

                if (base.Update(user.Result)==0)
                {
                    user.AddError(ErrorMessageCode.UserCouldNotUpdated, "Xəta baş verdi. İstifadəçi yenilənmədi");
                }
            }
            else
            {
                user.AddError(ErrorMessageCode.UserCouldNotFind, "Xəta baş verdi. İstifadəçi tapılmadı.");
            }
            return user;
        }
    }
}
