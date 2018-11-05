using JIC.Base;
using JIC.Services.ServicesInterfaces;
using JIC.Base.Views;
using JIC.Crime.View.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.TestService
{
    public class AttachmentService : IAttachmentsService
    {
        static List<FolderAttachment> FolderAttachment;

        static AttachmentService()
        {
            FolderAttachment = new List<Models.FolderAttachment>
            {
                new Models.FolderAttachment{
                    Name = "حافظة 1",
                    FolderCount = 5,
                    AttachmentID = Guid.NewGuid(),
                    Attachments = new List<AttachmentBase> {
                        new FileAttachment { Name="مرفق واحد",AttachmentID = Guid.NewGuid(),AttachmentType = "شيك",UploadDate = DateTime.Today }
                    }
                }
            };
        }

        public AttachmentsSaveStatus EditDocument(vw_Documents _Document)
        {
            throw new NotImplementedException();
        }

        public IQueryable<vw_Folders> GetAllCaseFolders(int CaseID)
        {
            throw new NotImplementedException();
        }

        public vw_Documents GetDocumentData(Guid attachmentID)
        {
            throw new NotImplementedException();
        }

        public IQueryable<vw_Documents> GetDocuments(int CaseID, long? SessionID)
        {
            throw new NotImplementedException();
        }

        public IQueryable<vw_Documents> GetFolderDocumentsData(Guid FolderID)
        {
            throw new NotImplementedException();
        }

        AttachmentsSaveStatus IAttachmentsService.AddDocument(int CaseID, long? SessionID, vw_Documents _Document)
        {
            throw new NotImplementedException();
        }

        AttachmentsSaveStatus IAttachmentsService.AddFolder(int CaseID, long? SessionID, vw_Folders _Folder)
        {
            throw new NotImplementedException();
        }

        AttachmentsSaveStatus IAttachmentsService.DeleteDocument(Guid DocumentGuid, string CurentUserName)
        {
            throw new NotImplementedException();
        }

        AttachmentsSaveStatus IAttachmentsService.DeleteFolder(Guid FolderGuid,string CurentUserName)
        {
            throw new NotImplementedException();
        }

        AttachmentsSaveStatus IAttachmentsService.EditFolder(Guid FolderGuid, string Name, int DocumentCount,string CurentUserName)
        {
            throw new NotImplementedException();
        }

        IQueryable<vw_Documents> IAttachmentsService.GetDocuments(int CaseID, long? SessionID,Guid? ParentFolder)
        {
            List<vw_Documents> Documents = new List<vw_Documents>();
            foreach (var Folder in FolderAttachment)
            {
                foreach (FileAttachment Attachment in Folder.Attachments.Where(attachment => attachment is FileAttachment))
                {
                    Documents.Add(new vw_Documents
                    {
                        ID = Attachment.AttachmentID,
                        DocumentTitle = Attachment.Name,
                        FileName = Attachment.Name,
                        FolderID = Folder.AttachmentID,
                        TypeID = 1,
                        UploadDate = Attachment.UploadDate
                    });
                }
            }
            return Documents.AsQueryable();
        }

        IQueryable<vw_Folders> IAttachmentsService.GetFolders(int CaseID, long? SessionID)
        {
            var ListFolders =  new List<vw_Folders>();
            foreach (var Folder in FolderAttachment)
            {
                ListFolders.Add(new vw_Folders
                {
                    ID = Folder.AttachmentID,
                    ComputedCount = Folder.FolderCount,
                    DocumentsCount = Folder.FolderCount,
                    ParentFolderID = null,
                    Name = Folder.Name,
                    UploadDate = DateTime.Now
                });
            }
            return ListFolders.AsQueryable();
        }
    }
}