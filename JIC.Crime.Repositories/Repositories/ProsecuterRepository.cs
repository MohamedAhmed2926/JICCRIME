using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.views;
using JIC.Crime.Entities.Models;
using JIC.Repositories.DBInteractions;
using System.Collections.Generic;
using System.Linq;


namespace JIC.Crime.Repositories.Repositories
{
    public class ProsecuterRepository : EntityRepositoryBase<Configurations_Prosecuters>, IProsecuterRepository

    {

        public ProsecutorStatus AddProsecuter(vw_ProcecuterData ProsecuterData, out int ProsecuterID)
        {
            try
            {
                var NationalIDExistBefore = 0;

                if (!string.IsNullOrEmpty(ProsecuterData.NationalID))
                {
                    NationalIDExistBefore = (from prosecuter in DataContext.Configurations_Prosecuters
                                             where prosecuter.NationalID == ProsecuterData.NationalID
                                             select prosecuter.ID).FirstOrDefault();
                }

                if (NationalIDExistBefore != 0)
                {
                    ProsecuterID = NationalIDExistBefore;
                    return ProsecutorStatus.NationalNO_Exist_Before;
                }
                else
                {
                    Configurations_Prosecuters ProsecuterObj = new Configurations_Prosecuters();
                    ProsecuterObj.NationalID = ProsecuterData.NationalID;
                    ProsecuterObj.ProsecutionID = ProsecuterData.ProcecutionID;
                    ProsecuterObj.Name = ProsecuterData.ProcecutoerName;
                    this.Add(ProsecuterObj);
                    this.Save();
                    ProsecuterID = ProsecuterObj.ID;
                    return ProsecutorStatus.Succeeded;
                }

            }
            catch (System.Exception)
            {
               
                ProsecuterID = 0;
                return ProsecutorStatus.Failed;
            }
        }

        public ProsecutorStatus DeleteProsecuter(int ProsecuterID)
        {
            try
            {
                bool InsertedInSession = (from sessions in DataContext.Cases_CaseSessions
                                          where sessions.ProsecuterID == ProsecuterID
                                          select sessions.ID).Any();
                if (InsertedInSession)
                {
                    return ProsecutorStatus.ProsecuterHasSession;
                }
                else
                {
                    var Prosecuter = this.GetByID(ProsecuterID);
                    this.Delete(Prosecuter);
                    this.Save();
                    return ProsecutorStatus.Succeeded;
                }

            }
            catch (System.Exception)
            {
                return ProsecutorStatus.Failed;
            }
        }

        public ProsecutorStatus EditProsecuter(vw_ProcecuterData prosecuterData)
        {
            try
            {
                bool InsertedInSession = (from sessions in DataContext.Cases_CaseSessions
                                          where sessions.ProsecuterID == prosecuterData.ID
                                          select sessions.ID).Any();
                var NationalIDExistBefore = 0;

                if (!string.IsNullOrEmpty(prosecuterData.NationalID))
                {
                     NationalIDExistBefore = (from prosecuter in DataContext.Configurations_Prosecuters
                                                 where prosecuter.NationalID == prosecuterData.NationalID
                                                 && prosecuter.ID != prosecuterData.ID
                                                 select prosecuter.ID).FirstOrDefault();
                }
                
                if (InsertedInSession)
                {
                    return ProsecutorStatus.ProsecuterHasSession;
                }
                else if ( NationalIDExistBefore != 0)
                {
                    return ProsecutorStatus.NationalNO_Exist_Before;
                }
                else
                {
                    var Prosecuter = this.GetByID(prosecuterData.ID);
                    Prosecuter.ProsecutionID = prosecuterData.ProcecutionID;
                    Prosecuter.Name = prosecuterData.ProcecutoerName;
                    Prosecuter.NationalID = prosecuterData.NationalID;
                    this.Update(Prosecuter);
                    this.Save();
                    return ProsecutorStatus.Succeeded;
                }
            }
            catch (System.Exception)
            {
                return ProsecutorStatus.Failed;
            }
        }

        public vw_ProcecuterData GetProsecutorByID(int? ProsecuterID)
        {
            return (from prosecuter in DataContext.Configurations_Prosecuters
                    join prosecution in DataContext.Configurations_Prosecutions on prosecuter.ProsecutionID equals prosecution.ID
                    where prosecuter.ID == ProsecuterID
                    select new vw_ProcecuterData
                    {
                        NationalID = prosecuter.NationalID,
                        ID = prosecuter.ID,
                        ProcecutionID = prosecuter.ProsecutionID,
                        ProcecutoerName = prosecuter.Name,
                        ProsecutionName = prosecution.Name


                    }).FirstOrDefault();
        }

        public List<vw_ProcecuterData> GetProsecutors(int? courtID)
        {
            return (from _prosecutor in DataContext.Configurations_Prosecuters
                    join _prosecution in DataContext.Configurations_Prosecutions on _prosecutor.ProsecutionID equals _prosecution.ID
                    join _court in DataContext.Configurations_Courts on _prosecution.CourtID equals _court.ID
                    where ((_court.ID == courtID && courtID.HasValue) || !courtID.HasValue)
                    select new vw_ProcecuterData
                    {
                        ID = _prosecutor.ID,
                        ProcecutoerName = _prosecutor.Name,
                        NationalID = _prosecutor.NationalID,
                        ProcecutionID = _prosecution.ID,
                        ProsecutionName = _prosecution.Name
                    }).ToList();
        }
    }
}
