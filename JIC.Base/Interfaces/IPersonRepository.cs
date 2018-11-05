using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base.Views;

namespace JIC.Base.Interfaces
{
    public interface IPersonRepository
    {
        bool AddPerson(vw_PersonData personData, out long personID);
        PersonStatus EditPerson(vw_PersonData personData);
        List<vw_PersonData> GetPersons(string natID);
        bool CanDeletePerson(long iD);
        PersonStatus Delete(long iD);
        vw_PersonData FindByID(long personID);
        bool IsNationalNoExist(string text, long? ignoreID);
        bool IsPassportNoExist(string text, long? ignoreID);
        int NumberOfAttachedCases(long personID);
    }
}
