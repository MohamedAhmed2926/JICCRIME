using JIC.Base.Interfaces;
using JIC.Base.Views;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Crime.Entities.Models;
using JIC.Base.Entities.Models;

namespace JIC.Crime.Repositories.Repositories
{
    public class UserRepository : EntityRepositoryBase<User>, IUserRepostiory
    {
        public bool IsUserNameExist(string userName)
        {
            return DataContext.Users.Where(user => user.UserName == userName).Count() > 0;
        }

        public bool Login(string userName, string password, out vw_UserData userData)
        {
            userData = null;
            var User = DataContext.Users.Where(user => user.UserName == userName && user.PasswordHash == password).FirstOrDefault();
            if (User == null)
                return false;
            else
            {
                userData = new vw_UserData
                {
                    ID = User.Id,
                    CourtID = User.CourtID,
                    UserName = User.UserName
                };
                return true;
            }
        }
    }
}
