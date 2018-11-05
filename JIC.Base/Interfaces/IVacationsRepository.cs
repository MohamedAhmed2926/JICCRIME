using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base.Views;
using JIC.Base;

namespace JIC.Base.Interfaces
{
    public interface IVacationsRepository
    {
        SaveStatus AddVacation(vw_VacationData VacationsObj , out int vacationID);
        SaveStatus EditVacation(vw_VacationData VacationsObj);
        DeleteStatus DeleteVacation(int vacationID);
        List<vw_VacationData> GetVacations();
    }
}
