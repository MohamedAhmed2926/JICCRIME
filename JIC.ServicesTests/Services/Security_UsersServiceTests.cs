using Microsoft.VisualStudio.TestTools.UnitTesting;
using JIC.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.ServicesJicCrimTests;
using JIC.Base;

namespace JIC.Services.Services.Tests
{
    [TestClass()]
    public class Security_UsersServiceTests : TestBase
    {
        [TestMethod()]
        public void AddUserTest()
        {
            HabyBase2(() =>
            {
                var serv = new JIC.Services.Services.Security_UsersService(Base.CaseType.Crime);
                int? id;
                serv.AddUser(new Base.Views.vw_UserData {UserName = "heba" , Password = "12345" , PhoneNo = "01114487150" , UserTypeID = 3 , UserType = "Secretary" } , out id);
                
                if (id != null)
                {
                    return Base.TestStat.Pass;
                }
                else
                {
                    return Base.TestStat.Fail;
                }
            }
            );
        }
    }
}