using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using JIC.Base;
using JIC.Base.Views;
using JIC.Components.Components;

namespace JIC.Services.Services
{
    public class RollsService : ServiceBase, IRollService
    {
        public RollsService() : base(CaseType.Crime)
        {
        }
        public RollsComponent RollsComp { get { return GetComponent<RollsComponent>(); } }
        public SessionsComponent  SessionComp { get { return GetComponent<SessionsComponent>(); } }

        public List<vw_RollCases> GetRollCasesForOpening(int CircuitID ,int SecretaryID )
        {
            List<vw_RollCases> SessionsList= RollsComp.GetRollSessions(CircuitID, SecretaryID);
            foreach (vw_RollCases RC in SessionsList)
            {
                if (SessionComp.IsSessionMinutesSaved((int)RC.SessionID))
                {
                    RC.MinutesOfSession = MinutesStatus.Saved;
                }
                else
                {
                    RC.MinutesOfSession = MinutesStatus.NotSaved ;
                }
            }
            return SessionsList;
        }

        public List<vw_RollCases> GetUnApprovedMovmentCases(int RollID)
        {
            return RollsComp.GetUnApprovedMovmentCases(RollID);
        }

        public List<vw_RollCases> PrintRoll(int RollID)
        {
            throw new NotImplementedException();
        }

        public SaveRollOrderStatus SaveRollOrder(int RollID, List<vw_CaseOrder> CaseOrders)
        {
            try
            {

             return SessionComp.SaveRollOrder( RollID, CaseOrders);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return SaveRollOrderStatus.Failed;
            }
        }

        public SetCasesRollStatus SetCasesRoll(List<int> CaseID, int RollID)
        {
            throw new NotImplementedException();
        }

        public RollStatus OpenSessionRoll(int ProsecuterID, int CourtHall,int RollID)
        {
            try
            {
              
                RollStatus RollStat = RollsComp.OpenSessionRoll( RollID);

                if (RollStat == RollStatus.InProgress)
                {
                    RollsComp.UpdateRollProsecuterAndCourtHall(RollID, ProsecuterID,CourtHall);
                
                    return RollStatus.InProgress;
                }
                else
                {
                    return RollStat;
                }
            }
            catch (Exception ex)
            {
                RollID = 0;
                HandleException(ex);
                return RollStatus.NotStarted ;
            }
        }
        public List<vw_RollCases> GetCasesINRoll(int SessionID)
        {
            return RollsComp.GetCasesINRoll(SessionID);

        }
        public bool UpdateRoll(vw_CaseConfiguration vw_CaseConfiguration)
        {
            return RollsComp.UpdateRoll(vw_CaseConfiguration);
        }
        public int? GetRollID(int circuitid, DateTime SessionDate)
        {
            return RollsComp.GetRollID(circuitid, SessionDate);
        }
        public List<vw_SessionData> GetRollsOpend(int SecretaryID, int UserTypeID)
        {
            return RollsComp.GetRollsOpend(SecretaryID, UserTypeID);

        }
        public List<vw_RollCases> GetRollCasesForOrdering(int CircuitID, int  RollID)
        {
            try
            {
               return  RollsComp.GetSessionRollCasesOrder(CircuitID, RollID);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return null;
            }
        }


        public List<vw_KeyValueDate> GetCircuitRollDates(int CircuitID, int SecretaryID)
        {
            try
            {
                return RollsComp.GetCircuitRollDates (CircuitID ,SecretaryID );
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return null;
            }
        }

        public List<vw_RollCases> GetOpenedRolls(int SecretaryID)
        {
            List<vw_RollCases> SessionsList = RollsComp.GetRollSessionsForOpenedRolls(SecretaryID);
            foreach (vw_RollCases RC in SessionsList)
            {
                if (SessionComp.IsSessionMinutesSaved((int)RC.SessionID))
                {
                    RC.MinutesOfSession = MinutesStatus.Saved;
                }
                else
                {
                    RC.MinutesOfSession = MinutesStatus.NotSaved;
                }
            }
            return SessionsList;
        }

        public void UpdateRollProsecuterAndCourtHall(long RollID, int ProsecuterID, int CourtHall)
        {
            RollsComp.UpdateRollProsecuterAndCourtHall(RollID, ProsecuterID, CourtHall);
        }
    }
}
