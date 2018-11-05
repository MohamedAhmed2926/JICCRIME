using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.Views;
using JIC.Crime.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Crime.Repositories.Repositories
{
    public class SearchCaseRepository : RepositoryBase, ISearchCaseRepository
    {
        public List<vw_AddSessionSearchResult> AddSessionsSearch(vw_AddSessionsSearchData searchData)
        {
            List<vw_AddSessionSearchResult> result2 = new List<vw_AddSessionSearchResult>();

            var result = (from MasterCase in this.DataContext.Cases_MasterCase
                          join Cases in this.DataContext.Cases_Cases on MasterCase.ID equals Cases.MasterCaseID
                          join Courts in this.DataContext.Configurations_Courts on Cases.CourtID equals Courts.ID
                          join PoliceStation in this.DataContext.Configurations_PoliceStations on MasterCase.PoliceStationID equals PoliceStation.ID
                          join FirstLevelProsecution in this.DataContext.Configurations_Prosecutions on PoliceStation.ProsecutionID equals FirstLevelProsecution.ID
                          join SecondLevelProsecution in this.DataContext.Configurations_Prosecutions on MasterCase.ProsecutionID equals SecondLevelProsecution.ID
                          join CT in this.DataContext.Cases_CrimeTypes on MasterCase.CrimeType equals CT.ID
                          join MainCrimelookup in this.DataContext.Configurations_Lookups on MasterCase.CrimeID equals MainCrimelookup.ID
                          join CaseStatuslookup in this.DataContext.Configurations_Lookups on Cases.CaseStatusID equals CaseStatuslookup.ID
                          where
                          ((MasterCase.IsComplete && Cases.IsDeleted != true)
                            && ((Courts.ID == searchData.CourtID && searchData.CourtID.HasValue) || !searchData.CourtID.HasValue)
                            && ((MasterCase.FirstLevelNumber.Contains(searchData.FirstLevelNumber.ToString())) || string.IsNullOrEmpty(searchData.FirstLevelNumber.ToString()))
                            && ((MasterCase.SecondLevelNumber.Contains(searchData.SecondLevelNumber.ToString())) || string.IsNullOrEmpty(searchData.SecondLevelNumber.ToString()))
                            && ((MasterCase.FirstLevelYear == searchData.FirstLevelYear.ToString() && searchData.FirstLevelYear.HasValue) || string.IsNullOrEmpty(searchData.FirstLevelYear.ToString()))
                            && ((MasterCase.SecondLevelYear == searchData.SecondLevelYear.ToString() && searchData.SecondLevelYear.HasValue) || string.IsNullOrEmpty(searchData.SecondLevelYear.ToString()))
                            && (((PoliceStation.ProsecutionID == searchData.FirstLevelProsecutionID && searchData.FirstLevelProsecutionID.HasValue)) || !searchData.FirstLevelProsecutionID.HasValue)
                            && ((MasterCase.ProsecutionID == searchData.SecondLevelProsecutionID && searchData.SecondLevelProsecutionID.HasValue) || !searchData.SecondLevelProsecutionID.HasValue)
                            && ((MasterCase.PoliceStationID == searchData.PoliceStationID && searchData.PoliceStationID.HasValue) || !searchData.PoliceStationID.HasValue)
                            && ((CT.ID == searchData.CrimeType && searchData.CrimeType.HasValue) || !searchData.CrimeType.HasValue)
                            && ((MasterCase.CrimeType == searchData.CrimeType && searchData.CrimeType.HasValue) || !searchData.CrimeType.HasValue)
                            && ((Cases.CaseStatusID == searchData.CaseStatusID && searchData.CaseStatusID.HasValue) || !searchData.CaseStatusID.HasValue)
                            && ((MasterCase.CrimeID == searchData.CrimeID && searchData.CrimeID.HasValue) || !searchData.CrimeID.HasValue)
                            && ((Cases.Cases_CaseDefendants.Any(x => x.Cases_DefendatnsCaseLog.Where(y => y.PoliceStationStatusID == searchData.DefendantStatus).Any())) || !searchData.DefendantStatus.HasValue)
                            )
                          select new
                          {
                              CaseID = Cases.ID,
                              FirstLevelNumber = MasterCase.FirstLevelNumber,
                              SecondLevelNumber = MasterCase.SecondLevelNumber,
                              CrimeType = CT.Name,
                              PoliceStation = PoliceStation.Name,
                              MainCrime = MainCrimelookup.Name,
                              CaseStatusID = CaseStatuslookup.Name,
                              CrimeID = CT.ID
                          }).ToList();

            result2 = (from res in result
                       where !(from r in result
                               join sessions in DataContext.Cases_CaseSessions on r.CaseID equals sessions.CaseID
                               join rolls in DataContext.CourtConfigurations_CircuitRolls on sessions.RollID equals rolls.ID
                               select r.CaseID).ToList().Contains(res.CaseID)
                       select new vw_AddSessionSearchResult
                       {
                           CaseID = res.CaseID,
                           FirstLevelNumber = res.FirstLevelNumber,
                           SecondLevelNumber = res.SecondLevelNumber,
                           CrimeType = res.CrimeType,
                           PoliceStation = res.PoliceStation,
                           MainCrime = res.MainCrime,
                           CaseStatus = res.CaseStatusID,
                           CrimeID = res.CrimeID
                       }).ToList();

            return result2;
        }

        public List<vw_AddSessionSearchResult> EditSessionSearch(vw_AddSessionsSearchData searchData)
        {
            List<vw_AddSessionSearchResult> result2 = new List<vw_AddSessionSearchResult>();

            var result = (from MasterCase in this.DataContext.Cases_MasterCase
                          join Cases in this.DataContext.Cases_Cases on MasterCase.ID equals Cases.MasterCaseID
                          join Courts in this.DataContext.Configurations_Courts on Cases.CourtID equals Courts.ID
                          join PoliceStation in this.DataContext.Configurations_PoliceStations on MasterCase.PoliceStationID equals PoliceStation.ID
                          join FirstLevelProsecution in this.DataContext.Configurations_Prosecutions on PoliceStation.ProsecutionID equals FirstLevelProsecution.ID
                          join SecondLevelProsecution in this.DataContext.Configurations_Prosecutions on MasterCase.ProsecutionID equals SecondLevelProsecution.ID
                          join CT in this.DataContext.Cases_CrimeTypes on MasterCase.CrimeType equals CT.ID
                          join MainCrimelookup in this.DataContext.Configurations_Lookups on MasterCase.CrimeID equals MainCrimelookup.ID
                          join CaseStatuslookup in this.DataContext.Configurations_Lookups on Cases.CaseStatusID equals CaseStatuslookup.ID

                          where
                          ((MasterCase.IsComplete && Cases.IsDeleted != true)
                            && ((Courts.ID == searchData.CourtID && searchData.CourtID.HasValue) || !searchData.CourtID.HasValue)
                            && ((MasterCase.FirstLevelNumber.Contains(searchData.FirstLevelNumber.ToString())) || string.IsNullOrEmpty(searchData.FirstLevelNumber.ToString()))
                            && ((MasterCase.SecondLevelNumber.Contains(searchData.SecondLevelNumber.ToString())) || string.IsNullOrEmpty(searchData.SecondLevelNumber.ToString()))
                            && ((MasterCase.FirstLevelYear == searchData.FirstLevelYear.ToString() && searchData.FirstLevelYear.HasValue) || string.IsNullOrEmpty(searchData.FirstLevelYear.ToString()))
                            && ((MasterCase.SecondLevelYear == searchData.SecondLevelYear.ToString() && searchData.SecondLevelYear.HasValue) || string.IsNullOrEmpty(searchData.SecondLevelYear.ToString()))
                            && (((PoliceStation.ProsecutionID == searchData.FirstLevelProsecutionID && searchData.FirstLevelProsecutionID.HasValue)) || !searchData.FirstLevelProsecutionID.HasValue)
                            && ((MasterCase.ProsecutionID == searchData.SecondLevelProsecutionID && searchData.SecondLevelProsecutionID.HasValue) || !searchData.SecondLevelProsecutionID.HasValue)
                            && ((MasterCase.PoliceStationID == searchData.PoliceStationID && searchData.PoliceStationID.HasValue) || !searchData.PoliceStationID.HasValue)
                            //&& ((Cases.CourtConfigurations_Circuits.ID == searchData.CircuitID && searchData.CircuitID.HasValue) || !searchData.CircuitID.HasValue)
                            && ((CT.ID == searchData.CrimeType && searchData.CrimeType.HasValue) || !searchData.CrimeType.HasValue)
                            && ((MasterCase.CrimeType == searchData.CrimeType && searchData.CrimeType.HasValue) || !searchData.CrimeType.HasValue)
                            && ((Cases.CaseStatusID == searchData.CaseStatusID && searchData.CaseStatusID.HasValue) || !searchData.CaseStatusID.HasValue)
                            && ((MasterCase.CrimeID == searchData.CrimeID && searchData.CrimeID.HasValue) || !searchData.CrimeID.HasValue)
                            //&& ((Cases.Cases_CaseDefendants.Where(defendant => defendant.DefendantStatusID == searchData.DefendantStatus).Any()) || !searchData.DefendantStatus.HasValue)
                            && ((Cases.Cases_CaseDefendants.Any(x => x.Cases_DefendatnsCaseLog.Where(y => y.PoliceStationStatusID == searchData.DefendantStatus).Any())) || !searchData.DefendantStatus.HasValue)
                            )
                          select new
                          {
                              CaseID = Cases.ID,
                              FirstLevelNumber = MasterCase.FirstLevelNumber,
                              SecondLevelNumber = MasterCase.SecondLevelNumber,
                              CrimeType = CT.Name,
                              PoliceStation = PoliceStation.Name,
                              MainCrime = MainCrimelookup.Name,
                              CaseStatusID = CaseStatuslookup.Name,
                              CircuitName = Cases.CourtConfigurations_Circuits.Name,
                              CrimeID = CT.ID
                          }).ToList();

            result2 = (from res in result
                       where (from r in result
                              join sessions in DataContext.Cases_CaseSessions on r.CaseID equals sessions.CaseID
                              join rolls in DataContext.CourtConfigurations_CircuitRolls on sessions.RollID equals rolls.ID
                              where ((rolls.SessionDate == searchData.SessionDate && searchData.SessionDate.HasValue) || !searchData.SessionDate.HasValue)
                              select r.CaseID).ToList().Contains(res.CaseID)
                       select new vw_AddSessionSearchResult
                       {
                           CaseID = res.CaseID,
                           FirstLevelNumber = res.FirstLevelNumber,
                           SecondLevelNumber = res.SecondLevelNumber,
                           CrimeType = res.CrimeType,
                           PoliceStation = res.PoliceStation,
                           MainCrime = res.MainCrime,
                           CaseStatus = res.CaseStatusID,
                           CircuitName = res.CircuitName,
                           CrimeID = res.CrimeID

                       }).ToList();

            result2 = (from r in result2
                       join sessions in DataContext.Cases_CaseSessions on r.CaseID equals sessions.CaseID
                       join rolls in DataContext.CourtConfigurations_CircuitRolls on sessions.RollID equals rolls.ID
                       where ((rolls.SessionDate == searchData.SessionDate && searchData.SessionDate.HasValue) || !searchData.SessionDate.HasValue)
                       && ((rolls.CircuitID == searchData.CircuitID && searchData.CircuitID.HasValue) || !searchData.CircuitID.HasValue)
                       select new vw_AddSessionSearchResult
                       {
                           CaseID = r.CaseID,
                           FirstLevelNumber = r.FirstLevelNumber,
                           SecondLevelNumber = r.SecondLevelNumber,
                           CrimeType = r.CrimeType,
                           PoliceStation = r.PoliceStation,
                           MainCrime = r.MainCrime,
                           CaseStatus = r.CaseStatus,
                           CircuitName = rolls.CourtConfigurations_Circuits.Name,
                           SessionDate = rolls.SessionDate,
                           CrimeID = r.CrimeID
                       }).ToList();

            return result2;
        }

        public List<SearchResult> SearchCase(vw_SearchData searchData)
        {
            List<SearchResult> result2 = new List<SearchResult>();

            string PartyCleanName = null;

            if (!string.IsNullOrEmpty(searchData.PartyName))
            {
                PartyCleanName = Utilities.RemoveSpecialCharacters(searchData.PartyName);
                PartyCleanName = PartyCleanName.Replace(" ", string.Empty);
            }

            var result = (from MasterCase in this.DataContext.Cases_MasterCase
                          join Cases in this.DataContext.Cases_Cases on MasterCase.ID equals Cases.MasterCaseID
                          join OvelallNumber in this.DataContext.Configurations_OverallNumbers on MasterCase.OverallID equals OvelallNumber.ID
                          join Courts in this.DataContext.Configurations_Courts on Cases.CourtID equals Courts.ID
                          join PoliceStation in this.DataContext.Configurations_PoliceStations on MasterCase.PoliceStationID equals PoliceStation.ID
                          join FirstLevelProsecution in this.DataContext.Configurations_Prosecutions on PoliceStation.ProsecutionID equals FirstLevelProsecution.ID
                          join SecondLevelProsecution in this.DataContext.Configurations_Prosecutions on MasterCase.ProsecutionID equals SecondLevelProsecution.ID
                          join CT in this.DataContext.Cases_CrimeTypes on MasterCase.CrimeType equals CT.ID
                          where
                          ((MasterCase.IsComplete && Cases.IsDeleted != true)
                            && ((Courts.ID == searchData.CourtID && searchData.CourtID.HasValue) || !searchData.CourtID.HasValue)

                            && ((MasterCase.FirstLevelNumber.Contains(searchData.FirstLevelNumber.ToString())) || string.IsNullOrEmpty(searchData.FirstLevelNumber.ToString()))
                            && ((MasterCase.SecondLevelNumber.Contains(searchData.SecondLevelNumber.ToString())) || string.IsNullOrEmpty(searchData.SecondLevelNumber.ToString()))
                            && (((OvelallNumber.InclosiveSierial == searchData.OverAllNumber && searchData.OverAllNumber.HasValue)) || !searchData.OverAllNumber.HasValue)

                            && ((MasterCase.FirstLevelYear == searchData.FirstLevelYear.ToString() && searchData.FirstLevelYear.HasValue) || string.IsNullOrEmpty(searchData.FirstLevelYear.ToString()))
                            && ((MasterCase.SecondLevelYear == searchData.SecondLevelYear.ToString() && searchData.SecondLevelYear.HasValue) || string.IsNullOrEmpty(searchData.SecondLevelYear.ToString()))
                            && ((OvelallNumber.Year == searchData.OverAllNumberYear && searchData.OverAllNumberYear.HasValue) || !searchData.OverAllNumberYear.HasValue)

                            && (((PoliceStation.ProsecutionID == searchData.FirstLevelProsecutionID && searchData.FirstLevelProsecutionID.HasValue)) || !searchData.FirstLevelProsecutionID.HasValue)
                            && ((MasterCase.ProsecutionID == searchData.SecondLevelProsecutionID && searchData.SecondLevelProsecutionID.HasValue) || !searchData.SecondLevelProsecutionID.HasValue)
                            && ((MasterCase.PoliceStationID == searchData.PoliceStationID && searchData.PoliceStationID.HasValue) || !searchData.PoliceStationID.HasValue)
                            && ((OvelallNumber.ProsecutionID == searchData.OverAllNumberProsecution && searchData.OverAllNumberProsecution.HasValue) || !searchData.OverAllNumberProsecution.HasValue)

                            && ((CT.ID == searchData.CrimeType && searchData.CrimeType.HasValue) || !searchData.CrimeType.HasValue)

                            //   && ((Cases.CourtConfigurations_Circuits.ID == searchData.CircuitID && searchData.CircuitID.HasValue) || !searchData.CircuitID.HasValue)

                            && ((MasterCase.CrimeType == searchData.CrimeType && searchData.CrimeType.HasValue) || !searchData.CrimeType.HasValue)

                            && ((Cases.CaseStatusID == searchData.JudgeType && searchData.JudgeType.HasValue) || !searchData.JudgeType.HasValue)
                            //&&
                            //(((Cases.Cases_CaseDefendants.Where(defendant => defendant.Configurations_Persons.CleanFullName == PartyCleanName).Any()) || string.IsNullOrEmpty(PartyCleanName))
                            //|| ((Cases.Cases_CaseVictims.Where(victim => victim.Configurations_Persons.CleanFullName == PartyCleanName).Any()) || string.IsNullOrEmpty(PartyCleanName)))
                            )

                          select new //SearchResult
                          {
                              CaseID = Cases.ID,
                              FirstLevelNumber = MasterCase.FirstLevelYear + "-" + MasterCase.FirstLevelNumber + "-" + FirstLevelProsecution.Name,
                              SecondLevelNumber = MasterCase.SecondLevelYear + "-" + MasterCase.SecondLevelNumber + "-" + SecondLevelProsecution.Name,
                              OverAllNumber = OvelallNumber.Year + "-" + OvelallNumber.InclosiveSierial + "-" + SecondLevelProsecution.Name,
                              CrimeType = CT.Name,
                              MasterCase = MasterCase.ID,

                          }).ToList();

            //var result1 = (from r in result
            //               join sessions in DataContext.Cases_CaseSessions on r.CaseID equals sessions.CaseID
            //               join sessiondecisions in DataContext.Cases_SessionDecision on sessions.ID equals sessiondecisions.CaseSessionID
            //               join rolls in DataContext.CourtConfigurations_CircuitRolls on sessions.RollID equals rolls.ID

            //               select new //SearchResult
            //               {
            //                   CaseID = r.CaseID,
            //                   FirstLevelNumber = r.FirstLevelNumber,
            //                   SecondLevelNumber = r.SecondLevelNumber,
            //                   OverAllNumber = r.OverAllNumber,
            //                   CrimeType = r.CrimeType,
            //                   MasterCase = r.MasterCase,
            //               //    LastSessionDate = rolls.SessionDate,
            //             //      LastDecision = sessiondecisions.DecisionText,

            //               }).ToList();

            //var result3 = (from r in result
            //               join sessions in DataContext.Cases_CaseSessions on r.CaseID equals sessions.CaseID
            //               // join sessiondecisions in DataContext.Cases_SessionDecision on sessions.ID equals sessiondecisions.CaseSessionID
            //               join rolls in DataContext.CourtConfigurations_CircuitRolls on sessions.RollID equals rolls.ID

            //               select new //SearchResult
            //               {
            //                   CaseID = r.CaseID,
            //                   FirstLevelNumber = r.FirstLevelNumber,
            //                   SecondLevelNumber = r.SecondLevelNumber,
            //                   OverAllNumber = r.OverAllNumber,
            //                   CrimeType = r.CrimeType,
            //                   MasterCase = r.MasterCase,
            //               //    LastSessionDate = rolls.SessionDate,
            //                 //  LastDecision = "",


            //               }).ToList();
             
           //if (result1.Count() != 0)
           // {
           //         for (int i = result1.Count; i < result.Count(); i++)
           //         {
           //         result1.Add(result[i]);
           //         }
           //     result = result1;
           // }
           // else if (result3.Count() != 0)
           // {

           //     for (int i = result3.Count; i < result.Count(); i++)
           //     {
           //         result3.Add(result[i]);
           //     }
           //     result = result3;
           // }
               
            if (searchData.JudgeDate != null)
            {
                result = (from r in result
                          join sessions in DataContext.Cases_CaseSessions on r.CaseID equals sessions.CaseID
                          join sessiondecisions in DataContext.Cases_SessionDecision on sessions.ID equals sessiondecisions.CaseSessionID
                          join rolls in DataContext.CourtConfigurations_CircuitRolls on sessions.RollID equals rolls.ID
                          where ((rolls.SessionDate == searchData.JudgeDate && searchData.JudgeDate.HasValue) || !searchData.JudgeDate.HasValue)
                          select new //SearchResult
                          {
                              CaseID = r.CaseID,
                              FirstLevelNumber = r.FirstLevelNumber,
                              SecondLevelNumber = r.SecondLevelNumber,
                              OverAllNumber = r.OverAllNumber,
                              CrimeType = r.CrimeType,
                              MasterCase = r.MasterCase,
                             // LastSessionDate = rolls.SessionDate,
                             // LastDecision = sessiondecisions.DecisionText,
                          }).Distinct().ToList();
            }
            ////// party type filter ////////
            if (searchData.PartyType == (int)PartyTypes.Defendant && ! string.IsNullOrEmpty(PartyCleanName))
            {
                result = (from r in result
                          join defendant in DataContext.Cases_CaseDefendants on r.CaseID equals defendant.CaseID
                          join persons in DataContext.Configurations_Persons on defendant.PersonID equals persons.ID
                          where (persons.CleanFullName.Contains(PartyCleanName))
                          select new //SearchResult
                          {
                              CaseID = r.CaseID,
                              FirstLevelNumber = r.FirstLevelNumber,
                              SecondLevelNumber = r.SecondLevelNumber,
                              OverAllNumber = r.OverAllNumber,
                              CrimeType = r.CrimeType,
                              MasterCase = r.MasterCase,
                             // LastSessionDate = r.LastSessionDate,
                             // LastDecision = r.LastDecision,
                          }).Distinct().ToList();
            }
            else if (searchData.PartyType == (int)PartyTypes.Victim && !string.IsNullOrEmpty(PartyCleanName))
            {
                result = (from r in result
                          join victim in DataContext.Cases_CaseVictims on r.CaseID equals victim.CaseID
                          join persons in DataContext.Configurations_Persons on victim.PersonID equals persons.ID
                          where (persons.CleanFullName.Contains(PartyCleanName))
                          select new //SearchResult
                          {
                              CaseID = r.CaseID,
                              FirstLevelNumber = r.FirstLevelNumber,
                              SecondLevelNumber = r.SecondLevelNumber,
                              OverAllNumber = r.OverAllNumber,
                              CrimeType = r.CrimeType,
                              MasterCase = r.MasterCase
                              ,
                           //   LastSessionDate = r.LastSessionDate,
                           //   LastDecision = r.LastDecision,
                          }).Distinct().ToList();
            }
            else if (searchData.PartyType == (int)PartyTypes.VictimAndDefendant && !string.IsNullOrEmpty(PartyCleanName))
            {
                result = (from r in result
                          join victim in DataContext.Cases_CaseVictims on r.CaseID equals victim.CaseID
                          join defendant in DataContext.Cases_CaseDefendants on r.CaseID equals defendant.CaseID
                          join victimperson in DataContext.Configurations_Persons on victim.PersonID equals victimperson.ID
                          join defendantperson in DataContext.Configurations_Persons on defendant.PersonID equals defendantperson.ID
                          where (victimperson.CleanFullName == PartyCleanName && defendantperson.CleanFullName == PartyCleanName || string.IsNullOrEmpty(PartyCleanName))
                          select new //SearchResult
                          {
                              CaseID = r.CaseID,
                              FirstLevelNumber = r.FirstLevelNumber,
                              SecondLevelNumber = r.SecondLevelNumber,
                              OverAllNumber = r.OverAllNumber,
                              CrimeType = r.CrimeType,
                              MasterCase = r.MasterCase,
                            //  LastSessionDate = r.LastSessionDate,
                            //  LastDecision = r.LastDecision,
                          }).Distinct().ToList();
            }
            else if (!string.IsNullOrEmpty(PartyCleanName))
            {
                result = (from r in result
                          join victim in DataContext.Cases_CaseVictims on r.CaseID equals victim.CaseID
                          join defendant in DataContext.Cases_CaseDefendants on r.CaseID equals defendant.CaseID
                          join victimperson in DataContext.Configurations_Persons on victim.PersonID equals victimperson.ID
                          join defendantperson in DataContext.Configurations_Persons on defendant.PersonID equals defendantperson.ID
                          where (victimperson.CleanFullName.Contains(PartyCleanName) || defendantperson.CleanFullName.Contains(PartyCleanName) || string.IsNullOrEmpty(PartyCleanName))
                          select new //SearchResult
                          {
                              CaseID = r.CaseID,
                              FirstLevelNumber = r.FirstLevelNumber,
                              SecondLevelNumber = r.SecondLevelNumber,
                              OverAllNumber = r.OverAllNumber,
                              CrimeType = r.CrimeType,
                              MasterCase = r.MasterCase,
                          //    LastSessionDate = r.LastSessionDate,
                           //   LastDecision = r.LastDecision,
                          }).Distinct().ToList();
            }

            ////// obtainments filter ////////

            if (searchData.HasObtainment == ObtainmentStatus.HasObtainment)
            {
                result = (from r in result
                          join master in DataContext.Cases_MasterCase on r.MasterCase equals master.ID
                          where (master.HasObtainments)
                          select new //SearchResult
                          {
                              CaseID = r.CaseID,
                              FirstLevelNumber = r.FirstLevelNumber,
                              SecondLevelNumber = r.SecondLevelNumber,
                              OverAllNumber = r.OverAllNumber,
                              CrimeType = r.CrimeType,
                              MasterCase = master.ID,
                            //  LastSessionDate = r.LastSessionDate,
                            //  LastDecision = r.LastDecision,
                          }).Distinct().ToList();
            }
            else if (searchData.HasObtainment == ObtainmentStatus.NotHasObtainment)
            {
                result = (from r in result
                          join master in DataContext.Cases_MasterCase on r.MasterCase equals master.ID
                          where !(master.HasObtainments)
                          select new //SearchResult
                          {
                              CaseID = r.CaseID,
                              FirstLevelNumber = r.FirstLevelNumber,
                              SecondLevelNumber = r.SecondLevelNumber,
                              OverAllNumber = r.OverAllNumber,
                              CrimeType = r.CrimeType,
                              MasterCase = master.ID,
                             // LastSessionDate = r.LastSessionDate,
                             // LastDecision = r.LastDecision,
                          }).Distinct().ToList();
            }

            ////// session dates type filter ////////
            if (result != null || result.Count > 0)
            {

                switch (searchData.SessionDateType)
                {
                    case SessionSearchMode.All:
                        if (searchData.SessionDate != null)
                        {
                            result2 = (from r in result
                                       join sessions in DataContext.Cases_CaseSessions on r.CaseID equals sessions.CaseID
                                       //join sessiondecisions in DataContext.Cases_SessionDecision on sessions.ID equals sessiondecisions.CaseSessionID
                                       join rolls in DataContext.CourtConfigurations_CircuitRolls on sessions.RollID equals rolls.ID
                                       where (
                                       ((rolls.SessionDate == searchData.SessionDate && searchData.SessionDate.HasValue) || !searchData.SessionDate.HasValue)
                                       || ((rolls.CircuitID == searchData.CircuitID && searchData.CircuitID.HasValue) || !searchData.CircuitID.HasValue)
                                       )
                                       select new SearchResult
                                       {
                                           CaseID = r.CaseID,
                                           FirstLevelNumber = r.FirstLevelNumber,
                                           SecondLevelNumber = r.SecondLevelNumber,
                                           OverAllNumber = r.OverAllNumber,
                                           CrimeType = r.CrimeType,
                                           MasterCase = r.MasterCase,
                                            LastSessionDate = rolls.SessionDate,
                                            LastDecision = ""//sessiondecisions.DecisionText
                                       }).Distinct().ToList();
                        }
                        else if (searchData.CircuitID.HasValue)
                        {
                            result2 = (from r in result
                                       join sessions in DataContext.Cases_CaseSessions on r.CaseID equals sessions.CaseID
                                       //join sessiondecisions in DataContext.Cases_SessionDecision on sessions.ID equals sessiondecisions.CaseSessionID
                                       join rolls in DataContext.CourtConfigurations_CircuitRolls on sessions.RollID equals rolls.ID
                                       where ((rolls.CircuitID == searchData.CircuitID && searchData.CircuitID.HasValue) || !searchData.CircuitID.HasValue)

                                       select new SearchResult
                                       {
                                           CaseID = r.CaseID,
                                           FirstLevelNumber = r.FirstLevelNumber,
                                           SecondLevelNumber = r.SecondLevelNumber,
                                           OverAllNumber = r.OverAllNumber,
                                           CrimeType = r.CrimeType,
                                           MasterCase = r.MasterCase,
                                           LastSessionDate = rolls.SessionDate,
                                            LastDecision = ""//sessiondecisions.DecisionText
                                       }).Distinct().ToList();
                        }
                        else
                        {
                            result2 = result.Select(r => new SearchResult
                            {
                                CaseID = r.CaseID,
                                FirstLevelNumber = r.FirstLevelNumber,
                                SecondLevelNumber = r.SecondLevelNumber,
                                OverAllNumber = r.OverAllNumber,
                                CrimeType = r.CrimeType
                            }).Distinct().ToList();
                        }
                        return result2;

                    case SessionSearchMode.NotReservedSession:

                        result2 = (from res in result
                                   where !(from r in result
                                           join sessions in DataContext.Cases_CaseSessions on r.CaseID equals sessions.CaseID
                                           join rolls in DataContext.CourtConfigurations_CircuitRolls on sessions.RollID equals rolls.ID
                                           select r.CaseID).ToList().Contains(res.CaseID)
                                   select new SearchResult
                                   {
                                       CaseID = res.CaseID,
                                       FirstLevelNumber = res.FirstLevelNumber,
                                       SecondLevelNumber = res.SecondLevelNumber,
                                       OverAllNumber = res.OverAllNumber,
                                       CrimeType = res.CrimeType
                                   }).Distinct().ToList();

                        return result2;

                    case SessionSearchMode.LastSessionDate:

                        SearchResult element = (from r in result
                                                join sessions in DataContext.Cases_CaseSessions on r.CaseID equals sessions.CaseID
                                                //join sessiondecisions in DataContext.Cases_SessionDecision on sessions.ID equals sessiondecisions.CaseSessionID
                                                join rolls in DataContext.CourtConfigurations_CircuitRolls on sessions.RollID equals rolls.ID
                                                select new SearchResult
                                                {
                                                    CaseID = r.CaseID,
                                                    FirstLevelNumber = r.FirstLevelNumber,
                                                    SecondLevelNumber = r.SecondLevelNumber,
                                                    OverAllNumber = r.OverAllNumber,
                                                    CrimeType = r.CrimeType,
                                                    LastSessionDate = rolls.SessionDate,
                                                    LastDecision = ""//sessiondecisions.DecisionText
                                                }).OrderByDescending(x => x.LastSessionDate).FirstOrDefault();
                        result2.Add(element);
                        return result2;

                    case SessionSearchMode.NextSessionDate:
                        result2 = (from r in result
                                   join sessions in DataContext.Cases_CaseSessions on r.CaseID equals sessions.CaseID
                                   join rolls in DataContext.CourtConfigurations_CircuitRolls on sessions.RollID equals rolls.ID
                                   where rolls.SessionDate > DateTime.Now
                                   select new SearchResult
                                   {
                                       CaseID = r.CaseID,
                                       FirstLevelNumber = r.FirstLevelNumber,
                                       SecondLevelNumber = r.SecondLevelNumber,
                                       OverAllNumber = r.OverAllNumber,
                                       CrimeType = r.CrimeType,
                                       LastSessionDate = rolls.SessionDate

                                   }).Distinct().ToList();

                        return result2;
                    default:
                        break;
                }
                return result2;
            }
            else
            {
                return null;
            }
        }
    }
}
