using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Components.Components;
using JIC.Base.Views;
using System.Text.RegularExpressions;
using JIC.Services.ServicesInterfaces;
using JIC.Crime.Repositories;
using JIC.Repositories.DBInteractions;

namespace JIC.Services.Services
{
    public class Security_UsersService : ServiceBase, IUserService
    {
        public UserComponent UserComponent { get { return GetComponent<UserComponent>(); } }
        public CircuitMembersComponent CircuitMembersComponent { get { return GetComponent<CircuitMembersComponent>(); } }
        public CircuitsComponent CircuitsComponent { get { return GetComponent<CircuitsComponent>(); } }
        public Security_UsersService(CaseType caseType) : base(caseType)
        {
        }

        public UserStatus AddUser(vw_UserData UserData, out int? UserID)
        {
            UserID = null;
            return UserComponent.AddUser(UserData, out UserID);
        }

        public UserStatus UpdateUser(int id, vw_UserData userData)
        {
            try
            {
                return UserComponent.UpdateUser(id, userData);
            }catch(Exception ex)
            {
                HandleException(ex);
                return UserStatus.Failed;
            }
        }

        public IQueryable<vw_UserData> GetUsers(int? courtID)
        {
            return UserComponent.GetUsers(courtID);
        }

        public Deactive DeActivateUser(int UserID)
        {
            if (CircuitMembersComponent.IsCircuitMember(UserID) || CircuitsComponent.GetCircuitsBySecretairyID(UserID).Count > 0)
            {
                return Deactive.CannotDeactive;
            }

            if (UserComponent.DeActivateUser(UserID) == Deactive.Deactive)
                    return Deactive.Deactive;
 
          else
                {
                    return Deactive.failed;
                }
    //else if (SystemConfigurations.Settings_InDevelopmentMode)
    //{
    //    return Deactive.CannotDeactive;
    //    throw new ValidationExceptions("لا يمكن تعطيل مستخدم ضمن تشكيل حالي لدائرة");
    //}



}

        public void ActivateUser(int UserID)
        {
            UserComponent.ActivateUser(UserID);
        }

        public DeleteUserStatus DeleteUser(int UserID)
        {
            try
            {
                if (CircuitMembersComponent.IsCircuitMember(UserID))
                {
                    return DeleteUserStatus.IsMember;
                }
                else if (CircuitsComponent.GetCircuitsBySecretairyID(UserID).Count > 0)
                {
                    return DeleteUserStatus.IsSecretary;
                }
                else
                {
                    UserComponent.Delete(UserID);
                    return DeleteUserStatus.Succeeded;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return DeleteUserStatus.Failed;
            }
        }

        public vw_UserData FindUserByID(int id)
        {
            return UserComponent.FindUserByID(id);
        }
        public bool CanAccess(string UserName)
        {
            return UserComponent.CanAccess(UserName);
        }
            public List<vw_KeyValue> GetAllSecretaries(int courtID)
        {
            return UserComponent.GetUsersBasedOnType(courtID,SystemUserTypes.Secretary).Select(user => new vw_KeyValue
            {
                ID = user.ID,
                Name = user.FullName
            }).ToList();
        }

        public List<vw_KeyValue> GetAllJudges(int courtID)
        {
            return UserComponent.GetUsersBasedOnType(courtID, SystemUserTypes.Judge).Select(user => new vw_KeyValue
            {
                ID = user.ID,
                Name = user.FullName
            }).ToList();
        }

        public List<vw_KeyValue> GetUserSecritary(int CircuitID)
        {
            return CircuitsComponent.GetCircuitUsers(CircuitID);
        }

        public bool IsUserNameExist(string userName, int? ignoreID = null)
        {
            return UserComponent.IsUserNameExist(userName,ignoreID);
        }
        public bool IsPhoneExist(string PhoneNo, int? ignoreID = null)
        {
            return UserComponent.IsPhoneExist(PhoneNo, ignoreID);
        }
        public bool IsPassporeExist(string PassportNo, int? ignoreID = null)
        {
            return UserComponent.IsPassporeExist(PassportNo, ignoreID);
        }


        public bool IsUserOnSystem(int UserID, string UserName)
        {

            return UserComponent.IsUserOnSystem(UserID, UserName );
        }
    }
}
