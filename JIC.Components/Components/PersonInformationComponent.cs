using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Views;
using JIC.Base.Resources;
using JIC.Base.Interfaces;

namespace JIC.Components.Components
{
   
    public class PersonInformationComponent
    {
        public IPersonInformationRepository PersonInformationRepository;
        public PersonInformationComponent(IPersonInformationRepository PersonInformationRepository)
        {
            this.PersonInformationRepository = PersonInformationRepository;
            
        }

       public vw_InformationPerson GetInformationPerson(string NatNo, string Name)
        {
            return PersonInformationRepository.GetInformationPerson (NatNo ,Name);
        }
    }
}
