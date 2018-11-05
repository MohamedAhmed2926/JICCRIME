using JIC.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using JIC.ServicesJicCrimTests.Similator;
using JIC.ServicesJicCrimTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JIC.Base.Views;

namespace JIC.ServicesJicCrimTests.Services
{
    [TestClass()]
    public class SearchCasesTests : TestBase
    {
        [TestMethod()]
        public void SearchTest()
        {
            HabyBase(delegate ()
            {
                var serv = new JIC.Services.Services.SearchCasesService(Base.CaseType.Crime);

                serv.Search(new vw_SearchData
                {
                    FirstLevelNumber = null,
                    SecondLevelNumber = null,
                    FirstLevelYear = null,
                    SecondLevelYear = null,
                    CircuitID = null,
                    CourtID = 1,
                    CrimeType = null,
                    FirstLevelProsecutionID = null,
                    HasObtainment = null,
                    JudgeDate = null,
                    JudgeType = null,
                    OverAllNumber = null,
                    OverAllNumberProsecution = null,
                    OverAllNumberYear = null,
                    PartyName = null,
                    PartyType = null,
                    PoliceStationID = null,
                    SecondLevelProsecutionID = null,
                    SessionDate = null,
                    SessionDateType = Base.SessionSearchMode.LastSessionDate
                });
            });

        }
    }
}
