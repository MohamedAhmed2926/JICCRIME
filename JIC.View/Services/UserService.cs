using JIC.Base;
using JIC.Services.ServicesInterfaces;
using JIC.Base.Views;
using JIC.Crime.Entities.Models;
using JIC.Crime.Repositories;
using JIC.Crime.View.Models;
using JIC.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;

namespace JIC.Crime.View.Services
{
    public class UserService : ServiceBase, JIC.Services.ServicesInterfaces.IUserService
    {
        private ApplicationSignInManager signInManager;
        private ApplicationUserManager userManager;
        private IPrincipal User;
        private IUserService SecurityUserService;
         
        public UserService(ApplicationUserManager userManager,ApplicationSignInManager signInManager, IPrincipal User, IUserService SecurityUserService) : base(CaseType.Crime)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.User = User;
            this.SecurityUserService = SecurityUserService;

        }
       
        public async Task<SignInStatus> PasswordSignIn(string userName,string Password, bool isPersistent)
        {
            var User = await userManager.FindByNameAsync(userName);
            if (User == null)
                return SignInStatus.Failure;
            //else if (!await userManager.IsLockedOutAsync(User.Id) && !await userManager.CheckPasswordAsync(User, Password))
            //    return SignInStatus.Failure;
            else if (!User.Active)
                return SignInStatus.Blocked;
            
            else
            {
                var result = await signInManager.PasswordSignInAsync(userName, Password, isPersistent, shouldLockout: true);
                switch (result)
                {
                    case Microsoft.AspNet.Identity.Owin.SignInStatus.Success:
                        if (await userManager.CheckPasswordAsync(User, JIC.Base.SystemConfigurations.Defaults_DefaultPassword))
                            return SignInStatus.PasswordNeedChange;
                        return SignInStatus.Success;
                 //   case Microsoft.AspNet.Identity.Owin.SignInStatus.LockedOut:
                        //Current Behaviour is to Block User
                        //AG: I don't Like IT :D
                   //     DeActivateUser(User.Id);
                   //     return SignInStatus.LockedOut;
                    case Microsoft.AspNet.Identity.Owin.SignInStatus.RequiresVerification:
                        return SignInStatus.RequiresVerification;
                    
                }
            }

            var user = SecurityUserService.FindUserByID(User.Id);
            user.AccessFailedCount++;
            SecurityUserService.UpdateUser(user.ID,user);
            if (user.AccessFailedCount == 4)
            {
                DeActivateUser(User.Id);
                return SignInStatus.LockedOut;
            }
            return SignInStatus.Failure;
        }

        internal async Task<IdentityResult> UpdatePasswordAsync(vw_UserData currentUser, string password)
        {
            string HashedPassword = userManager.PasswordHasher.HashPassword(password);
            var User = await userManager.FindByIdAsync(currentUser.ID);
            User.PasswordHash = HashedPassword;
            return await userManager.UpdateAsync(User);
        }

        public async Task<IdentityResult> CreateUserAsync(long? PersonID,string PersonName, vw_UserDataModel UserData)
        {
            var Security_User = new Entities.Models.Security_Users
            {
                UserName = UserData.UserName,
                UserTypeID = (int)UserData.UserType,
                CourtID = UserData.CourtID,
                ProsecutionID = UserData.ProsecutionID,
                Email = UserData.UserName + "@jic.com",
                FullName = PersonName,
                CreatedBy = (User.Identity.Name == null || User.Identity.Name.Equals("") ? "System" : User.Identity.Name),
                CreatedAt = DateTime.Now,
                PersonsId = PersonID,
                PhoneNumber = UserData.PhoneNo,
                Active = true,
                TitleID = UserData.UserJudgeLevel,
                
            };
            var CreateUserResult = await userManager.CreateAsync(Security_User, SystemConfigurations.Defaults_DefaultPassword);
            if (CreateUserResult.Succeeded)
                return (await AddRolesToUser(UserData.UserType.Value, Security_User.Id));
            else
                return CreateUserResult;

        }

        private async Task<IdentityResult> AddRolesToUser(SystemUserTypes UserType, int UserID)
        {
            return await userManager.AddToRoleAsync(UserID, UserType.ToString());
        }

        internal vw_UserData FindByID(int id)
        {
            return FindUserByID(id);
            //return userManager.FindById(id);
        }
        public bool CanAccess(string UserName)
        {
            return SecurityUserService.CanAccess(UserName);
        }

        public UserStatus AddUser(vw_UserData UserData, out int? UserID)
        {
            return SecurityUserService.AddUser(UserData, out UserID);
        }


        public UserStatus UpdateUser(int id, vw_UserData userData)
        {
            return SecurityUserService.UpdateUser(id, userData);
        }

        public Deactive DeActivateUser(int UserID)
        {
          return  SecurityUserService.DeActivateUser(UserID);
        }

        public void ActivateUser(int UserID)
        {
            SecurityUserService.ActivateUser(UserID);
        }

        public IQueryable<vw_UserData> GetUsers(int? courtID)
        {
            return SecurityUserService.GetUsers(courtID);
        }
        


        public DeleteUserStatus  DeleteUser(int id)
        {
            return SecurityUserService.DeleteUser(id);
        }

        public vw_UserData FindUserByID(int id)
        {
            return SecurityUserService.FindUserByID(id);
        }
        public List<vw_KeyValue> GetAllSecretaries(int CourtID)
        {
            return SecurityUserService.GetAllSecretaries(CourtID);
        }

        public List<vw_KeyValue> GetAllJudges(int CourtID)
        {
            return SecurityUserService.GetAllJudges(CourtID);
        }

        public List<vw_KeyValue> GetUserSecritary(int CircuitID)
        {
            return SecurityUserService.GetUserSecritary(CircuitID);
        }

        public bool IsUserNameExist(string userName, int? ignoreID = null)
        {
            return SecurityUserService.IsUserNameExist(userName, ignoreID);
        }

        public bool IsPhoneExist(string PhoneNo, int? ignoreID = null)
        {
            return SecurityUserService.IsPhoneExist(PhoneNo, ignoreID);
        }
        public bool IsPassporeExist(string PassportNo, int? ignoreID = null)
        {
            return SecurityUserService.IsPassporeExist(PassportNo, ignoreID);
        }
    }
}