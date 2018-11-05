using System;
using JIC.Base;
using JIC.Base.Views.Services;
using JIC.Fault.Repositories;
using JIC.Prosecution.Service.Fault.Test.MockFactory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JIC.Prosecution.Service.Fault.Test
{
    [TestClass]
    public class FaultServiceTest
    {
        private readonly FaultCourtService faultCourtService;
        private readonly JICFaultContext JICFaultContext;
        private readonly ViewFactory viewFactory;

        public FaultServiceTest()
        {
            var faultUnityServiceHostFactory = new FaultUnityServiceHostFactory();

            faultCourtService = faultUnityServiceHostFactory.Resolve<FaultCourtService>();
            JICFaultContext = JIC.Fault.Repositories.DBInteractions.DBFactory.Get();
            viewFactory = faultUnityServiceHostFactory.Resolve<ViewFactory>();

        }
        [TestMethod]
        public void AddFaultNewCaseService()
        {
            Random random = new Random();
            using (var Transaction = JICFaultContext.Database.BeginTransaction())
            {
                var response =  faultCourtService.AddNewCase(new NewCase
                {
                    Business_Case_Id = random.Next(),
                    CaseDescription = new CaseDescription
                    {
                        Description = "Test",
                        LawItems = "test"
                    },
                    CaseTypeID = 2,
                    Court_ID = 1,
                    CrimeID = 45,
                    First_Case_No = 1,
                    First_Case_Year = 2015,
                    First_Case_Police_Station_ID = 1,
                    ProcedureTypeID = 13,
                    First_Session = viewFactory.GetRealCaseSession(4, 2),
                    CaseParties = viewFactory.GetCaseParties()
                });

                Assert.IsTrue(response.Result);
                Assert.AreEqual(response.ErrorCodes.Count, 0);

                //Transaction.Commit();

            }
        }
    }
}
