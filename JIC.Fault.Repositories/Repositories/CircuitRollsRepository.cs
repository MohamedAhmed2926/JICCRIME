using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.Views;
using JIC.Fault.Entities.Models;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Fault.Repositories.Repositories
{
    public class CircuitRollsRepository : EntityRepositoryBase<CourtConfigurations_CircuitRolls>, ICircuitRollsRepository
    {
        public DeleteStatus DeleteCircuitRollsByCircuitID(int CircuitID)
        {
            var CircuitRolls = (from _circuitRoll in DataContext.CourtConfigurations_CircuitRolls where _circuitRoll.CircuitID == CircuitID select _circuitRoll);
            DataContext.CourtConfigurations_CircuitRolls.RemoveRange(CircuitRolls);
            return DeleteStatus.Deleted;
        }

        public bool HasSession(DateTime fromDate, DateTime toDate)
        {
            return DataContext.CourtConfigurations_CircuitRolls.Where(session => session.SessionDate >= fromDate && session.SessionDate <= toDate).Count() > 0;
        }


        public RollStatus OpenSessionRoll(int RollID)
        {

            var Roll = GetByID(RollID);

            if (IsEmptyRoll(RollID))
                return RollStatus.EmptyRoll;
            //else if (IsPreviousRollsOpened(Roll.SecretaryID, Roll.CircuitID, Roll.ID))
            //    return RollStatus.PreviousRollNotClosed;
            //else if (IsThereOpenRollForSecretary(Roll.SessionDate, Roll.SecretaryID))
            //    return RollStatus.OtherRollOpenForSecretary;
            else if (IsRollNotSorted(RollID))
            {
                return RollStatus.NotSorted;
            }
            else
            {
                Roll.RollStatusID = (int)RollStatus.InProgress;
                Update(Roll);
                Save();

                return RollStatus.InProgress;
            }

        }



        private bool IsThereOpenRollForSecretary(DateTime sessionDate, int? secretaryID)
        {
            return (from Roll in DataContext.CourtConfigurations_CircuitRolls where Roll.SecretaryID == secretaryID && Roll.SessionDate == sessionDate && Roll.RollStatusID != (byte)RollStatus.NotStarted && Roll.ApprovedByJudge == false select Roll).Count() > 0;
        }
        private bool IsPreviousRollsOpened(int? secretaryID, int circuitID, long IgnoreRoll)
        {
            return (from roll in DataContext.CourtConfigurations_CircuitRolls where roll.SecretaryID == secretaryID && roll.CircuitID == circuitID && roll.ApprovedByJudge == false && roll.RollStatusID != (byte)RollStatus.NotStarted && roll.ID != IgnoreRoll select roll).Count() > 0;
        }

        private bool IsRollNotSorted(long rollID)
        {
            return DataContext.Cases_CaseSessions.Where(s => s.RollID == rollID && s.RollIndex == 0 && (s.ApprovedByJudge != true)).Count() > 0;
        }

        private bool IsEmptyRoll(long rollID)
        {
            return DataContext.Cases_CaseSessions.Where(case_session => case_session.RollID == rollID).Count() == 0;
        }


        public List<vw_RollCases> GetSessionRollCases(int CircuitID, int SecretaryID)
        {
            throw new NotImplementedException();
        }
        public List<vw_RollCases> GetCasesINRoll(int SessionID)
        {
            throw new NotImplementedException();
        }
        public List<vw_SessionData> GetRollsOpend(int SecretaryID,int UserTypeID)
        {
            throw new NotImplementedException();
        }
        public List<vw_RollCases> GetSessionRollCasesOrder(int CircuitID, int RollID)
        {
            throw new NotImplementedException();
        }

        public List<vw_KeyValueDate> GetCircuitSessionDates(int CircuitID)
        {
            return (from Circuit in DataContext.CourtConfigurations_CircuitRolls
                    where Circuit.CircuitID == CircuitID && Circuit.SessionDate >= DateTime.Now
                    select new vw_KeyValueDate
                    {
                        ID = (int)Circuit.ID,
                        Date = Circuit.SessionDate
                    }).ToList();
        }

        public List<vw_KeyValueDate> GetCircuitRolls(int CircuitID,int? CaseTypeID = null)
        {
            DateTime DT = DateTime.Now.AddDays(-1);
            return (from rolls in this.DataContext.CourtConfigurations_CircuitRolls
                    where rolls.CaseTypeID == CaseTypeID && rolls.CircuitID == CircuitID
                          && rolls.RollStatusID == (byte)RollStatus.NotStarted && !rolls.GeneratedBySystem && rolls.SessionDate > DT

                    orderby rolls.SessionDate
                    select new
                    {
                        ID = rolls.ID,
                        Date = rolls.SessionDate
                    }).Distinct().ToList().OrderBy(r => r.Date).Select(c => new vw_KeyValueDate
                    {
                        ID = c.ID,
                        Date = c.Date
                    }).ToList();
        }
        public CircuitRollsSavestatus AddRoll(vw_CaseConfiguration caseConfigurationData, out long Roll_ID)
        {
            CourtConfigurations_CircuitRolls CircuitRollsObject = GetAllQuery().Where(circuitRoll => circuitRoll.CaseTypeID == ((vw_FaultCaseConfiguration)caseConfigurationData).CaseTypeID
            && circuitRoll.CircuitID == caseConfigurationData.CircuitID
            && circuitRoll.SessionDate == caseConfigurationData.SessionDate)
            .FirstOrDefault();
            if (CircuitRollsObject == null)
            {
                CircuitRollsObject = new CourtConfigurations_CircuitRolls
                {
                    SessionDate = caseConfigurationData.SessionDate,
                    CircuitID = caseConfigurationData.CircuitID,
                    CaseTypeID = ((vw_FaultCaseConfiguration)caseConfigurationData).CaseTypeID,
                    RollStatusID = (int)RollStatus.NotStarted
                };
                this.Add(CircuitRollsObject);
                this.Save();
            }
            Roll_ID = CircuitRollsObject.ID;
            return CircuitRollsSavestatus.Saved;

        }

        public List<vw_KeyValueDate> GetCircuitRollDates(int CircuitID, int SecretaryID)
        {
            return (from Circuit in DataContext.CourtConfigurations_CircuitRolls
                    where Circuit.CircuitID == CircuitID && Circuit.SecretaryID == Circuit.SecretaryID && Circuit.RollStatusID == (int)RollStatus.NotStarted
                    select new vw_KeyValueDate
                    {
                        ID = (int)Circuit.ID,
                        Date = Circuit.SessionDate
                    }).ToList();
        }


        public List<vw_RollCases> GetUnApprovedMovmentCases(int RollID)
        {

            throw new NotImplementedException();
        }

        public bool IsRollOpened(int RollID)
        {
            return DataContext.CourtConfigurations_CircuitRolls.Where(session => session.ID == RollID && session.RollStatusID != (byte)RollStatus.NotStarted).Count() > 0;

        }

        public bool IsApprovedByJudge(int RollID)
        {
            return DataContext.CourtConfigurations_CircuitRolls.Where(session => session.ID == RollID && session.ApprovedByJudge == true).Count() > 0;

        }

        public bool IsRollExist(int CycleRollID, int CircuitID, DateTime NextSessionDate)
        {
            throw new NotImplementedException();
        }

        public int GetCircuitRollID(int CycleRollID, int CircuitID, DateTime NextSessionDate)
        {
            throw new NotImplementedException();
        }

        public void AddNewCircuitRoll(int CircuitID, int CaseID, int SessionID, DateTime NextSessionDate, out long roll)
        {
            AddRoll(new vw_CaseConfiguration { SessionDate = NextSessionDate, CircuitID = CircuitID }, out roll);
        }

        public List<vw_RollCases> GetRollSessionsForOpenedRolls(int SecretaryID)
        {
            throw new NotImplementedException();
        }

        public void UpdateRollProsecuterAndCourtHall(long RollID, int ProsecuterID, int CourtHall)
        {
            throw new NotImplementedException();

        }

        public DateTime GetSessionDate(int rollID)
        {
            return GetAll().Where(s => s.ID == rollID).Select(d => d.SessionDate).FirstOrDefault();
        }

        public bool UpdateRoll(vw_CaseConfiguration vw_CaseConfiguration)
        {
            throw new NotImplementedException();
        }

        public int? GetRollID(int circuitid, DateTime SessionDate)
        {
            throw new NotImplementedException();
        }
    }
}
