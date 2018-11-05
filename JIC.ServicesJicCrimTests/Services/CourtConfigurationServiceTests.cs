using Microsoft.VisualStudio.TestTools.UnitTesting;
using JIC.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using JIC.ServicesJicCrimTests.Similator;
using JIC.ServicesJicCrimTests;

namespace JIC.Services.Services.Tests
{
    [TestClass()]
    public class CourtConfigurationServiceTests : TestBase
    {

        //private HttpContextBase rmContext;
        //private HttpRequestBase rmRequest;
        //private Mock<HttpContextBase> moqContext;
        //private Mock<HttpRequestBase> moqRequest;
        //[TestInitialize]
        //public void SetupTests()
        //{
        //    // Setup Rhino Mocks
        //    rmContext = MockRepository.GenerateMock<HttpContextBase>();
        //    rmRequest = MockRepository.GenerateMock<HttpRequestBase>();
        //    rmContext.Stub(x => x.Request).Return(rmRequest);
        //    // Setup Moq
        //    moqContext = new Mock<HttpContextBase>();
        //    moqRequest = new Mock<HttpRequestBase>();
        //    moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
        //}
        [TestMethod()]
        public void AddCycleTest()
        {
            HabyBase(delegate ()
            {
                var serv = new JIC.Services.Services.CourtConfigurationService(Base.CaseType.Crime);
                serv.AddCycle(new List<DateTime> { DateTime.Now.AddDays(100), DateTime.Now.AddDays(200), DateTime.Now.AddDays(300), DateTime.Now.AddDays(400) }, Base.Cycle.FirstCycle, 1);
            });

            //using (HttpSimulator simulator = new HttpSimulator())
            //{

            //        simulator.SimulateRequest();
            // JIC.Base.TestStat t=       JIC.Repositories.DBInteractions.Utitlities.Transe(JIC.Crime.Repositories.DBInteractions.DBFactory.Get(), ()=> {

            //     return Base.TestStat.Pass;
            // });
            //    switch (t)
            //    {
            //        case Base.TestStat.Pass:
            //            Assert.AreEqual(1, 1);
            //            break;
            //        case Base.TestStat.Fail:
            //            Assert.Fail();
            //            break;
            //        default:
            //            break;
            //    }


            //}


        }

        [TestMethod()]
        public void AddCycleTestSad()
        {

            using (HttpSimulator simulator = new HttpSimulator())
            {

                simulator.SimulateRequest();
                JIC.Base.TestStat t = JIC.Repositories.DBInteractions.Utitlities.Transe(JIC.Crime.Repositories.DBInteractions.DBFactory.Get(), () =>
                {
                    var serv = new JIC.Services.Services.CourtConfigurationService(Base.CaseType.Crime);
                    serv.AddCycle(new List<DateTime> { DateTime.Now.AddDays(1), DateTime.Now.AddDays(3), DateTime.Now.AddDays(3), DateTime.Now.AddDays(4) }, Base.Cycle.FirstCycle, 1);
                    return Base.TestStat.Pass;
                });
                switch (t)
                {
                    case Base.TestStat.Fail:
                        Assert.AreEqual(1, 1);
                        break;
                    case Base.TestStat.Pass:
                        Assert.Fail();
                        break;
                    default:
                        break;
                }


            }


        }

        [TestMethod()]
        public void HapyTest()
        {

            using (HttpSimulator simulator = new HttpSimulator())
            {

                simulator.SimulateRequest();
                JIC.Base.TestStat t = JIC.Repositories.DBInteractions.Utitlities.Transe(JIC.Crime.Repositories.DBInteractions.DBFactory.Get(), () =>
                {
                    return Base.TestStat.Pass;
                });
                switch (t)
                {
                    case Base.TestStat.Pass:
                        Assert.AreEqual(1, 1);
                        break;
                    case Base.TestStat.Fail:
                        Assert.Fail();
                        break;
                    default:
                        break;
                }


            }


        }
        [TestMethod()]
        public void SadTest()
        {

            using (HttpSimulator simulator = new HttpSimulator())
            {

                simulator.SimulateRequest();
                JIC.Base.TestStat t = JIC.Repositories.DBInteractions.Utitlities.Transe(JIC.Crime.Repositories.DBInteractions.DBFactory.Get(), () =>
                {
                    return Base.TestStat.Fail;
                });
                switch (t)
                {
                    case Base.TestStat.Fail:
                        Assert.AreEqual(1, 1);
                        break;
                    case Base.TestStat.Pass:
                        Assert.Fail();
                        break;
                    default:
                        break;
                }


            }


        }
        [TestMethod()]
        public void Simulator_Assigns_CurrentContext()
        {
            using (HttpSimulator simulator = new HttpSimulator())
            {
                simulator.SimulateRequest();
                Assert.IsNotNull(HttpContext.Current);
            }
        }

        [TestMethod()]
        public void AddVacationTest()
        {
            HabyBase2(() =>
            {
                var serv = new JIC.Services.Services.CourtConfigurationService(Base.CaseType.Crime);
                int id;
                serv.AddVacation(new Base.Views.vw_VacationData { VacationName= "test" , VacationFrom = DateTime.Now , VacationTo = DateTime.Now},out id);
                if (id != 0 )
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

        public void EditVacationTest()
        {
            //HabyBase(delegate ()
            //{
            //    var serv = new JIC.Services.Services.CourtConfigurationService(Base.CaseType.Crime);
            //    serv.EditVacation(new Base.Views.vw_VacationData { });
            //});

        }
    }
}