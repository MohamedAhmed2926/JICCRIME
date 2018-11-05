using JIC.Base;
using JIC.Services.ServicesInterfaces;
using JIC.Crime.View.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JIC.Base.Views;
using JIC.Crime.View.Helpers;

namespace JIC.Crime.View.Controllers
{
    [CAuthorize(SystemUserTypes.schedualEmployee, SystemUserTypes.Secretary, SystemUserTypes.ElementaryCourtAdministrator,SystemUserTypes.Judge)]
    public class AttachmentController : ControllerBase
    {
        private IAttachmentsService AttachmentService;
        private ILookupService LookupService;
        private ISessionService sessionService;
        private ICrimeCaseService CrimeCaseService;


        public AttachmentController(IAttachmentsService AttachmentService, ILookupService LookupService, ISessionService sessionService, ICrimeCaseService CrimeCaseService)
        {
            this.AttachmentService = AttachmentService;
            this.LookupService = LookupService;
            this.sessionService = sessionService;
            this.CrimeCaseService = CrimeCaseService;
        }

        // GET: Attachment
        public ActionResult Index(int id, int? SessionID = null/*, bool SavedEdite = false, bool SavedAdd = false*/)
        {
            if (CurrentUser != null)
            {
                //ViewBag.SavedEdit = SavedEdite;
                //string x = Request.Url.LocalPath;
                //if (ViewBag.SavedEdit)
                //{
                //    return JavaScript("$(document).trigger('FileAttachment:Edited');");

                //}
                //if (SavedAdd)
                //{
                //    return JavaScript("$(document).trigger('FolderAttachment:Saved');");
                //}
                if (Request.Url.LocalPath.Contains("/ALLMinutesOfSession/Index/"))
            {
                return CPartialView(GetAddAttachmentViewModel(id, SessionID));
            }
            else
                return View(GetAddAttachmentViewModel(id, SessionID));
            }

            else
            {
                return RedirectTo(Url.Action("login", "User", new { returnUrl = "/" })).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");
            }
        }
        public ActionResult DownloadAttachment(Guid AttachmentID)
        {
            Base.Views.vw_Documents Document = AttachmentService.GetDocumentData(AttachmentID);
            return File(Document.FileData, MimeMapping.GetMimeMapping(Document.FileName), Document.FileName);
        }

        private AddAttachmentViewModel GetAddAttachmentViewModel(int CaseID, long? SessionID)
        {
            return new AddAttachmentViewModel
            {
                UserTypeID = CurrentUser.UserTypeID,
                CaseID = CaseID,
                SessionID = SessionID,
                //FoldersList = AttachmentService.GetFolders(CaseID, SessionID).Select(folder => new Base.Views.vw_KeyValueStringID { ID = folder.ID.ToString(), Name = folder.Name }).ToList(),
                AttachmentTypes = LookupService.GetLookupsByCategory(LookupsCategories.AttachementTypes)
            };
        }

        public ActionResult CreateAttachment(int id, int? SessionID = null)
        {
            return CPartialView(GetAddAttachmentViewModel(id, SessionID));
        }

        [HttpPost]
        public ActionResult CreateAttachment(int id, AddAttachmentViewModel _Attachment, long? SessionID = null)
        {
            if (CurrentUser != null)
            {
                if (sessionService.IsSessionSentToJudge(Convert.ToInt32(SessionID)))
                    return JavaScript("$(document).trigger('FileAttachment:SentToJudge');");

                if (ModelState.IsValid || _Attachment.FolderID.ToString() != "00000000-0000-0000-0000-000000000000")
                {
                    var AddDocumentStatus = AttachmentsSaveStatus.Folder_Has_Documents;
                    if (_Attachment.FolderID.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        AddDocumentStatus = AttachmentService.AddDocument(id, null, new Base.Views.vw_Documents
                        {
                            FolderID = _Attachment.FolderID == Guid.Empty ? (Guid?)null : _Attachment.FolderID,
                            DocumentTitle = _Attachment.Name,
                            TypeID = (int)_Attachment.AttachmentType,
                            FileName = _Attachment.FileName,
                            FileData = System.IO.File.ReadAllBytes(_Attachment.filePath)
                            ,
                            CurentUserName = CurrentUser.UserName
                        });

                    }
                    else
                    {
                        AddDocumentStatus = AttachmentService.AddDocument(id, SessionID, new Base.Views.vw_Documents
                        {
                            FolderID = _Attachment.FolderID == Guid.Empty ? (Guid?)null : _Attachment.FolderID,
                            DocumentTitle = _Attachment.Name,
                            TypeID = (int)_Attachment.AttachmentType,
                            FileName = _Attachment.FileName,
                            FileData = System.IO.File.ReadAllBytes(_Attachment.filePath)
                            ,
                            CurentUserName = CurrentUser.UserName
                        });
                    }
                    // bool SavedAdd = true;
                    switch (AddDocumentStatus)
                    {
                        case AttachmentsSaveStatus.Saved:
                            return JavaScript("$(document).trigger('FolderAttachment2:Saved');");
                        //    return RedirectTo(Url.Action("Index", new { id = _Attachment.CaseID, SessionID = SessionID, SavedAdd = SavedAdd })).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);
                        // return JavaScript("<script>$('#CaseAttachmentsDiv').trigger('update');</script>");
                        case AttachmentsSaveStatus.Failed:
                            break;
                        case AttachmentsSaveStatus.NumberOfDocumentOverFlow:
                            //   return RedirectTo(Url.Action("Index", new { id = _Attachment.CaseID, SessionID = SessionID, SavedAdd = SavedAdd })).WithErrorMessages("الحافظة ممتلئة");
                            return JavaScript("$(document).trigger('NumberOfDocumentOverFlow:error');");
                    }
                }
                else
                {
                    ViewData["SessionEnded"] = false;
                    Response.StatusCode = 404;
                    //return CaseAttachment(id);
                    return CPartialView(_Attachment);
                }
                   return Json(new { });
            }
            else
            {
                ViewData["SessionEnded"] = true;
                return PartialView();
            }
            }

        public ActionResult CaseAttachment(int id, int? SessionID = null)
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
            ViewData["ShowModal"] = true;
            return CPartialView("_FolderGrid", AttachmentViewModel);
        }



        public ActionResult Delete(int CaseID, Guid AttachmentID, long? SessionID = null)
        {
            vw_Documents DocumentData = AttachmentService.GetDocumentData(AttachmentID);
            AttachmentViewModel model = new AttachmentViewModel
            { AttachmentTypeID = DocumentData.TypeID,
            };
            return CPartialView(new FileAttachment { AttachmentID = AttachmentID, CaseID = CaseID, SessionID = SessionID, AttachmentType= model.AttachmentTypeID.ToString() });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(FileAttachment fileAttachment)
        {
            if (CurrentUser != null)
            {
                if (sessionService.IsSessionSentToJudge(Convert.ToInt32(fileAttachment.SessionID)))
                    return JavaScript("$(document).trigger('FileAttachment:SentToJudge');");

                var documents = AttachmentService.GetDocuments(fileAttachment.CaseID.Value, null).ToList();
                int countfile;
                int caseid = 0;
                if (fileAttachment.CaseID.HasValue)
                    caseid = fileAttachment.CaseID.Value;
                vw_CrimeCaseBasicData CaseData = CrimeCaseService.GetCaseBasicData(caseid);

                if (CaseData.HasObtainment)
                {
                    if (fileAttachment.AttachmentType == "80")
                    {
                        countfile = documents.Where(e => e.TypeID == int.Parse(fileAttachment.AttachmentType)).ToList().Count();

                        if (countfile == 1)
                        {
                            return JavaScript("$(document).trigger('FileCritical:NoDelete');");
                        }
                    }
                }

                if (fileAttachment.AttachmentType == "82")
                {
                    countfile = documents.Where(e => e.TypeID == int.Parse(fileAttachment.AttachmentType)).ToList().Count();

                    if (countfile == 1)
                    {
                        return JavaScript("$(document).trigger('FileCritical:NoDelete');");
                    }
                }
                string UserName = CurrentUser.UserName;
                if (CurrentUser.UserTypeID == 1)
                {
                    UserName = "System";
                }
                switch (AttachmentService.DeleteDocument(fileAttachment.AttachmentID, UserName))
                {
                    case AttachmentsSaveStatus.Saved:
                        return JavaScript("$(document).trigger('FileAttachment:Delete');");
                    case AttachmentsSaveStatus.Failed:
                        return CPartialView(fileAttachment).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);
                    case AttachmentsSaveStatus.UserNotCreate:
                        return JavaScript("$(document).trigger('UserNotDelete:Delete');");
                }
                ViewData["SessionEnded"] = false;
                return CPartialView(fileAttachment);
            }
            else
            {
                ViewData["SessionEnded"] = true;
                return CPartialView();
            }

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

        private List<AttachmentBase> GetAttachments(int CaseId, int? SessionID, Guid? FolderID)
        {
            List<AttachmentBase> attachments = new List<AttachmentBase>();
            //Get Folders in Folder
            attachments.AddRange(GetDocumentFolders(CaseId, SessionID, FolderID));

            //Get Files in Folder
            attachments.AddRange(GetDocumentsInFolder(CaseId, SessionID, FolderID));

            return attachments;
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
      
        [HttpGet]
        public ActionResult Edit(Guid AttachmentID, int CaseID, long? SessionID = null)
        {
            vw_Documents DocumentData = AttachmentService.GetDocumentData(AttachmentID);
            AttachmentViewModel model = new AttachmentViewModel
            {SessionID=SessionID,
                AttachmentID = AttachmentID,
                AttachmentTypeID = DocumentData.TypeID,
                Name = DocumentData.DocumentTitle,
                AttachmentTypes = LookupService.GetLookupsByCategory(LookupsCategories.AttachementTypes),
                FoldersList = AttachmentService.GetAllCaseFolders(CaseID).Select(x => new vw_KeyValueStringID
                {
                    ID = x.ID.ToString(),
                    Name = (x.Name == "مستندات قضية جديدة") ? "بلا حافظة" : x.Name
                }).ToList(),
                FolderID = DocumentData.FolderID,
                FileName = DocumentData.FileName,
                filePath = System.Text.Encoding.Default.GetString(DocumentData.FileData)

            };
            return CPartialView(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AttachmentViewModel model)
        {
            if (sessionService.IsSessionSentToJudge(Convert.ToInt32(model.SessionID)))
                return JavaScript("$(document).trigger('FileAttachment:SentToJudge');");
            var documents = AttachmentService.GetDocuments(model.CaseID, null).ToList();
            int documentTypeEdite = documents.Single(e => e.ID == model.AttachmentID).TypeID;
            int countfile;
            var CaseData = CrimeCaseService.GetCaseBasicData(model.CaseID);
            if (CaseData.HasObtainment)
            {
                if (documentTypeEdite == 80)
                {
                    countfile = documents.Where(e => e.TypeID == documentTypeEdite).ToList().Count();

                    if (countfile == 1)
                    {
                        return JavaScript("$(document).trigger('FileCritical:NoEdite');");
                    }
                }
            }

            if (documentTypeEdite == 82)
            {
                countfile = documents.Where(e => e.TypeID == documentTypeEdite).ToList().Count();

                if (countfile == 1)
                {
                    return JavaScript("$(document).trigger('FileCritical:NoEdite');");
                }
            }
            if (CurrentUser != null)
            {
                if (ModelState.IsValid)
            {
                vw_Documents document = new vw_Documents
                {
                    DocumentTitle = model.Name,
                    FolderID = model.FolderID,
                    ID = model.AttachmentID,
                    TypeID = model.AttachmentTypeID,
                    FileName = model.FileName,
                    CurentUserName =CurrentUser.UserName
                };
                if (CurrentUser.UserTypeID == 1)
                {
                    document.CurentUserName = "System";
                }
                switch (AttachmentService.EditDocument(document))
                {
                    case AttachmentsSaveStatus.Saved:
                      //  bool SavedEdite = true;
                        // return RedirectTo(Url.Action("Index",new { id=model.CaseID, SessionID=model.SessionID, SavedEdite = SavedEdite })).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);
                        return JavaScript("$(document).trigger('FileAttachment:Edited');");
                    //     return JavaScript("$(document).trigger('FileAttachment:Edited');");
                    case AttachmentsSaveStatus.Failed:
                        return CPartialView(model).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);
                    case AttachmentsSaveStatus.UserNotCreate:
               
                        return JavaScript("$(document).trigger('UserNotEdit:Edited');");
                    case AttachmentsSaveStatus.NumberOfDocumentOverFlow:
                        //   return RedirectTo(Url.Action("Index", new { id = _Attachment.CaseID, SessionID = SessionID, SavedAdd = SavedAdd })).WithErrorMessages("الحافظة ممتلئة");
                        return JavaScript("$(document).trigger('NumberOfDocumentOverFlowEdite:error');");
                }
     }
                ViewData["SessionEnded"] = false;
                return CPartialView(model);
            }

            else
            {
                ViewData["SessionEnded"] = true;
                return CPartialView();
            }

        }
    }
}