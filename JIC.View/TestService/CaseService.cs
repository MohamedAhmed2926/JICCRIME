using JIC.Base;
using JIC.Services.ServicesInterfaces;
using JIC.Base.Views;
using JIC.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.TestService
{
    public class CaseService : ICrimeCaseService
    {
        //public CaseSaveStatus AddBasicData(vw_CaseBasicData caseBasicData, out int CaseID)
        //{
        //    return new CrimeCaseServise().AddBasicData(caseBasicData, out CaseID);

        //}

        public CaseSaveStatus AddBasicData(vw_CrimeCaseBasicData caseBasicData, out int CaseID)
        {
            throw new NotImplementedException();
        }

        public void AddCaseDefendant(int CaseID, vw_DefendantData DefendantData, out int DefendantID)
        {
            throw new NotImplementedException();
        }

        public bool AddCaseDescription(vw_CaseDescription CaseDescription, int CaseID)
        {
            throw new NotImplementedException();
        }

        public void AddCaseVictim(int CaseID, vw_VictimData vw_VictimData, out int VictimID)
        {
            throw new NotImplementedException();
        }

        public CaseSaveStatus AddFaultCaseBasicData(vw_CaseBasicData caseBasicData, out int CaseID)
        {
            throw new NotImplementedException();
        }

        public DeleteStatus DeleteBasicData(int CaseID)
        {
            throw new NotImplementedException();
        }

        public void DeleteCaseDefendant(int DefendantID)
        {
            throw new NotImplementedException();
        }

        public void DeleteCaseVictim(int DefendantID)
        {
            throw new NotImplementedException();
        }

        public bool DownloadDocument(int DocumentID)
        {
            return true;
        }

        public void EndCase(int CaseID)
        {
            throw new NotImplementedException();
        }

        public IQueryable<vw_CrimeCaseBasicData> GetAllCasesPendingDate(int CourtID)
        {
            return new List<vw_CrimeCaseBasicData>()
            {
                new vw_CrimeCaseBasicData()
                {
                    CaseID=1,
                    SecondNumberInt = 13,
                    SecondProsecutionID = 13,
                    SecondYearInt = 2017,
                    FirstNumberInt = 12,
                    FirstProsecutionID = 12,
                    FirstYearInt = 2016,
                    PoliceStationName = "قسم عين شمس",
                    MainCrimeName ="ارهاب",
                    CaseStatus = "xxx",
                    CrimeTypeName = "ارهاب"
                },
                new vw_CrimeCaseBasicData()
                {
                    CaseID=2,
                    SecondNumberInt = 14,
                    SecondProsecutionID = 14,
                    SecondYearInt = 2020,
                    FirstNumberInt = 15,
                    FirstProsecutionID = 15,
                    FirstYearInt = 2012,
                    PoliceStationName = "قسم مدينة نصر ",
                    MainCrimeName ="ارهاب",
                    CaseStatus = "xxx",
                    CrimeTypeName = "ارهاب"
                },
                new vw_CrimeCaseBasicData()
                {
                    CaseID=3,
                    SecondNumberInt = 14,
                    SecondProsecutionID = 14,
                    SecondYearInt = 2020,
                    FirstNumberInt = 15,
                    FirstProsecutionID = 15,
                    FirstYearInt = 2012,
                    PoliceStationName = "قسم مدينة نصر ",
                    MainCrimeName ="ارهاب",
                    CaseStatus = "xxx",
                    CrimeTypeName = "ارهاب"
                }
            }.AsQueryable();
        }

        public vw_CrimeCaseBasicData GetCaseBasicData(int CaseID)
        {
            throw new NotImplementedException();
        }

        public vw_CaseData GetCaseData(int CaseID)
        {
            throw new NotImplementedException();
        }

        //public vw_CaseData GetCaseData(int CaseID)
        //{
        //    vw_CaseData CaseData = new vw_CaseData();
        //    vw_CaseBasicData CaseBasicData = new vw_CaseBasicData();
        //    CaseBasicData.CourtID = 1;
        //    CaseBasicData.CaseID = CaseID;
        //    CaseBasicData.FirstNumber =45544;
        //    CaseBasicData.SecondNumber =44447;
        //    CaseBasicData.OverAllNumber =47745;
        //    CaseBasicData.CaseName ="dsfsd";
        //    CaseBasicData.MainCrimeName ="dfdsf";
        //    CaseBasicData.HasObtainment = true;
        //    CaseData.CaseBasicData = CaseBasicData;
        //    List<vw_CaseDefectsData > Defendantslist = new List<vw_CaseDefectsData>();
        //    vw_CaseDefectsData Defendants = new vw_CaseDefectsData();
        //    vw_DefendantData part = new vw_DefendantData();
        //    vw_PersonData Person = new vw_PersonData();
        //    Person.Name = "dsfdsf";
        //    part.IsCivilRights = true;
        //    Person.NatNo = "68653";
        //    part.Order = 54;
        //    //part.DefendantStatus = DefendantStatus.Guilty;
        //    part.PartyType = "dasfd";

        //    Defendants.Defendant = part;
        //    Defendants.Person = Person;
        //    Defendantslist.Add(Defendants);
        //    CaseData.Defendants = Defendantslist;
        //    List<vw_CaseDocuments> DocumentsList = new List<vw_CaseDocuments>();
        //    vw_CaseDocuments Documents = new vw_CaseDocuments();
        //    Documents.DocumentID = 1;
        //    Documents.DocumentName = "sadas";
        //    DocumentsList.Add(Documents);
        //    CaseData.Documents = DocumentsList;
        //    vw_CaseDescription CaseDescription = new vw_CaseDescription();
        //    CaseDescription.CaseDescrptionByCourts = "sdasd";
        //    CaseDescription.CaseLawItemsByCourts = "sdadsa";
        //    CaseDescription.DateCourts = DateTime.Today;
        //    CaseDescription.CaseDescrptionByProsecution = "dsad";
        //    CaseDescription.CaseLawItemsByProsecution = "weww";
        //    CaseDescription.DateProsecution = DateTime.Today;
        //    CaseData.CaseDescription = CaseDescription;
        //    List<vw_CaseDecision> CaseDecisionList = new List<vw_CaseDecision>();
        //    vw_CaseDecision CaseDecision = new vw_CaseDecision();
        //    CaseDecision.DecisionDescription = "fgdsfds";
        //    CaseDecision.DecisionDate = DateTime.Today;
        //    CaseDecisionList.Add(CaseDecision);
        //    CaseData.CaseDecision = CaseDecisionList;

        //    return CaseData;

        //}

        public IQueryable<vw_unCompletCase> GetUnCompletCases(int courtId)
        {
            return new CrimeCaseServise().GetUnCompletCases(courtId);
        }
        public CaseSaveStatus UpdateBasicData(vw_CrimeCaseBasicData caseBasicData, int CaseID)
        {
            throw new NotImplementedException();
        }

        public CaseSaveStatus UpdateBasicData(vw_CrimeCaseBasicData caseBasicData)
        {
            throw new NotImplementedException();
        }

        public void UpdateCaseStatus_AfterJudgeApprove(int CaseID)
        {
            throw new NotImplementedException();
        }
    }
}