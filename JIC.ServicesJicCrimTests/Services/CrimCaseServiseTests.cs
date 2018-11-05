using Microsoft.VisualStudio.TestTools.UnitTesting;
using JIC.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.ServicesJicCrimTests;
using JIC.Base.Views;

namespace JIC.Services.Services.Tests
{
    [TestClass()]
    public class CrimCaseServiseTests: TestBase
    {
        [TestMethod()]
        public void AddBasicDataTest()
        {
            HabyBase2( ()=> {

                vw_CrimeCaseBasicData data = new vw_CrimeCaseBasicData() {
                 CaseName="قضية أختبارية",
                SecondProsecutionID=1,
                MainCrimeID=41,
                CrimeTypeID=21,
                FirstNumberInt=66,
                FirstYearInt=2015,
                SecondNumberInt=111,
                SecondYearInt=2018,
                HasObtainment=false,
                OverAllId=23,
                FirstPoliceStationID=1,
                NationalID="00-00-00-00-011"
                };
                JIC.Services.ServicesInterfaces.ICrimeCaseService serv = new JIC.Services.Services.CrimeCaseServise();
                int casId;
                serv.AddBasicData(data, out casId);
                if (casId==0)
                {
                    return Base.TestStat.Fail;
                }
                else
                {
                    return Base.TestStat.Pass;
                }
            });
        }
    }
}