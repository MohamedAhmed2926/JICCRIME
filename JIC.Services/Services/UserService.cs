using JIC.Base;
using JIC.Base.ServicesInterfaces;
using JIC.Base.Views;
using JIC.Components.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.Services
{
    public class UserService : ServiceBase, IUserService
    {
        public UserComponent UserComponent { get { return ComponentFactory.GetUserComponent(); } }
        public UserService(CaseType caseType) : base(caseType)
        {
        }

        public void ActivateUser(int UserID)
        {
            throw new NotImplementedException();
        }

        public void AddUser(vw_UserData UserData, out int UserID)
        {
            throw new NotImplementedException();
        }

        public void DeActivateUser(int UserID)
        {
            throw new NotImplementedException();
        }

        public IQueryable<vw_UserData> GetUsers(int? CourtID)
        {
            return UserComponent.GetUsers(CourtID);
        }
    }
}
