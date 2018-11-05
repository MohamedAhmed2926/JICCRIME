using JIC.Base;
using JIC.Services.ServicesInterfaces;
using JIC.Base.Views;
using JIC.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.TestInterfaces
{
    public class LookupService : ILookupService
    {
        public List<vw_KeyValue> GetAllCitites()
        {
            return new List<vw_KeyValue>
            {
                new vw_KeyValue { ID = 1,Name = "محكمة القاهرة الجديدة الإبتدائية"},
                new vw_KeyValue { ID = 2,Name = "محكمة العباسية"}
            };
        }

        public List<vw_KeyValue> GetLawyersLevel()
        {
            return new List<vw_KeyValue>
            {
                 new vw_KeyValue((int)Cycle.FirstCycle ,JIC.Base.Resources.Resources.FirstCycle),
                 new vw_KeyValue((int) Cycle.SecondCycle ,JIC.Base.Resources.Resources.SecondCycle),
                new vw_KeyValue((int) Cycle.ThridCycle ,JIC.Base.Resources.Resources.ThirdCycle),
                 new vw_KeyValue((int) Cycle.FourthCycle ,JIC.Base.Resources.Resources.FourthCycle)
            };

        }

        public List<vw_KeyValue> GetCourts()
        {
            return new LookUpService(CaseType.Crime).GetCourts();
        }

        public List<vw_KeyValue> GetCrimeTypes(int? CourtID = null)
        {
            return new LookUpService(CaseType.Crime).GetCrimeTypes();
        }
        

        //public List<vw_Prosecution> GetIntialProsecutions(int Prosecution)
        //{
        //    return new List<vw_Prosecution>
        //    {
        //        new vw_Prosecution { ID = 1,ProsecutionName = "IntialElementaryUser"},
        //        new vw_Prosecution { ID = 2,ProsecutionName = "IntialInitialUser"}
        //    };
        //}

        

        public List<vw_KeyValue> GetLookupsByCategory(LookupsCategories judgLevel)
        {
            return new LookUpService(CaseType.Crime).GetLookupsByCategory(judgLevel);
            //return new List<vw_KeyValue>
            //{
            //    new vw_KeyValue { ID = 1,Name = "محكمة القاهرة الجديدة الإبتدائية"},
            //    new vw_KeyValue { ID = 2,Name = "محكمة العباسية"}
            //};
        }

        public List<vw_KeyValue> GetPoliceStations(int ProsecutionID)
        {
            return new LookUpService(CaseType.Crime).GetPoliceStations(ProsecutionID);
        }

       

        public List<vw_KeyValue> GetUserTypes()
        {
            return new List<vw_KeyValue>
            {
                new vw_KeyValue { ID = (int)SystemUserTypes.ElementaryCourtAdministrator,Name = SystemUserTypes.ElementaryCourtAdministrator.ToString()},
                new vw_KeyValue { ID = (int)SystemUserTypes.Judge,Name = SystemUserTypes.Judge.ToString()},
                new vw_KeyValue { ID = (int)SystemUserTypes.Secretary,Name = SystemUserTypes.Secretary.ToString()},
                new vw_KeyValue { ID = (int)SystemUserTypes.schedualEmployee,Name = SystemUserTypes.schedualEmployee.ToString()},
                new vw_KeyValue { ID = (int)SystemUserTypes.InitialCourtAdministrator,Name = SystemUserTypes.InitialCourtAdministrator.ToString()},
                new vw_KeyValue { ID = (int)SystemUserTypes.CourtHead,Name = SystemUserTypes.CourtHead.ToString()},
                new vw_KeyValue { ID = (int)SystemUserTypes.InquiriesEmployee,Name = SystemUserTypes.InquiriesEmployee.ToString()},
                new vw_KeyValue { ID = (int)SystemUserTypes.JICAdmin,Name = SystemUserTypes.JICAdmin.ToString()},
                new vw_KeyValue { ID = (int)SystemUserTypes.schedualEmployee,Name = SystemUserTypes.schedualEmployee.ToString()}

            };
        }

        public List<vw_KeyValue> GetJudgeTypes()
        {
            return new JIC.Services.Services.LookUpService(CaseType.Crime).GetJudgeTypes();
        }

        public List<vw_KeyValue> GetPartyTypes()
        {
            return new JIC.Services.Services.LookUpService(CaseType.Crime).GetPartyTypes();
        }

        public List<vw_KeyValue> GetSessionsDateTypes()
        {
            return new JIC.Services.Services.LookUpService(CaseType.Crime).GetSessionsDateTypes();
        }

        List<vw_KeyValue> ILookupService.GetIntialProsecutions(int Prosecution)
        {
            return new LookUpService(CaseType.Crime).GetIntialProsecutions(Prosecution);
        }
        List<vw_KeyValue> ILookupService.GetProsecutions(int? CourtID)
        {
            return new LookUpService(CaseType.Crime).GetProsecutions(CourtID);
        }

        public List<vw_KeyValue> GetAllCycles()
        {
            return new List<vw_KeyValue>()
            {
                new vw_KeyValue()
                {
                    ID=1,
                    Name="الدور الأول"
                }
                ,new vw_KeyValue()
                {
                    ID=2,
                    Name="الدور الثانى"
                }
                ,new vw_KeyValue()
                {
                    ID=3,
                    Name="الدور الثالث"
                }
                ,new vw_KeyValue()
                {
                    ID=4,
                    Name="الدور الرابع"
                }
            };
        }
      
        List<vw_KeyValue> ILookupService.GetPoliceStationsByCourtID(int CourtID)
        {
            return new JIC.Services.Services.LookUpService(CaseType.Crime).GetPoliceStationsByCourtID(CourtID );
        }
        List<vw_KeyValue> ILookupService.GetIntialProsecutionsByCourtID(int CourtID)
        {
            //return new List<vw_KeyValue>()
            //{
            //    new vw_KeyValue()
            //    {
            //        ID=1,
            //        Name="IN1"
            //    }
            //    ,new vw_KeyValue()
            //    {
            //        ID=2,
            //        Name="IN2"
            //    }
            //};
            return new JIC.Services.Services.LookUpService(CaseType.Crime).GetIntialProsecutionsByCourtID(CourtID);
        }


        public List<vw_KeyValue> GetCourtHalls(int? CourtID)
        {
            List<vw_KeyValue> list = new List<vw_KeyValue>();
            vw_KeyValue vw_Key = new vw_KeyValue();
            vw_Key.ID = 1;
            vw_Key.Name ="sad";
            vw_KeyValue vw_Key1 = new vw_KeyValue();
            vw_Key1.ID = 2;
            vw_Key1.Name = "sadfsdf";
            vw_KeyValue vw_Key2 = new vw_KeyValue();
            vw_Key2.ID = 3;
            vw_Key2.Name = "adsfwe";
            list.Add(vw_Key);
            list.Add(vw_Key1);
            list.Add(vw_Key2);
            return list;

        }

        public List<DecisionTypes> GetJudgeOrders_Initial()
        {
            throw new NotImplementedException();
        }

        public List<DecisionTypes> GetJudgeOrders_Elementary()
        {
            throw new NotImplementedException();
        }

        public List<DecisionTypes> GetDecisionTypes()
        {
            throw new NotImplementedException();
        }

        public List<vw_KeyValue> GetDecisionTypes(CaseLevels CaseLevel)
        {
            throw new NotImplementedException();
        }

        public List<vw_KeyValue> GetJudjementTypes()
        {
            throw new NotImplementedException();
        }

        public List<vw_KeyValue> GetElementaryProsecutions(int CourtID)
        {
            throw new NotImplementedException();
        }

        public List<vw_KeyValue> GetDecisionTypes(CaseStatuses caseStatuses)
        {
            throw new NotImplementedException();
        }

        public List<vw_KeyValue> GetCrimeTypes(int UserId)
        {
            throw new NotImplementedException();
        }

        public List<vw_KeyValue> GetObtainmentStatuses()
        {
            throw new NotImplementedException();
        }

        public bool SecritaryHaveCircuit(string UserName)
        {
            throw new NotImplementedException();
        }

        public List<vw_KeyValue> GetAllPoliceStations()
        {
            throw new NotImplementedException();
        }

        public List<vw_KeyValue> GetCaseTypes()
        {
            throw new NotImplementedException();
        }

        public int GetNationalityIDOrCreate(string nationality)
        {
            throw new NotImplementedException();
        }



        //List<vw_KeyValue> ILookupService.GetProsecutions(int? CourtID)
        //{
        //    return new List<vw_KeyValue>()
        //    {
        //        new vw_KeyValue()
        //        {
        //            ID=3,
        //            Name="الدور الثالث"
        //        }
        //        ,new vw_KeyValue()
        //        {
        //            ID=4,
        //            Name="الدور الرابع"
        //        }
        //    };
        //}
    }
}