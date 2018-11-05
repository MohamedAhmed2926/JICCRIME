using JIC.Base;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.ServicesInterfaces
{
    public interface ICircuitService
    {
        SaveCircuitStatus AddCircuit(vw_CircuitData circuitData, out int CircuitID);
        SaveCircuitStatus EditCircuit(vw_CircuitData circuitData);
        DeleteCircuitStatus DeleteCircuit(int ID);
        List<vw_KeyValue> GetCircuits();
        List<vw_KeyValue> GetCircuitsByCourtID(int courtID);
        List<vw_CircuitsGrid> GetCircuitsFullData(int CourtID);
    
        vw_CircuitsGrid GetCircuitsFullDataByID(int CircuitID);
        List<vw_KeyValue> GetCircuits(int CourtID);
        List<vw_KeyValue> GetCircuitsBySecretairyID(int SecretairyID, int? CourtID);
        List<vw_KeyValue> GetCircuitsBySecretairyID(int SecretairyID);
        List<DateTime> GetSessions(int CircuitID);
        List<vw_KeyValue> GetCircuitsByCrime(int CrimeID , int CourtID);

    }
}
