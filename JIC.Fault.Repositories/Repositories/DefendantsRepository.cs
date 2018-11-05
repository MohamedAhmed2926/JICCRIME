using JIC.Base.Interfaces;
using JIC.Fault.Entities.Models;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base.Views;
using JIC.Base;

namespace JIC.Fault.Repositories.Repositories
{
    public class DefendantsRepository : EntityRepositoryBase<Cases_CaseDefendants>, IDefendantsRepository
    {
        public void AddDefendant(int caseID, vw_DefendantData vw_DefendantData)
        {
            var CaseDefendant = new Cases_CaseDefendants
            {
                CaseID = caseID,
                IsActive = true,
                IsCivilRightProsecutor = vw_DefendantData.IsCivilRights,
                PersonID = vw_DefendantData.PersonID,
                //Order = vw_DefendantData.Order
            };
            Add(CaseDefendant);
            Save();
            vw_DefendantData.DefendantID = CaseDefendant.ID;
        }

        public void DeleteDefendant(int CaseID,long DefendantID)
        {
            var DefendantObj = GetAllQuery().Where(x => x.CaseID == CaseID && x.ID == DefendantID).FirstOrDefault(); //GetByID(VictimID );

            Delete(DefendantObj);
            Save();
            //ReOrderDefendantOrders(CaseID);
        }

        public void EditDefendant(int caseID, vw_DefendantData vw_DefendantData)
        {
            var CaseDefendant = GetByID(vw_DefendantData.DefendantID);
            CaseDefendant.CaseID = caseID;
            CaseDefendant.IsCivilRightProsecutor = vw_DefendantData.IsCivilRights;
            CaseDefendant.PersonID = vw_DefendantData.PersonID;
           // CaseDefendant.Order = vw_DefendantData.Order;
            
            Update(CaseDefendant);
            Save();
            vw_DefendantData.DefendantID = CaseDefendant.ID;
        }

        public vw_CaseDefectsData GetDefendant(int caseID, long partyID)
        {
            return GetAllQuery().Where(defendant => defendant.CaseID == caseID && defendant.ID == partyID)
                .Select(defendant => new vw_CaseDefectsData
                {
                    ID = defendant.ID,
                    CaseID = caseID,
                    Crimes = defendant.Cases_DefendatnsCaseLog.Last().Cases_DefendantsCharges.Select(charge =>new vw_KeyValue { ID = charge.Configurations_Lookups.ID, Name = charge.Configurations_Lookups.Name }).ToList(),
                    DefectType = PartyTypes.Defendant,
                    IsCivilRightProsecutor = defendant.IsCivilRightProsecutor,
                    //Order = defendant.Order,
                    PersonID = defendant.PersonID,
                    Status = defendant.Cases_DefendatnsCaseLog.Where(defendantCaseLog=>defendantCaseLog.ToDate == null).FirstOrDefault().PoliceStationStatusID,
                    Address = defendant.Configurations_Persons.Address,
                    Birthdate = defendant.Configurations_Persons.Birthdate,
                    JobName = defendant.Configurations_Persons.JobTitle,
                    Name = defendant.Configurations_Persons.FullName,
                    NationalID = defendant.Configurations_Persons.NationalID,
                    PassportNumber = defendant.Configurations_Persons.PassportNumber,
                    NationalityType = defendant.Configurations_Persons.NationalityID,
                }).First();
        }
        public SavePartSOrder SaveOrderDefect(vw_CaseDefectData party)
        {
            try
            {
                var defect = GetByID(party.ID);
                //defect.Order = party.Order;
                Update(defect);
                Save();
                return SavePartSOrder.SavedOrder;
            }
            catch
            {
                return SavePartSOrder.Faild_To_Save;
            }

        }
        public List<vw_CaseDefectsData> GetDefendantsByCaseID(int CaseID, int SessionID)
        {
            var defendants = (from _case in DataContext.Cases_Cases
                              join _session in DataContext.Cases_CaseSessions on _case.ID equals _session.CaseID
                              join _defendant in DataContext.Cases_CaseDefendants on _case.ID equals _defendant.CaseID
                              join _defendantCaseLog in DataContext.Cases_DefendatnsCaseLog on _defendant.ID equals _defendantCaseLog.DefendantID
                              join _personDefendant in DataContext.Configurations_Persons on _defendant.PersonID equals _personDefendant.ID
                              join Look in DataContext.Configurations_Lookups on _personDefendant.NationalityID equals Look.ID
                              join _defendantsSessionLog in DataContext.Cases_DefendatnsSessionsLog on _defendant.ID equals _defendantsSessionLog.DefendantID into DSLog
                              from _dslog in DSLog.DefaultIfEmpty()
                              join _PresenceStatus in DataContext.Configurations_Lookups on _dslog.PresenceStatusID equals _PresenceStatus.ID into PStatus
                              from _ps in PStatus.DefaultIfEmpty()
                              join _PoliceStationStatus in DataContext.Configurations_Lookups on _defendantCaseLog.PoliceStationStatusID equals _PoliceStationStatus.ID
                              
                              where _case.ID == CaseID && _session.ID == SessionID && !_defendantCaseLog.ToDate.HasValue
                              select new vw_CaseDefectsData
                              {

                                  ID = (int)_defendant.ID,
                                  PersonID = _defendant.PersonID,
                                  Name = _personDefendant.FullName,
                                  NationalID = _personDefendant.NationalID,
                                  DefectType = PartyTypes.Defendant,
                                  IsCivilRightProsecutor = _defendant.IsCivilRightProsecutor,
                                  Presence = _dslog == null ? 0 : (PresenceStatus)_dslog.PresenceStatusID,
                                  Status = _defendantCaseLog.PoliceStationStatusID,
                                  JobName = _personDefendant.JobTitle,
                                  NationalityType = _personDefendant.NationalityID,
                                  PassportNumber = _personDefendant.PassportNumber,
                                  Birthdate = _personDefendant.Birthdate,
                                  //Order = _defendant.Order,
                                  Nationality=Look.Name
                                  // Age = DateTime.Now.Year - (DateTime)_personDefendant.Year
                              }).Distinct().ToList();
            return defendants;
        }

        public List<vw_CaseDefectsData> GetDefendantsByCaseID(int CaseID)
        {
            var defendants = (from _case in DataContext.Cases_Cases
                              join _defendant in DataContext.Cases_CaseDefendants on _case.ID equals _defendant.CaseID
                              join _personDefendant in DataContext.Configurations_Persons on _defendant.PersonID equals _personDefendant.ID

                              where _case.ID == CaseID && _defendant.IsActive
                              select new vw_CaseDefectsData
                              {
                                  PersonID = _personDefendant.ID,
                                  Name = _personDefendant.FullName,
                                  NationalID = _personDefendant.NationalID,
                                  DefectType = PartyTypes.Defendant,
                                  IsCivilRightProsecutor = _defendant.IsCivilRightProsecutor,
                                  JobName = _personDefendant.JobTitle,
                                  NationalityType = _personDefendant.NationalityID,
                                  PassportNumber = _personDefendant.PassportNumber,
                                  Birthdate = _personDefendant.Birthdate,
                                  //Order = _defendant.Order,
                                  ID = _defendant.ID,
                                  Status = _defendant.Cases_DefendatnsCaseLog.Where(defendantCaseLog=>defendantCaseLog.ToDate == null).FirstOrDefault().PoliceStationStatusID
                                  //Age = DateTime.Now.Year - _personDefendant.Year
                              }).ToList();
            return defendants;
        }

        public int GetLastDefendantOrder(long caseID)
        {
            return GetAllQuery().Where(defendant => defendant.CaseID == caseID && defendant.IsActive).Count();
        }

        public string GetName(int iD)
        {
            return GetAllQuery().Where(defendant => defendant.ID==iD)
                .Select(defendant =>  defendant.Configurations_Persons.FullName
                ).First();
        }

        public bool HasDefendant(int caseID)
        {
            return GetAllQuery().Where(defendant => defendant.CaseID == caseID).Count() > 0;
        }

        public bool IsPersonInCase(long personID, int caseID)
        {
            return GetAllQuery().Where(defendant => defendant.CaseID == caseID && defendant.PersonID == personID).Count() > 0;
        }

        public void ReOrderDefendantOrders(int caseID)
        {
            throw new NotImplementedException();

        }

        public CaseStatus CaseInFlow(int CaseID)
        {
            throw new NotImplementedException();
        }
    }

}
