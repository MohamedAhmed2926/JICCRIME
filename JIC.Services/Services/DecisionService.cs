using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Views;
using JIC.Components.Components;
using JIC.Services.EventHandler;
using System.Data.Entity.Validation;

namespace JIC.Services.Services
{
    public class DecisionService : ServiceBase, IDecisionService
    {
        public DefendantsDecisionComponent DefentantsComp { get { return GetComponent<DefendantsDecisionComponent>(); } }
        public DecisionsComponent DecisionsComp { get { return GetComponent<DecisionsComponent>(); } }
        public CrimeCaseComponent CaseComp { get { return GetComponent<CrimeCaseComponent>(); } }
        public SessionsComponent SessionsComp { get { return GetComponent<SessionsComponent>(); } }

        public RollsComponent RollsComp { get { return GetComponent<RollsComponent>(); } }
        public DecisionService(CaseType caseType) : base(caseType)
        {

        }

        ///////// check ApprovedByJudge from CaseSessions in case of editing Decision

        public SaveDecisionStatus SaveDecision(vw_CaseDecision DecisionData)
        {


            try
            {


                // From SessionID get CaseID & RollID to set old rollID
                DecisionData.RollID = SessionsComp.GetRollID(DecisionData.CaseSessionID);
                DecisionData.OldCircuitID = SessionsComp.GetCircuitID(DecisionData.CaseSessionID);

                // check RollStatusID from CircuitRolls before Saving data
                if (!RollsComp.IsRollOpened((int)DecisionData.RollID))
                {
                    return SaveDecisionStatus.RollNotOpenedYet;
                }
                if (SessionsComp.IsSentToJudge(DecisionData.CaseSessionID))
                {
                   
                    return SaveDecisionStatus.SessionSentToJudge;
                }
                if (RollsComp.IsApprovedByJudge((int)DecisionData.RollID))
                {
                    return SaveDecisionStatus.SentToJudge;
                }

                DeleteIfSavedBefore(DecisionData); // override saved before decision

                if (DecisionData.DecisionLevel == DecisionLevels.Post || (DecisionData.DecisionLevel == DecisionLevels.Decision && DecisionData.DecisionType == DecisionTypes.L3_Postponed))
                {
                    //check if new roll exist, if not create it
                    DecisionData.RollID = SetNewRollID((int)DecisionData.CycleRollID, (int)DecisionData.OldCircuitID, (DateTime)DecisionData.NextSessionDate);
                    vw_SessionData SessionData;
                    SessionData = new vw_SessionData
                    {
                        CaseID = DecisionData.CaseID,
                        RollID = (int)DecisionData.RollID,
                        DoneByDefaultCircuit = true,
                        ApprovedByJudge = false,
                        RollIndex = 0
                    };
                    // add session for the created roll
                    SessionsComp.AddSession(SessionData);
                }

                // Save Decision
                DecisionsComp.SaveDecision(DecisionData);

                Event(new CaseDessionSaved { CaseID = DecisionData.CaseID, DecisionLevel = DecisionData.DecisionLevel, ReservedForJudgement = DecisionData.ReservedForJudgement, DecisionType = DecisionData.DecisionType });
                if (DecisionData.DecisionLevel == DecisionLevels.Final)
                {
                    foreach (vw_DefendantsDecisionData Def in DecisionData.DefendantsListJudges)
                    {
                        if (Def.DecisionType == (int)DecisionTypes.L1_Guilty)
                        { Def.IsGuilty = true; }
                        else if (Def.DecisionType == (int)DecisionTypes.L1_NotGuilty)
                        { Def.IsGuilty = false; }
                        Def.SessionDessionId = DecisionData.CaseSessionID;
                        DefentantsComp.AddDefendantDecision(Def);
                    }
                }



                return SaveDecisionStatus.Saved;
            }
            catch (DbEntityValidationException ex)
            {
                HandleException(ex);
                return SaveDecisionStatus.Failed_To_Save;
            }
        }

        int SetNewRollID(int CycleRollID, int CircuitID, DateTime NextSessionDate)
        {
            long roll = 0;
            if (RollsComp.IsRollExist(CycleRollID, CircuitID, NextSessionDate))
            {
                roll = (long)RollsComp.GetCircuitRollID(CycleRollID, CircuitID, NextSessionDate);
            }
            else
            {
                RollsComp.AddNewCircuitRoll(CircuitID, NextSessionDate, out roll);
            }


            return (int)roll;
        }

        public List<vw_KeyValueDate> GetCycleSessionDates(int SessionID)
        {
            int CircuitID = SessionsComp.GetCircuitID(SessionID);
            return RollsComp.GetCircuitRolls(CircuitID).Where(x => x.Date > DateTime.Now).ToList();
        }


        void DeleteIfSavedBefore(vw_CaseDecision DecisionData)
        {
            List<vw_CaseDecision> CaseDec = DecisionsComp.GetCaseDecisions(DecisionData.CaseID);
            foreach (vw_CaseDecision r in CaseDec)
            {
                if (r.CaseSessionID == DecisionData.CaseSessionID)
                {

                    if (r.DecisionTypeID == (int)DecisionTypes.L1_Guilty || r.DecisionTypeID == (int)DecisionTypes.L1_NotGuilty)
                    {
                        //Delete From Defendants Table
                        DefentantsComp.DeleteDefendantDecision(DecisionData.CaseSessionID);
                    }
                    DecisionsComp.DeleteDecision(r);
                    break;
                }


            }



        }


        vw_CaseDecision IDecisionService.GetCaseLastDecision(int CaseID, int SessionID)
        {
            vw_CaseDecision CD = new vw_CaseDecision();

            List<vw_CaseDecision> Decisions = DecisionsComp.GetCaseDecisions(CaseID);
            CD = Decisions.Where(x => x.CaseSessionID == SessionID).FirstOrDefault();
            if (CD.DecisionTypeID == (int)DecisionTypes.L1_Guilty || CD.DecisionTypeID == (int)DecisionTypes.L1_NotGuilty)
            {
                CD.DefendantsListJudges = DefentantsComp.GetSessionDefendantsDecision(SessionID);

            }

            if (CD.DecisionTypeID == (int)DecisionTypes.L2_Experts || CD.DecisionTypeID == (int)DecisionTypes.L2_Forensic ||
                CD.DecisionTypeID == (int)DecisionTypes.L2_Investigations || CD.DecisionTypeID == (int)DecisionTypes.L3_Postponed)
            {
                CD.NextSessionDate = RollsComp.GetSessionDate((int)CD.RollID);
                CD.CycleRollID = (int)RollsComp.GetCircuitRolls((int)CD.OldCircuitID).Where(z => z.Date == CD.NextSessionDate).Select(x => x.ID).FirstOrDefault();

                if (CD.DecisionTypeID == (int)DecisionTypes.L3_Postponed)
                {
                    if (IsReservedForJudge(CaseID))
                    {
                        CD.ReservedForJudgement = true;
                    }
                    else
                    {
                        CD.ReservedForJudgement = false;
                    }
                }
            }

            if (CD.DecisionTypeID == (int)DecisionTypes.L2_Experts || CD.DecisionTypeID == (int)DecisionTypes.L2_Forensic ||
               CD.DecisionTypeID == (int)DecisionTypes.L2_Investigations)
            {
                CD.DecisionLevel = DecisionLevels.Post;
            }
            else if (CD.DecisionTypeID == (int)DecisionTypes.L1_Guilty || CD.DecisionTypeID == (int)DecisionTypes.L1_NotGuilty)
            { CD.DecisionLevel = DecisionLevels.Final; }
            else
            { CD.DecisionLevel = DecisionLevels.Decision; }

            return CD;
        }

        public bool IsDecisionSaved(int CaseID, int SessionID)
        {
            vw_CaseDecision CD = new vw_CaseDecision();

            List<vw_CaseDecision> Decisions = DecisionsComp.GetCaseDecisions(CaseID);
            CD = Decisions.Where(x => x.CaseSessionID == SessionID).FirstOrDefault();
            if (CD == null)
                return false;
            else
                return true;
        }
            bool IsReservedForJudge(int CaseID)
            {
          return  CaseComp.IsCaseReservedForJudge(CaseID);
        }
    }

}
