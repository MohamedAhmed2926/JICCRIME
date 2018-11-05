using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.Views;
using JIC.Crime.Entities.Models;
using JIC.Repositories.DBInteractions;

namespace JIC.Crime.Repositories.Repositories
{
   public class SessionRepository : EntityRepositoryBase<Cases_CaseSessions>, ISessionRepository
    {


        public SaveRollOrderStatus SaveRollOrder(int RollID, List<vw_CaseOrder> CaseOrders)
        {
          
            if (IsSessionRollOpened(RollID))
            { return Base.SaveRollOrderStatus.RollOpened; }

            foreach (vw_CaseOrder Order in CaseOrders)
            {
                Cases_CaseSessions  Roll = GetAll().Where(e => e.RollID == RollID && e.CaseID == Order.CaseID).FirstOrDefault(); // (select * from cs in DataContext.Cases_CaseSessions where cs.RollID == RollID && cs.CaseID == Order.CaseID).FirstOrDefault();


                Roll.RollIndex = Order.Order;
                Roll.SecretaryID = Order.SecritaryID;
                                              
                                      
                Update(Roll);
                Save();
            }
            
            return Base.SaveRollOrderStatus.SuccessFull;
        }
        bool IsSessionRollOpened(int RollID)
        {
            return DataContext.CourtConfigurations_CircuitRolls.Where(session => session.RollStatusID !=(int) RollStatus.NotStarted   && session.ID==RollID ).Count() > 0;

        }

    

        public int GetRollID(int SessionID)
        {
            return (int)GetByID(SessionID).RollID;
        }

        public int GetCircuitID(int SessionID)
        {
            return (from cs in DataContext.Cases_CaseSessions
                           join roll in DataContext.CourtConfigurations_CircuitRolls on cs.RollID equals roll.ID
                           where cs.ID == SessionID
                           select roll.CircuitID).FirstOrDefault();
        }

        public void AddSession(vw_SessionData SessionData)
        {
            Cases_CaseSessions SessionObj = new Cases_CaseSessions();
            Add(new Cases_CaseSessions
            {
                CaseID = SessionData.CaseID,
                RollID = SessionData.RollID,
                DoneByDefaultCircuit = SessionData.DoneByDefaultCircuit ,
                ApprovedByJudge = SessionData.ApprovedByJudge ,
                RollIndex = SessionData.RollIndex 

            });

        }

        public void SaveMinutesOfSession(vw_MinutesOfSession MinutesOfSession)
        {
            Cases_CaseSessions session = GetByID(MinutesOfSession.SessionID);
            session.MunitesOfSession = MinutesOfSession.Text;
            Update(session);
            Save();
        }

        public string GetMinutesOfSession(int SessionID)
        {
            Cases_CaseSessions session = GetByID(SessionID);
            if (session != null)
            {
                return session.MunitesOfSession;
            }
            else { return null; }
        }

        public vw_SessionData GetSessionData(int SessionID)
        {
            return (from cs in DataContext.Cases_CaseSessions
                    join roll in DataContext.CourtConfigurations_CircuitRolls on cs.RollID equals roll.ID
                    join prosecutor in DataContext.Configurations_Prosecuters on roll.ProsecuterID equals prosecutor.ID
                    join user in DataContext.Users on cs.SecretaryID equals user.Id
                    join person in DataContext.Configurations_Persons on user.PersonsId equals person.ID
                    where cs.ID == SessionID
                    select new vw_SessionData
                    {
                        CaseID = cs.CaseID,
                        SessionID = (int)cs.ID,
                        SessionDate = roll.SessionDate,
                        RollID = roll.ID,
                        ProsecutorName=prosecutor.Name,
                        SecretaryAssistantName=person.FullName
                   //     DayName = roll.SessionDate.DayOfWeek.ToString(), 

            }).FirstOrDefault();

           

        }

        public bool IsApprovedByJudge(int SessionID)
        {
            return GetAll().Where(session => session.ID == SessionID && session.ApprovedByJudge==true).Count() > 0;

        }

        public bool IsSessionMinutesSaved(int SessionID)
        {
            return GetAll().Where(session => session.ID == SessionID && session.MunitesOfSession != null).Count() > 0;

        }

        public void ApproveSessionByJudge(int SessionID)
        {
            Cases_CaseSessions session = GetByID(SessionID);
            session.ApprovedByJudge = true;
            Update(session);
            Save();
        }

        public List<vw_SessionData> GetSessionDataByCaseID(int CaseID)
        {
            List<vw_SessionData> SessionData = new List<vw_SessionData>();
           var sessions=  GetAll().Where(session => session.CaseID  == CaseID );
            foreach (var s in sessions)
            {
                SessionData.Add(new vw_SessionData {ID=(int)s.ID,MunitesOfSession =s.MunitesOfSession  });
            }
            return SessionData;


        }

        public IQueryable<vw_KeyValueDate> SessionByCircut(int CircuitId)
        {
            var result=(from cs in DataContext.CourtConfigurations_Circuits
                    join roll in DataContext.CourtConfigurations_CircuitRolls on cs.ID equals roll.CircuitID
                    where cs.ID == CircuitId
                    select new vw_KeyValueDate
                    {
                        ID = roll.ID,
                        Date = roll.SessionDate

                        //     DayName = roll.SessionDate.DayOfWeek.ToString(), 

                    });
            return result;
        }

        public void SendSessionToJudge(int SessionID)
        {
            var session = GetByID(SessionID);
            session.IsSentToJudge = true;
            //if (cases.NewCaseStatusID != null)
            //{ cases.CaseStatusID = (int)cases.NewCaseStatusID; }
            Update(session);
            Save();
        }
        public bool IsSentToJudge(int SessionID)
        {
            return GetAll().Where(session => session.ID == SessionID && session.IsSentToJudge == true).Count() > 0;
        }
        public List<int> GetRollIDs(List<int> CaseIDs)
        {
            List<int> Rolls = new List<int>();
            foreach (var cases in CaseIDs)
                Rolls.Add(int.Parse(DataContext.Cases_CaseSessions.Single(e => e.CaseID == cases).RollID.ToString()));
            return Rolls;
        }
        public List<vw_RollCases> GetSessionsToApprove(int JudgeID,int RollID)
        {

           return (from cs in DataContext.Cases_CaseSessions
            join roll in DataContext.CourtConfigurations_CircuitRolls on cs.RollID equals roll.ID
            join circuit in DataContext.CourtConfigurations_Circuits on roll.CircuitID equals circuit.ID
            join circuitmem in DataContext.CourtConfigurations_CircuitMembers on circuit.ID equals circuitmem.CircuitID 
            join _Case in DataContext.Cases_Cases on cs.CaseID equals _Case.ID
            join _MasterCase in DataContext.Cases_MasterCase on _Case.MasterCaseID equals _MasterCase.ID
                  join _PoliceStation in DataContext.Configurations_PoliceStations on _MasterCase.PoliceStationID equals _PoliceStation.ID
                  join _Courts in DataContext.Configurations_Courts on _Case.CourtID equals _Courts.ID
                  join prosecution in DataContext.Configurations_Prosecutions on _PoliceStation.ProsecutionID equals prosecution.ID
                   join overall in DataContext.Configurations_OverallNumbers on _MasterCase.OverallID equals overall.ID
                   join crimes in DataContext.Configurations_Lookups on _MasterCase.CrimeID equals crimes.ID
                   where cs.IsSentToJudge  == true && _Case.IsDeleted != true 
                   && circuitmem.UserID == JudgeID && cs.ApprovedByJudge==false && roll.ID==RollID
                   && circuitmem.JudgeType == (int)JudgePodiumType.HeadJudge 
                   select new vw_RollCases
                {
                    SecretaryID = cs.SecretaryID,
                    FirstLevelNumber = _MasterCase.FirstLevelNumber + " / " + _MasterCase.FirstLevelYear + " / " + _PoliceStation.Configurations_Prosecutions.Name,
                    SecondLevelNumber = _MasterCase.SecondLevelNumber + " / " + _MasterCase.SecondLevelYear + " / " + _MasterCase.Configurations_Prosecutions.Name,
                    OverAllNumber = overall.InclosiveSierial + " / " + overall.Year + " / " + overall.Configurations_Prosecutions.Name,
                    Order = cs.RollIndex,
                    MainCrime = crimes.Name,
                    CaseID = _Case.ID,
                    CircuitID = circuit.ID,
                    SessionID = cs.ID,
                    RollDate = roll.SessionDate,
                    Title=_MasterCase.Title,

                }).ToList ();

        }

        public List<vw_KeyValueDate> GetSentToJudgeSessionDates(int judgeID)
        {
            return (from cs in DataContext.Cases_CaseSessions
                    join roll in DataContext.CourtConfigurations_CircuitRolls on cs.RollID equals roll.ID
                    join circuit in DataContext.CourtConfigurations_Circuits on roll.CircuitID equals circuit.ID
                    join circuitmem in DataContext.CourtConfigurations_CircuitMembers on circuit.ID equals circuitmem.CircuitID
                 
                    where cs.IsSentToJudge == true && circuitmem.UserID == judgeID && cs.ApprovedByJudge == false 
                    && circuitmem.JudgeType==(int)JudgePodiumType.HeadJudge  && circuitmem.ToDate == null
                    select new vw_KeyValueDate
                    {
                        ID=roll.ID,
                        Date=roll.SessionDate 
                        
                       
                    }).ToList();
        }

     
        public void ReturnSessionToSecretary(int SessionID)
        {
            Cases_CaseSessions session = GetByID(SessionID);
           
            session.IsSentToJudge = false;
            Update(session);
            Save();
        }
    }
}
