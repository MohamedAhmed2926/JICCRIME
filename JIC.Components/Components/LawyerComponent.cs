using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.views;
using JIC.Base.Views;

namespace JIC.Components.Components
{
    public class LawyerComponent
    {
        public ILawyerRepository LawyerRepository;

        public LawyerComponent(ILawyerRepository LawyerRepository)
        {
            this.LawyerRepository = LawyerRepository;
        }

        public LawyerStatus AddLawyer(vw_LawyerData lawyerData, out int LawyerID)
        {
            return LawyerRepository.AddLawyer(lawyerData, out LawyerID);
        }
        //public LawyerStatus SearchBYPerson(int PersonID)
        //{
        //    return LawyerRepository.SearchBYPerson(PersonID);
        //}
        public LawyerStatus EditLawyer(vw_LawyerData lawyerData)
        {
            return LawyerRepository.EditLawyer(lawyerData);
        }

        public vw_LawyerData GetLawyerByID(int? LawyerID)
        {
            return LawyerRepository.GetLawyerByID(LawyerID);
        }

        public List<vw_LawyerData> GetLawyers()
        {
            return LawyerRepository.GetLawyers();
        }

    }
}
