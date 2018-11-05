using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIC.Base.Views;
using JIC.Crime.View.Models;
using JIC.Base;

namespace JIC.Crime.View.TestService
{
    public class VacationService : IVacationService
    {
        public bool AddVacationDays(vw_VacationData vacationData, out int VacationID)
        {
            VacationID = 1;
            return true;
        }

        public SaveStatus AddVacation(vw_VacationData vacationData, out int VacationID)
        {
            VacationID = 1;
            return SaveStatus.Saved_Before ;
        }
        public SaveStatus EditVacation(vw_VacationData vacationData)
        {
            return SaveStatus.Saved;
        }

        public DeleteStatus DeleteVacation(int VacationID)
        {
            return DeleteStatus.Deleted;
        }

        public List<DateTime> GetVacationDates()
        {
            return null;
        }

        public List<vw_VacationData> GetVacations()
        {
            List<vw_VacationData> VList = new List<vw_VacationData>();

            VList.Add(new vw_VacationData { ID = 1, VacationName = "xyz", VacationFrom = new DateTime(2017, 3, 3), VacationTo  = new DateTime(2017, 3, 5) });
            VList.Add(new vw_VacationData { ID = 2, VacationName = "abc", VacationFrom = new DateTime(2017, 12, 2), VacationTo = new DateTime(2017, 12, 3) });
            VList.Add(new vw_VacationData { ID = 3, VacationName = "uvw", VacationFrom = new DateTime(2017, 4, 2), VacationTo = new DateTime(2017, 4, 3) });

            return VList;
        }
    }
}