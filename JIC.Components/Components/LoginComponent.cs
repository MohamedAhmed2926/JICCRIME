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
    public class LoginComponent
    {
        public IUserRepostiory UserRepostiory;
        public LoginComponent( IUserRepostiory UserRepostiory)
        {
            this.UserRepostiory = UserRepostiory;
        }

        public LoginStatus Login(string UserName,string Password,out vw_UserData userData)
        {
            userData = null;
            if (!UserRepostiory.IsUserNameExist(UserName))
                return LoginStatus.UserNameNotCorrect;
            else if(UserRepostiory.Login(UserName,Password,out userData))
                return LoginStatus.LoginSuccess;
            else
                return LoginStatus.PasswordNotCorrect;
            
        }

    }
}
