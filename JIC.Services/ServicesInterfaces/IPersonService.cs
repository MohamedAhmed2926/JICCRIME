using JIC.Base;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.ServicesInterfaces
{
    public interface IPersonService
    {
        PersonStatus AddPerson(vw_PersonData PersonData, out long PersonID);
        PersonStatus EditPerson(vw_PersonData PersonData);
        PersonStatus DeletePerson(vw_PersonData PersonData);
        List<vw_PersonData> GetPersons(string natID);
        vw_PersonData GetPerson(long personID);
        bool IsPassportNoExist(string text, long? ignoreID);
        bool IsNationalNoExist(string text, long? ignoreID);
        int NumberOfAttachedCases(long iD);
    }
}
