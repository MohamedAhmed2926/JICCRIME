using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIC.Base.Views;
using JIC.Base;

namespace JIC.Crime.View.TestService
{
    public class PersonService : IPersonService
    {
        public bool AddPerson(vw_PersonData PersonData, out long PersonID, out bool NationalIDValid)
        {
            PersonID = 0;
            NationalIDValid = true;
            return true;
        }

        public PersonStatus AddPerson(vw_PersonData PersonData, out long PersonID)
        {
            throw new NotImplementedException();
        }

        public PersonStatus DeletePerson(vw_PersonData PersonData)
        {
            return PersonStatus.SuccefullSave;
        }

        public bool EditPerson(vw_PersonData PersonData)
        {
            return true;
        }

        public vw_PersonData GetPerson(long personID)
        {
            throw new NotImplementedException();
        }

        public List<vw_PersonData> GetPersons(string natID)
        {
            throw new NotImplementedException();
        }

        public bool IsNationalNoExist(string text, int? ignoreID)
        {
            throw new NotImplementedException();
        }

        public bool IsNationalNoExist(string text, long? ignoreID)
        {
            throw new NotImplementedException();
        }

        public bool IsPassportNoExist(string text, int? ignoreID)
        {
            throw new NotImplementedException();
        }

        public bool IsPassportNoExist(string text, long? ignoreID)
        {
            throw new NotImplementedException();
        }

        public bool NumberOfAttachedCases(long iD)
        {
            throw new NotImplementedException();
        }

        PersonStatus IPersonService.EditPerson(vw_PersonData PersonData)
        {
            throw new NotImplementedException();
        }

        int IPersonService.NumberOfAttachedCases(long iD)
        {
            throw new NotImplementedException();
        }
    }
}