using JIC.Base.Interfaces;
using JIC.Fault.Entities.Models;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base.Views;
using JIC.Base.Resources;
using JIC.Base;

namespace JIC.Fault.Repositories.Repositories
{
   public class VictimsRepository : EntityRepositoryBase<Cases_CaseVictims>, IVictimsRepository
    {
        public void AddVictim(int caseID, vw_VictimData vw_VictimData)
        {
            var Victim = new Cases_CaseVictims
            {
                CaseID = caseID,
                IsActive = true,
                IsCivilRightProsecutor = vw_VictimData.IsCivilRights,
                PersonID = vw_VictimData.PersonID,
                //Order = vw_VictimData.Order
            };
            Add(Victim);
            Save();
            vw_VictimData.VictimID = Victim.ID;
        }
   
       public SavePartSOrder SaveOrderVictim(vw_CaseDefectData party)
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
        public void DeleteVictim(int CaseID, long VictimID)
        {
            try
            {
                var VictimObj = GetAllQuery().Where(x => x.CaseID == CaseID && x.ID == VictimID).FirstOrDefault(); //GetByID(VictimID );
                Delete(VictimObj);
                Save();
            }
            catch (Exception ex) {
                
            }
            //ReOrderVictimOrders(CaseID);
        }

        public void EditVictim(int CaseID, vw_VictimData vw_VictimData)
        {
            var Victim = GetAllQuery().Where(victim => victim.ID == vw_VictimData.VictimID).First();
            Victim.IsCivilRightProsecutor = vw_VictimData.IsCivilRights;
            Victim.PersonID = vw_VictimData.PersonID;
            //Victim.Order = vw_VictimData.Order;
            Update(Victim);
            Save();
        }

        public int GetLastVictimOrder(long caseID)
        {
            return GetAllQuery().Where(victim => victim.CaseID == caseID && victim.IsActive).Count();
        }

        public vw_CaseDefectsData GetVictim(int caseID, long partyID)
        {
            return (from _victim in DataContext.Cases_CaseVictims
                    join _person in DataContext.Configurations_Persons on _victim.PersonID equals _person.ID
                    where _victim.ID == partyID && _victim.CaseID == caseID
                    select new vw_CaseDefectsData
                    {
                        CaseID = caseID,
                        Birthdate = _person.Birthdate,
                        DefectType = PartyTypes.Victim,
                        IsCivilRightProsecutor = _victim.IsCivilRightProsecutor,
                        ID = partyID,
                        Address = _person.Address,
                        JobName = _person.JobTitle,
                        Name = _person.FullName,
                        NationalID = _person.NationalID,
                        PassportNumber = _person.PassportNumber,
                        NationalityType = _person.NationalityID,
                        PersonID= _victim.PersonID,
                   
                    }).First();
        }

        public List<vw_CaseDefectsData> GetVictimsByCaseID(int CaseID,int SessionID)
        {
            var victims = (from _case in DataContext.Cases_Cases
                           join _session in DataContext.Cases_CaseSessions on _case.ID equals _session.CaseID
                           join _victim in DataContext.Cases_CaseVictims on _case.ID equals _victim.CaseID
                           join _personVictim in DataContext.Configurations_Persons on _victim.PersonID equals _personVictim.ID
                           join _victimsSessionLog in DataContext.Cases_VictimsSessionsLog on _victim.ID equals _victimsSessionLog.VictimID into VSLog
                           from _vslog in VSLog.DefaultIfEmpty()
                           join _PresenceStatus in DataContext.Configurations_Lookups on _vslog.PresenceStatusID equals _PresenceStatus.ID into PStatus
                           from _ps in PStatus.DefaultIfEmpty()
                           where _case.ID == CaseID && _session.ID == SessionID
                           select new vw_CaseDefectsData
                           {
                               ID = _victim.ID,
                               PersonID  = _victim.PersonID ,
                               Name = _personVictim.FullName,
                               NationalID = _personVictim.NationalID,
                               DefectType  = PartyTypes.Victim,
                               IsCivilRightProsecutor = _victim.IsCivilRightProsecutor,
                               Presence = _vslog == null ? 0 :(PresenceStatus) _vslog.PresenceStatusID,
                               JobName = _personVictim.JobTitle,
                               NationalityType = _personVictim.NationalityID,
                               PassportNumber = _personVictim.PassportNumber,
                               Birthdate = _personVictim.Birthdate,
                               //Order = _victim.Order
                           }).ToList();

            return victims; 
        }

        public List<vw_CaseDefectsData> GetVictimsByCaseID(int CaseID)
        {
            var victims = (from _case in DataContext.Cases_Cases
                           join _victim in DataContext.Cases_CaseVictims on _case.ID equals _victim.CaseID
                           join _personVictim in DataContext.Configurations_Persons on _victim.PersonID equals _personVictim.ID
                           where _case.ID == CaseID && _victim.IsActive
                           select new vw_CaseDefectsData
                           {
                               PersonID = _personVictim.ID,
                               Name = _personVictim.FullName,
                               NationalID = _personVictim.NationalID,
                               DefectType = PartyTypes.Victim,
                               IsCivilRightProsecutor = _victim.IsCivilRightProsecutor,
                               JobName = _personVictim.JobTitle,
                               NationalityType = _personVictim.NationalityID,
                               PassportNumber = _personVictim.PassportNumber,
                               Birthdate = _personVictim.Birthdate,
                               //Order = _victim.Order,
                               ID = _victim.ID 
                           }).ToList();

            return victims;
        }

        public bool IsPersonInCase(long personID, int caseID)
        {
            return GetAllQuery().Where(victim => victim.CaseID == caseID && victim.PersonID == personID).Count() > 0;
        }

        public void ReOrderVictimOrders(int caseID)
        {
            throw new NotImplementedException();

        }
    }
}
