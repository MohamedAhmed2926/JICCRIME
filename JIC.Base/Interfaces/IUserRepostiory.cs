using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base.Views;

namespace JIC.Base.Interfaces
{
    public interface IUserRepostiory
    {
        bool IsUserNameExist(string userName);
        bool Login(string userName, string password, out vw_UserData userData);
    }
}
