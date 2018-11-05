using JIC.Base;
using JIC.Services.ServicesInterfaces;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.TestService
{
    public class RollService : IRollService
    {
        public List<vw_RollCases> GetCasesINRoll(int SessionID)
        {
            throw new NotImplementedException();
        }

        public List<DateTime> GetCircuitRollDates(int CircuitID, int SecretaryID)
        {
            return (new List<DateTime>()
            {
                DateTime.Now,
                DateTime.Now
            }
            );
        }

        public List<vw_RollCases> GetOpenedRolls(int SecretaryID)
        {
            throw new NotImplementedException();
        }

        public List<vw_RollCases> GetRollCases(int RollID)
        {
            List<Base.Views.vw_RollCases> list = new List<Base.Views.vw_RollCases>();
            if (RollID == 1)
            {
                Base.Views.vw_RollCases vw_Key = new Base.Views.vw_RollCases()
                {
                    Order = 0,
                    CaseID = 5,
                    CaseStatus = "sadfwefad",
                    OverAllNumber = "12/2015/2",
                  //  FirstLevelNumber = 12,
                   // SecondLevelNumber = 72,
                    MainCrime = "asfwed",
                    SecretaryID = 1,
                };
                Base.Views.vw_RollCases vw_Key2 = new Base.Views.vw_RollCases()
                {
                    Order = 6,
                    CaseID = 6,
                    CaseStatus = "dsafewedsfa",
                    OverAllNumber = "12/2010/9",
                   // FirstLevelNumber = 345,
                   // SecondLevelNumber = 454,
                    MainCrime = "tyerwtre",
                    SecretaryID = 2,
                };

                Base.Views.vw_RollCases vw_Key3 = new Base.Views.vw_RollCases()
                {
                    Order = 10,
                    CaseID = 6,
                    CaseStatus = "ewarew",
                    OverAllNumber = "2/2005/9",
                  //  FirstLevelNumber = 3445,
                  //  SecondLevelNumber = 4564564,
                    MainCrime = "erwaree",
                    SecretaryID = 1,
                };
                list.Add(vw_Key);
                list.Add(vw_Key2);
                list.Add(vw_Key3);
                return list;


            }
            else if (RollID == 2)
            {

                Base.Views.vw_RollCases vw_Key = new Base.Views.vw_RollCases()
                {
                    Order = 5,
                    CaseID = 7,
                    CaseStatus = "daferqr",
                    OverAllNumber = "1/2013/6",
                    //FirstLevelNumber = 54,
                    //SecondLevelNumber = 5654,
                    MainCrime = "adffdgetwty",
                    SecretaryID = 1,
                };
                Base.Views.vw_RollCases vw_Key2 = new Base.Views.vw_RollCases()
                {
                    Order = 6,
                    CaseID = 8,
                    CaseStatus = "dafsewtrt",
                    OverAllNumber = "32/2003/4",
                    //FirstLevelNumber = 4534,
                    //SecondLevelNumber = 444,
                    MainCrime = "fdgsdsdftwqewr",
                    SecretaryID = 2,


                };
                list.Add(vw_Key);
                list.Add(vw_Key2);
                return list;
            }
            return list;
        }

        public List<vw_RollCases> GetRollCasesForOpening(int RollID)
        {
            throw new NotImplementedException();
        }

        public List<vw_RollCases> GetRollCasesForOpening(int CircuitID, int RollID)
        {
            List<vw_RollCases> list = new List<vw_RollCases>();
            vw_RollCases _RollCases = new vw_RollCases();
            _RollCases.Order = 1;
            _RollCases.CaseID = 1;
            _RollCases.CaseStatus = "";
            _RollCases.OverAllNumber = "";
            //_RollCases.FirstLevelNumber =5645;
            //_RollCases.SecondLevelNumber = 544;

            _RollCases.MainCrime = "";
            _RollCases.SecretaryID = 1;
            _RollCases.rollStatus = RollStatus.InProgress;
            _RollCases.CircuitID =5;

            list.Add(_RollCases);
            return list;
        }

        public List<Base.Views.vw_RollCases> GetRollCasesForOrdering(int RollID)
        {
            List<Base.Views.vw_RollCases> list = new List<Base.Views.vw_RollCases>();
            if (RollID == 1)
            {
                Base.Views.vw_RollCases vw_Key = new Base.Views.vw_RollCases()
                {
                    Order = 0,
                    CaseID = 5,
                    CaseStatus = "sadfwefad",
                    OverAllNumber = "12/2015/2",
                    //FirstLevelNumber = 12,
                    //SecondLevelNumber = 72,
                    MainCrime = "asfwed",
                    SecretaryID = 1,
                };
                Base.Views.vw_RollCases vw_Key2 = new Base.Views.vw_RollCases()
                {
                    Order = 6,
                    CaseID = 6,
                    CaseStatus = "dsafewedsfa",
                    OverAllNumber = "12/2010/9",
                    //FirstLevelNumber = 345,
                    //SecondLevelNumber = 454,
                    MainCrime = "tyerwtre",
                    SecretaryID = 2,
                };

                Base.Views.vw_RollCases vw_Key3 = new Base.Views.vw_RollCases()
                {
                    Order = 10,
                    CaseID = 6,
                    CaseStatus = "ewarew",
                    OverAllNumber = "2/2005/9",
                    //FirstLevelNumber = 3445,
                    //SecondLevelNumber = 4564564,
                    MainCrime = "erwaree",
                    SecretaryID = 1,
                };
                list.Add(vw_Key);
                list.Add(vw_Key2);
                list.Add(vw_Key3);
                return list;


            }
            else if (RollID == 2)
            {

                Base.Views.vw_RollCases vw_Key = new Base.Views.vw_RollCases()
                {
                    Order = 5,
                    CaseID = 7,
                    CaseStatus = "daferqr",
                    OverAllNumber = "1/2013/6",
                    //FirstLevelNumber = 54,
                    //SecondLevelNumber = 5654,
                    MainCrime = "adffdgetwty",
                    SecretaryID = 1,
                };
                Base.Views.vw_RollCases vw_Key2 = new Base.Views.vw_RollCases()
                {
                    Order = 6,
                    CaseID = 8,
                    CaseStatus = "dafsewtrt",
                    OverAllNumber = "32/2003/4",
                    //FirstLevelNumber = 4534,
                    //SecondLevelNumber = 444,
                    MainCrime = "fdgsdsdftwqewr",
                    SecretaryID = 2,


                };
                list.Add(vw_Key);
                list.Add(vw_Key2);
                return list;
            }
            return list;
        }

        public List<vw_RollCases> GetRollCasesForOrdering(int CircuitID, DateTime SessionDate)
        {
            throw new NotImplementedException();
        }

        public List<vw_RollCases> GetRollCasesForOrdering(int CircuitID, int RollID)
        {
            throw new NotImplementedException();
        }

        public int? GetRollID(int circuitid, DateTime SessionDate)
        {
            throw new NotImplementedException();
        }

        public List<vw_SessionData> GetRollsOpend(int SecretaryID)
        {
            throw new NotImplementedException();
        }

        public List<vw_SessionData> GetRollsOpend(int SecretaryID, int UserTypeID)
        {
            throw new NotImplementedException();
        }

        public List<vw_KeyValue> GetRollsReadyToOrder(int CircuitID)
        {
            List<vw_KeyValue> list = new List<vw_KeyValue>();
            if (CircuitID == 1)
            {
                vw_KeyValue vw_Key = new vw_KeyValue()
                {
                    ID = 1,
                    Name = "wqewqed",
                };
                vw_KeyValue vw_Key2 = new vw_KeyValue()
                {
                    ID = 2,
                    Name = "DQEWFAS",
                };
                list.Add(vw_Key);
                list.Add(vw_Key2);
                return list;
            }
            else if (CircuitID == 2)
            {

                vw_KeyValue vw_Key = new vw_KeyValue()
                {
                    ID = 1,
                    Name = "AWERQWS",
                };
                vw_KeyValue vw_Key2 = new vw_KeyValue()
                {
                    ID = 2,
                    Name = "GFDSGERW",
                };
                list.Add(vw_Key);
                list.Add(vw_Key2);
                return list;
            }
            return list;
        }

        public List<vw_CrimeCaseBasicData> GetUnApprovedMovmentCases(int RollID)
        {
            List<vw_CrimeCaseBasicData> list = new List<vw_CrimeCaseBasicData>();
            if (RollID == 1)
            {
                vw_CrimeCaseBasicData vw_Key = new vw_CrimeCaseBasicData()
                {

                    CaseID = 1,
                    CaseStatus = "weddasd",
                    overAllNumber = "10/2017/2",
                    FirstNumberInt = 1,
                    SecondNumberInt = 2,
                    MainCrimeName = "sad",
                };
                vw_CrimeCaseBasicData vw_Key2 = new vw_CrimeCaseBasicData()
                {

                    CaseID = 2,
                    CaseStatus = "dsfa",
                    overAllNumber = "11/2015/9",
                    FirstNumberInt = 3,
                    SecondNumberInt = 4,
                    MainCrimeName = "dsafwad",

                };
                list.Add(vw_Key);
                list.Add(vw_Key2);
                return list;
            }
            else if (RollID == 2)
            {

                vw_CrimeCaseBasicData vw_Key = new vw_CrimeCaseBasicData()
                {

                    CaseID = 3,
                    CaseStatus = "erqr",
                    overAllNumber = "13/2013/6",
                    FirstNumberInt = 54,
                    SecondNumberInt = 5654,
                    MainCrimeName = "fdgetwty",
                };
                vw_CrimeCaseBasicData vw_Key2 = new vw_CrimeCaseBasicData()
                {

                    CaseID = 4,
                    CaseStatus = "sewtrt",
                    overAllNumber = "3/2003/4",
                    FirstNumberInt = 34,
                    SecondNumberInt = 44,
                    MainCrimeName = "sdftwqewr",

                };
                list.Add(vw_Key);
                list.Add(vw_Key2);
                return list;
            }
            return list;
        }

        public RollStatus OpenSessionRoll(int CircuitID, int SecretairyID, int ProsecuterID, int CourtHall, out long RollID)
        {
            throw new NotImplementedException();
        }

        public RollStatus OpenSessionRoll(int ProsecuterID, int CourtHall, int RollID)
        {
            return RollStatus.InProgress;
        }

        public List<vw_RollCases> PrintRoll(int RollID)
        {
            throw new NotImplementedException();
        }

        public SaveRollOrderStatus SaveRollOrder(int RollID, List<vw_CaseOrder> CaseOrders)
        {
            //  return Base.SaveRollOrder.SuccessFull;
            return Base.SaveRollOrderStatus.Failed;
            //return Base.SaveRollOrder.RollOpened;


        }

        public SetCasesRollStatus SetCasesRoll(List<int> CaseID, int RollID)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRoll(vw_CaseConfiguration vw_CaseConfiguration)
        {
            throw new NotImplementedException();
        }

        public void UpdateRollProsecuterAndCourtHall(long RollID, int ProsecuterID, int CourtHall)
        {
            throw new NotImplementedException();
        }

        List<vw_KeyValueDate> IRollService.GetCircuitRollDates(int CircuitID, int SecretaryID)
        {
            throw new NotImplementedException();
        }

        List<vw_RollCases> IRollService.GetUnApprovedMovmentCases(int RollID)
        {
            throw new NotImplementedException();
        }

        //List<vw_RollCases> IRollService.GetRollCasesForOpening(int RollID)
        //{
        //    throw new NotImplementedException();
        //}
    }
}