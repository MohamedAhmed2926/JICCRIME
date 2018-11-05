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
    public class FoldersRepository : EntityRepositoryBase<Cases_CaseDocumentFolders>, IFoldersRepository
    {
        private Cases_CaseDocumentFolders Folder = new Cases_CaseDocumentFolders();

        public AttachmentsSaveStatus AddFolder(int CaseID, long? SessionID, vw_Folders _Folder)
        {
            if (SessionID.HasValue)
            {
                Folder.ID = Guid.NewGuid();
                Folder.Name = _Folder.Name;
                Folder.ParentFolderID = SessionParentFolder(CaseID, SessionID.Value);
                Folder.CaseID = CaseID;
                Folder.SessionID = SessionID;
                Folder.DocumentsCount = _Folder.DocumentsCount;
                Folder.ComputedDocumentsCount = _Folder.ComputedCount;
                Folder.Date = DateTime.Now;
                Folder.CreatedBy = _Folder.CurentUserName;
                this.Add(Folder);
                this.Save();
                return AttachmentsSaveStatus.Saved;
            }
            else
            {
                Folder.ID = Guid.NewGuid();
                Folder.Name = _Folder.Name;
                Folder.ParentFolderID = CaseParentFolder(CaseID);
                Folder.CaseID = CaseID;
                Folder.SessionID = SessionID;
                Folder.DocumentsCount = _Folder.DocumentsCount;
                Folder.ComputedDocumentsCount = _Folder.ComputedCount;
                Folder.Date = DateTime.Now;
                Folder.CreatedBy = _Folder.CurentUserName;
                this.Add(Folder);
                this.Save();
                return AttachmentsSaveStatus.Saved;
            }
        }

        public AttachmentsSaveStatus DeleteFolder(Guid FolderGuid,string CurentUserName)
        {
            try
            {
                if (FolderHasDocuments(FolderGuid))
                {
                    return AttachmentsSaveStatus.Folder_Has_Documents;
                }
                else
                {
                    var folder = this.GetByID(FolderGuid);
                    if (CurentUserName == folder.CreatedBy||CurentUserName=="System")
                    {
                        this.Delete(folder);
                        this.Save();
                        return AttachmentsSaveStatus.Saved;
                    }
                    else
                    {
                        return AttachmentsSaveStatus.UserNotCreate;
                    }
                }
            }
            catch (Exception)
            {
                return AttachmentsSaveStatus.Failed;
            }
        }

        public AttachmentsSaveStatus EditFolder(Guid FolderGuid, string Name, int DocumentCount,string CurentUserName)
        {
            try
            {
                var folder = this.GetByID(FolderGuid);
                folder.Name = Name;
                folder.DocumentsCount = DocumentCount;
                if (CurentUserName ==  folder.CreatedBy||CurentUserName=="System") {
                    this.Update(folder);
                    this.Save();

                    return AttachmentsSaveStatus.Saved;
                }
                else
                {
                    return AttachmentsSaveStatus.UserNotCreate;
                }
            }
            catch (Exception)
            {
                return AttachmentsSaveStatus.Failed;
            }
        }

        public IQueryable<vw_Folders> GetFolders(int CaseID, long? SessionID)
        {
            IQueryable<vw_Folders> Result;

            if (SessionID.HasValue)
            {
                Result = (from folders in DataContext.Cases_CaseDocumentFolders
                          where (folders.CaseID == CaseID && folders.SessionID == SessionID && folders.ParentFolderID != null)
                          select new vw_Folders
                          {
                              ID = folders.ID,
                              Name = folders.Name,
                              DocumentsCount = folders.DocumentsCount,
                              UploadDate = folders.Date
                          });
            }
            else
            {
                Result = (from folders in DataContext.Cases_CaseDocumentFolders
                          where (folders.CaseID == CaseID && folders.ParentFolderID != null)
                          select new vw_Folders
                          {
                              ID = folders.ID,
                              Name = folders.Name,
                              DocumentsCount = folders.DocumentsCount,
                              UploadDate = folders.Date
                          });
            }
            return Result;
        }

        public vw_Folders GetFolderByID(Guid FolderGuid)
        {
            var folder = this.GetByID(FolderGuid);
            vw_Folders ResultFolder = new vw_Folders();

            ResultFolder.Name = folder.Name;
            ResultFolder.DocumentsCount = folder.DocumentsCount;

            return ResultFolder;
        }

        #region Methods
        public Guid AddParentFolder(int CaseID)
        {
            int StatusID = (from _case in DataContext.Cases_Cases
                            where _case.ID == CaseID && _case.IsDeleted != true
                            select _case.CaseStatusID).FirstOrDefault();

            if (StatusID == (int)CaseStatuses.New)
            {
                Folder.Name = Resources.NewCaseDocuments;
            }
            else
            {
                Folder.Name = Resources.ProsecutionDocuments;
            }
            Folder.ID = Guid.NewGuid();
            Folder.CaseID = CaseID;
            Folder.SessionID = null;
            Folder.Date = DateTime.Now;

            this.Add(Folder);
            this.Save();

            return Folder.ID;
        }
        public Guid AddSessionFolder(int CaseID, long SessionID)
        {
            long RollID = (from session in DataContext.Cases_CaseSessions
                           where session.ID == SessionID
                           select session.RollID).FirstOrDefault();

            DateTime RollDate = (from rolls in DataContext.CourtConfigurations_CircuitRolls
                                 where rolls.ID == RollID
                                 select rolls.SessionDate).FirstOrDefault();

            Folder.Name = Resources.SessionFolder + RollDate.Date.ToShortDateString();
            Folder.ID = Guid.NewGuid();
            Folder.CaseID = CaseID;
            Folder.SessionID = SessionID;
            Folder.Date = DateTime.Now;

            this.Add(Folder);
            this.Save();

            return Folder.ID;
        }
        public Guid CaseParentFolder(int CaseID)
        {
            Guid FolderID;
            Guid ParentFolder = (from folder in DataContext.Cases_CaseDocumentFolders
                                 where folder.CaseID == CaseID && folder.SessionID == null && folder.ParentFolderID == null
                                 select folder.ID).FirstOrDefault();

            if (string.IsNullOrEmpty(ParentFolder.ToString()) || ParentFolder == Guid.Empty)
            {
                FolderID = AddParentFolder(CaseID);
                return FolderID;
            }
            else
            {
                FolderID = ParentFolder;
                return FolderID;
            }

        }
        public Guid SessionParentFolder(int CaseID, long SessionID)
        {
            Guid FolderID;

            Guid ParentFolder = (from folder in DataContext.Cases_CaseDocumentFolders
                                 where folder.CaseID == CaseID && folder.SessionID == SessionID && folder.ParentFolderID == null
                                 select folder.ID).FirstOrDefault();

            if (string.IsNullOrEmpty(ParentFolder.ToString()) || ParentFolder == Guid.Empty)
            {
                FolderID = AddSessionFolder(CaseID, SessionID);
                return FolderID;
            }
            else
            {
                FolderID = ParentFolder;
                return FolderID;
            }

        }
        public vw_Folders GetSessionFolder(int CaseID, long SessionID)
        {
            var result = (from folder in DataContext.Cases_CaseDocumentFolders
                          where folder.CaseID == CaseID && folder.SessionID == SessionID && folder.ParentFolderID == null
                          select new vw_Folders
                          {
                              ID = folder.ID
                          }).FirstOrDefault();
            return result;
        }

        public bool FolderHasDocuments(Guid FolderGuid)
        {
            return (from Documents in DataContext.Cases_CaseDocuments
                    join folder in DataContext.Cases_CaseDocumentFolders on Documents.FolderID equals FolderGuid
                    select Documents.ID).Any();
        }

        public Guid GetParentFolderID(int CaseID, long? SessionID)
        {
            if (SessionID.HasValue)
            {
                return (from folder in DataContext.Cases_CaseDocumentFolders
                        where folder.CaseID == CaseID && folder.SessionID == SessionID.Value && folder.ParentFolderID == null
                        select folder.ID).FirstOrDefault();
            }
            else
            {
                return (from folder in DataContext.Cases_CaseDocumentFolders
                        where folder.CaseID == CaseID && folder.SessionID == null && folder.ParentFolderID == null
                        select folder.ID).FirstOrDefault();
            }
        }

        public IQueryable<vw_Folders> GetAllFolders(int caseID)
        {
            return (from folders in DataContext.Cases_CaseDocumentFolders
             where (folders.CaseID == caseID)
             select new vw_Folders
             {
                 ID = folders.ID,
                 Name = folders.Name,
                 DocumentsCount = folders.DocumentsCount,
                 UploadDate = folders.Date,
                 CurentUserName= folders.CreatedBy

             });
        }
      public bool DocumentsOverFlowNumber(Guid? FolderID)
        {
            if (FolderID == null)
            {
                return false;
            }
                var DocumentCount = GetByID(FolderID.Value).DocumentsCount;
                var numberDocumentInFolder = (from folders in DataContext.Cases_CaseDocumentFolders
                                              join document in DataContext.Cases_CaseDocuments on folders.ID equals document.FolderID
                                              where folders.ID == FolderID
                                              select new
                                              {

                                              }).Count();
                if (DocumentCount > numberDocumentInFolder)
                {
                    return false;
                }
            
            else
            {
                return true;
            }
        }
        #endregion
    }
}
