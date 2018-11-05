using JIC.Base.Views;
using JIC.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.ServicesJicCrimTests.Services
{
    [TestClass()]
    class CircuitServiceTests : TestBase
    {
        [TestMethod()]
        public void AddCircuitTest()
        {
            HabyBase2(() =>
            {

                vw_CircuitData data = new vw_CircuitData()
                {
                    CourtID = 1,
                    CrimeTypeID = 2,
                    SecretaryID = 1,
                    AssistantSecretaryID = 1,
                    CircuitName = "new Circuit",
                    CircuitStartDate = DateTime.Now.AddMonths(5),
                    IsActive = true,
                    CycleID = 4,
                    CenterJudgeID = 1,
                    IsFutureCircuit = true,
                };
                JIC.Services.Services.CourtConfigurationService serv = new JIC.Services.Services.CourtConfigurationService(Base.CaseType.Crime);
                int casId;
                serv.AddCircuit(data, out casId);
                if (casId == 0)
                {
                    return Base.TestStat.Fail;
                }
                else
                {
                    return Base.TestStat.Pass;
                }
            });
        }

        [TestMethod()]
        public void AddParentFolderTest()
        {
            HabyBase(delegate ()
            {
                CaseSessionsService serv = new JIC.Services.Services.CaseSessionsService(Base.CaseType.Crime);
                serv.GetCircuitRolls(2007);
                //serv.AddParentFolder(1023);
            });
        }
    }
}