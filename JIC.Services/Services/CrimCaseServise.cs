using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Views;
using JIC.Components.Components;
using JIC.Services.EventHandler;
using JIC.Services.ServicesInterfaces;

namespace JIC.Services.Services
{
    public class CrimeCaseServise : ServiceBase, ICrimeCaseService, IOverAllNumberService

    {
        #region Property
        public MasterCaseComponent MasterCaseComponent { get { return GetComponent<MasterCaseComponent>(); } }
        public CrimeCaseComponent CaseComponent { get { return GetComponent<CrimeCaseComponent>(); } }
        public OverallNumberComponent OverAllNumberCombonent { get { return GetComponent<OverallNumberComponent>(); } }
        public DefendantsComponent DefendantsComp { get { return GetComponent<DefendantsComponent>(); } }
        public VictimsComponent VictimsComp { get { return GetComponent<VictimsComponent>(); } }
        public CaseDescriptionComponent DescComponent { get { return GetComponent<CaseDescriptionComponent>(); } }
        public DecisionsComponent DecisionsComp { get { return GetComponent<DecisionsComponent>(); } }
        public AttachmentsComponent AttachComponent { get { return GetComponent<AttachmentsComponent>(); } }
        public SessionsComponent SessionsComp { get { return GetComponent<SessionsComponent>(); } }
        public OverallNumberComponent OverAllNumberComponent { get { return GetComponent<OverallNumberComponent>(); } }
        public AttachmentsComponent AttachmentsComponent { get { return GetComponent<AttachmentsComponent>(); } }
        public DefendantsComponent DefendantsComponent { get { return GetComponent<DefendantsComponent>(); } }
        public OrderOfAssignmentComponent OrderOfAssignmentComponent { get { return GetComponent<OrderOfAssignmentComponent>(); } }
        #endregion
        public CrimeCaseServise() : base(CaseType.Crime)
        {
        }
        #region CaseService


        public CaseSaveStatus AddBasicData(vw_CrimeCaseBasicData caseBasicData, out int CaseID)
        {
            try
            {
                int MasterCaseID;
                CaseSaveStatus CaseResult = MasterCaseComponent.AddCaseBasicData(caseBasicData, out MasterCaseID);
                if (CaseResult == CaseSaveStatus.SecondNumberExistBefore)
                {
                    CaseID = 0;
                    return CaseSaveStatus.SecondNumberExistBefore;
                }

                else
                {
                    using (var Transaction = BeginDatabaseTransaction())
                    {
                        CaseID = CaseComponent.AddCaseData(caseBasicData, MasterCaseID);
                        if (Transaction != null)
                            Transaction.Commit();
                        return CaseSaveStatus.Saved;
                    }
                }
            }
            catch (Exception ex)
            {
                CaseID = 0;
                HandleException(ex);
                return CaseSaveStatus.Failed;
            }

        }

        public DeleteStatus DeleteBasicData(int CaseID)
        {
            try
            {
                using (var Transaction = BeginDatabaseTransaction())
                {
                  int MasterCase = 0;
                    var cases = GetCaseBasicData(CaseID);
                    //AttachmentsComponent.DeleteCaseDocuments(CaseID);
                    //  List<vw_CaseDefectsData> Def=DefendantsComp.GetDefendantsByCaseID(CaseID);
                    //foreach (vw_CaseDefectsData X in Def)
                    //{ DefendantsComp.DeleteDefendant(CaseID, X.ID); }
                    //List<vw_CaseDefectsData> Vic = VictimsComp.GetVictimsByCaseID (CaseID);
                    //foreach (vw_CaseDefectsData Y in Vic)
                    //{ VictimsComp.DeleteVictim(CaseID, Y.ID); }
                    CaseComponent.DeleteCaseBasicData(CaseID,cases.IsComplete, out MasterCase);
                    if (cases.IsComplete)
                    { OverAllNumberCombonent.DisableOverAll(CaseID); }
                    else
                    { MasterCaseComponent.DeleteCaseBasicData(MasterCase); }
                   
                   // 

                    
                   
                    

                    if (Transaction != null)
                        Transaction.Commit();
                    return DeleteStatus.Deleted;
                }

            }
            catch (Exception ex)
            {
                HandleException(ex);
                return DeleteStatus.NotDeleted;
            }
        }
        public bool AddCaseDescription(vw_CaseDescription CaseDescription)
        {
            throw new NotImplementedException();
        }

        public vw_CaseData GetCaseData(int CaseID)
        {
            vw_CaseData CaseFulldata = new vw_CaseData();


            CaseFulldata.CaseBasicData = CaseComponent.GetCaseData(CaseID);
            CaseFulldata.Defendants = DefendantsComp.GetDefendantsByCaseID(CaseID);
            CaseFulldata.Victims = VictimsComp.GetVictimsByCaseID(CaseID);
            CaseFulldata.CaseDecision = DecisionsComp.GetCaseDecisions(CaseID);
            CaseFulldata.CaseSessions = SessionsComp.GetSessionDataByCaseID(CaseID);
            CaseFulldata.OrderOfAssignment = OrderOfAssignmentComponent.GetOrderByID(CaseID);
            //  CaseFulldata.CaseDescription = DescComponent .GetCaseDescriptionByCaseID (CaseID);
            List<vw_Documents> Doc = AttachComponent.GetDocuments(CaseID, null, null).ToList();
            List<vw_CaseDocuments> Documents = new List<vw_CaseDocuments>();
            foreach (vw_Documents d in Doc)
            {
                Documents.Add(new vw_CaseDocuments { DocumentID_Guid = d.ID, DocumentName = d.DocumentTitle });
            }
            CaseFulldata.Documents = Documents;

            return CaseFulldata;

        }

        public CaseSaveStatus UpdateBasicData(vw_CrimeCaseBasicData caseBasicData)
        {
            try
            {
                // using (var Transaction = BeginDatabaseTransaction())
                {
                    int MasterCaseID = CaseComponent.GetMasterCaseID(caseBasicData.CaseID);
                    if (MasterCaseComponent.IsSecondLevelNumberExistBefore(caseBasicData.SecondNumberInt, caseBasicData.SecondYearInt, caseBasicData.SecondProsecutionID.Value, MasterCaseID))
                    {
                        return CaseSaveStatus.SecondNumberExistBefore;
                    }
                    else
                    {
                        CaseComponent.UpdateCaseBasicData(caseBasicData);

                        MasterCaseComponent.UpdateCaseBasicData(MasterCaseID, caseBasicData);


                        //if (Transaction != null)
                        //        Transaction.Commit();
                        return CaseSaveStatus.Saved;
                    }
                }

            }
            catch (Exception ex)
            {
                HandleException(ex);
                return CaseSaveStatus.Failed;
            }
        }

        public IQueryable<vw_unCompletCase> GetUnCompletCases(int courtId)
        {
            return MasterCaseComponent.GetUnComptapleCases(courtId);
        }

        public vw_CrimeCaseBasicData GetCaseBasicData(int CaseID)
        {
            try
            {
                return CaseComponent.GetCaseData(CaseID);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return null;
            }
        }
        public AddOverAllStatus AddOverAllNumber(int CaseID, out long Number, out int ProsecutionID, out int Year, out List<AddOverAllStatus> Messages)
        {
            try
            {
                Number = ProsecutionID = Year = 0;
                Messages = new List<AddOverAllStatus>();

                using (var Trans = BeginDatabaseTransaction())
                {
                    AddOverAllStatus CaseComplete;
                    if (!IsCaseComplete(CaseID, out CaseComplete, out Messages))
                    {
                        return CaseComplete;
                    }

                   //vw_CrimeCaseBasicData CBD= GetCaseBasicData(CaseID);
                   
                   // CBD.OverAllId = OverAllNumberCombonent.AddOverAll(CBD);
                   // MasterCaseComponent.UpdateCaseBasicData(CaseID,CBD);

                    else if (OverAllNumberComponent.AddOverAll(CaseID, out Number, out ProsecutionID, out Year))
                    {
                        Event(new OverAllNumberAdded { CaseID = CaseID });
                        Trans.Commit();
                        return AddOverAllStatus.Saved;
                    }
                    else
                        return AddOverAllStatus.Fail;
                }

            }
            catch (Exception ex)
            {
                HandleException(ex);
                Messages = new List<AddOverAllStatus>();
                Number = ProsecutionID = Year = 0;
                return AddOverAllStatus.Fail;
            }
        }

        public AddOverAllStatus EditOverAllNumber(int CaseID, long Number, int Year)
        {
            return OverAllNumberComponent.EditOverAll(CaseID, Number, CaseComponent.GetCaseData(CaseID).FirstProsecutionID, Year);
        }

        public AddOverAllStatus DeleteOverAllNumber(int CaseID)
        {
            throw new NotImplementedException();
        }
        private bool IsCaseComplete(int CaseID, out AddOverAllStatus OverAllStatus, out List<AddOverAllStatus> Messages)
        {
            Messages = new List<AddOverAllStatus>();
            if (!AttachmentsComponent.HasAttachmentOfType(CaseID, AttachmentTypes.ProofOfEvidence) && !AttachmentsComponent.HasAttachmentOfType(CaseID, AttachmentTypes.Referral))
            {
                Messages.Add(AddOverAllStatus.Document);
            }
            if (GetCaseBasicData(CaseID).HasObtainment && !AttachmentsComponent.HasAttachmentOfType(CaseID, AttachmentTypes.Obtainment))
            {
                Messages.Add(AddOverAllStatus.Obtainment);
            }
            if (!OrderOfAssignmentComponent.IsValid(CaseID))
            {
                Messages.Add(AddOverAllStatus.Description);
            }
            if (!DefendantsComponent.IsValid(CaseID))
            {
                Messages.Add(AddOverAllStatus.Defendent);
            }

            if (Messages.Count != 0)
            {
                OverAllStatus = AddOverAllStatus.Error;
                return false;
            }
            else if(string.IsNullOrEmpty(GetCaseBasicData(CaseID).OverAllId.ToString()))
            {
                OverAllStatus = AddOverAllStatus.Saved;
                return true;
            }
            else
            {
                OverAllStatus = AddOverAllStatus.OverAllReserved;
                return false;
            }
        }

        public IQueryable<vw_CrimeCaseBasicData> GetAllCasesPendingDate(int CourtID)
        {
            return CaseComponent.GetAllCasesPendingDate(CourtID);
        }

        public List<vw_CrimeCaseBasicData> GetAllCaseBasicData(int CourtID)
        {
            throw new NotImplementedException();
        }

        public void UpdateCaseStatus_AfterJudgeApprove(int CaseID)
        {
            CaseComponent.UpdateCaseStatus_AfterJudgeApprove(CaseID);
        }

        public CaseSaveStatus AddFaultCaseBasicData(vw_CaseBasicData caseBasicData, out int CaseID)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
