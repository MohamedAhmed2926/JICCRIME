using System;
using System.Collections.Generic;
using JIC.Base;
using JIC.Services.ServicesInterfaces;
using JIC.Base.Views;
using JIC.Components.Components;

namespace JIC.Services.Services
{
    public class PersonService : ServiceBase, IPersonService
    {
        private PersonComponent PersonComponent { get { return GetComponent<PersonComponent>(); } }
        public PersonService(CaseType caseType) : base(caseType)
        {
        }

        public PersonStatus AddPerson(vw_PersonData PersonData, out long PersonID)
        {
            try
            {
                if (PersonData.CleanFullName == null || PersonData.CleanFullName.Trim().Equals(""))
                    PersonData.CleanFullName = Utilities.RemoveSpaces(PersonData.Name);
                if (IsNationalNoExist(PersonData.NatNo, null))
                {
                    PersonID = 0;
                    return PersonStatus.NatIDExist;
                }
                else if (PersonComponent.AddPerson(PersonData, out PersonID))
                    return PersonStatus.SuccefullSave;

                return PersonStatus.FailedSave;
            }catch(Exception ex)
            {
                HandleException(ex);
                PersonID = 0;
                return PersonStatus.FailedSave;
            }
        }
        public PersonStatus EditPerson(vw_PersonData PersonData)
        {
            try
            {
                if (IsNationalNoExist(PersonData.NatNo, PersonData.ID))

                    return PersonStatus.NatIDExist;

                else
                    return PersonComponent.EditPerson(PersonData);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return PersonStatus.FailedSave;
        }

        public PersonStatus DeletePerson(vw_PersonData PersonData)
        {
           
            if (PersonComponent.CanBeDeleted(PersonData.ID))
                return PersonComponent.DeletePerson(PersonData.ID);
            else
                return PersonStatus.FailedSave;
        }

        public List<vw_PersonData> GetPersons(string natID)
        {
            return PersonComponent.GetPersons(natID);
        }

        public vw_PersonData GetPerson(long personID)
        {
            try
            {
                return PersonComponent.FindPerson(personID);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return null;
        }

        public bool IsPassportNoExist(string text, long? ignoreID)
        {
            try
            {
                return PersonComponent.IsPassportNoExist(text, ignoreID);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return false;
        }

        public bool IsNationalNoExist(string text, long? ignoreID)
        {
            try
            {
                return PersonComponent.IsNationalNoExist(text, ignoreID);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return false;
        }

        public int NumberOfAttachedCases(long PersonID)
        {
            try
            {
                return PersonComponent.NumberOfAttachedCases(PersonID);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return 0;
        }
    }
}
