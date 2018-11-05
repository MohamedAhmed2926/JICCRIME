using JIC.Base.Interfaces;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Views;
using JIC.Crime.Entities.Models;

namespace JIC.Crime.Repositories.Repositories
{
    public class CaseWitnessesRepository : EntityRepositoryBase<JIC.Crime.Entities.Models.Cases_CaseWitnesses>, ICaseWitnessesRepository
    {
        public void AddNewWitness(int PersonID, int CaseID, byte[] FileData, int UserID)
        {
            var CW = new Cases_CaseWitnesses();
            CW.CaseID = CaseID;
            CW.PersonID = PersonID ;
            CW.TestimonyFileData = FileData;
            CW.UserID = UserID;
            
            this.Add(CW);
            this.Save();
        }

        public List<vw_CaseDefectsData> GetWitnessesByCaseID(int CaseID)
        {
        
            return (from cw in DataContext.Cases_CaseWitnesses
                    join Person in DataContext.Configurations_Persons on cw.PersonID equals Person.ID
                    join cases in DataContext.Cases_Cases on cw.CaseID equals cases.ID
                    join Lookup in DataContext.Configurations_Lookups on Person.NationalityID equals Lookup.ID
                    //join WitnessSessionLog in DataContext.Cases_WitnessSessionLog on cw.ID equals WitnessSessionLog.WitnessID
                    where cw.CaseID == CaseID && cases.IsDeleted != true
                    select new vw_CaseDefectsData
                    {
                        PersonID = Person.ID,
                        Name = Person.FullName,
                        NationalID = Person.NationalID,
                        JobName = Person.JobTitle,
                        NationalityType = Person.NationalityID,
                        NationalityName = Lookup.Name,
                        PassportNumber = Person.PassportNumber,
                        Birthdate = Person.Birthdate,
                        ID = cw.ID,
                        Address = Person.Address,
                        WitnessTestimonyFile = cw.TestimonyFileData,
                        
                        //Attendence=WitnessSessionLog.PresenceStatus
                    }).ToList();
        }

        public AddTestimonyStatus AddTestimonyToWitness(int CaseID, int SessionID, string TestimonyText, int WitnessID, byte[] FileData)
        {
            throw new NotImplementedException();
        }

        public AddWitnessStatus ConnectPersonToCaseAsWitness(int PersonID, int CaseID, int UserID, SystemUserTypes UserType)
        {
            throw new NotImplementedException();
        }

      
        public bool IsConnectedToTheCase(int witnessID, int caseID)
        {
            return GetAll().Where(z => z.CaseID  == caseID && z.PersonID == witnessID).Count() > 0;
        }

        public UpdatePresenceStatus UpdateWitnessesPresence(List<vw_WitnessAttendance> WitnessesAttendanceList)
        {
            throw new NotImplementedException();
        }
    }
}
