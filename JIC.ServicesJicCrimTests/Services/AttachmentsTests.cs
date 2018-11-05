using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.ServicesJicCrimTests.Services
{
    [TestClass()]
    public class AttachmentsTests : TestBase
    {
        [TestMethod()]
        public void AddParentFolderTest()
        {
            HabyBase(delegate ()
            {
                var serv = new JIC.Crime.Repositories.Repositories.DocumentsRepository();
                //serv.AddParentFolder(1023);
            });

            //HabyBase2(() =>
            //{
            //    var serv = new JIC.Crime.Repositories.Repositories.DocumentsRepository();
            //    int id;
            //    serv.AddParentFolder(10);
            //    //serv.AddVacation(new Base.Views.vw_VacationData { VacationName = "test", VacationFrom = DateTime.Now, VacationTo = DateTime.Now }, out id);
            //    if (id != 0)
            //    {
            //        return Base.TestStat.Pass;
            //    }
            //    else
            //    {
            //        return Base.TestStat.Fail;
            //    }
            //}
            //);
        }

    }
}
