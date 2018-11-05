using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.Views;

namespace JIC.Components.Components
{
    public class UserComponent
    {
        public ISecurity_Users SecurityUserRepostiory;
        public UserComponent( ISecurity_Users SecurityUserRepostiory)
        {
            this.SecurityUserRepostiory = SecurityUserRepostiory;
        }

        public UserStatus AddUser(vw_UserData user, out int? UserId)
        {
            UserId = null;

            if (SecurityUserRepostiory.IsUserNameExist(user.UserName))
                return UserStatus.UserNameExist;
            else
            {
                return SecurityUserRepostiory.AddUser(user, out UserId);
            }
        }

        public UserStatus UpdateUser(int id, vw_UserData userData)
        {
            return SecurityUserRepostiory.UpdateUser(id, userData);
        }

        public IQueryable<vw_UserData> GetUsers(int? courtID)
        {
            return SecurityUserRepostiory.GetUsers(courtID);
        }

        public void ActivateUser(int UserID)
        {
            SecurityUserRepostiory.ActivateUser(UserID);
        }

        public Deactive DeActivateUser(int UserID)
        {
         return SecurityUserRepostiory.DeActivateUser(UserID);
        }

        public void Delete(int userID)
        {
            SecurityUserRepostiory.DeleteUser(userID);
        }

        public vw_UserData FindUserByID(int id)
        {
            return SecurityUserRepostiory.FindByID(id);
        }
        public bool CanAccess(string UserName)
        {
            return SecurityUserRepostiory.CanAccess(UserName);
        }
        public IQueryable<vw_UserData> GetUsersBasedOnType(int courtID, SystemUserTypes userType)
        {
            return SecurityUserRepostiory.GetUsersByType(courtID, userType);
        }

        public bool IsUserNameExist(string userName, int? ignoreID)
        {
            return SecurityUserRepostiory.IsUserNameExist(userName,ignoreID);
        }

        public bool IsUserOnSystem(int userID, string userName)
        {
            return SecurityUserRepostiory.IsUserOnSystem( userID,  userName);
        }

        public bool IsPhoneExist(string PhoneNo, int? ignoreID = null)
        {
            return SecurityUserRepostiory.IsPhoneExist(PhoneNo, ignoreID);
        }
        public bool IsPassporeExist(string PassportNo, int? ignoreID = null)
        {
            return SecurityUserRepostiory.IsPassporeExist(PassportNo, ignoreID);
        }
    }
}
