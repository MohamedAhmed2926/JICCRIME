using JIC.Base.Interfaces;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Crime.Entities.Models;

namespace JIC.Crime.Repositories.Repositories
{
    public class PoliceStationsCircuitsRepository : EntityRepositoryBase<CourtConfigurations_CircuitsPoliceStation>, IPoliceStationsCircuitsRepository
    {
        public void SaveCircuitPoliceStations(List<int> PoliceStations, int CircuitID)
        {
            CourtConfigurations_CircuitsPoliceStation Obj;
            foreach (int i in PoliceStations)
            {
                Obj = new CourtConfigurations_CircuitsPoliceStation { CircuitID = CircuitID, PoliceStationID = i };
                Add(Obj);
                Save();

            }
        }

        public void DeleteCircuitPoliceStations( int CircuitID)
        {
            List<CourtConfigurations_CircuitsPoliceStation> PS = GetAll().Where(x => x.CircuitID == CircuitID).ToList();
           
            foreach (CourtConfigurations_CircuitsPoliceStation i in PS)
            {
                Delete(i);
                Save();

            }
        }
    }
}
