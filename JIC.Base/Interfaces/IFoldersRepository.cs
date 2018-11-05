using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
    public interface IFoldersRepository
    {
        AttachmentsSaveStatus AddFolder(int CaseID, long? SessionID, vw_Folders _Folder);
        AttachmentsSaveStatus EditFolder(Guid FolderGuid, string Name, int DocumentCount,string CurentUserName);
        AttachmentsSaveStatus DeleteFolder(Guid FolderGuid,string CurentUserName);
        IQueryable<vw_Folders> GetFolders(int CaseID, long? SessionID);
        vw_Folders GetFolderByID(Guid FolderGuid);
        Guid CaseParentFolder(int CaseID);
        Guid SessionParentFolder(int CaseID, long SessionID);
        bool FolderHasDocuments(Guid FolderGuid);
        Guid GetParentFolderID(int CaseID, long? SessionID);
        IQueryable<vw_Folders> GetAllFolders(int caseID);
        bool DocumentsOverFlowNumber(Guid? FolderID);
    }
}
