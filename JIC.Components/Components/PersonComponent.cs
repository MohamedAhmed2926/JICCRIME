using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.Views;

namespace JIC.Components.Components
{
    public class PersonComponent
    {
        private IPersonRepository PersonRepository;
        public PersonComponent( IPersonRepository PersonRepository)
        {
            this.PersonRepository = PersonRepository;
        }

        public bool AddPerson(vw_PersonData personData, out long personID)
        {
            return PersonRepository.AddPerson(personData, out personID);
        }

        public PersonStatus EditPerson(vw_PersonData personData)
        {
            return PersonRepository.EditPerson(personData);
        }

        public List<vw_PersonData> GetPersons(string natID)
        {
            return PersonRepository.GetPersons(natID);
        }

        public bool CanBeDeleted(long ID)
        {
            return PersonRepository.CanDeletePerson(ID);
        }

        public PersonStatus DeletePerson(long ID)
        {
            return PersonRepository.Delete(ID);
        }

        public vw_PersonData FindPerson(long personID)
        {
            return PersonRepository.FindByID(personID);
        }

        public bool IsPassportNoExist(string text, long? ignoreID)
        {
            return PersonRepository.IsPassportNoExist(text, ignoreID);
        }

        public bool IsNationalNoExist(string text, long? ignoreID)
        {
            return PersonRepository.IsNationalNoExist(text, ignoreID);
        }

        public int NumberOfAttachedCases(long personID)
        {
            return PersonRepository.NumberOfAttachedCases(personID);
        }
    }
}
