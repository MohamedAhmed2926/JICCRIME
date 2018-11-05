using JIC.Services.ServicesInterfaces;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIC.Base;
using System.Threading.Tasks;

namespace JIC.Crime.View.TestInterfaces
{
    public class UserService : IUserService
    {
        public void ActivateUser(int UserID)
        {
            throw new NotImplementedException();
        }
        public UserStatus AddUser(vw_UserData UserData, out int? UserID)
        {
            throw new NotImplementedException();
        }

        public bool CanAccess(string UserName)
        {
            throw new NotImplementedException();
        }

        public Deactive DeActivateUser(int UserID)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public vw_UserData FindUserByID(int id)
        {
            vw_UserData vw_UserData = new vw_UserData();
            return vw_UserData;
            //throw new NotImplementedException();
        }

        public List<vw_KeyValue> GetAllJudges(int CourtID)
        {
            throw new NotImplementedException();
        }

        public List<vw_KeyValue> GetAllSecretaries(int CourtID)
        {
            throw new NotImplementedException();
        }

        public IQueryable<vw_UserData> GetUsers(int? CourtID)
        {
            return new List<vw_UserData>
            {
                new vw_UserData
                {
                    UserName = "test1"
                },
                new vw_UserData
                {
                    UserName = "test2"
                }
            }.AsQueryable();
        }

        public List<vw_KeyValue> GetUserSecritary(int CircuitID)
        {
            List<vw_KeyValue> list = new List<vw_KeyValue>();
            if (CircuitID == 1)
            {
                vw_KeyValue vw_Key = new vw_KeyValue()
                {
                    ID = 1,
                    Name = "wqewqed",
                };
                vw_KeyValue vw_Key2 = new vw_KeyValue()
                {
                    ID = 2,
                    Name = "DQEWFAS",
                };
                list.Add(vw_Key);
                list.Add(vw_Key2);
                return list;
            }
            else if (CircuitID == 2)
            {

                vw_KeyValue vw_Key = new vw_KeyValue()
                {
                    ID = 1,
                    Name = "AWERQWS",
                };
                vw_KeyValue vw_Key2 = new vw_KeyValue()
                {
                    ID = 2,
                    Name = "GFDSGERW",
                };
                list.Add(vw_Key);
                list.Add(vw_Key2);
                return list;
            }
            return list;
        }

        public bool IsPassporeExist(string PassportNo, int? ignoreID = null)
        {
            throw new NotImplementedException();
        }

        public bool IsPhoneExist(string PhoneNo, int? ignoreID = null)
        {
            throw new NotImplementedException();
        }

        public bool IsUserNameExist(string userName,int? ignoreID = null)
        {
            throw new NotImplementedException();
        }

        public Task<SignInStatus> PasswordSignIn(string userName, string Password, bool isPersistent)
        {
            throw new NotImplementedException();
        }

        public UserStatus UpdateUser(int id, vw_UserData userData)
        {
            throw new NotImplementedException();
        }

        DeleteUserStatus IUserService.DeleteUser(int id)
        {
            throw new NotImplementedException();
        }
    }
}