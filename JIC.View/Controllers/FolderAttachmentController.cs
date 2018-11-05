using JIC.Base;
using JIC.Services.ServicesInterfaces;
using JIC.Crime.View.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIC.Crime.View.Controllers
{
    public class FolderAttachmentController : ControllerBase
    {
        public IAttachmentsService AttachmentService;
        public ISessionService sessionService;
        private ICrimeCaseService CrimeCaseService;
        public FolderAttachmentController(IAttachmentsService AttachmentService, ISessionService sessionService, ICrimeCaseService CrimeCaseService)
        {
            this.AttachmentService = AttachmentService;
            this.sessionService = sessionService;
            this.CrimeCaseService = CrimeCaseService;
        }
        // GET: FolderAttachment
        public ActionResult CaseFolders(int id,int? SessionID = null)
        {
            return CPartialView("CaseFolders",GetFullFolderViewModel(id, SessionID));
        }

        public ActionResult GetFolders(int CaseID,int? SessionID = null)
        {
            List<Base.Views.vw_KeyValueStringID> FolderLists = AttachmentService.GetFolders(CaseID, SessionID).Select(folder => new Base.Views.vw_KeyValueStringID { ID = folder.ID.ToString(), Name = folder.Name }).ToList();
            FolderLists.Insert(0, new Base.Views.vw_KeyValueStringID { ID = Guid.Empty.ToString(), Name = "بلا حافظة" });
            return CPartialView(FolderLists);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int CaseID, FolderViewModel FolderModel, long? SessionID = null)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                if (sessionService.IsSessionSentToJudge(Convert.ToInt32(SessionID)))
                return JavaScript("$(document).trigger('FolderAttachment:SentToJudge');");

            if (ModelState.IsValid)
            {
                var AddFolderSatus = AttachmentService.AddFolder(CaseID, SessionID, new Base.Views.vw_Folders
                {
                    DocumentsCount = FolderModel.FolderCount,
                    Name = FolderModel.Name,
                    UploadDate = DateTime.Now,
                    CurentUserName=CurrentUser.UserName,
                });
                switch (AddFolderSatus)
                {
                    case AttachmentsSaveStatus.Saved:
                        return JavaScript("$(document).trigger('CreateFolder:Saved');");
                    case AttachmentsSaveStatus.Failed:
                        return CPartialView("CaseFolders", GetFullFolderViewModel(CaseID, SessionID, null, FolderModel)).WithErrorMessages("خطا فى الحفظ");
                }
            }
               
                return CPartialView("CaseFolders", GetFullFolderViewModel(CaseID, SessionID, null, FolderModel));
            }

            else
            {
                ViewData["SessionEnded"] = true;
                return CPartialView("CaseFolders");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFolders(int id, List<FolderViewModel> FolderViewModel, int? SessionID = null)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;

                if (sessionService.IsSessionSentToJudge(Convert.ToInt32(SessionID)))
                return JavaScript("$(document).trigger('FolderAttachment:SentToJudge');");
            string[] messageInFolders = new string[FolderViewModel.Count];
            bool error = false;
            int count = 0;
            if (ModelState.IsValid)
            {
              
                foreach (var Folder in FolderViewModel)
                {
                    
                    int NumberDocumentinFolder = AttachmentService.GetFolderDocumentsData(Folder.AttachmentID).Count();
                    if (NumberDocumentinFolder>Folder.FolderCount)
                    {
                        //    return JavaScript("$(document).trigger('NumberOfDocumentHigh:NoChange');");
                        messageInFolders[count] = " لا يمكن تغيير عدد المستندات لقيمة أقل من عدد المستندات الحالية الحافظة رقم " + (count + 1) + "";
                        count++;
                        error = true;
                        continue;
                    }
                    string UserName = CurrentUser.UserName;
                    if (CurrentUser.UserTypeID == 1)
                    {
                        UserName = "System";
                    }
                    var UpdateFolder = AttachmentService.EditFolder(Folder.AttachmentID, Folder.Name, Folder.FolderCount.Value, UserName);
                    switch (UpdateFolder)
                    {
                        case AttachmentsSaveStatus.Saved:
                            messageInFolders[count] = "تمت العملية بنجاح  الحافظة رقم " + count + 1 + "";
                         //   count++;
                            break;
                        // return JavaScript("$(document).trigger('CreateFolder:Saved');");
                     //   return CPartialView("CaseFolders", GetFullFolderViewModel(id, SessionID, FolderViewModel)).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);
                        case AttachmentsSaveStatus.Failed:
                            messageInFolders[count] = "لم تتم العملية بنجاح الحافظة رقم " + count + 1 + "";
                            error = true;
                            count++;
                            break;
                        //    return CPartialView("GetEditFolders", FolderViewModel).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);
                        case AttachmentsSaveStatus.UserNotCreate:
                            messageInFolders[count] = "لا يمكن تعديل حافظة تم اضافته من مستخدم أخر الحافظة رقم " + count + 1 + "";
                            error = true;
                            count++;
                            break;
                            //       return JavaScript("$(document).trigger('UserNotEdite:Folder');");
                    }
                }
                string message="";
                for (int i=0;i<messageInFolders.Length; i++)
                {
                    message+=messageInFolders[i];
                   
                }
                if (error==false)
                {
                    return JavaScript("$(document).trigger('CreateFolder:Saved');");
                 //   return CPartialView("GetEditFolders", FolderViewModel).WithSuccessMessages("تمت العملية بنجاح");
                    //return JavaScript("$(document).trigger('SuccessMessage', '" + message + "');");
                }
                else
                {

                    /// return JavaScript("$(document).trigger('CreateFolder:Saved');");
                    // return CPartialView("GetEditFolders", FolderViewModel).WithErrorMessages(messageInFolders);
                    return JavaScript("$(document).trigger('ErrorMessage', '" + message + "');");

                }

                // return JavaScript("$(document).trigger('CreateFolder:Saved');");
            }
            return CPartialView("GetEditFolders", FolderViewModel);
            }

            else
            {
                ViewData["SessionEnded"] = true;
                return CPartialView("GetEditFolders");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int CaseID, Guid DeleteFolderID, long? SessionID = null)
        {
            if (sessionService.IsSessionSentToJudge(Convert.ToInt32(SessionID)))
                return JavaScript("$(document).trigger('FolderAttachmentDelete:SentToJudge');");
            var documents = AttachmentService.GetDocuments(CaseID, null).ToList();
            var documentInFolder = AttachmentService.GetFolderDocumentsData(DeleteFolderID).ToList();
            int countfile;
            var CaseData = CrimeCaseService.GetCaseBasicData(CaseID);
            if (CaseData.HasObtainment)
            {
                foreach (var documentType in documentInFolder)
                {
                    if (documentType.TypeID == 80)
                    {
                        countfile = documents.Where(e => e.TypeID == 80).ToList().Count();

                        if (countfile == 1)
                        {
                            return JavaScript("$(document).trigger('FolderHasFileCritical:NoDelete');");
                        }
                    }
                }
            }

            foreach (var documentType in documentInFolder)
            {
                if (documentType.TypeID == 82)
                {
                    countfile = documents.Where(e => e.TypeID == 80).ToList().Count();

                    if (countfile == 1)
                    {
                        return JavaScript("$(document).trigger('FolderHasFileCritical:NoDelete');");
                    }
                }
            }

            string UserName = CurrentUser.UserName;
            if (CurrentUser.UserTypeID == 1)
            {
                UserName = "System";
            }
            var DeleteFolder = AttachmentService.DeleteFolder(DeleteFolderID, UserName);
            switch (DeleteFolder)
            {
                case AttachmentsSaveStatus.Saved:
                    return JavaScript("$(document).trigger('CreateFolder:Saved');");
                case AttachmentsSaveStatus.Failed:
                    return JavaScript("$(document).trigger('CreateFolder:Failed');");
                   //return CPartialView("CaseFolders", GetFullFolderViewModel(CaseID, SessionID)).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);
                case AttachmentsSaveStatus.Folder_Has_Documents:
                    return JavaScript("$(document).trigger('CreateFolder:Folder_Has_Documents');");
                //  return CPartialView("CaseFolders", GetFullFolderViewModel(CaseID, SessionID)).WithErrorMessages("تحتوى الحافظة على ملفات");
                case AttachmentsSaveStatus.UserNotCreate:
                    return JavaScript("$(document).trigger('UserNotDelete:Folder');");
            }
           
            return CPartialView("CaseFolders", GetFullFolderViewModel(CaseID, SessionID));
        }


        #region Helpers
        private FullFolderViewModel GetFullFolderViewModel(int CaseID, long? SessionID, List<FolderViewModel> FolderViewModel = null,FolderViewModel FolderModel = null)
        {
            return new FullFolderViewModel
            {
                CaseID = CaseID,
                SessionID = SessionID,
                FolderViewModel = FolderViewModel ?? GetCaseFolders(CaseID, SessionID),
                FolderModel = FolderModel ?? new FolderViewModel()
            };
        }

        private List<FolderViewModel> GetCaseFolders(int CaseID, long? SessionID)
        {
            return AttachmentService.GetFolders(CaseID, SessionID)
                .Select(Folder => new FolderViewModel
                {
                    Name = Folder.Name,
                    FolderCount = Folder.DocumentsCount.Value,
                    AttachmentID = Folder.ID
                }).ToList();
        }
        #endregion
    }
}