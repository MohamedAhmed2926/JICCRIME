using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
    public interface IPoliceStationsCircuitsRepository
    {
        void SaveCircuitPoliceStations(List<int> PoliceStations, int CircuitID);
        void DeleteCircuitPoliceStations(int CircuitID);
    }
}
