using JIC.Fault.Entities.Models;
using JIC.Repositories.DBInteractions;
using JIC.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Views;

namespace JIC.Fault.Repositories.Repositories
{
    public class CircuitsRepository : EntityRepositoryBase<CourtConfigurations_Circuits>, ICircuitsRepository
    {
        public void AddCircuit(vw_CircuitData CircuitData, out int CircuitID)
        {
            CourtConfigurations_Circuits CircuitsObj = GetObjectFromView(CircuitData);
            this.Add(CircuitsObj);
            this.Save();
            CircuitID = CircuitsObj.ID;

        }

        private CourtConfigurations_Circuits GetObjectFromView(vw_CircuitData circuitData)
        {
            throw new NotImplementedException();
        }

        public void DeleteCircuit(int ID)
        {

            var Circuit = this.GetByID(ID);
            this.Delete(Circuit);
            this.Save();

        }

        public bool IsStartDateAfterToday(int ID)
        {

            throw new NotImplementedException();

        }

        

        public List<vw_KeyValue> GetCircuits()
        {
            return (from _circle in DataContext.CourtConfigurations_Circuits
                    where _circle.IsActive
                    select new vw_KeyValue
                    {
                        ID = _circle.ID,
                        Name = _circle.Name
                    }).ToList();
        }



        public List<vw_KeyValue> GetCircuitsByCourtID(int CourtID)
        {
            return (from _circle in DataContext.CourtConfigurations_Circuits
                    where _circle.CourtID == CourtID && _circle.IsActive
                    select new vw_KeyValue
                    {
                        ID = _circle.ID,
                        Name = _circle.Name
                    }).ToList();
        }
        

        public bool IsSavedBefore(bool isSameYearCircuit, string CircuitName, int CircuitID)
        {

            return DataContext.CourtConfigurations_Circuits.Where(_circuit => _circuit.Name == CircuitName && _circuit.IsActive == isSameYearCircuit && _circuit.IsFutureCircuit != isSameYearCircuit && _circuit.ID != CircuitID).Count() > 0;

        }

        public void EditCircuit(vw_CircuitData circuitData)
        {
            throw new NotImplementedException();
        }

        public List<vw_CircuitsGrid> GetCircuitsFullData(int CourtID)
        {
            throw new NotImplementedException();
        }

        public vw_CircuitsGrid GetCircuitsFullDataByID(int CircuitID)
        {
            throw new NotImplementedException();
        }

        public List<vw_KeyValue> GetCircuitsBySecretairyID(int SecretairyID, int? CourtID)
        {
            throw new NotImplementedException();
        }

        public List<vw_KeyValue> GetCircuitsBySecretairyID(int SecretairyID)
        {
            throw new NotImplementedException();
        }

        public List<vw_KeyValue> GetCircuitSecretaries(int circuitID)
        {
            throw new NotImplementedException();
        }

        public List<vw_KeyValue> GetCircuitsByCrime(int crimeID, int courtID)
        {
            throw new NotImplementedException();
        }
    }
}
