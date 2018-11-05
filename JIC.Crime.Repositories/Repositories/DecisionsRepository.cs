using JIC.Base.Interfaces;
using JIC.Crime.Entities.Models;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base.Views;
using JIC.Base;

namespace JIC.Crime.Repositories.Repositories
{
    public class DecisionsRepository : EntityRepositoryBase<Cases_SessionDecision > , IDecisionsRepository
    {
        public void DeleteDecision(vw_CaseDecision Decision)
        {
            Cases_SessionDecision DecisionData= GetAll().Where(session => session.CaseSessionID == Decision.CaseSessionID ).FirstOrDefault();
            Delete(DecisionData);
            Save();
        }

        public List<vw_CaseDecision> GetCaseDecisions(int CaseID)
        {
            return (from _Case in DataContext.Cases_Cases
                     join _MasterCase in DataContext.Cases_MasterCase on _Case.MasterCaseID equals _MasterCase.ID
                     join _sessions in DataContext.Cases_CaseSessions on _Case.ID equals _sessions.CaseID
                     join _Dec in DataContext.Cases_SessionDecision on _sessions.ID equals _Dec.CaseSessionID

                     where _Case.ID == CaseID && _Case.IsDeleted != true
                    select new vw_CaseDecision
                     {
                         CaseSessionID =(int) _Dec.CaseSessionID ,
                         DecisionDescription = _Dec.DecisionText ,
                         DecisionTypeID = _Dec.DecisionTypeID,
                         RollID =(int) _Dec.FirstRollID ,
                         NewCircuitID = _Dec.NewCircuitID,
                         OldCircuitID = _Dec.OldCircuitID
                     }).ToList();
        }

        public bool IsDecisionSaved(int SessionID)
        {
            return GetAll().Where(session => session.CaseSessionID  == SessionID ).Count() > 0;

        }

        public void SaveDecision(vw_CaseDecision DecisionData)
        {

            Add(new Cases_SessionDecision
            {
                CaseSessionID = DecisionData.CaseSessionID,
                DecisionText = DecisionData.DecisionDescription,
                DecisionTypeID = (short)DecisionData.DecisionType,
                FirstRollID = DecisionData.RollID,
                NewCircuitID = DecisionData.NewCircuitID,
                OldCircuitID = DecisionData.OldCircuitID


            });

            Save();

        }
       
    }
}
