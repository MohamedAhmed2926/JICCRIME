using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Services.ServicesInterfaces;
using JIC.Base.Views;
using JIC.Components.Components;

namespace JIC.Services.Services
{
    public class AttachmentsService : ServiceBase, IAttachmentsService
    {
        private AttachmentsComponent AttachmentsComponent;

        public AttachmentsService(CaseType caseType) : base(caseType)
        {
            AttachmentsComponent = GetComponent<AttachmentsComponent>();
        }



        public AttachmentsSaveStatus AddDocument(int CaseID, long? SessionID, vw_Documents _Document)
        {
            try
            {
                return AttachmentsComponent.AddDocument(CaseID, SessionID, _Document);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return AttachmentsSaveStatus.Failed;
            }
        }

        public AttachmentsSaveStatus AddFolder(int CaseID, long? SessionID, vw_Folders _Folder)
        {
            try
            {
                return AttachmentsComponent.AddFolder(CaseID, SessionID, _Folder);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return AttachmentsSaveStatus.Failed;
            }
        }

        public AttachmentsSaveStatus DeleteDocument(Guid DocumentGuid, string CurentUserName)
        {
            return AttachmentsComponent.DeleteDocument(DocumentGuid, CurentUserName);
        }

        public AttachmentsSaveStatus DeleteFolder(Guid FolderGuid, string CurentUserName)
        {
            return AttachmentsComponent.DeleteFolder(FolderGuid, CurentUserName);
        }

        public AttachmentsSaveStatus EditDocument(vw_Documents _Document)
        {
            
            return AttachmentsComponent.EditDocument(_Document);
        }
        public AttachmentsSaveStatus EditFolder(Guid FolderGuid, string Name, int DocumentCount,string CurentUserName)
        {
            return AttachmentsComponent.EditFolder(FolderGuid, Name, DocumentCount, CurentUserName);
        }

        public vw_Documents GetDocumentData(Guid attachmentID)
        {
            return AttachmentsComponent.GetDocumentData(attachmentID);
        }

        public IQueryable<vw_Documents> GetDocuments(int CaseID, long? SessionID, Guid? ParentFolder = null)
        {
            return AttachmentsComponent.GetDocuments(CaseID, SessionID, ParentFolder);
        }

        public IQueryable<vw_Documents> GetFolderDocumentsData(Guid FolderID)
        {
            return AttachmentsComponent.GetFolderDocumentsData(FolderID);
        }

        public IQueryable<vw_Folders> GetFolders(int CaseID, long? SessionID)
        {
            return AttachmentsComponent.GetFolders(CaseID, SessionID);
        }

        public IQueryable<vw_Folders> GetAllCaseFolders(int CaseID)
        {
            return AttachmentsComponent.GetAllCaseFolders(CaseID);
        }

        public IQueryable<vw_Documents> GetDocuments(int CaseID, long? SessionID)
        {
            return AttachmentsComponent.GetDocuments(CaseID,SessionID);
        }
    }
}
