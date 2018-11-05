using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.Views;

namespace JIC.Components.Components
{
    public class PoliceStationComponent
    {
        private IPoliceStationsRepository PoliceStationsRepository;
        public PoliceStationComponent( IPoliceStationsRepository PoliceStationsRepository)
        {
            this.PoliceStationsRepository = PoliceStationsRepository;
        }

        public List<vw_KeyValue> GetPoliceStationsInPros(int prosecutionID)
        {
            return PoliceStationsRepository.GetPoliceStationsInPros(prosecutionID);
        }

        public List<vw_KeyValue> GetPoliceStationsInCourt(int courtID)
        {
            return PoliceStationsRepository.GetPoliceStationsInCourt(courtID);
        }

        public List<vw_KeyValue> GetPoliceStationsByCircuitID(int CircuitID)
        {
            return PoliceStationsRepository.GetPoliceStationsByCircuitID (CircuitID );
        }

        public List<vw_KeyValue> GetAllPoliceStations()
        {
            return PoliceStationsRepository.GetAllPoliceStations();
        }
    }
}
