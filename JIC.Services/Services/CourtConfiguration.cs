using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Services.ServicesInterfaces;
using JIC.Components.Components;
using JIC.Base.Views;
using System.Data.Entity.Validation;

namespace JIC.Services.Services
{
    public class CourtConfigurationService : ServiceBase, IVacationService, ICycleService , ICircuitService 
    {
        public CourtConfigurationService(CaseType caseType) : base(caseType)
        {

        }
        #region Property
        public CycleComponent CycleComponent { get { return GetComponent<CycleComponent>(); } }
        public WorkDaysComponent  WorkDaysComp { get { return GetComponent<WorkDaysComponent >(); } }
        public VacationsComponent VacationsComponent { get { return GetComponent<VacationsComponent>(); } }
        public MasterCaseComponent  CaseComponent { get { return GetComponent<MasterCaseComponent>(); } }
        public PoliceStationCircuitsComponent PoliceComp { get { return GetComponent<PoliceStationCircuitsComponent>(); } }
        public RollsComponent CircuitRollsComponent { get { return GetComponent<RollsComponent>(); } }

        public CircuitsComponent CircuitComponent { get { return GetComponent<CircuitsComponent>(); } }
        public CircuitMembersComponent CircuitMembersComponent { get { return GetComponent<CircuitMembersComponent>(); } }

        public PoliceStationComponent  PsComponent { get { return GetComponent<PoliceStationComponent>(); } }

        #endregion

        #region Vacations

        public SaveStatus AddVacation(vw_VacationData vacations, out int vacationID)
        {
            // Validate(vacations);
            if (CircuitRollsComponent.HasSession(vacations.VacationFrom, vacations.VacationTo) )
            {
                vacationID = 0;
                return SaveStatus.WorkingDay;
               // throw new ValidationExceptions("لا يمكن ادراج الاجازة .. يوجد جلسات خلال هذه الفترة");
            }
            return VacationsComponent.AddVacation(vacations, out vacationID);
        }

        public SaveStatus EditVacation(vw_VacationData vacations)
        {
           return VacationsComponent.EditVacation(vacations);
        }

        public DeleteStatus DeleteVacation(int vacationID)
        {
            return VacationsComponent.DeleteVacation(vacationID);
        }

        public List<vw_VacationData> GetVacations()
        {
            return VacationsComponent.GetVacation();
        }

        public void Validate(vw_VacationData vacations)
        {
            //if (vacations.VacationFrom <= DateTime.Now && SystemConfigurations.Settings_InDevelopmentMode)
            //{
            //    throw new ValidationExceptions("تاريخ بدايةالأجازة يجب ان يكون أكبر من تاريخ اليوم");
            //}

            //if (vacations.VacationTo < vacations.VacationFrom && SystemConfigurations.Settings_InDevelopmentMode)
            //{
            //    throw new ValidationExceptions("تاريخ نهاية الأجازة يجب ان يكون اكبر من او يساوي تاريخ البداية");
            //}
            //if (CircuitRollsComponent.HasSession(vacations.VacationFrom , vacations.VacationTo) && SystemConfigurations.Settings_InDevelopmentMode)
            //{
            //    throw new ValidationExceptions("لا يمكن ادراج الاجازة .. يوجد جلسات خلال هذه الفترة");
            //}
        }

        #endregion

        #region Cycle
        public bool IsPreviousMonthSaved(int month, int year)
        {
            return CycleComponent.IsPreviousMonthSaved(month, year);
        }

        public bool AddCycle(List<DateTime> dates, Cycle CycleID, int CourtID)
        {
            try
            {
                CycleComponent.AddCycle(dates, CycleID, CourtID);
                return true;
            }catch(Exception ex)
            {
                HandleException(ex);
                return false;
            }
        }
        public bool AddCycles(List<vw_AddCycle> Cycles, int CourtID)
        {
            try
            {
                List<DateTime> Dates;
                DateTime startDate;
                List<DateTime> DatesWithoutWeekends;
                using (var Transaction = this.BeginDatabaseTransaction())
                {
                    foreach (var Cycle in Cycles)
                    {
                        this.DeleteCycleByMonth(Cycle.DateFrom.Year, Cycle.DateFrom.Month, CourtID, Cycle.Cycle);
                    }
                    foreach (var Cycle in Cycles)
                    {
                        
                        startDate = new DateTime(Cycle.DateFrom.Year, Cycle.DateFrom.Month, Cycle.DateFrom.Day);
                         Dates = Enumerable.Range(0, 1 + Cycle.DateTo.Subtract(Cycle.DateFrom).Days)
                                .Select(offset => startDate.AddDays(offset))
                                .ToList();

                        DatesWithoutWeekends=RemoveWeeekends(Dates);


                        if (!this.AddCycle(DatesWithoutWeekends, Cycle.Cycle, CourtID))
                            return false;
                    }
                    if (Transaction != null)
                        Transaction.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return false;
        }

        List<DateTime> RemoveWeeekends(List<DateTime> Dates)
        {
            List<DateTime> newDates = new List<DateTime>();
            var WorkDays = WorkDaysComp.GetAllDays().Select(workDay => workDay.ID );
            var NonWorkDays = Enumerable.Range(0, 7).Where(nonWorkDay => !WorkDays.Contains(nonWorkDay)).Select(nonWorkDay => (DayOfWeek)nonWorkDay).ToList();

            foreach (var z in Dates)
            {
                foreach (var k in NonWorkDays)
                {
                    if (z.DayOfWeek != k)
                    {
                        newDates.Add(z);
                    }
                }

              
            }


            return newDates;
        }


        public List<DateTime> GetCycleDates(int CourtID, Cycle cycle)
        {
            return CycleComponent.GetCycleDates(CourtID, cycle);
        }

        public IQueryable<DateTime> GetCycleDatesQuery(int CourtID, Cycle cycle)
        {
            return CycleComponent.GetCycleDatesQuery(CourtID, cycle);
        }

        public void DeleteCycle(int Year, int CourtID, Cycle cycle)
        {
            CycleComponent.DeletCycle(Year, CourtID, cycle);
        }

        public void DeleteCycleByMonth(int Year, int Month, int CourtID, Cycle cycle)
        {
            CycleComponent.DeletCycleByMonth(Year,Month, CourtID, cycle);
        }
        #endregion

        #region Circuits

    
        public bool ValidateJudges(List<vw_CircuitsJudges> JudgesList)
        {

            var CountJudgeIDs = from x in JudgesList
                                group x by x.JudgeID into g
                                let count = g.Count()
                                orderby count descending
                                select new { Value = g.Key, Count = count };
            return CountJudgeIDs.Where(judge => judge.Count > 1).Count() > 0;
        }
        public SaveCircuitStatus AddCircuit(vw_CircuitData circuitData, out int CircuitID)
        {
            try
            {
                DateTime EndCircuitDate = new DateTime(DateTime.Today.Year, SystemConfigurations.Settings_CircuitEndMonth, SystemConfigurations.Settings_CircuitEndDay);

                if (DateTime.Today > EndCircuitDate)
                    EndCircuitDate.AddYears(1);

                bool isSameYearCircuit = (circuitData.CircuitStartDate < EndCircuitDate) ? true : false;

                DateTime CircuitEndDate = new DateTime(circuitData.CircuitStartDate.Year, SystemConfigurations.Settings_CircuitEndMonth, SystemConfigurations.Settings_CircuitEndDay);

                if (circuitData.CircuitStartDate > CircuitEndDate)
                    CircuitEndDate = circuitData.CircuitStartDate.AddYears(1);

                if (ValidateJudges(circuitData.JudgesID))
                {
                    CircuitID = 0;
                    return SaveCircuitStatus.Judge_Used_Twice;
                }
                if (circuitData.SecretaryID == circuitData.AssistantSecretaryID )
                {
                    CircuitID = 0;
                    return SaveCircuitStatus.Secretary_Used_Twice;
                }
                if (CircuitComponent.IsSavedBefore(isSameYearCircuit, circuitData.CircuitName,circuitData.ID))
                {
                    CircuitID = 0;
                    return SaveCircuitStatus.Saved_Before;
                }

            circuitData.IsActive = isSameYearCircuit;
            circuitData.IsFutureCircuit = !isSameYearCircuit;
                if (circuitData.AssistantSecretaryID == 0)
                    circuitData.AssistantSecretaryID = null;
                using (var Transaction = BeginDatabaseTransaction())
                {
               
                    CircuitComponent.AddCircuit(circuitData, out CircuitID);
                   PoliceComp.SaveCircuitPoliceStations(circuitData.PoliceStations, CircuitID);
                    SaveCircuitStatus SaveJudgesStatus = CircuitMembersComponent.AddCircuitJudges(circuitData.JudgesID, CircuitID, circuitData.CircuitStartDate);

                    if (Transaction != null)
                        Transaction.Commit();
                    return SaveCircuitStatus.Saved_Successfully;
                }
    
            }
            catch (Exception ex)
            {
                CircuitID = 0;
                HandleException(ex);
                return SaveCircuitStatus.Failed_To_Save ;
            }
        }

        public SaveCircuitStatus EditCircuit(vw_CircuitData circuitData)
        {
            try
            {
                DateTime EndCircuitDate = new DateTime(DateTime.Today.Year, SystemConfigurations.Settings_CircuitEndMonth, SystemConfigurations.Settings_CircuitEndDay);

                bool isSameYearCircuit = (circuitData.CircuitStartDate < EndCircuitDate || (circuitData.CircuitStartDate > EndCircuitDate && DateTime.Today > EndCircuitDate) ? true : false);

                DateTime CircuitEndDate = new DateTime(circuitData.CircuitStartDate.Year, SystemConfigurations.Settings_CircuitEndMonth, SystemConfigurations.Settings_CircuitEndDay);

                if (circuitData.CircuitStartDate > CircuitEndDate)
                    CircuitEndDate = circuitData.CircuitStartDate.AddYears(1);

                //if (CircuitComponent.IsStartDateAfterToday(circuitData.ID) == false)
                //{
                //    return SaveCircuitStatus.CircuitStartDateBeforeToday;
                //}

                if (ValidateJudges(circuitData.JudgesID))
                {
                   
                    return SaveCircuitStatus.Judge_Used_Twice;
                }

                if (CircuitComponent.IsSavedBefore(isSameYearCircuit, circuitData.CircuitName, circuitData.ID ))
                {

                    return SaveCircuitStatus.Saved_Before;
                }

                circuitData.IsActive = isSameYearCircuit;
                circuitData.IsFutureCircuit = !isSameYearCircuit;


                using (var Transaction = BeginDatabaseTransaction())
                {
                    PoliceComp.DeleteCircuitPoliceStations(circuitData.ID);
                    PoliceComp.SaveCircuitPoliceStations(circuitData.PoliceStations, circuitData.ID);
                    CircuitMembersComponent.EditCircuitJudges(circuitData.JudgesID, circuitData.ID, circuitData.CircuitStartDate);
                    
                    //CircuitMembersComponent.DeleteCircuitMemberByCircuitID (circuitData.ID);
                    //CircuitMembersComponent.AddCircuitJudges ( circuitData.JudgesID  ,circuitData.ID,circuitData.CircuitStartDate );
                    
                    CircuitComponent.EditCircuit(circuitData);
                  
                   
                    if (Transaction != null)
                        Transaction.Commit();
                    return SaveCircuitStatus.Saved_Successfully;
                }
            }
            catch (DbEntityValidationException ex)
            {
               
                HandleException(ex); 
                return SaveCircuitStatus.Failed_To_Save;
            }
        }

        public DeleteCircuitStatus DeleteCircuit(int ID)
        {
            try
            {
                if (CaseComponent.IsCaseConnectedToCircuit(ID))
                {
                    return DeleteCircuitStatus.CaseConnectedToCircuit;
                }
                //if (CircuitComponent.IsStartDateAfterToday(ID)==false)
                //{
                //    return DeleteCircuitStatus.CircuitStartDateBeforeToday;
                //}
                else
                {
                    using (var Transaction = BeginDatabaseTransaction())
                    {
                        var r1 = CircuitRollsComponent.DeleteCircuitRollsByCircuitID(ID);
                        var r2 = CircuitMembersComponent.DeleteCircuitMemberByCircuitID(ID);
                       PoliceComp.DeleteCircuitPoliceStations (ID);
                        CircuitComponent.DeleteCircuit(ID);
                        if (Transaction != null)
                            Transaction.Commit();
                        return DeleteCircuitStatus.Deleted;

                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return DeleteCircuitStatus.NotDeleted;
            }
        }

        public List<vw_KeyValue> GetCircuits()
        {
            return CircuitComponent.GetCircuits();
        }
        public List<vw_CircuitsGrid > GetCircuitsFullData(int CourtID)
        {
            List<vw_CircuitsGrid> CircuitsList= CircuitComponent.GetCircuitsFullData(CourtID);

            foreach (vw_CircuitsGrid CObj in CircuitsList)
            {
                CObj.CircuitMembers = CircuitMembersComponent.GetCircuitMembersByCircuitID(CObj.ID);
                CObj.PoliceStations = PsComponent.GetPoliceStationsByCircuitID(CObj.ID);
            }

            return CircuitsList;
        }
        public List<vw_KeyValue> GetCircuitsByCourtID(int courtID)
        {
            return CircuitComponent.GetCircuitsByCourtID (courtID );
        }
         

        public vw_CircuitsGrid GetCircuitsFullDataByID(int CircuitID)
        {

            vw_CircuitsGrid CObj= CircuitComponent.GetCircuitsFullDataByID(CircuitID);
            CObj.CircuitMembers = CircuitMembersComponent.GetCircuitMembersByCircuitID(CObj.ID);
            CObj.PoliceStations = PsComponent.GetPoliceStationsByCircuitID(CObj.ID);

            return CObj;
        }

   

     

        public List<vw_KeyValue> GetCircuitsBySecretairyID(int SecretairyID, int? CourtID)
        {
            return CircuitComponent.GetCircuitsBySecretairyID(SecretairyID ,CourtID);
        }
        public List<vw_KeyValue> GetCircuitsBySecretairyID(int SecretairyID)
        {
            return CircuitComponent.GetCircuitsBySecretairyID(SecretairyID);
        }
        public List<vw_KeyValue> GetCircuitsByCrime(int CrimeID,int CourtID)
        {
            return CircuitComponent.GetCircuitsByCrime(CrimeID , CourtID);
        }
        public List<DateTime> GetSessions(int CircuitID)
        {
            throw new NotImplementedException();
        }

        public List<vw_KeyValue> GetCircuitID(int UserID)
        {
            throw new NotImplementedException();
        }
        public List<vw_KeyValue> GetCircuits(int CourtID)
        {
            throw new NotImplementedException();
        }










        #endregion
    }
}
