using JIC.Base.Interfaces;
using JIC.Crime.Entities;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base.Views;
using JIC.Crime.Entities.Models;
using JIC.Base;

namespace JIC.Crime.Repositories.Repositories
{
    public class Security_UsersRepository : EntityRepositoryBase<Security_Users>, ISecurity_Users
    {
        public UserStatus AddUser(vw_UserData User, out int? UserId)
        {
            Security_Users user = new Security_Users();
            user.UserName = User.UserName;
            user.PasswordHash = User.Password;
            user.CourtID = User.CourtID;
            user.UserTypeID = int.Parse(User.UserType);
            user.ProsecutionID = User.ProsecutionID;
            user.LevelID = User.UserJudgeLevel;
            user.MobileNo = User.PhoneNo;
            user.PersonsId = User.PersonID;
            user.Active = true;
            this.Add(user);
            this.Save();
            UserId = user.Id;
            return UserStatus.AddSuccess;
        }
        public bool IsUserNameExist(string userName,int? ignoreID)
        {
            return DataContext.Users.Where(User => User.UserName == userName && ((ignoreID.HasValue && User.Id != ignoreID.Value) || !ignoreID.HasValue)).Count() > 0;
        }
        public IQueryable<vw_UserData> GetUsers(int? courtID)
        {
            return (from _user in DataContext.Users
                    join _userType in DataContext.Security_UserTypes on _user.UserTypeID equals _userType.ID
                    where ((courtID == _user.CourtID && courtID.HasValue) || !courtID.HasValue)
                    select new vw_UserData
                    {
                        ID = _user.Id,
                        CourtID = _user.CourtID,
                        PersonID = _user.PersonsId,
                        PhoneNo = _user.PhoneNumber,
                        ProsecutionID = _user.ProsecutionID,
                        UserJudgeLevel = _user.TitleID,
                        UserName = _user.UserName,
                        UserTypeID = _user.UserTypeID,
                        UserType = _userType.Name,
                        Active = _user.Active,
                        FullName = (_user.PersonsId.HasValue ? _user.Configurations_Persons.FullName : "")
                    });
        }

        public void ActivateUser(int UserID)
        {
            var user = this.GetByID(UserID);
            user.Active = true;
            this.Update(user);
            this.Save();
        }

        public Deactive DeActivateUser(int UserID)
        {
            try
            {
                var user = this.GetByID(UserID);
                user.Active = false;
                this.Update(user);
                this.Save();
                return Deactive.Deactive;
            }
            catch (Exception ex)
            {
                return Deactive.failed;
            }
        }

        public void DeleteUser(int userID)
        {
            var User = GetByID(userID);
            Delete(User);
            Save();
        }
        public bool IsPhoneExist(string PhoneNo, int? ignoreID = null)
        {
            return DataContext.Users.Where(User => User.PhoneNumber == PhoneNo && ((ignoreID.HasValue && User.Id != ignoreID.Value) || !ignoreID.HasValue)).Count() > 0;
        }
        public bool IsPassporeExist(string PassportNo, int? ignoreID = null)
        {
            var result=(from person in DataContext.Configurations_Persons
                          join user in DataContext.Users on person.ID equals user.PersonsId
                          where ((person.PassportNumber == PassportNo) && ((ignoreID.HasValue && user.Id != ignoreID.Value) || !ignoreID.HasValue))

                          select new
                          {


                          }).Count() > 0;
            if (ignoreID == null)
            {
                result=(from person in DataContext.Configurations_Persons
                where person.PassportNumber == PassportNo
                select new
                {


                }).Count() > 0;
            }
            return result;
         
        }

        public vw_UserData FindByID(int id)
        {
            return (from user in DataContext.Users
                    join UserType in DataContext.Security_UserTypes on user.UserTypeID equals UserType.ID
                    where user.Id == id
                    select new vw_UserData
                    {
                        ID = user.Id,
                        Active = user.Active,
                        CourtID = user.CourtID,
                        Password = user.PasswordHash,
                        PersonID = user.PersonsId,
                        PhoneNo = user.PhoneNumber,
                        ProsecutionID = user.ProsecutionID,
                        UserJudgeLevel = user.TitleID,
                        UserName = user.UserName,
                        UserTypeID = user.UserTypeID,
                        UserType = UserType.Name,
                        AccessFailedCount=user.AccessFailedCount,
                        FullName=user.FullName
                        
                    }).First();
        }
        public bool CanAccess(string UserName)
        {
        int userTypeId=DataContext.Users.Single(e => e.UserName == UserName).UserTypeID;
            if (userTypeId == 3)
                {
                var result = (from circuit in DataContext.CourtConfigurations_Circuits
                              join users in DataContext.Users on circuit.SecretaryID equals users.Id

                              where users.UserName == UserName
                              select new vw_KeyValue
                              {

                              }).Union(
                from circuit in DataContext.CourtConfigurations_Circuits
                join users in DataContext.Users on circuit.AssistantSecretaryID equals users.Id

                where users.UserName == UserName
                select new vw_KeyValue
                {

                }).Count() > 0;
                return result;
            }
            else if(userTypeId == 2)
            {
                var result = (from circuitMembers in DataContext.CourtConfigurations_CircuitMembers
                              join users in DataContext.Users on circuitMembers.UserID equals users.Id

                              where users.UserName == UserName && circuitMembers.ToDate == null
                              select new vw_KeyValue
                              {

                              }).ToList().Count > 0;
                return result;
            }
            return true;
        }

         

            public IQueryable<vw_UserData> GetUsersByType(int? courtID, SystemUserTypes userType)
        {
            return GetUsers(courtID).Where(user => user.UserTypeID == (int)userType);
        }

        public UserStatus UpdateUser(int id, vw_UserData userData)
        {
            var User = GetByID(id);
            User.CourtID = userData.CourtID;
            User.UserName = userData.UserName;
            User.UserTypeID = userData.UserTypeID;
            User.TitleID = userData.UserJudgeLevel;
            User.PhoneNumber = userData.PhoneNo;
            User.ProsecutionID = userData.ProsecutionID;
            User.PersonsId = userData.PersonID;
            User.MobileNo = userData.PhoneNo;
            User.FullName = userData.FullName;
            User.AccessFailedCount = userData.AccessFailedCount;
            Update(User);
            Save();
            return UserStatus.AddSuccess;
        }

        public bool IsUserOnSystem(int userID, string userName)
        {
            vw_UserData User = FindByID(userID);
            if (User.UserTypeID == (int) SystemUserTypes.schedualEmployee)
            {
                //var caseres= (from cases in DataContext.Cases_Cases 
                //         where cases.CreatedBy  == userName
                //         select new { }).ToList().Count > 0;
                //var vacres
            }



            return true;
        }
    }
}
