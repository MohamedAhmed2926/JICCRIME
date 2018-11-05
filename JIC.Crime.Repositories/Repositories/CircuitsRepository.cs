using JIC.Crime.Entities.Models;
using JIC.Repositories.DBInteractions;
using JIC.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Views;
using System.Data.Linq;

namespace JIC.Crime.Repositories.Repositories
{
    public class CircuitsRepository : EntityRepositoryBase<CourtConfigurations_Circuits>, ICircuitsRepository
    {
        public void AddCircuit(vw_CircuitData CircuitData, out int CircuitID)
        {
            CourtConfigurations_Circuits CircuitsObj = GetObjectFromView(CircuitData);
            CircuitsObj.CreatedBy = CircuitData.UserName;
            CircuitsObj.CreatedAt = DateTime.Now;
            this.Add(CircuitsObj);
            this.Save();
            CircuitID = CircuitsObj.ID;

        }

        public void DeleteCircuit(int ID)
        {

            var Circuit = this.GetByID(ID);
            this.Delete(Circuit);
            this.Save();

        }

        public bool IsStartDateAfterToday(int ID)
        {

            var CircuitDate = this.GetByID(ID).CircuitStartDate;
            if (CircuitDate > DateTime.Now)
            { return true; }
            else
            { return false; }

        }

        public void EditCircuit(vw_CircuitData CircuitData)
        {


            CourtConfigurations_Circuits CircuitsObj = GetAll().Where(x => x.ID == CircuitData.ID).FirstOrDefault();

            CircuitsObj.CourtID = CircuitData.CourtID;
            CircuitsObj.CrimeType = CircuitData.CrimeTypeID;
            CircuitsObj.SecretaryID = CircuitData.SecretaryID;
            if (CircuitData.AssistantSecretaryID == 0)
                CircuitsObj.AssistantSecretaryID = null;
            else
                CircuitsObj.AssistantSecretaryID = CircuitData.AssistantSecretaryID;
            CircuitsObj.Name = CircuitData.CircuitName;
            CircuitsObj.CircuitStartDate = CircuitData.CircuitStartDate;
            CircuitsObj.IsActive = CircuitData.IsActive;
            CircuitsObj.CycleID = CircuitData.CycleID;
            CircuitsObj.IsFutureCircuit = CircuitData.IsFutureCircuit;
            CircuitsObj.LastModifiedBy = CircuitData.UserName;
            CircuitsObj.LastModifiedAt = DateTime.Now;
            this.Update(CircuitsObj);
            this.Save();

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
        public List<vw_KeyValue> GetCircuitsByCrime(int crimeID , int courtID)
        {
            return DataContext.CourtConfigurations_Circuits.Where(id => id.CourtID == courtID && id.CrimeType == crimeID && id.IsActive).Select(x => new vw_KeyValue
            {
                ID = x.ID,
                Name = x.Name
            }).ToList();
        }

        public List<vw_KeyValue> GetCircuitsBySecretairyID(int SecretairyID, int? CourtID)
        {
            // get circuits for secretairy
      
            var result= (from circuit in DataContext.CourtConfigurations_Circuits
                    join roll in DataContext.CourtConfigurations_CircuitRolls on circuit.ID equals roll.CircuitID
                    where circuit.SecretaryID == SecretairyID || circuit.AssistantSecretaryID==SecretairyID
                    && roll.RollStatusID == (int)RollStatus.NotStarted
                     && ((circuit.CourtID == CourtID && CourtID.HasValue) || !CourtID.HasValue )
             
                         select new 
                    {
                        ID = circuit.ID,
                        Name = circuit.Name,
                        SessionDate=roll.SessionDate 
                    }).Distinct().ToList();


            List<vw_KeyValue> result2;
            if (CourtID != null) // open Roll
            {
              result2  = (from r in result
                                             where (r.SessionDate.ToShortDateString () == DateTime.Now.ToShortDateString())
                                               select new vw_KeyValue
                                               {
                                                   ID = r.ID,
                                                   Name = r.Name
                                               }).ToList();
            }

            else // Sort Roll
            {
                result2 = result.Select(r => new vw_KeyValue
                {
                    ID = r.ID,
                    Name = r.Name
                }).ToList();

            }
            return result2;
        }
        public List<vw_KeyValue> GetCircuitsBySecretairyID(int SecretairyID)
        {
            var result = (from circuit in DataContext.CourtConfigurations_Circuits
                           where circuit.SecretaryID == SecretairyID || circuit.AssistantSecretaryID== SecretairyID
                          
                          select new vw_KeyValue
                          {
                              ID = circuit.ID,
                              Name = circuit.Name,
                             
                          }).ToList();


            return result;
        }
        public List<vw_CircuitsGrid> GetCircuitsFullData(int CourtID)
        {
            return (from _circuit in DataContext.CourtConfigurations_Circuits
                    join Member in DataContext.CourtConfigurations_CircuitMembers on _circuit.ID equals Member.CircuitID
                    join user in DataContext.Users on Member.UserID equals user.Id
                    join person in DataContext.Configurations_Persons on user.PersonsId equals person.ID
                    join ct in DataContext.Cases_CrimeTypes on _circuit.CrimeType equals ct.ID
                    join c in DataContext.CourtConfigurations_Cycles on _circuit.CycleID equals c.ID
                    where (_circuit.CourtID == CourtID && Member.JudgeType == (int)JudgePodiumType.HeadJudge && Member.ToDate == null)
                    select new vw_CircuitsGrid
                    {
                        ID = _circuit.ID,
                        CircuitName = _circuit.Name,
                        CenterJudgeName = person.FullName,
                        CircuitStartDate = _circuit.CircuitStartDate,
                        CenterJudgeID = Member.ID,
                        CrimeType = _circuit.CrimeType,
                        CrimeTypeName = ct.Name,
                        CycleID = _circuit.CycleID,
                        CycleName = c.Name,
                        SecretaryHead = _circuit.SecretaryID,
                        SecretaryAssistant = _circuit.AssistantSecretaryID
                    }).ToList();


        }

        public vw_CircuitsGrid GetCircuitsFullDataByID(int CircuitID)
        {
            return (from _circuit in DataContext.CourtConfigurations_Circuits
                    join Member in DataContext.CourtConfigurations_CircuitMembers on _circuit.ID equals Member.CircuitID
                    join user in DataContext.Users on Member.UserID equals user.Id
                    join person in DataContext.Configurations_Persons on user.PersonsId equals person.ID
                    join ct in DataContext.Cases_CrimeTypes on _circuit.CrimeType equals ct.ID
                    join c in DataContext.CourtConfigurations_Cycles on _circuit.CycleID equals c.ID
                    where (_circuit.ID == CircuitID)
                    select new vw_CircuitsGrid
                    {
                        ID = _circuit.ID,
                        CircuitName = _circuit.Name,
                        CenterJudgeName = person.FullName,
                        CircuitStartDate = _circuit.CircuitStartDate,
                        CenterJudgeID = Member.ID,
                        CrimeType = _circuit.CrimeType,
                        CrimeTypeName = ct.Name,
                        CycleID = _circuit.CycleID,
                        CycleName = c.Name,
                        SecretaryHead = _circuit.SecretaryID,
                        SecretaryAssistant = _circuit.AssistantSecretaryID

                    }).FirstOrDefault();
        }

        public List<vw_KeyValue> GetCircuitSecretaries(int circuitID)
        {
            var Circuit = GetByID(circuitID);
            List<vw_KeyValue> Secretaries = new List<vw_KeyValue>
            {
                new vw_KeyValue
                {
                    ID = Circuit.SecretaryID,
                    Name = Circuit.Secretary_Users.Configurations_Persons.FullName
                }
            };
            if (Circuit.AssistantSecretaryID.HasValue)
                Secretaries.Add(new vw_KeyValue
                {
                    ID = Circuit.AssistantSecretaryID.Value,
                    Name = Circuit.Assistant_Secretary_Users.Configurations_Persons.FullName
                });
            return Secretaries;
        }

        public bool IsSavedBefore(bool isSameYearCircuit, string CircuitName, int CircuitID)
        {

            return DataContext.CourtConfigurations_Circuits.Where(_circuit => _circuit.Name == CircuitName && _circuit.IsActive == isSameYearCircuit && _circuit.IsFutureCircuit != isSameYearCircuit && _circuit.ID != CircuitID).Count() > 0;

        }


        CourtConfigurations_Circuits GetObjectFromView(vw_CircuitData CircuitData)
        {
            return new CourtConfigurations_Circuits
            {
                CourtID = CircuitData.CourtID,
                CrimeType = CircuitData.CrimeTypeID,
                SecretaryID = CircuitData.SecretaryID,
                AssistantSecretaryID = CircuitData.AssistantSecretaryID,
                Name = CircuitData.CircuitName,
                CircuitStartDate = CircuitData.CircuitStartDate,
                IsActive = CircuitData.IsActive,
                CycleID = CircuitData.CycleID,
                IsFutureCircuit = CircuitData.IsFutureCircuit,
                CreatedAt = DateTime.Now,
                CreatedBy = "Name2"
            };
        }

       
    }
}
