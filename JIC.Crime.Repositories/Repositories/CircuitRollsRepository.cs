using JIC.Base.Interfaces;
using JIC.Crime.Entities.Models;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Views;

namespace JIC.Crime.Repositories.Repositories
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
            return DataContext.Cases_CaseSessions.Where (s => s.RollID == rollID && s.RollIndex ==0 && (s.ApprovedByJudge != true)).Count()>0;
        }

        private bool IsEmptyRoll(long rollID)
        {
            return DataContext.Cases_CaseSessions.Where(case_session => case_session.RollID == rollID).Count() == 0;
        }


        public List<vw_RollCases> GetSessionRollCases(int CircuitID, int SecretaryID)
        {
            List<vw_RollCases> sessionList = (from master in DataContext.Cases_MasterCase
                                              join cases in DataContext.Cases_Cases on master.ID equals cases.MasterCaseID
                                              join overall in DataContext.Configurations_OverallNumbers on master.OverallID equals overall.ID
                                              join sessions in DataContext.Cases_CaseSessions on cases.ID equals sessions.CaseID
                                              join crimes in DataContext.Configurations_Lookups on master.CrimeID equals crimes.ID
                                              join status in DataContext.Configurations_Lookups on cases.CaseStatusID equals status.ID
                                              
                                              join CR in DataContext.CourtConfigurations_CircuitRolls on sessions.RollID equals CR.ID
                                              join C in DataContext.CourtConfigurations_Circuits on CircuitID  equals C.ID
                                              orderby sessions.RollIndex
                                              where (C.SecretaryID == SecretaryID||C.AssistantSecretaryID==SecretaryID)  && CR.CircuitID == CircuitID  && CR.RollStatusID == (int)RollStatus.NotStarted  && (sessions.ApprovedByJudge != true ) && cases.IsDeleted != true
                                              select new vw_RollCases
                                              {
                                                  SessionID = sessions.ID,
                                                  Title = master.Title,
                                                  FirstLevelNumber = master.FirstLevelNumber + " / " + master.FirstLevelYear + " / " + master.Configurations_PoliceStations.Configurations_Prosecutions.Name,
                                                  SecondLevelNumber = master.SecondLevelNumber + " / " + master.SecondLevelYear + " / " + master.Configurations_Prosecutions.Name,
                                                  OverAllNumber = overall.InclosiveSierial + " / " + overall.Year + " / " + overall.Configurations_Prosecutions.Name,
                                                  Order = sessions.RollIndex,
                                                  MainCrime = crimes.Name,
                                                  CaseStatus = status.Name,
                                                  CaseID = cases.ID,
                                                  RollID=(int)CR.ID ,
                                                RollDate=CR.SessionDate,
                                              }).OrderBy(e => e.Order).ToList();
            List<vw_RollCases> result2;

            result2 = (from r in sessionList
                       where (r.RollDate.ToShortDateString() == DateTime.Now.ToShortDateString())
                       select new vw_RollCases
                       {
                           SessionID = r.SessionID,
                           Title = r.Title,
                           FirstLevelNumber = r.FirstLevelNumber,
                           SecondLevelNumber = r.SecondLevelNumber,
                           OverAllNumber =r.OverAllNumber,
                           Order = r.Order,
                           MainCrime = r.MainCrime,
                           CaseStatus = r.CaseStatus,
                           CaseID = r.CaseID,
                           RollID =  r.RollID

                       }).ToList();
            if (result2.Count == 0)
            {
                List<vw_RollCases> Rollid = (

                         from CR in DataContext.CourtConfigurations_CircuitRolls 
                         join C in DataContext.CourtConfigurations_Circuits on CircuitID equals C.ID
                         where (C.SecretaryID == SecretaryID || C.AssistantSecretaryID == SecretaryID) && CR.CircuitID == CircuitID && CR.RollStatusID == (int)RollStatus.NotStarted 

                         select new vw_RollCases
                         {
                            RollID = (int)CR.ID,
                             RollDate = CR.SessionDate,
                         }).ToList();
                    List<vw_RollCases> Rollid2 = (from r in Rollid
                                                  where (r.RollDate.ToShortDateString() == DateTime.Now.ToShortDateString())
                           select new vw_RollCases
                           { 
                               RollID = r.RollID

                           }).ToList();
                return Rollid2;
            }
            return result2;


   
        }
        public List<vw_RollCases> GetCasesINRoll(int SessionID)
        {
            List<vw_RollCases> CasesINsession = (from master in DataContext.Cases_MasterCase
                                                 join cases in DataContext.Cases_Cases on master.ID equals cases.MasterCaseID
                                                 join overall in DataContext.Configurations_OverallNumbers on master.OverallID equals overall.ID
                                                 join crimes in DataContext.Configurations_Lookups on master.CrimeID equals crimes.ID
                                                 join status in DataContext.Configurations_Lookups on cases.CaseStatusID equals status.ID
                                                 join sessions in DataContext.Cases_CaseSessions on cases.ID equals sessions.CaseID
                                                 join CR in DataContext.CourtConfigurations_CircuitRolls on sessions.RollID equals CR.ID
                                                 join C in DataContext.CourtConfigurations_Circuits on CR.CircuitID equals C.ID
                                                 join _PoliceStation in DataContext.Configurations_PoliceStations on master.PoliceStationID equals _PoliceStation.ID

                                                 where CR.ID == SessionID && cases.IsDeleted != true && sessions.RollIndex != 0
                                                 select new vw_RollCases
                                                 {
                                                     SecretaryID = sessions.SecretaryID,
                                                     FirstLevelNumber = master.FirstLevelNumber + " / " + master.FirstLevelYear + " / " + master.Configurations_PoliceStations.Configurations_Prosecutions.Name,
                                                     SecondLevelNumber = master.SecondLevelNumber + " / " + master.SecondLevelYear + " / " + master.Configurations_Prosecutions.Name,
                                                     OverAllNumber = overall.InclosiveSierial + " / " + overall.Year + " / " + overall.Configurations_Prosecutions.Name,
                                                     Order = sessions.RollIndex,
                                                     MainCrime = crimes.Name,
                                                     CaseStatus = status.Name,
                                                     CaseID = cases.ID,
                                                     CircuitID=C.ID,
                                                     SessionID=sessions.ID,
                                                     RollDate=CR.SessionDate
                                                 }).OrderBy(e => e.Order).ToList();
            return CasesINsession;
        }
        public bool UpdateRoll(vw_CaseConfiguration vw_CaseConfiguration)
        {
             try
             {      foreach (var roll in vw_CaseConfiguration.CasesIDs)
                    {
                    CourtConfigurations_CircuitRolls Roll = DataContext.CourtConfigurations_CircuitRolls.Where(rollid => roll == vw_CaseConfiguration.SessionID).Single();
                    Roll.CircuitID = vw_CaseConfiguration.CircuitID;
                    Roll.SessionDate = vw_CaseConfiguration.SessionDate;
                    this.Update(Roll);
                    this.Save();
                    }
                    return true;
            }
            catch(Exception ex)
            {
                return false;
            }
           
        }

        public int? GetRollID(int circuitid, DateTime SessionDate)
        {
            CourtConfigurations_CircuitRolls circuit = new CourtConfigurations_CircuitRolls();
             circuit = DataContext.CourtConfigurations_CircuitRolls.SingleOrDefault(roll => roll.CircuitID == circuitid && roll.SessionDate != null);
            long? rollid = null;
            if (circuit != null)
            {
             rollid = DataContext.CourtConfigurations_CircuitRolls.SingleOrDefault(roll => roll.CircuitID == circuitid && roll.SessionDate != null && roll.SessionDate.ToShortDateString() == SessionDate.ToShortDateString()).ID;
            }
            if (rollid == null)
            {

                vw_CaseConfiguration caseConfigurationData = new vw_CaseConfiguration();
                caseConfigurationData.CircuitID = circuitid;
                caseConfigurationData.SessionDate = SessionDate;
               // caseConfigurationData.r
                long RollID;
                AddRoll(caseConfigurationData, out  RollID);
                // AddRoll()
                rollid = RollID;
            }
            if (rollid!=null)
            return int.Parse(rollid.ToString());
            else return null;
        }

        public List<vw_SessionData> GetRollsOpend(int SecretaryID,int UserTypeID)
        {
            List<vw_SessionData> rollsOpens = new List<vw_SessionData>();
            List<vw_SessionData> result2=new List<vw_SessionData>();

            if (UserTypeID != 1)
            {
                rollsOpens = (from CR in DataContext.CourtConfigurations_CircuitRolls
                              join C in DataContext.CourtConfigurations_Circuits on CR.CircuitID equals C.ID

                              where (C.SecretaryID == SecretaryID || C.AssistantSecretaryID == SecretaryID) && CR.RollStatusID == (int)RollStatus.InProgress
                              && (CR.ApprovedByJudge != true)
                              select new vw_SessionData
                              {
                                  SessionDate = CR.SessionDate,
                                  SessionID = (int)CR.ID,
                                  CircuitName = C.Name,


                              }).OrderBy(e => e.SessionDate).ToList();

            }
            else
            {

                rollsOpens = (from CR in DataContext.CourtConfigurations_CircuitRolls
                              join Circuit in DataContext.CourtConfigurations_Circuits on CR.CircuitID equals Circuit.ID
                              join c in DataContext.Configurations_Courts on Circuit.CourtID equals c.ID
                              join user in DataContext.Users on c.ID equals user.CourtID
                              join userType in DataContext.Security_UserTypes on user.UserTypeID equals userType.ID
                              where  CR.RollStatusID == (int)RollStatus.InProgress && (CR.ApprovedByJudge != true) && userType.ID==UserTypeID

                              select new vw_SessionData
                              {
                                  SessionDate = CR.SessionDate,
                                  SessionID = (int)CR.ID,
                                  CircuitName = Circuit.Name,
                              }).OrderBy(e => e.SessionDate).Distinct().ToList();


            }
            result2 = (from r in rollsOpens
                       where (r.SessionDate.ToShortDateString() == DateTime.Now.ToShortDateString() || r.SessionDate < DateTime.Now)
                       select new vw_SessionData
                       {
                           SessionDate = r.SessionDate,
                           SessionID = (int)r.SessionID,
                           CircuitName = r.CircuitName,

                       }).ToList();
            return result2;

        }
        public List<vw_RollCases> GetSessionRollCasesOrder(int CircuitID, int RollID)
        {
            List<vw_RollCases> sessionList = (from master in DataContext.Cases_MasterCase
                                              join cases in DataContext.Cases_Cases on master.ID equals cases.MasterCaseID
                                              join overall in DataContext.Configurations_OverallNumbers on master.OverallID equals overall.ID
                                              join crimes in DataContext.Configurations_Lookups on master.CrimeID equals crimes.ID
                                              join status in DataContext.Configurations_Lookups on cases.CaseStatusID equals status.ID
                                              join sessions in DataContext.Cases_CaseSessions  on cases.ID equals sessions.CaseID 
                                              join CR in DataContext.CourtConfigurations_CircuitRolls on sessions.RollID equals CR.ID
                                              join C in DataContext.CourtConfigurations_Circuits on CR.CircuitID equals C.ID
                                              
                                              orderby sessions.RollIndex
                                              where CR.ID == RollID && cases.IsDeleted != true && CR.CircuitID == CircuitID && (sessions.ApprovedByJudge == false )
                                              select new vw_RollCases
                                              {
                                                  SecretaryID = sessions.SecretaryID,
                                                  FirstLevelNumber = master.FirstLevelYear +"/"+ master.FirstLevelNumber + "/"+master.Configurations_PoliceStations.Configurations_Prosecutions.Name,
                                                  SecondLevelNumber = master.SecondLevelYear + "/"+ master.SecondLevelNumber+"/"+master.Configurations_Prosecutions.Name,
                                                  OverAllNumber = overall.Year + "/"+ overall.InclosiveSierial  + "/" + overall.Configurations_Prosecutions.Name,
                                                  Order = sessions.RollIndex,
                                                  MainCrime = crimes.Name,
                                                  CaseStatus = status.Name,
                                                  CaseID = cases.ID

                                              }).OrderBy(e => e.Order).ToList();


            return sessionList;
        }

        public List<vw_KeyValueDate > GetCircuitSessionDates(int CircuitID)
        {
           return  (from Circuit in DataContext.CourtConfigurations_CircuitRolls
                    where Circuit.CircuitID == CircuitID && Circuit.SessionDate >=DateTime.Now
                                               select new vw_KeyValueDate
                                               {
                                                   ID = (int)Circuit.ID,
                                                   Date = Circuit.SessionDate
                                               }).ToList();
        }

        public List<vw_KeyValueDate> GetCircuitRolls(int CircuitID, int? CaseTypeID = null)
        {
            List<vw_VacationData> vacations = (from Vacations in DataContext.CourtConfigurations_Vacations
                                               select new vw_VacationData
                                               {
                                                   ID = Vacations.ID,
                                                   VacationName = Vacations.Name,
                                                   VacationFrom = Vacations.FromDate,
                                                   VacationTo = Vacations.EndDate
                                               }).ToList();

            List<DateTime> VacationsDates = new List<DateTime>();

            foreach (var vacation in vacations)
            {
                IEnumerable<DateTime> vac_dates = Enumerable.Range(0, 1 + vacation.VacationTo.Subtract(vacation.VacationFrom).Days)
                    .Select(offset => vacation.VacationFrom.AddDays(offset));
                VacationsDates.AddRange(vac_dates);
            }

            var result = (from circuit in DataContext.CourtConfigurations_Circuits
                          join cycle in DataContext.CourtConfigurations_Cycles on circuit.CycleID equals cycle.ID
                          join cycleRoll in DataContext.CourtConfigurations_CycleRolls on cycle.ID equals cycleRoll.CycleID

                          where circuit.ID == CircuitID
                          orderby cycleRoll.RollDate
                          select new vw_KeyValueDate
                          {
                              ID = cycleRoll.ID,
                              Date = cycleRoll.RollDate
                          }).ToList();

            var result2 = (from item in result
                           where !(VacationsDates.Any(vacationdate => vacationdate.Date == item.Date))
                           select item).ToList();

            return result2;
        }
        public CircuitRollsSavestatus AddRoll(vw_CaseConfiguration caseConfigurationData, out long Roll_ID)
        {
            try
            {
                CourtConfigurations_CircuitRolls CircuitRollsObject = new CourtConfigurations_CircuitRolls();
                CircuitRollsObject.SessionDate = caseConfigurationData.SessionDate;
                CircuitRollsObject.CircuitID = caseConfigurationData.CircuitID;
                CircuitRollsObject.RollStatusID = (int)RollStatus.NotStarted;
                

                this.Add(CircuitRollsObject);
                this.Save();
                Roll_ID = CircuitRollsObject.ID;
                return CircuitRollsSavestatus.Saved;

            }
            catch (Exception)
            {
                Roll_ID = 0;
                return CircuitRollsSavestatus.Failed;
            }
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
            List<vw_RollCases> sessionList = (from master in DataContext.Cases_MasterCase
                                              join cases in DataContext.Cases_Cases on master.ID equals cases.MasterCaseID
                                              join overall in DataContext.Configurations_OverallNumbers on master.OverallID equals overall.ID
                                              join sessions in DataContext.Cases_CaseSessions on cases.ID equals sessions.CaseID
                                              join crimes in DataContext.Configurations_Lookups on master.CrimeID equals crimes.ID
                                              join status in DataContext.Configurations_Lookups on cases.CaseStatusID equals status.ID
                                              join CR in DataContext.CourtConfigurations_CircuitRolls on sessions.RollID equals CR.ID
                                              orderby sessions.RollIndex
                                              where CR.ID == RollID && cases.IsDeleted != true && (sessions.ApprovedByJudge != true && sessions.ApprovedByJudge != true && sessions.IsTransferedApproved == false && sessions.IsTransferedFromSession == true)
                                              select new vw_RollCases
                                              {
                                                  SecretaryID = (int)CR.SecretaryID,
                                                  FirstLevelNumber = master.FirstLevelNumber +"/"+master.FirstLevelYear+"/"+ master.Configurations_PoliceStations.Configurations_Prosecutions.Name,
                                                  SecondLevelNumber = master.SecondLevelNumber+"/"+ master.SecondLevelYear +"/"+master.Configurations_Prosecutions.Name ,
                                                  OverAllNumber = overall.InclosiveSierial + "/" + overall.Year + "/" + overall.Configurations_Prosecutions.Name,
                                                  Order = sessions.RollIndex,
                                                  MainCrime = crimes.Name,
                                                  CaseStatus = status.Name,
                                                  CaseID = cases.ID

                                              }).OrderBy(e => e.Order).ToList();

            return sessionList;
        }

        public bool IsRollOpened(int RollID)
        {
            return DataContext.CourtConfigurations_CircuitRolls.Where(session => session.ID  == RollID && session.RollStatusID!=(byte)RollStatus.NotStarted  ).Count() > 0;

        }

        public bool IsApprovedByJudge(int RollID)
        {
            return DataContext.CourtConfigurations_CircuitRolls.Where(session => session.ID == RollID && session.ApprovedByJudge  == true).Count() > 0;

        }

        public bool IsRollExist(int CycleRollID, int CircuitID, DateTime NextSessionDate)
        {
           return  (//from cycleRoll in DataContext.CourtConfigurations_CycleRolls
            from circuit in DataContext.CourtConfigurations_CircuitRolls where NextSessionDate == circuit.SessionDate
            && circuit.ID == CircuitID select circuit.ID).Count()>0;
        }

        public int GetCircuitRollID(int CycleRollID, int CircuitID, DateTime NextSessionDate)
        {
            return (int)(from cycleRoll in DataContext.CourtConfigurations_CycleRolls
                    join circuit in DataContext.CourtConfigurations_CircuitRolls on cycleRoll.RollDate equals circuit.SessionDate
                    where circuit.ID == CircuitID
                    select circuit.ID).FirstOrDefault();
        }

        public void AddNewCircuitRoll(int CircuitID, int CaseID, int SessionID, DateTime NextSessionDate, out long roll)
        {
            AddRoll(new vw_CaseConfiguration { SessionDate = NextSessionDate,CircuitID = CircuitID }, out roll);
        }

        public List<vw_RollCases> GetRollSessionsForOpenedRolls(int SecretaryID)
        {
            List<vw_RollCases> sessionList = (from master in DataContext.Cases_MasterCase
                                              join cases in DataContext.Cases_Cases on master.ID equals cases.MasterCaseID
                                              join overall in DataContext.Configurations_OverallNumbers on master.OverallID equals overall.ID
                                              join sessions in DataContext.Cases_CaseSessions on cases.ID equals sessions.CaseID
                                              join crimes in DataContext.Configurations_Lookups on master.CrimeID equals crimes.ID
                                              join status in DataContext.Configurations_Lookups on cases.CaseStatusID equals status.ID
                                              join CR in DataContext.CourtConfigurations_CircuitRolls on sessions.RollID equals CR.ID
                                              join Circuit in DataContext.CourtConfigurations_Circuits on CR.CircuitID equals Circuit.ID

                                              orderby sessions.RollIndex
                                              where (Circuit.SecretaryID  == SecretaryID||Circuit.AssistantSecretaryID==SecretaryID) && cases.IsDeleted != true  /*&& CR.SessionDate <= DateTime.Now */&& CR.RollStatusID ==(int)RollStatus.InProgress   && (sessions.ApprovedByJudge != true )
                                              select new vw_RollCases
                                              {
                                                  SessionID = sessions.ID,
                                                  Title = master.Title,
                                                  FirstLevelNumber = master.FirstLevelNumber + "/" + master.FirstLevelYear + "/" + master.Configurations_PoliceStations.Configurations_Prosecutions.Name,
                                                  SecondLevelNumber = master.SecondLevelNumber + "/" + master.SecondLevelYear + "/" + master.Configurations_Prosecutions.Name,
                                                  OverAllNumber = overall.InclosiveSierial + "/" + overall.Year + "/" + overall.Configurations_Prosecutions.Name,
                                                  Order = sessions.RollIndex,
                                                  MainCrime = crimes.Name,
                                                  CaseStatus = status.Name,
                                                  CaseID = cases.ID,
                                                  RollDate=CR.SessionDate ,
                                                  CircuitID=Circuit.ID
                                                  
                                              }).OrderBy(e => e.Order).ToList();


            List<vw_RollCases> result2;
          
                result2 = (from r in sessionList
                           where (r.RollDate.ToShortDateString() ==DateTime.Now.ToShortDateString() || r.RollDate <= DateTime.Now)
                           select new vw_RollCases
                           {
                               SessionID = r.SessionID,
                               Title = r.Title,
                               FirstLevelNumber = r.FirstLevelNumber,
                               SecondLevelNumber = r.SecondLevelNumber,
                               OverAllNumber = r.OverAllNumber,
                               Order = r.Order,
                               MainCrime = r.MainCrime,
                               CaseStatus = r.CaseStatus,
                               CaseID = r.CaseID,
                               RollDate = r.RollDate,
                               CircuitID = r.CircuitID


                           }).ToList();

            return result2;
        }

        public void UpdateRollProsecuterAndCourtHall(long RollID, int ProsecuterID, int CourtHall)
        {
            var item = GetByID(RollID);
             
                item.ProsecuterID = ProsecuterID;
                item.HallID = CourtHall;
                Update(item);
                Save();
             
        }

        public DateTime GetSessionDate(int rollID)
        {
          return GetAll().Where(s => s.ID == rollID).Select(d=>d.SessionDate ).FirstOrDefault() ; 
        }
    }
}
