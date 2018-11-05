using JIC.Base.Interfaces;
using JIC.Base.Views;
using JIC.Fault.Entities.Models;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Fault.Repositories.Repositories
{
    public class CourtRepository : EntityRepositoryBase<Configurations_Courts>, ICourtRepository
    {
        public List<vw_KeyValue> GetCourtHalls(int? CourtID)
        {
            throw new NotImplementedException();
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
