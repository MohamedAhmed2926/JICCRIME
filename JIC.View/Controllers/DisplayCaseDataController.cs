using JIC.Base;
using JIC.Services.ServicesInterfaces;
using JIC.Base.Views;
using JIC.Crime.View.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JIC.Crime.View.Helpers;

namespace JIC.Crime.View.Controllers
{
    [CAuthorize()]

    public class DisplayCaseDataController : ControllerBase
    {
        private ICrimeCaseService CaseService;
        private IAttachmentsService AttachmentService;
        private ILookupService LookupService;

        public DisplayCaseDataController(ICrimeCaseService CaseService, IAttachmentsService AttachmentService, ILookupService LookupService)
        {
            this.AttachmentService = AttachmentService;
            this.CaseService = CaseService;
            this.LookupService = LookupService;
        }
        // GET: DisplayCaseData
        public ActionResult Index(int id)
            {
            if (CurrentUser != null)
            {
                if (CurrentUser.UserTypeID==(int)SystemUserTypes.schedualEmployee || CurrentUser.UserTypeID == (int)SystemUserTypes.InquiriesEmployee)
            {
                ViewBag.AllowUser =true;
            }
            try
            {
                CaseDataViewModels caseData = new CaseDataViewModels();
                vw_CaseData BasicData = CaseService.GetCaseData(id);
                CaseBasicDataViewModel caseBasicDataViewModel = new CaseBasicDataViewModel()
                {
                    CaseID = BasicData.CaseBasicData.CaseID,
                    FirstNumber = BasicData.CaseBasicData.FirstNumberInt,
                    FirstYear = BasicData.CaseBasicData.FirstYearInt,
                    FirstLevelProsecutionID = BasicData.CaseBasicData.FirstProsecutionID,
                    SecondNumber = BasicData.CaseBasicData.SecondNumberInt,
                    SecondYear = BasicData.CaseBasicData.SecondYearInt,
                    SecondLevelProcID = BasicData.CaseBasicData.SecondProsecutionID,
                  
                    OverAllNumber = BasicData.CaseBasicData.OverAllNumber,
                    OverAllNumberProsecution = BasicData.CaseBasicData.OverAllNumberProsecution,
                    OverAllNumberYear = BasicData.CaseBasicData.OverAllNumberYear,
            
                    CaseName = BasicData.CaseBasicData.CaseName,
                    MainCrime = BasicData.CaseBasicData.MainCrimeName,
                    HasObtainment = BasicData.CaseBasicData.HasObtainment,
                    FirstprosecutionName=BasicData.CaseBasicData.FirstprosecutionName,
                    SecoundProsecutionName=BasicData.CaseBasicData.SecoundProsecutionName,
                };
                if (caseBasicDataViewModel.HasObtainment)
                {
                    caseBasicDataViewModel.Obtainment = JIC.Base.Resources.Resources.HasObtainment;

                }
                else
                {
                    caseBasicDataViewModel.Obtainment = JIC.Base.Resources.Resources.NotHasObtainment;

                }
                caseData.CaseBasicDataViewModel = caseBasicDataViewModel;

                ////////////parties//المتهمين
                List<CasePartyViewModels> CasePartiesList = BasicData.Defendants
                    .Select(part => new CasePartyViewModels
                    {
                        PartyName = part.Name,
                        IsCivilRightProsecutor = part.IsCivilRightProsecutor ,
                        NationalID = part.NationalID ,
                        PartiesOrder = part.Order ,
                        DefendantStatus = part.Status,
                    }).OrderBy(e=>e.PartiesOrder).ToList();
               
                foreach (var parties in CasePartiesList)
                {
                    if (parties.IsCivilRightProsecutor)
                        parties.IsCivilRights = JIC.Base.Resources.Resources.IsCivilRightProsecutor;
                    else
                        parties.IsCivilRights = JIC.Base.Resources.Resources.IsNotCivilRightProsecutor;
                    
                    if (parties.DefendantStatus == 20)
                    {
                        parties.Status = JIC.Base.Resources.Resources.Fugitive;
                    }
                    else if(parties.DefendantStatus == 19)
                        parties.Status = JIC.Base.Resources.Resources.Arrested;
                    else if (parties.DefendantStatus == 21)
                        parties.Status = JIC.Base.Resources.Resources.UnWanted;
                }
                caseData.Parties = CasePartiesList;
                //المجنى عليهم
                List<CasePartyViewModels> VictimsList = BasicData.Victims
                   .Select(part => new CasePartyViewModels
                   {
                       PartyName = part.Name,
                       IsCivilRightProsecutor = part.IsCivilRightProsecutor,
                       NationalID = part.NationalID,
                       PartiesOrder = part.Order,
                     
    
                   }).OrderBy(e => e.PartiesOrder).ToList();

                foreach (var parties in VictimsList)
                {
                    if (parties.IsCivilRightProsecutor)
                        parties.IsCivilRights = JIC.Base.Resources.Resources.IsCivilRightProsecutor;
                    else
                        parties.IsCivilRights = JIC.Base.Resources.Resources.IsNotCivilRightProsecutor;         
                }
                caseData.Victims = VictimsList;
                //امر الاحالة 
                OrderOfAssignmentViewModels OrderOfAssignment = new OrderOfAssignmentViewModels();
                OrderOfAssignment.CaseID = BasicData.OrderOfAssignment.CaseID;
                OrderOfAssignment.Description = BasicData.OrderOfAssignment.Description;

                caseData.OrderOfAssignment = OrderOfAssignment;

                ///مرفقات القضية
                List<DocumentsViewModels> DocumentsList = BasicData.Documents.
                     Select(Document => new DocumentsViewModels
                     {
                         DocumentID = Document.DocumentID,
                         DocumentName = Document.DocumentName,
                     }).ToList();
                caseData.Documents = DocumentsList;

                //if (CurrentUser.UserTypeID.ToString() == SystemUserTypes.schedualEmployee.ToString()
                //   || CurrentUser.UserTypeID.ToString() == SystemUserTypes.InquiriesEmployee.ToString())
                //{
                //    ViewBag.AllowUser = true;
                //}
                //else
                //{
                //    ViewBag.AllowUser = false;
                //}

                ///// القرارت السابقة
                //List<DecisionViewModels> DecisionList = BasicData.CaseDecision
                //  .Select(Decision => new DecisionViewModels
                //  {
                //      DecionDesc = Decision.DecisionDescription,
                //      DecisionDate = Decision.DecisionDate,
                //  }).ToList();

                //caseData.CaseDecision = DecisionList;

                return View(caseData);
            }
            catch (Exception ex)
            {
                return ErrorPage(ex);
            }

            }

            else
            {
                return RedirectTo(Url.Action("login", "User", new { returnUrl = "/" })).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");
            }
        }
        
       
        public ActionResult CaseAttachment(int id, int? SessionID = null, Modes Mode = Modes.View)
        {
            AttachmentFolderViewModel AttachmentViewModel = new AttachmentFolderViewModel
            {
                Attachments = GetDocumentFolders(id, SessionID),
                AcordianTitle = "accordion",
                CaseID = id
            };
            AttachmentViewModel.Attachments.Add(new FolderAttachment
            {
                AttachmentID = Guid.Empty,
                Name = "ملفات منفرده",
                Attachments = GetDocumentsInFolder(id, SessionID, null).Select(document => (AttachmentBase)document).ToList()
            });
            ViewData["Mode"] = Mode;
            return CPartialView("_CaseAttachment", AttachmentViewModel);
        }
        private List<FolderAttachment> GetDocumentFolders(int CaseID, int? SessionID, Guid? ParentFolderID = null)
        {
            List<FolderAttachment> AttachmentFolders = new List<FolderAttachment>();

            var Folders = AttachmentService.GetFolders(CaseID, SessionID).ToList().Where(Folder => ((Folder.ParentFolderID == ParentFolderID && ParentFolderID.HasValue) || !ParentFolderID.HasValue)).ToList();

            foreach (var Folder in Folders)
            {
                AttachmentFolders.Add(new FolderAttachment
                {
                    Name = Folder.Name,
                    FolderCount = Folder.DocumentsCount,
                    AttachmentID = Folder.ID,
                    Attachments = GetAttachments(CaseID, SessionID, Folder.ID == Guid.Empty ? (Guid?)null : Folder.ID)
                });
            }
            return AttachmentFolders;
        }

        private List<FileAttachment> GetDocumentsInFolder(int CaseId, int? SessionID, Guid? FolderID)
        {
            var AttachmentTypes = LookupService.GetLookupsByCategory(LookupsCategories.AttachementTypes);
            var Documents = AttachmentService.GetDocuments(CaseId, SessionID, FolderID).ToList().Select(document => new FileAttachment
            {
                AttachmentID = document.ID,
                Name = document.DocumentTitle,
                UploadDate = document.UploadDate,
                AttachmentType = AttachmentTypes.Where(type => type.ID == document.TypeID).Select(type => type.Name).FirstOrDefault()
            }).ToList();
            return Documents;
        }

        private List<AttachmentBase> GetAttachments(int CaseId, int? SessionID, Guid? FolderID)
        {
            List<AttachmentBase> attachments = new List<AttachmentBase>();
            //Get Folders in Folder
            attachments.AddRange(GetDocumentFolders(CaseId, SessionID, FolderID));

            //Get Files in Folder
            attachments.AddRange(GetDocumentsInFolder(CaseId, SessionID, FolderID));

            return attachments;
        }

    }
}