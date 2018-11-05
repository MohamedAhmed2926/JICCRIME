using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
    public interface ISecurity_Users
    {
        bool IsPhoneExist(string PhoneNo, int? ignoreID = null);
        bool IsPassporeExist(string PassportNo, int? ignoreID = null);
        bool IsUserNameExist(string userName, int? ignoreID = null);
        UserStatus AddUser(vw_UserData User, out int? UserId);
        Deactive DeActivateUser(int UserID);
        void ActivateUser(int UserID);
        IQueryable<vw_UserData> GetUsers(int? courtID);
        IQueryable<vw_UserData> GetUsersByType(int? courtID,SystemUserTypes userType);
        void DeleteUser(int userID);
        vw_UserData FindByID(int id);
        UserStatus UpdateUser(int id, vw_UserData userData);
        bool CanAccess(string UserName);
        bool IsUserOnSystem(int userID, string userName);
    }
}
