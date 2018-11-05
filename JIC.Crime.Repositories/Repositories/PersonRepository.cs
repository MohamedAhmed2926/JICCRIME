using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.Views;
using JIC.Crime.Entities.Models;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Crime.Repositories.Repositories
{
    public class PersonRepository : EntityRepositoryBase<Configurations_Persons>, IPersonRepository
    {
        public bool AddPerson(vw_PersonData personData, out long personID)
        {
            personID = 0;
            var Person = new Configurations_Persons
            {
                Address = (personData.address != null ? personData.address.address +"/"+ personData.address.CityID+"/"+ personData.address.PoliceStationID: null),
                Birthdate = personData.BirthDate,
                CleanFullName = personData.CleanFullName,
                FullName = personData.Name,
                NationalityID = personData.NationalityID,
                IsLegalPerson = true,
                PassportNumber = personData.PassportNo,
                NationalID = personData.NatNo,
                JobTitle = personData.Job,
                IsEgyption = (personData.NationalityID == (int)Base.Nationality.Egyptian)
            };
            Add(Person);
            Save();
            personID = Person.ID;
            return true;
        }

        public bool CanDeletePerson(long ID)
        {
            return DataContext.Users.Where(user => user.PersonsId == ID).Count() == 0
                && DataContext.Cases_CaseDefendants.Where(defendant => defendant.PersonID == ID).Count() == 0
                && DataContext.Cases_CaseVictims.Where(victim => victim.PersonID == ID).Count() == 0;
        }

        public PersonStatus Delete(long ID)
        {
            var Person = this.GetByID(ID);
            Delete(Person);
            Save();
            return PersonStatus.SuccefullSave;
        }

        public PersonStatus EditPerson(vw_PersonData personData)
        {
            if (personData.ID == 0)
            {
                long ID;
                var AddPersonStatus = AddPerson(personData, out ID);
                personData.ID = ID;
                return AddPersonStatus ? PersonStatus.SuccefullSave : PersonStatus.FailedSave;
            }
            else
            {
                var Person = this.GetByID(personData.ID);
                Person.PassportNumber = personData.PassportNo;
                Person.NationalityID = personData.NationalityID;
                Person.NationalID = personData.NatNo;
                Person.JobTitle = personData.Job;
                Person.FullName = personData.Name;
                Person.Address = (personData.address != null ? personData.address.address + "/" + personData.address.CityID + "/" + personData.address.PoliceStationID : null);
               
                Person.Birthdate = personData.BirthDate;
                
                Person.CleanFullName = Base.Utilities.RemoveSpaces(Base.Utilities.RemoveSpecialCharacters(personData.Name));
                Update(Person);
                Save();
                return PersonStatus.SuccefullSave;
            }
        }

        public vw_PersonData FindByID(long personID)
        {
            var Person = this.GetByID(personID);
            return new vw_PersonData
            {
                ID = Person.ID,
                address = new vw_Address { address = Person.Address },
                BirthDate = Person.Birthdate,
                CleanFullName = Person.CleanFullName,
                Job = Person.JobTitle,
                Name = Person.FullName,
                NationalityID = Person.NationalityID,
                NatNo = Person.NationalID,
                PassportNo = Person.PassportNumber
            };
        }

        public List<vw_PersonData> GetPersons(string natID)
        {
            return DataContext.Configurations_Persons.Where(person => person.NationalID == natID)
                .Select(person => new vw_PersonData
                {
                    ID = person.ID,
                    BirthDate = person.Birthdate.Value,
                    Job = person.JobTitle,
                    Name = person.FullName,
                    CleanFullName = person.CleanFullName,
                    NationalityID = person.NationalityID,
                    NatNo = person.NationalID,
                    PassportNo = person.PassportNumber,
                    address = new vw_Address { address = person.Address },

                }).ToList();
        }
        public bool IsNationalNoExist(string text, long? ignoreID)
        {
            if (String.IsNullOrEmpty(text))
                return false;

            return (from person in GetAllQuery()
                    where person.NationalID == text && ((person.ID != ignoreID && ignoreID.HasValue) || !ignoreID.HasValue)
                    select person
             ).Count() > 0;
        }

        public bool IsPassportNoExist(string text, long? ignoreID)
        {
            return (from person in GetAllQuery()
                    where person.PassportNumber == text && ((person.ID != ignoreID && ignoreID.HasValue) || !ignoreID.HasValue)
                    select person
             ).Count() > 0;
        }

        public int NumberOfAttachedCases(long personID)
        {
            return DataContext.Cases_Cases.Where(_Case => _Case.Cases_CaseDefendants.Any(defendant => defendant.PersonID == personID) || _Case.Cases_CaseVictims.Any(victim => victim.PersonID == personID)).Count();

        }
    }
}
