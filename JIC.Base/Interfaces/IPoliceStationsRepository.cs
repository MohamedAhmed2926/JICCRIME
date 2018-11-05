using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
  public  interface IPoliceStationsRepository
    {
        IQueryable<vw_KeyValue> GetPoliceStations(int courtId);
        List<vw_KeyValue> GetPoliceStationsInPros(int prosecutionID);
        List<vw_KeyValue> GetPoliceStationsInCourt(int courtID);
        List<vw_KeyValue> GetPoliceStationsByCircuitID(int CircuitID);
        List<vw_KeyValue> GetAllPoliceStations();
    }
}
