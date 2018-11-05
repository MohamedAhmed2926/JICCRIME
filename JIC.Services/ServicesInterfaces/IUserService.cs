using JIC.Base;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.ServicesInterfaces
{
    public interface IUserService
    {
        UserStatus AddUser(vw_UserData UserData, out int? UserID);
        Deactive DeActivateUser(int UserID);
        void ActivateUser(int UserID);
        IQueryable<vw_UserData> GetUsers(int? courtID);
        DeleteUserStatus  DeleteUser(int id);
        vw_UserData FindUserByID(int id);
        bool CanAccess(string UserName);
        List<vw_KeyValue> GetAllSecretaries(int CourtID);
        List<vw_KeyValue> GetAllJudges(int CourtID);
        bool IsUserNameExist(string userName, int? ignoreID = null);
        bool IsPhoneExist(string PhoneNo,int? ignoreID = null);
        bool IsPassporeExist(string PassportNo, int? ignoreID = null);
        List<vw_KeyValue> GetUserSecritary(int CircuitID);
        UserStatus UpdateUser(int id, vw_UserData userData);

    }
}
