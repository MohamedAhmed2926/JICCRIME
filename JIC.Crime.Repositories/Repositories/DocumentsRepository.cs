using JIC.Base.Interfaces;
using JIC.Crime.Entities.Models;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Views;
using JIC.Base.Resources;

namespace JIC.Crime.Repositories.Repositories
{
    public class DocumentsRepository : EntityRepositoryBase<Cases_CaseDocuments>, IDocumentsRepository
    {
        //private Cases_CaseDocuments Document = new Cases_CaseDocuments();
        public AttachmentsSaveStatus AddDocument(int CaseID, long? SessionID, vw_Documents _Document)
        {
            var attchmentName = DataContext.Configurations_Lookups.Find(_Document.TypeID);
            var Document = new Cases_CaseDocuments
            {
                ID = Guid.NewGuid(),
                FileName = _Document.FileName,
                DocumentTitle = _Document.DocumentTitle + " " + attchmentName.Name,
                FileData = _Document.FileData,
                FolderID = _Document.FolderID,
                UploadDate = DateTime.Now,
                UploadedBy = _Document.UploadBy,
                AttachmentTypeID = _Document.TypeID,
                CreatedBy = _Document.CurentUserName,
            };
            this.Add(Document);
            this.Save();
            return AttachmentsSaveStatus.Saved;
        }

        public AttachmentsSaveStatus AddFolderDocuments(int CaseID, long? SessionID, List<vw_Documents> DocumentsList)
        {
            foreach (var item in DocumentsList)
            {
                var Document = new Cases_CaseDocuments
                {
                    ID = Guid.NewGuid(),
                    FileName = item.FileName,
                    DocumentTitle = item.DocumentTitle,
                    FileData = item.FileData,
                    FolderID = item.FolderID,
                    UploadDate = DateTime.Now,
                    UploadedBy = item.UploadBy,
                    AttachmentTypeID = item.TypeID,
                    CreatedBy = item.CurentUserName,
                };
                this.Add(Document);
                this.Save();
            }
            return AttachmentsSaveStatus.Saved;
        }

        public AttachmentsSaveStatus DeleteDocument(Guid DocumentGuid,string CurentUserName)
        {
            var document = this.GetByID(DocumentGuid);
            if (CurentUserName == document.CreatedBy||CurentUserName=="System")
            {
                this.Delete(document);
                this.Save();
                return AttachmentsSaveStatus.Saved;
            }
            else
            {
                return AttachmentsSaveStatus.UserNotCreate;
            }
        }

        public AttachmentsSaveStatus EditDocument(vw_Documents _Document)
        {
            var attchmentName = DataContext.Configurations_Lookups.Find(_Document.TypeID);
            var Document = this.GetByID(_Document.ID);

            Document.DocumentTitle = _Document.DocumentTitle + " " + attchmentName.Name;
            //Document.FileData = _Document.FileData;
            Document.FolderID = _Document.FolderID;
            Document.AttachmentTypeID = _Document.TypeID;
            if (_Document.CurentUserName == Document.CreatedBy||_Document.CurentUserName== "System")
            {
                this.Update(Document);
                this.Save();
                return AttachmentsSaveStatus.Saved;
            }
            else
            {
                return AttachmentsSaveStatus.UserNotCreate;
            }
        }

        public IQueryable<vw_Documents> GetDocuments(int CaseID, long? SessionID)
        {
            return (from documents in DataContext.Cases_CaseDocuments
                    join folders in DataContext.Cases_CaseDocumentFolders on documents.FolderID equals folders.ID
                    where (SessionID.HasValue?folders.SessionID==SessionID:true) && folders.CaseID == CaseID
                    select new vw_Documents
                    {
                        ID = documents.ID,
                        FileName = documents.FileName,
                        FolderID = documents.FolderID,
                        DocumentTitle = documents.DocumentTitle,
                        UploadDate = documents.UploadDate,
                        TypeID = documents.AttachmentTypeID,
                        
                    });
        }
        public IQueryable<vw_Documents> GetFolderDocumentsData(Guid FolderID)
        {
            var result = (from documents in DataContext.Cases_CaseDocuments
                          where (documents.FolderID == FolderID)
                          select new vw_Documents
                          {
                              ID = documents.ID,
                              FileName = documents.FileName,
                              FolderID = documents.FolderID,
                              DocumentTitle = documents.DocumentTitle,
                              TypeID = documents.AttachmentTypeID,
                              FileData = documents.FileData,
                              CurentUserName =documents.CreatedBy
                          });
            return result;
        }
        public vw_Documents GetDocumentByID(Guid DocumentGuid)
        {
            var document = this.GetByID(DocumentGuid);
            return new vw_Documents
            {
                FileName = document.FileName,
                DocumentTitle = document.DocumentTitle,
                FileData = document.FileData,
                TypeID = document.AttachmentTypeID,
                FolderID = document.FolderID
            };
        }

        public bool IsAttachementsSaved(int CaseID)
        {
            return (from documents in DataContext.Cases_CaseDocuments
                    join folders in DataContext.Cases_CaseDocumentFolders on documents.FolderID equals folders.ID
                    where folders.CaseID == CaseID
                    && (documents.AttachmentTypeID == (int)AttachmentTypes.ProofOfEvidence || documents.AttachmentTypeID == (int)AttachmentTypes.Witness)
                    select documents.ID).Count() > 0;
        }

        public bool HasAttachmentOfType(int caseID, AttachmentTypes attachmentType)
        {
            return (from document in GetAllQuery() where document.Cases_CaseDocumentFolders.CaseID == caseID && document.AttachmentTypeID == (int)attachmentType select document).Count() > 0;
        }
    }
}
