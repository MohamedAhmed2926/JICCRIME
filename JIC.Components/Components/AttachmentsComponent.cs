using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Views;
using JIC.Base.Resources;
using JIC.Base.Interfaces;

namespace JIC.Components.Components
{
    public class AttachmentsComponent
    {
        private IDocumentsRepository DocumentsRepository;
        private IFoldersRepository FoldersRepository;

        public AttachmentsComponent( IDocumentsRepository DocumentsRepository, IFoldersRepository FoldersRepository)
        {
            this.DocumentsRepository = DocumentsRepository;
            this.FoldersRepository = FoldersRepository;
        }


        public bool IsAttachementsSaved(int CaseID)
        {
            return DocumentsRepository.IsAttachementsSaved(CaseID);
        }

        #region Documents

        public AttachmentsSaveStatus AddDocument(int CaseID, long? SessionID, vw_Documents Document)
        {
            if (FoldersRepository.DocumentsOverFlowNumber(Document.FolderID))
            {
                return AttachmentsSaveStatus.NumberOfDocumentOverFlow;
            }
            else {
                if (SessionID.HasValue)
                {
                    Document.FolderID = FoldersRepository.SessionParentFolder(CaseID, SessionID.Value);
                }
                else if (!Document.FolderID.HasValue)
                {
                    Document.FolderID = FoldersRepository.CaseParentFolder(CaseID);
                }

                return DocumentsRepository.AddDocument(CaseID, SessionID, Document);
            }
        }

        public bool DeleteCaseDocuments(int caseID)
        {
            foreach (var Folder in GetAllCaseFolders(caseID).ToList())
            {
                foreach (var Document in GetFolderDocumentsData(Folder.ID).ToList())
                {
                    if (DeleteDocument(Document.ID,Document.CurentUserName) != AttachmentsSaveStatus.Saved)
                        return false;

                }
                if (DeleteFolder(Folder.ID,Folder.CurentUserName) != AttachmentsSaveStatus.Saved)
                    return false;
            }
            return true;
        }

        public AttachmentsSaveStatus AddFolderDocuments(int CaseID, long? SessionID, List<vw_Documents> DocumentsList)
        {
            return DocumentsRepository.AddFolderDocuments(CaseID , SessionID , DocumentsList);
        }

        public AttachmentsSaveStatus DeleteDocument(Guid DocumentGuid,string CurentUserName)
        {
            return DocumentsRepository.DeleteDocument(DocumentGuid, CurentUserName);
        }

        public AttachmentsSaveStatus EditDocument(vw_Documents Document)
        {
            if (FoldersRepository.DocumentsOverFlowNumber(Document.FolderID))
            {
                return AttachmentsSaveStatus.NumberOfDocumentOverFlow;
            }
            return DocumentsRepository.EditDocument(Document);
        }

        public IQueryable<vw_Documents> GetDocuments(int CaseID, long? SessionID, Guid? ParentFolder = null)
        {
            Guid FolderID;
            if (ParentFolder.HasValue)
                FolderID = ParentFolder.Value;
            else
                FolderID = FoldersRepository.GetParentFolderID(CaseID, SessionID);
            return DocumentsRepository.GetDocuments(CaseID, SessionID).Where(p => p.FolderID == FolderID);
        }

        public vw_Documents GetDocumentData(Guid attachmentID)
        {
            return DocumentsRepository.GetDocumentByID(attachmentID);
        }

        public IQueryable<vw_Documents> GetFolderDocumentsData(Guid FolderID)
        {
            return DocumentsRepository.GetFolderDocumentsData(FolderID);
        }

        vw_Documents GetDocumentByID(Guid DocumentGuid)
        {
            return DocumentsRepository.GetDocumentByID(DocumentGuid);
        }
        #endregion

        #region Folders

        public AttachmentsSaveStatus AddFolder(int CaseID, long? SessionID, vw_Folders _Folder)
        {
            return FoldersRepository.AddFolder(CaseID, SessionID, _Folder);
        }
        public AttachmentsSaveStatus EditFolder(Guid FolderGuid, string Name, int DocumentCount, string CurentUserName)
        {
            return FoldersRepository.EditFolder(FolderGuid, Name, DocumentCount, CurentUserName);
        }
        public AttachmentsSaveStatus DeleteFolder(Guid FolderGuid,string CurentUserName)
        {
            return FoldersRepository.DeleteFolder(FolderGuid, CurentUserName);
        }
        public IQueryable<vw_Folders> GetFolders(int CaseID, long? SessionID)
        {
            return FoldersRepository.GetFolders(CaseID, SessionID);
        }
        public IQueryable<vw_Folders> GetAllCaseFolders(int CaseID)
        {
            return FoldersRepository.GetAllFolders(CaseID);
        }
        public IQueryable<vw_Documents> GetDocuments(int CaseID, long? SessionID)
        {
            return DocumentsRepository.GetDocuments(CaseID, SessionID);
        }
        public vw_Folders GetFolderByID(Guid FolderGuid)
        {
            return FoldersRepository.GetFolderByID(FolderGuid);
        }

        public bool HasAttachmentOfType(int caseID,AttachmentTypes attachmentType)
        {
            return DocumentsRepository.HasAttachmentOfType(caseID, attachmentType);
        }
        #endregion
    }
}
