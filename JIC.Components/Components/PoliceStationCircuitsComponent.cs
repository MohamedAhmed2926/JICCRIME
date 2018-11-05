using JIC.Base;
using JIC.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Components.Components
{
   public  class PoliceStationCircuitsComponent
    {
        public IPoliceStationsCircuitsRepository PoliceStationsRepository;
        public PoliceStationCircuitsComponent( IPoliceStationsCircuitsRepository PoliceStationsRepository)
        {
            this.PoliceStationsRepository = PoliceStationsRepository;
        }
        
        public void SaveCircuitPoliceStations(List<int> PoliceStations, int CircuitID)
        {
             PoliceStationsRepository.SaveCircuitPoliceStations (PoliceStations,CircuitID);
        }
        public void DeleteCircuitPoliceStations( int CircuitID)
        {
            PoliceStationsRepository.DeleteCircuitPoliceStations(CircuitID);
        }
    }
}
