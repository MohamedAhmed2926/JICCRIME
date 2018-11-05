using JIC.Crime.Repositories.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.ServicesJicCrimTests.Repositories
{
    [TestClass()]
    public  class OverallNumberRepositoryTests : TestBase
    {
        [TestMethod()]
        public void AddVacationTest()
        {
            HabyBase2(() => {
                 
                long i= new  OverallNumberRepository().CurrentOverAllNumber(2017, 500,1);
                if (i==1)
                {
                    return Base.TestStat.Pass;
                }
                else
                return Base.TestStat.Fail;

            });
        }
        }
}
