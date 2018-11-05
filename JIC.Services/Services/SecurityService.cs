using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Views;
using JIC.Components.Components;

namespace JIC.Services.Services
{
    public class SecurityService : ServiceBase
    {
        public LoginComponent LoginComponent { get { return GetComponent<LoginComponent>(); } }
        public SecurityService(CaseType caseType) : base(caseType)
        {
        }

        public LoginStatus Login(string UserName,string Password,out vw_UserData userData)
        {
            userData = null;
            if (string.IsNullOrEmpty(UserName))
                return LoginStatus.EmptyUserName;

            return LoginComponent.Login(UserName, Password,out userData);
        }

    }
}
