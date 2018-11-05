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
    public class OverallNumberRepository : EntityRepositoryBase<Configurations_OverallNumbers>, IOverAllNumberRepository
    {
        public int AddOverAllNumber(vw_CrimeCaseBasicData caseBasicData)
        {
            var z = GetAllQuery().Where(a => a.Active == false && a.Year == caseBasicData.FirstYearInt && a.ProsecutionID == caseBasicData.SecondProsecutionID  && a.CourtID == caseBasicData.CourtID).FirstOrDefault();
            if (z == null)
            {
                Configurations_OverallNumbers overallNumber = InstanseOverAll(caseBasicData);
                overallNumber.Active = true;
                Add(overallNumber);
                Save();
                return overallNumber.ID;
            }

            else
            {

                z.Active = true;
                Update(z);
                Save();

                return z.ID;

            }
        }

        private Configurations_OverallNumbers InstanseOverAll(vw_CrimeCaseBasicData caseBasicData)
        {
            return new Configurations_OverallNumbers {
                Year = caseBasicData.SecondYearInt,
                ProsecutionID = (int)caseBasicData.SecondProsecutionID ,
                InclosiveSierial = CurrentOverAllNumber(caseBasicData.SecondYearInt ,(int) caseBasicData.SecondProsecutionID ,caseBasicData.CourtID),
                CourtID = caseBasicData.CourtID
            };
        }


        public long CurrentOverAllNumber(int year, int ProsectionId,int courtID)
        {
         
                var i = GetAllQuery().Where(a => a.Year == year && a.ProsecutionID == ProsectionId && a.CourtID == courtID).Select(a => (long?)a.InclosiveSierial).Max() ?? 0;
                return i + 1;
            
        }

        public bool AddOverAllNumber(int caseID, out long number, out int prosecutionID, out int year,out int OverAllID)
        {
            var basicData = (from _case in DataContext.Cases_Cases
             join _master in DataContext.Cases_MasterCase on _case.MasterCaseID equals _master.ID
             where _case.ID == caseID && _case.IsDeleted != true
                             select new 
             {
                 SecondLevelYear = _master.SecondLevelYear,
                 SecondProsecutoinID = _master.ProsecutionID.Value,
                 CourtID = _case.CourtID ,
                 
             }).First();

            int SecYear = int.Parse(basicData.SecondLevelYear);
            // is there an inactive number in the overAllnumbers
            var z = GetAllQuery().Where(a => a.Active == false && a.Year == SecYear && a.ProsecutionID == basicData.SecondProsecutoinID && a.CourtID == basicData.CourtID).FirstOrDefault();

            // if no , add new over all
            if (z == null)
            {
                Configurations_OverallNumbers overallNumber_ = InstanseOverAll(new vw_CrimeCaseBasicData { SecondYearInt  = SecYear, SecondProsecutionID  = basicData.SecondProsecutoinID, CourtID = basicData.CourtID });

                overallNumber_.Active = true;
                Add(overallNumber_);
                Save();
                number = overallNumber_.InclosiveSierial;
                prosecutionID = overallNumber_.ProsecutionID;
                year = overallNumber_.Year;
                OverAllID = overallNumber_.ID;
                return true;
            }
            // if found, update it to active
            else
            {

                z.Active = true;
                Update(z);
                Save();

                number = z.InclosiveSierial;
                prosecutionID = z.ProsecutionID;
                year = z.Year;
                OverAllID = z.ID;
                return true;
            }
            ////Configurations_OverallNumbers overallNumber = InstanseOverAll(new vw_CrimeCaseBasicData { FirstYearInt = int.Parse(basicData.FirstLevelYear),FirstProsecutionID = basicData.FirstProsecutionID, CourtID = basicData.CourtID });
            ////overallNumber.Active = true;
            ////Add(overallNumber);
            ////Save();
            //number = overallNumber.InclosiveSierial;
            //prosecutionID = overallNumber.ProsecutionID;
            //year = overallNumber.Year;
            //OverAllID = overallNumber.ID;
            
        }

        public int GetOverAllID(long number, int prosecutionID, int year,int CourtID)
        {
            return GetOverAllNumber(number, prosecutionID, year, CourtID).ID;
        }

        private Configurations_OverallNumbers GetOverAllNumber(long number, int prosecutionID, int year,int courtID)
        {
            var NewOverAllNumber = GetAllQuery().Where(overAll => overAll.InclosiveSierial == number && overAll.ProsecutionID == prosecutionID && overAll.Year == year && overAll.CourtID == courtID).FirstOrDefault();
            if(NewOverAllNumber == null)
            {
                NewOverAllNumber = new Configurations_OverallNumbers
                {
                    CourtID = courtID,
                    InclosiveSierial = number,
                    ProsecutionID = prosecutionID,
                    Year = year,
                    Active = true                    
                };
                Add(NewOverAllNumber);
                Save();
            }
            return NewOverAllNumber;
        }

        public bool IsOverAllEmpty(long number, int prosecutionID, int year)
        {
            var OverAll = GetAllQuery().Where(overAll => overAll.InclosiveSierial == number && overAll.ProsecutionID == prosecutionID && overAll.Year == year).FirstOrDefault();
            if (OverAll == null || !OverAll.Active)
                return true;
            else
                return false;
        }

        public void DisableOverAll(int caseID)
        {
            var OverAll = (from _master in DataContext.Cases_MasterCase
                           join _case in DataContext.Cases_Cases on _master.ID equals _case.MasterCaseID
                           where _case.ID == caseID 
                           select _master.Configurations_OverallNumbers).FirstOrDefault();
            if (OverAll != null)
            {
                OverAll.Active = false;
                Update(OverAll);
                Save();
            }
        }
    }
}
