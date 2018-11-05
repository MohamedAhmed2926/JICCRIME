using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace JIC.Base.Views.Services
{
    [DataContract]
    public class NewCase : CaseBase
    {
        [DataMember]
        public CaseSession First_Session
        {
            get { return GetFirstSession(); }
            set
            {
                firstSession = value;
            }
        }

        #region GoGoTricks
        private CaseSession firstSession;

        //to Add CourtId in the inner FirstSession Object :D
        //without the need to write Code
        private CaseSession GetFirstSession()
        {
            if (firstSession != null)
            {
                firstSession.SetCourtID(Court_ID);
                firstSession.SetCaseTypeID(CaseTypeID);
            }

            return firstSession;
        }
        #endregion
        #region Mapping Helper

        public vw_CaseConfiguration MapCaseSessionConfiguration(long sessionID, int caseID)
        {
            return new vw_FaultCaseConfiguration
            {
                CasesIDs = new List<int> { caseID },
                CircuitID = First_Session.Circuit_ID,
                SessionDate = First_Session.Session_Date,
                SessionID = sessionID,
                CaseTypeID = CaseTypeID
            };
        }
        public vw_FaultCaseBasicData MapToBasicData()
        {
            return new vw_FaultCaseBasicData
            {
                ProsecutionCaseID = Business_Case_Id,
                CourtID = Court_ID,
                FirstNumber = First_Case_No.ToString(),
                FirstPoliceStationID = First_Case_Police_Station_ID,
                FirstYear = First_Case_Year.ToString(),
                CaseStatusID = (int)CaseStatus.New,
                CaseLevelID = (int)CaseLevels.Initial,
                CircuitID = First_Session.Circuit_ID,
                MainCrimeID = CrimeID,
                ProcedureTypeID = ProcedureTypeID,
                CaseTypeID = CaseTypeID

            };
        }
        #endregion
    }
}