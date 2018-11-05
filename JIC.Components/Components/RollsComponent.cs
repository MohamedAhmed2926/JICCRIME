using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Components.Components
{
    public class RollsComponent
    {
        ICircuitRollsRepository CircuitRollsRepository;

        public RollsComponent( ICircuitRollsRepository CircuitRollsRepository)
        {
            this.CircuitRollsRepository = CircuitRollsRepository;
        }


        public bool HasSession(DateTime fromDate , DateTime toDate)
        {
            return CircuitRollsRepository.HasSession(fromDate , toDate);
        }

       public DeleteStatus DeleteCircuitRollsByCircuitID(int CircuitID)
        {
            return CircuitRollsRepository.DeleteCircuitRollsByCircuitID(CircuitID );
        }

        public RollStatus OpenSessionRoll(int  RollID)
        {
            return CircuitRollsRepository.OpenSessionRoll( RollID);
        }
        public List<vw_SessionData> GetRollsOpend(int SecretaryID,int UserTypeID)
        {
            return CircuitRollsRepository.GetRollsOpend(SecretaryID, UserTypeID);

        }
        public List<vw_RollCases> GetRollSessions(int CircuitID,int SecretaryID)
        {
            return CircuitRollsRepository.GetSessionRollCases(CircuitID, SecretaryID);
        }

        public List<vw_RollCases> GetSessionRollCasesOrder(int CircuitID, int  RollID)
        {
            return CircuitRollsRepository.GetSessionRollCasesOrder( CircuitID, RollID );
        }
        public List<vw_RollCases> GetCasesINRoll(int SessionID)
        {
            return CircuitRollsRepository.GetCasesINRoll(SessionID);
        }

        public bool UpdateRoll(vw_CaseConfiguration vw_CaseConfiguration)
        {
            return CircuitRollsRepository.UpdateRoll(vw_CaseConfiguration);
        }
        public int? GetRollID(int circuitid, DateTime SessionDate)
        {
            return CircuitRollsRepository.GetRollID(circuitid, SessionDate);
        }
        public List<vw_KeyValueDate> GetCircuitRollDates(int CircuitID, int SecretaryID)
        {
            return CircuitRollsRepository.GetCircuitRollDates (CircuitID,SecretaryID );
        }
        public List<vw_KeyValueDate> GetCircuitRolls(int CircuitID)
        {
            return CircuitRollsRepository.GetCircuitRolls(CircuitID);
        }
        public List<vw_RollCases> GetUnApprovedMovmentCases( int RollID)
        {
            return CircuitRollsRepository.GetUnApprovedMovmentCases(RollID);
        }
        public bool IsRollOpened(int RollID)
        {
            return CircuitRollsRepository.IsRollOpened(RollID);
        }
        public bool IsApprovedByJudge(int RollID)
        {
            return CircuitRollsRepository.IsApprovedByJudge (RollID);
        }
        public bool IsRollExist(int CycleRollID, int CircuitID, DateTime NextSessionDate)
        {
            return CircuitRollsRepository.IsRollExist( CycleRollID,  CircuitID,  NextSessionDate);
        }
        public int GetCircuitRollID(int CycleRollID, int CircuitID, DateTime NextSessionDate)
        {
            return CircuitRollsRepository.GetCircuitRollID(CycleRollID, CircuitID, NextSessionDate);
        }
        public void AddNewCircuitRoll(int CircuitID,DateTime NextSessionDate, out long roll)
        {
             CircuitRollsRepository.AddRoll(new vw_CaseConfiguration { SessionDate = NextSessionDate,CircuitID = CircuitID}, out roll);
        }

      public  List<vw_RollCases> GetRollSessionsForOpenedRolls(int SecretaryID)
        { return CircuitRollsRepository.GetRollSessionsForOpenedRolls(SecretaryID ); }

        public void UpdateRollProsecuterAndCourtHall(long RollID, int ProsecuterID, int CourtHall)
        {
            CircuitRollsRepository.UpdateRollProsecuterAndCourtHall(RollID, ProsecuterID, CourtHall);
        }

        public DateTime GetSessionDate(int rollID)
        {
          return  CircuitRollsRepository.GetSessionDate(rollID);
        }
    }
}
