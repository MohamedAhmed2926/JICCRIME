using JIC.Base;
using JIC.Base.Views;
using JIC.Components.Components;
using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.Services
{
   public class PersonInformationService : ServiceBase, IPersonInformationService
    {

        private PersonInformationComponent PersonInformationComponent { get { return GetComponent<PersonInformationComponent>(); } }
        //public PersonInformationService(CaseType caseType) : base(caseType)
        //{
         
        //}
        public PersonInformationService() : base(CaseType.Crime)
        {
        }

        public vw_InformationPerson GetInformationPerson(string NatNo, string Name)
        {
           return PersonInformationComponent.GetInformationPerson(NatNo, Name);
        }
    }
}
