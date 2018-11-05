using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.Views;
using JIC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Components.Components
{
   public class CircuitsComponent
    {
        public ICircuitsRepository CircuitRepository;

        public CircuitsComponent( ICircuitsRepository CircuitRepository)
        {
            this.CircuitRepository = CircuitRepository;
        }
        public bool IsSavedBefore(bool isSameYearCircuit, string CircuitName,int CircuitID)
        {
            return CircuitRepository.IsSavedBefore (isSameYearCircuit ,  CircuitName ,CircuitID);
        }

        public void AddCircuit(vw_CircuitData circuitData, out int CircuitID)
        {
            CircuitRepository.AddCircuit(circuitData, out CircuitID);
        }

        public List<vw_KeyValue> GetCircuits()
        {
            return CircuitRepository.GetCircuits ();
        }

        public List<vw_CircuitsGrid> GetCircuitsFullData(int CourtID)
        {
            return CircuitRepository.GetCircuitsFullData(CourtID);
        }
        public vw_CircuitsGrid GetCircuitsFullDataByID(int CircuitID)
        {
            return CircuitRepository.GetCircuitsFullDataByID(CircuitID);
        }
        public List<vw_KeyValue> GetCircuitsByCourtID(int courtID)
        {
            return CircuitRepository.GetCircuitsByCourtID ( courtID );
        }

        public List<vw_KeyValue> GetCircuitsBySecretairyID(int SecretairyID,int? CourtID)
        {
            return CircuitRepository.GetCircuitsBySecretairyID (SecretairyID,CourtID);
        }
        public List<vw_KeyValue> GetCircuitsBySecretairyID(int SecretairyID)
        {
            return CircuitRepository.GetCircuitsBySecretairyID(SecretairyID);
        }
        public void EditCircuit(vw_CircuitData circuitData)
        {
             CircuitRepository.EditCircuit(circuitData);
        }

        public void DeleteCircuit(int ID)
        {
             CircuitRepository.DeleteCircuit (ID);
        }

        public List<vw_KeyValue> GetCircuitUsers(int circuitID)
        {
            return CircuitRepository.GetCircuitSecretaries(circuitID);
        }

       public bool IsStartDateAfterToday(int ID)
        {
            return CircuitRepository.IsStartDateAfterToday(ID);
        }

        public List<vw_KeyValue> GetCircuitsByCrime(int crimeID , int CourtID)
        {
            return CircuitRepository.GetCircuitsByCrime(crimeID , CourtID);
        }
    }
}
