using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
    public interface IDocumentsRepository
    {
        AttachmentsSaveStatus AddDocument(int CaseID, long? SessionID, vw_Documents _Document);
        AttachmentsSaveStatus EditDocument(vw_Documents _Document);
        AttachmentsSaveStatus DeleteDocument(Guid DocumentGuid,string CurentUserName);
        AttachmentsSaveStatus AddFolderDocuments(int CaseID, long? SessionID, List<vw_Documents> DocumentsList);
        IQueryable<vw_Documents> GetDocuments(int CaseID, long? SessionID);
        IQueryable<vw_Documents> GetFolderDocumentsData(Guid FolderID);
        vw_Documents GetDocumentByID(Guid DocumentGuid);

        bool IsAttachementsSaved(int CaseID);
        bool HasAttachmentOfType(int caseID, AttachmentTypes attachmentType);
    }
}
