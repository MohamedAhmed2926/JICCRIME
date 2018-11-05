using JIC.Base;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.ServicesInterfaces
{
   public interface IAttachmentsService
    {
        AttachmentsSaveStatus AddFolder(int CaseID, long? SessionID, vw_Folders _Folder);
        AttachmentsSaveStatus EditFolder(Guid FolderGuid,string Name,int DocumentCount,string CurentUserName);
        AttachmentsSaveStatus DeleteFolder(Guid FolderGuid, string CurentUserName);
        IQueryable<vw_Folders> GetFolders(int CaseID, long? SessionID);
        AttachmentsSaveStatus AddDocument(int CaseID, long? SessionID, vw_Documents _Document);
        AttachmentsSaveStatus EditDocument(vw_Documents _Document);
        AttachmentsSaveStatus DeleteDocument(Guid DocumentGuid,string CurentUserName);
        IQueryable<vw_Documents> GetDocuments(int CaseID, long? SessionID,Guid? ParentFolder = null);
        IQueryable<vw_Documents> GetFolderDocumentsData(Guid FolderID);
        vw_Documents GetDocumentData(Guid attachmentID);
        IQueryable<vw_Folders> GetAllCaseFolders(int CaseID);
        IQueryable<vw_Documents> GetDocuments(int CaseID, long? SessionID);
    }
}
