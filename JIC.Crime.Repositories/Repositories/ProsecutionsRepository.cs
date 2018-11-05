using JIC.Base.Interfaces;
using JIC.Crime.Entities.Models;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base.Views;

namespace JIC.Crime.Repositories.Repositories
{
    public class ProsecutionsRepository : EntityRepositoryBase<Configurations_Prosecutions>, IProsecutionsRepository
    {
        

        public List<vw_KeyValue> GetElementaryProsecutions(int courtID)
        {
            return DataContext.Configurations_Prosecutions.Where(x => x.ParentID == null && x.CourtID == courtID).Select(y => new vw_KeyValue { ID = y.ID, Name = y.Name }).ToList();
        }

        public List<vw_KeyValue> GetInitialProsInCourt(int courtID)
        {
            var Court = DataContext.Configurations_Courts.Find(courtID);
            if (Court == null)
            {
                return (from prosecutaion in DataContext.Configurations_Prosecutions
                        join initialPros in DataContext.Configurations_Prosecutions on prosecutaion.ID equals initialPros.ParentID
                        where (prosecutaion.ParentID == null)
                        select new vw_KeyValue { ID = initialPros.ID, Name = initialPros.Name }).ToList();
            }
            else
            {
                switch ((Base.CaseLevels)Court.CourtLevelID)
                {
                    case Base.CaseLevels.Elementary:
                        return (from prosecutaion in DataContext.Configurations_Prosecutions
                                join initialPros in DataContext.Configurations_Prosecutions on prosecutaion.ID equals initialPros.ParentID
                                where (prosecutaion.CourtID == courtID)
                                select new vw_KeyValue { ID = initialPros.ID, Name = initialPros.Name }).ToList();
                    case Base.CaseLevels.Initial:
                        return (from prosecutaion in DataContext.Configurations_Prosecutions
                                where (prosecutaion.CourtID == courtID)
                                select new vw_KeyValue { ID = prosecutaion.ID, Name = prosecutaion.Name }).ToList();
                    default:
                        return null;

                }
            }
        }

        public IQueryable<vw_KeyValue> GetProsecution(int? courtId)
        {
            return (from prosecutaion in DataContext.Configurations_Prosecutions
                    where ((prosecutaion.CourtID == courtId && courtId.HasValue) || !courtId.HasValue)
                    select new vw_KeyValue { ID = prosecutaion.ID, Name = prosecutaion.Name });
        }

        public List<vw_KeyValue> GetProsecutionFromPros(int prosecutionID)
        {
            return (from prosecutaion in DataContext.Configurations_Prosecutions
                    where (prosecutaion.ParentID == prosecutionID)
                    select new vw_KeyValue { ID = prosecutaion.ID, Name = prosecutaion.Name }).ToList();
        }
    }
   
    }

