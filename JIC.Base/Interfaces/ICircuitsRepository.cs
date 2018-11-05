using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
    public interface ICircuitsRepository
    {
        void AddCircuit(vw_CircuitData circuitData, out int CircuitID);
        void EditCircuit(vw_CircuitData circuitData);
        void DeleteCircuit(int ID);
        List<vw_KeyValue> GetCircuits();
        bool IsStartDateAfterToday(int ID);
        List<vw_CircuitsGrid> GetCircuitsFullData(int CourtID);
        List<vw_KeyValue> GetCircuitsByCourtID(int courtID);
        vw_CircuitsGrid GetCircuitsFullDataByID(int CircuitID);
        List<vw_KeyValue> GetCircuitsBySecretairyID(int SecretairyID,int? CourtID);
        List<vw_KeyValue> GetCircuitsBySecretairyID(int SecretairyID);
        bool IsSavedBefore(bool isSameYearCircuit, string CircuitName,int CircuitID);
        List<vw_KeyValue> GetCircuitSecretaries(int circuitID);
        List<vw_KeyValue> GetCircuitsByCrime(int crimeID, int courtID);
    }
}
