using JIC.Base;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JIC.Services.ServicesInterfaces
{
    public interface IVacationService
    {
        SaveStatus AddVacation(vw_VacationData vacations , out int vacationID);
        SaveStatus EditVacation(vw_VacationData vacations);
        DeleteStatus DeleteVacation(int vacationsID);
        List<vw_VacationData> GetVacations();

    }
}
