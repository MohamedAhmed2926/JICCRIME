using JIC.ServicesJicCrimTests.Similator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.ServicesJicCrimTests
{
   public class TestBase
    {
        public delegate void TestVoid();
        public delegate Base.TestStat TestVoidReturns();
        private void SadBase(TestVoid test)
        {
            using (HttpSimulator simulator = new HttpSimulator())
            {

                simulator.SimulateRequest();
                JIC.Base.TestStat t = JIC.Repositories.DBInteractions.Utitlities.Transe(JIC.Crime.Repositories.DBInteractions.DBFactory.Get(), () => {
                    test.Invoke();
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
        protected void HabyBase(TestVoid test)
        {
            using (HttpSimulator simulator = new HttpSimulator())
            {

                simulator.SimulateRequest();
                JIC.Base.TestStat t = JIC.Repositories.DBInteractions.Utitlities.Transe(JIC.Crime.Repositories.DBInteractions.DBFactory.Get(), () => {
                    test.Invoke();
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

        protected void HabyBase2(TestVoidReturns test)
        {
            using (HttpSimulator simulator = new HttpSimulator())
            {

                simulator.SimulateRequest();
                JIC.Base.TestStat t = JIC.Repositories.DBInteractions.Utitlities.Transe(JIC.Crime.Repositories.DBInteractions.DBFactory.Get(), () => {
                    
                    return test.Invoke();
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
    }
}
