using JIC.Base;
using JIC.Services.ServicesInterfaces;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.TestInterfaces
{
    public class PersonService : IPersonService
    {
        public bool AddPerson(vw_PersonData PersonData, out long PersonID, out bool NationalIDValid)
        {
            PersonID = 5;
            NationalIDValid = true;
            return true;
        }

        public PersonStatus AddPerson(vw_PersonData PersonData, out long PersonID)
        {
            throw new NotImplementedException();
        }

        public PersonStatus DeletePerson(vw_PersonData PersonData)
        {
            throw new NotImplementedException();
        }

        public bool EditPerson(vw_PersonData PersonData)
        {
            throw new NotImplementedException();
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