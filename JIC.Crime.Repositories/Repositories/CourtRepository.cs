using JIC.Base.Interfaces;
using JIC.Base.Views;
using JIC.Crime.Entities.Models;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Crime.Repositories.Repositories
{
    public class CourtRepository : EntityRepositoryBase<Configurations_Courts>, ICourtRepository
    {
        public List<vw_KeyValue> GetCourtHalls(int? CourtID)
        {
            return ( from ch in DataContext.CourtConfigurations_CourtHalls where ch.CourtID==CourtID 
            select new vw_KeyValue
            {
                ID = (int)ch.ID,
                Name = ch.Name
            }).ToList();
        }

        public List<vw_KeyValue> GetCourts()
        {
            return GetAllQuery().Select(court => new vw_KeyValue
            {
                ID = court.ID,
                Name = court.Name
            }).ToList();
        }
    }
}
