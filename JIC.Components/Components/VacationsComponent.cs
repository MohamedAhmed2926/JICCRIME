using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Components.Components
{
    public class VacationsComponent
    {
        private IVacationsRepository VacationsRepository;

        public VacationsComponent( IVacationsRepository VacationsRepository)
        {
            this.VacationsRepository = VacationsRepository;
        }


        public SaveStatus AddVacation(vw_VacationData vw_VacationsObj, out int vacationID)
        {
            return VacationsRepository.AddVacation(vw_VacationsObj, out vacationID);
        }

        public void AddVacation(vw_VacationData prosecutorData)
        {
            throw new NotImplementedException();
        }

        public SaveStatus EditVacation(vw_VacationData vw_VacationsObj)
        {
            return VacationsRepository.EditVacation(vw_VacationsObj);
        }
        public DeleteStatus DeleteVacation(int vacationID)
        {
            return VacationsRepository.DeleteVacation(vacationID);
        }
        public List<vw_VacationData> GetVacation()
        {
            return VacationsRepository.GetVacations();
        }


    }
}
