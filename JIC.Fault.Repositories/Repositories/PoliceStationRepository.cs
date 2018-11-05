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
    public class PoliceStationRepository : EntityRepositoryBase<Configurations_PoliceStations>, IPoliceStationsRepository
    {
        public IQueryable<vw_KeyValue> GetPoliceStations(int courtId)
        {
            var Court = DataContext.Configurations_Courts.Find(courtId);
            switch ((Base.CaseLevels)Court.CourtLevelID)
            {
                case Base.CaseLevels.Elementary:
                    return (from policeStation in DataContext.Configurations_PoliceStations
                            join initialProsecution in DataContext.Configurations_Prosecutions on policeStation.ProsecutionID equals initialProsecution.ID
                            join prosecution in DataContext.Configurations_Prosecutions on initialProsecution.ParentID equals prosecution.ID
                            where prosecution.CourtID == courtId
                            select new vw_KeyValue
                            {
                                ID = policeStation.ID,
                                Name = policeStation.Name
                            });
                case Base.CaseLevels.Initial:
                    return (from policeStation in DataContext.Configurations_PoliceStations
                            join prosecution in DataContext.Configurations_Prosecutions on policeStation.ProsecutionID equals prosecution.ID
                            where prosecution.CourtID == courtId
                            select new vw_KeyValue
                            {
                                ID = policeStation.ID,
                                Name = policeStation.Name
                            });

                default:
                    return null;
            }
        }

        public List<vw_KeyValue> GetPoliceStationsInCourt(int courtID)
        {
            return GetPoliceStations(courtID).ToList();
        }

        public List<vw_KeyValue> GetPoliceStationsInPros(int prosecutionID)
        {
            return GetAllQuery().Where(policeStation => policeStation.ProsecutionID == prosecutionID).Select(policeStation => new vw_KeyValue
            {
                ID = policeStation.ID,
                Name = policeStation.Name
            }).ToList();
        }

        public List<vw_KeyValue> GetPoliceStationsByCircuitID(int CircuitID)
        {
            throw new NotImplementedException();
        }

        public List<vw_KeyValue> GetAllPoliceStations()
        {
            return DataContext.Configurations_PoliceStations.Select(x => new vw_KeyValue
            {
                ID = x.ID,
                Name = x.Name
            }).ToList();
        }
    }
}
