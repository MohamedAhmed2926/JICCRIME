using JIC.Base;
using JIC.Base.Views;
using JIC.Crime.View.Models;
using JIC.Crime.View.TestService;
using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JIC.Base.Resources;
using JIC.Crime.View.Helpers;
using System.Text.RegularExpressions;

namespace JIC.Crime.View.Controllers
{
    [CAuthorize(SystemUserTypes.Secretary, SystemUserTypes.Judge)]

    public class DecisionsController : ControllerBase
    {
        private ILookupService lookupService;
        private IDecisionService decisionService;
        private IDefectsService defectsService;
        public DecisionsController(ILookupService lookupService, IDecisionService decisionService, IDefectsService defectsService)
        {
            this.lookupService = lookupService;
            this.defectsService = defectsService;
            this.decisionService = decisionService;
        }

        #region
        public ActionResult Index()
        {
            return View();
        }
        #endregion
        #region Create
        [HttpGet]
        public ActionResult Create(int id, int SessionID)
        {
            if(decisionService.IsDecisionSaved(id,SessionID))
            {
                DecisionsViewModel decisions = GetCaseDecisions(id, SessionID);
                return CPartialView(PrepareDecisionsViewModel(id, SessionID, decisions));
            }
            else
                return CPartialView(PrepareDecisionsViewModel(id, SessionID));


        }
        public DecisionsViewModel GetCaseDecisions(int id, int SessionID)
        {
            vw_CaseDecision Casedecisions = (decisionService.GetCaseLastDecision(id, SessionID));
            DecisionsViewModel decisions = new DecisionsViewModel();
            decisions = new DecisionsViewModel
            {
                CaseID = id,
                DecionDescription = Casedecisions.DecisionDescription,
                CaseSessionID = Casedecisions.CaseSessionID,
                
                CaseResultType = (Casedecisions.DecisionLevel == DecisionLevels.Decision) ? CaseResultType.Decision.ToString() : CaseResultType.judgment.ToString(),
               
               
                IsReadyForFinalDecision = (Casedecisions.ReservedForJudgement == null) ? false : Casedecisions.ReservedForJudgement,
                NextSessionDate = (Casedecisions.NextSessionDate == null) ? null : Casedecisions.CycleRollID,
            };
            if(Casedecisions.DefendantsListJudges != null)
            {
                decisions.DefendantsList = Casedecisions.DefendantsListJudges.Select(x => new CaseDefentantsViewModel
                {
                    casejudgmentID = (x.IsGuilty) ? (int)DecisionTypes.L1_Guilty : (int)DecisionTypes.L1_NotGuilty,
                    ID = (int)x.CaseDefendantId,

                }).ToList();
            }
            if (Casedecisions.DecisionLevel == DecisionLevels.Decision)
                decisions.DecisionTypeID = Casedecisions.DecisionTypeID;

            if (Casedecisions.DecisionLevel == DecisionLevels.Final) // updated by heba basyony 11-3-2018
            {
                decisions.DefendantsList = Casedecisions.DefendantsListJudges.Select(x => new CaseDefentantsViewModel
                {
                    casejudgmentID = (x.IsGuilty) ? (int)DecisionTypes.L1_Guilty : (int)DecisionTypes.L1_NotGuilty,
                    ID = (int)x.CaseDefendantId,

                }).ToList();
            }

            decisions.JudgmentID = Casedecisions.DecisionTypeID;
            decisions.CaseJudmentTypeID = (int)Casedecisions.DecisionLevel;
            return decisions;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //workaround code (Bad Code)
        // todo write clean Code
        public ActionResult Create(List<vw_DefendantsDecisionData> CaseDefentants, string Description, int? CaseID, int? CaseSessionID, string CaseResultType, string DecisionTypeID, string CaseNumber, string NextSessionDate, string IsReadyForFinalDecision, string JudgmentID, string CaseJudmentTypeID, [Bind(Prefix = "model")]DecisionsViewModel model)
        {
            ModelState.Clear();
            // map parameter to model
            model.CaseID = CaseID.Value;
            model.CaseSessionID = CaseSessionID.Value;
            model.CaseID = CaseID.Value;
            model.CaseResultType = CaseResultType;
            model.CaseJudmentTypeID = (string.IsNullOrEmpty(CaseJudmentTypeID)) ? 0 : Convert.ToInt32(CaseJudmentTypeID);
            // Handle required
            if (string.IsNullOrEmpty(Description))
                ModelState.AddModelError("model.DecionDescription", Messages.RequiredErrorMessage);
            // if Decision
            if (Convert.ToInt32(CaseResultType) == (int)DecreeTypes.Decision)
            {
                if (string.IsNullOrEmpty(DecisionTypeID))
                    ModelState.AddModelError("model.DecisionTypeID", Messages.RequiredErrorMessage);
                else
                {
                    if (Convert.ToInt32(DecisionTypeID) == (int)DecisionTypes.L3_Postponed)
                    {
                        if (string.IsNullOrEmpty(NextSessionDate.ToString()))
                            ModelState.AddModelError("model.NextSessionDate", Messages.RequiredErrorMessage);
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(CaseJudmentTypeID))
                    ModelState.AddModelError("model.CaseJudmentTypeID", Messages.RequiredErrorMessage);
                else
                {
                    if (Convert.ToInt32(CaseJudmentTypeID) == (int)DecisionLevels.Post)
                    {
                        if (string.IsNullOrEmpty(NextSessionDate.ToString()))
                            ModelState.AddModelError("model.NextSessionDate", Messages.RequiredErrorMessage);
                        if (string.IsNullOrEmpty(JudgmentID.ToString()))
                            ModelState.AddModelError("model.JudgmentID", Messages.RequiredErrorMessage);
                    }
                }
            }

            if (ModelState.IsValid)
            {
                vw_CaseDecision _CaseDecision = new vw_CaseDecision();
                if (CaseDefentants != null)
                {
                    foreach (var def in CaseDefentants)
                    {
                        if (def.DecisionType == 0)
                        {
                            model = PrepareDecisionsViewModel(CaseID.Value, CaseSessionID.Value);
                            model.CaseID = CaseID.Value;
                            model.CaseResultType = "2";
                            model.CaseSessionID = CaseSessionID.Value;
                            model.CaseJudmentTypeID = (int)DecisionLevels.Final;
                            model.DecionDescription = Description;
                            return CPartialView(model).WithErrorMessages("يجب اختيار نوع الحكم لجميع المتهمين");
                        }
                    }
                    _CaseDecision.CaseID = CaseID.Value;
                    _CaseDecision.CaseSessionID = CaseSessionID.Value;
                    _CaseDecision.DecisionDescription = Description;
                    _CaseDecision.DecisionLevel = DecisionLevels.Final;
                    _CaseDecision.DefendantsListJudges = CaseDefentants;
                    _CaseDecision.DecisionType = DecisionTypes.L1_Guilty;
                }
                else
                {
                    _CaseDecision.CaseID = CaseID.Value;
                    _CaseDecision.CaseSessionID = Convert.ToInt32(CaseSessionID);
                    _CaseDecision.DecisionDescription = Description;
                    if (!string.IsNullOrEmpty(NextSessionDate))
                        _CaseDecision.NextSessionDate = decisionService.GetCycleSessionDates((int)CaseSessionID).Where(m => m.ID == Convert.ToInt64(NextSessionDate)).Select(z => z.Date).SingleOrDefault(); // ta2gel
                                                                                                                                                                                                             //CycleRollID added by Heba Basyony
                    _CaseDecision.CycleRollID = (int)decisionService.GetCycleSessionDates((int)CaseSessionID).Where(m => m.ID == model.NextSessionDate).Select(z => z.ID).SingleOrDefault(); // ta2gel
                    _CaseDecision.DecisionType = (CaseResultType == "2") ? (DecisionTypes)(Convert.ToInt32(model.JudgmentID)) : (DecisionTypes)Convert.ToInt32(DecisionTypeID);// No3 El Qarar 7okm
                                                                                                                                                                               // _CaseDecision.ReservedForJudgement =(IsReadyForFinalDecision == null)?null : Convert.ToBoolean(IsReadyForFinalDecision); 
                                                                                                                                                                               // ma7goza ll 7okm
                    if (IsReadyForFinalDecision != null)
                    { _CaseDecision.ReservedForJudgement = Convert.ToBoolean(IsReadyForFinalDecision); }
                    else { _CaseDecision.ReservedForJudgement = false; }
                    _CaseDecision.DecisionLevel = (CaseResultType == "2") ? (DecisionLevels)Convert.ToInt32(CaseJudmentTypeID) : DecisionLevels.Decision;

                }
                var SaveDecisionStatus = decisionService.SaveDecision(_CaseDecision);
                switch (SaveDecisionStatus)
                {
                    case SaveDecisionStatus.Saved:
                        ViewBag.Saved = true;
                        if (decisionService.IsDecisionSaved(model.CaseID, (int)model.CaseSessionID))
                        {
                            DecisionsViewModel decisions = GetCaseDecisions(model.CaseID, (int)model.CaseSessionID);
                            return CPartialView("Create", PrepareDecisionsViewModel(model.CaseID, Convert.ToInt32(model.CaseSessionID), decisions)).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);
                        }
                        break;
                    case SaveDecisionStatus.RollNotOpenedYet:
                        ViewBag.Saved = false;
                        if (decisionService.IsDecisionSaved(model.CaseID, (int)model.CaseSessionID))
                        {
                            DecisionsViewModel decisions = GetCaseDecisions(model.CaseID, (int)model.CaseSessionID);
                            return CPartialView("Create", PrepareDecisionsViewModel(model.CaseID, Convert.ToInt32(model.CaseSessionID), decisions)).WithErrorMessages("لم تتم العملية .. رول الجلسة لم يتم فتحة");
                        }
                        break;
                    case SaveDecisionStatus.Failed_To_Save:
                        ViewBag.Saved = false;
                        if (decisionService.IsDecisionSaved(model.CaseID, (int)model.CaseSessionID))
                        {
                            DecisionsViewModel decisions = GetCaseDecisions(model.CaseID, (int)model.CaseSessionID);
                            return CPartialView("Create", PrepareDecisionsViewModel(model.CaseID, Convert.ToInt32(model.CaseSessionID), decisions)).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);
                        }
                        break;
                    case SaveDecisionStatus.SentToJudge:
                        ViewBag.Saved = false;
                        if (decisionService.IsDecisionSaved(model.CaseID, (int)model.CaseSessionID))
                        {
                            DecisionsViewModel decisions = GetCaseDecisions(model.CaseID, (int)model.CaseSessionID);
                            return CPartialView("Create", PrepareDecisionsViewModel(model.CaseID, Convert.ToInt32(model.CaseSessionID), decisions)).WithErrorMessages("لم تتم العملية .. تم الارسال للقاضى");
                        }
                        break;

                    case SaveDecisionStatus.SessionSentToJudge:
                        ViewBag.Saved = false;
                        if (decisionService.IsDecisionSaved(model.CaseID, (int)model.CaseSessionID))
                        {
                            DecisionsViewModel decisions = GetCaseDecisions(model.CaseID, (int)model.CaseSessionID);
                            return CPartialView("Create", PrepareDecisionsViewModel(model.CaseID, Convert.ToInt32(model.CaseSessionID), decisions)).WithErrorMessages("لم تتم العملية .. تم الارسال للقاضى");
                        }
                        break;
        


                }
            }

            return CPartialView("_CaseDecisions", PrepareDecisionsViewModel(model.CaseID, (int)model.CaseSessionID, model)).WithPrefix("model");
        }
        #endregion
        #region Edit
        [HttpGet]
        public ActionResult Edit()
        {

            return View();
        }
        #endregion
        #region Helpers
        public DecisionsViewModel PrepareDecisionsViewModel(int CaseID, int SessionID, DecisionsViewModel model = null)
        {
            if (model == null)
                model = new DecisionsViewModel();

            model.CaseSessionID = SessionID;
            model.CaseJudmentType = lookupService.GetJudjementTypes();
            if(model.DefendantsList == null)
            {
                model.DefendantsList = defectsService.GetDefectsByCaseID(CaseID, SessionID).Where(x => x.DefectType == PartyTypes.Defendant).Select(x => new CaseDefentantsViewModel
                {
                    Address = x.Address,
                    Age = CalculateAge(x.Birthdate.Value),
                    Birthday = x.Birthdate,
                    CaseID = CaseID,
                    ID = Convert.ToInt32(x.ID),
                    Job = x.JobName,
                    Name = x.Name,
                    NationalID = x.NationalID,
                    PassportNumber = x.PassportNumber,
                    Nationality = x.Nationality,

                    DefentantJudments = lookupService.GetDecisionTypes(Base.CaseStatuses.FinalDecision).Select(judgment => new SelectListItem
                    {
                        Text = judgment.Name,
                        Value = judgment.ID.ToString()
                    }).ToList()
                }).ToList();
            }
            else
            {
                List< CaseDefentantsViewModel> defs = defectsService.GetDefectsByCaseID(CaseID, SessionID).Where(x => x.DefectType == PartyTypes.Defendant).Select(x => new CaseDefentantsViewModel
                {
                    Address = x.Address,
                    Age = CalculateAge(x.Birthdate.Value),
                    Birthday = x.Birthdate,
                    CaseID = CaseID,
                    ID = Convert.ToInt32(x.ID),
                    Job = x.JobName,
                    Name = x.Name,
                    NationalID = x.NationalID,
                    PassportNumber = x.PassportNumber,
                    Nationality = x.Nationality,

                    DefentantJudments = lookupService.GetDecisionTypes(Base.CaseStatuses.FinalDecision).Select(judgment => new SelectListItem
                    {
                        Text = judgment.Name,
                        Value = judgment.ID.ToString()
                    }).ToList()
                }).ToList();
               
                foreach (var def in defs)
                {
                    def.DefentantJudments = lookupService.GetDecisionTypes(Base.CaseStatuses.FinalDecision).Select(judgment => new SelectListItem
                    {
                        Text = judgment.Name,
                        Value = judgment.ID.ToString(),
                        Selected = model.DefendantsList.Where(x => x.ID == def.ID).Select(x => x.casejudgmentID).SingleOrDefault() == judgment.ID
                    }).ToList();
                }
                model.DefendantsList = defs;
    }
            
            model.CaseID = CaseID;
            model.DectionTypes = lookupService.GetDecisionTypes(Base.CaseStatuses.decision); // نوع القرار
            model.Judgments = lookupService.GetDecisionTypes(Base.CaseStatuses.PostDecision);
            model.IsReadyForFinalDecision = false;
            model.Sessions = decisionService.GetCycleSessionDates(SessionID).Select(x =>
                new vw_KeyValueLongID
                {
                    ID = (long)x.ID,
                    Name = x.Date.ToShortDateString()

                }).ToList();

            foreach (var defent in model.DefendantsList)
            {
                string[] li = Regex.Split(defent.Address, "/");
                if (li[0] != null)
                    defent.Address = li[0];
            }
            return model;
        }
        private int CalculateAge(DateTime birthdate)
        {
            // Save today's date.
            var today = DateTime.Today;
            // Calculate the age.
            var age = today.Year - birthdate.Year;
            // Go back to the year the person was born in case of a leap year
            if (birthdate > today.AddYears(-age)) age--;

            return age;
        }
        #endregion
    }
}