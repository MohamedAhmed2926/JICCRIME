using JIC.Base;
using JIC.Base.Views;
using JIC.Components.Components;
using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.Services
{
    public class NotCompleteCaseService : ServiceBase, INotCompleteCasesService
    {
        private NotCompleteCaseComponent NotCompleteCaseComponent { get { return GetComponent<NotCompleteCaseComponent>(); } }
        private CrimeCaseComponent CaseComponent { get { return GetComponent<CrimeCaseComponent>(); } }
        private DefectsService DefectsService { get { return GetComponent<DefectsService>(); } }
        private AttachmentsComponent AttachmentsComponent { get { return GetComponent<AttachmentsComponent>(); } }
        public MasterCaseComponent MasterCaseComp { get { return GetComponent<MasterCaseComponent>(); } }

        public NotCompleteCaseService(CaseType caseType) : base(caseType)
        {
        }

        public bool DeleteNotCompleteCase(int CaseID)
        {
            try
            {
                using (var Transaction = BeginDatabaseTransaction())
                {
                    int MasterID=0;
                    var Result = AttachmentsComponent.DeleteCaseDocuments(CaseID) && DefectsService.DeleteCaseDefects(CaseID) && CaseComponent.DeleteCaseBasicData(CaseID,false, out MasterID);
                    

                    if (Result)
                        MasterCaseComp.DeleteCaseBasicData(MasterID);

                    if (Result && Transaction != null)
                        Transaction.Commit();
                    return Result;
                }
            }
            catch(Exception ex)
            {
                HandleException(ex);
                return false;
            }
        }

        public IQueryable<vw_unCompletCase> GetNotCompleteCase(int CourtId)
        {
            try
            {
                return NotCompleteCaseComponent.GetNotCompleteCase(CourtId);
            }
            catch (Exception e)
            {
                HandleException(e);
                return null;
            }
        }
    }
}
