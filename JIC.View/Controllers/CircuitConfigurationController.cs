using JIC.Base;
using JIC.Base.Resources;
using JIC.Services.ServicesInterfaces;
using JIC.Base.Views;
using JIC.Crime.View.Models;
using JIC.Crime.View.TestInterfaces;
using JIC.Crime.View.TestService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JIC.Crime.View.Helpers;
using System.Activities;

namespace JIC.Crime.View.Controllers
{
    [CAuthorize(SystemUserTypes.ElementaryCourtAdministrator)]
    public class CircuitConfigurationController : ControllerBase
    {

        private ILookupService lookupService;
        private ICircuitConfigurationService CircuitConfigurationService;
        private ICircuitService CircuitService;
        private IUserService UserService;

        public CircuitConfigurationController(ILookupService lookupService, ICircuitConfigurationService CircuitConfigurationService, ICircuitService CircuitService, IUserService UserService)
        {
            this.lookupService = lookupService;
            this.CircuitConfigurationService = CircuitConfigurationService;
            this.CircuitService = CircuitService;
            this.UserService = UserService;
        }
        #region Index
        public ActionResult Index()
        {
            if (CurrentUser != null)
            {
                List<GetCircuitData> _CircuitData = new List<GetCircuitData>();
            List<vw_CircuitsGrid> vw_CircuitsGrid = CircuitService.GetCircuitsFullData(CurrentUser.CourtID.Value);
            foreach (var Circuit in vw_CircuitsGrid)
            {
                GetCircuitData getCircuitData = new GetCircuitData();
                getCircuitData.ID = Circuit.ID;
                getCircuitData.CircuitName = Circuit.CircuitName;
                getCircuitData.HeadJugeName = Circuit.CenterJudgeName;
                getCircuitData.CrimeTypeName = Circuit.CrimeTypeName;
                getCircuitData.CircuitStartDate = Circuit.CircuitStartDate.Date;
                getCircuitData.StartDate = Circuit.CircuitStartDate.ToShortDateString();
                getCircuitData.CycleName = Circuit.CycleName;
                List<string> AllPolicestations = new List<string>();
                if (Circuit.PoliceStations.Count > 0)
                {

                    foreach (var PS in Circuit.PoliceStations)
                    {
                        AllPolicestations.Add(PS.Name);
                    }
                    getCircuitData.PoliceStationsName = string.Join(" , ", AllPolicestations.ToArray());
                }
                else
                    getCircuitData.PoliceStationsName = "";


                _CircuitData.Add(getCircuitData);
            }
            return View(_CircuitData);
            }

            else
            {
                return RedirectTo(Url.Action("login", "User", new { returnUrl = "/" })).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");
            }

        }
        #endregion
        #region Create
        // GET: CircuitConfiguration
        [HttpGet]
        public ActionResult Create()
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                return CPartialView(GetMainData());
            }
            else
            {
                ViewData["SessionEnded"] = true;
                return CPartialView();
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CircuitConfigurationViewModel model, List<int> PoliceStation, string submitButton)
        {
            if (CurrentUser != null)
            {
                try
            {
                if (model.JudgeCount == 4)
                {
                    if (model.ThirdJudge == null)
                        ModelState.AddModelError("ThirdJudge", Resources.RequiredErrorMessage);
                    if (model.FourthJudge == null)
                        ModelState.AddModelError("FourthJudge", Resources.RequiredErrorMessage);
                }
                else if(model.JudgeCount == 6)
                {
                    if (model.ThirdJudge == null)
                        ModelState.AddModelError("ThirdJudge", Resources.RequiredErrorMessage);
                    if (model.FourthJudge == null)
                        ModelState.AddModelError("FourthJudge", Resources.RequiredErrorMessage);
                    if (model.FifthJudge == null)
                        ModelState.AddModelError("FifthJudge", Resources.RequiredErrorMessage);
                    if (model.SixthJudge == null)
                        ModelState.AddModelError("SixthJudge", Resources.RequiredErrorMessage);
                }
                model.PoliceStation = PoliceStation.ToArray();
                if (PoliceStation.Count() == 0 || PoliceStation == null)
                    ModelState.AddModelError("PoliceStation", Resources.RequiredErrorMessage);
                if (ModelState.IsValid)
                {
                    vw_CircuitData CircuitData = new vw_CircuitData();
                    CircuitData.CircuitName = model.CircuitName;
                    CircuitData.CircuitStartDate = Convert.ToDateTime(model.CircuitStartDate);
                    if (CurrentUser.CourtID != null)
                    {
                        CircuitData.CourtID = CurrentUser.CourtID.Value;
                        CircuitData.UserName = CurrentUser.UserName;
                    }
                    else
                        CircuitData.CourtID = model.CourtID.Value;
                    CircuitData.CrimeTypeID = model.CrimeType.Value;
                    CircuitData.CycleID = model.Cycle.Value;
                    CircuitData.JudgeCount = model.JudgeCount;
                    CircuitData.AssistantSecretaryID = model.SecretaryAssistant.GetValueOrDefault();
                    CircuitData.SecretaryID = model.SecretaryHead.Value;
                    CircuitData.PoliceStations = (from a in model.PoliceStation select a).ToList();
                    List<vw_CircuitsJudges> SelectedJudges = new List<vw_CircuitsJudges>();
                    SelectedJudges.Add(new vw_CircuitsJudges() { JudgeID = model.HeadJudge.Value, JudgePodiumType = (int)JudgePodiumType.HeadJudge });
                    SelectedJudges.Add(new vw_CircuitsJudges() { JudgeID = model.FirstJudge.Value, JudgePodiumType = (int)JudgePodiumType.RightJudge });
                    SelectedJudges.Add(new vw_CircuitsJudges() { JudgeID = model.SecondJudge.Value, JudgePodiumType = (int)JudgePodiumType.LeftJudge });
                    if (model.alternativeJudge.HasValue)
                        SelectedJudges.Add(new vw_CircuitsJudges() { JudgeID = model.alternativeJudge.Value, JudgePodiumType = (int)JudgePodiumType.OptionalJudge });

                    switch (model.JudgeCount)
                    {
                        case 4:
                            SelectedJudges.Add(new vw_CircuitsJudges() { JudgeID = model.ThirdJudge.Value, JudgePodiumType = (int)JudgePodiumType.LeftLeftJudge });
                            SelectedJudges.Add(new vw_CircuitsJudges() { JudgeID = model.FourthJudge.Value, JudgePodiumType = (int)JudgePodiumType.LeftLeftLeftJudge });
                            
                            break;
                        case 6:
                            SelectedJudges.Add(new vw_CircuitsJudges() { JudgeID = model.ThirdJudge.Value, JudgePodiumType = (int)JudgePodiumType.LeftLeftJudge });
                            SelectedJudges.Add(new vw_CircuitsJudges() { JudgeID = model.FourthJudge.Value, JudgePodiumType = (int)JudgePodiumType.LeftLeftLeftJudge });
                            SelectedJudges.Add(new vw_CircuitsJudges() { JudgeID = model.FifthJudge.Value, JudgePodiumType = (int)JudgePodiumType.LeftLeftLeftLeftJudge });
                            SelectedJudges.Add(new vw_CircuitsJudges() { JudgeID = model.SixthJudge.Value, JudgePodiumType = (int)JudgePodiumType.LeftLeftLeftLeftLeftJudge });
                            break;
                    }
                    CircuitData.JudgesID = SelectedJudges;
                    int id;
                    var AddCircuitResult = CircuitService.AddCircuit(CircuitData, out id);
                        ViewData["SessionEnded"] = false;
                        switch (AddCircuitResult)
                    {
                        case SaveCircuitStatus.Saved_Successfully:
                            if (submitButton.Equals(Resources.SaveAndClose))
                                return RedirectJS(Url.Action("Index")).WithSuccessMessages(Messages.OperationCompletedSuccessfully);
                            else
                            {
                                ModelState.Clear();
                                return CPartialView(GetMainData()).WithSuccessMessages(Messages.OperationCompletedSuccessfully);
                            }
                        case SaveCircuitStatus.Saved_Before:
                            ModelState.AddModelError("CircuitName", JIC.Base.Resources.Messages.CircuitExists);
                            return CPartialView(GetMainData(model)).WithErrorMessages(Messages.CircuitExists);
                        case SaveCircuitStatus.Judge_Used_Twice:
                            return CPartialView(GetMainData(model)).WithErrorMessages(Messages.JudgeUsedTwice);
                        case SaveCircuitStatus.Secretary_Used_Twice:
                            return CPartialView(GetMainData(model)).WithErrorMessages(Messages.SecretaryUsedTwice);
                        case SaveCircuitStatus.Failed_To_Save_Judges:
                            return CPartialView(model).WithErrorMessages(Messages.OperationNotCompleted);
                        default:
                            return CPartialView(GetMainData(model)).WithErrorMessages(Messages.OperationNotCompleted);
                    }

                }


            }
            catch (Exception ex)
            {
                return ErrorPage(ex);
            }
            return CPartialView(GetMainData(model));
                //return CPartialView(GetMainData(model));
            }
            else
            {
                ViewData["SessionEnded"] = true;
                return CPartialView();
            }
        }

        #endregion
        #region Edit
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (CurrentUser != null)
            {
                try
            {
                ViewData.TemplateInfo.HtmlFieldPrefix = "Edit";
                vw_CircuitsGrid vw_CircuitsGrid = CircuitService.GetCircuitsFullDataByID(id.Value);
                CircuitConfigurationViewModel circuitConfigurationViewModel = GetMainData();
                circuitConfigurationViewModel.CircuitID = id;
                circuitConfigurationViewModel.CircuitName = vw_CircuitsGrid.CircuitName;
                circuitConfigurationViewModel.SecretaryHead = vw_CircuitsGrid.SecretaryHead;
                circuitConfigurationViewModel.SecretaryAssistant = vw_CircuitsGrid.SecretaryAssistant.GetValueOrDefault();
                circuitConfigurationViewModel.PoliceStation = vw_CircuitsGrid.PoliceStations.Select(x => x.ID).ToArray();
                int count = 0;
                foreach (var _member in vw_CircuitsGrid.CircuitMembers)
                {
                    if (_member.JudgePodiumType != (int)JudgePodiumType.HeadJudge && _member.JudgePodiumType != (int)JudgePodiumType.OptionalJudge)
                        count++;
                }
                circuitConfigurationViewModel.JudgeCount = count;
                circuitConfigurationViewModel.CrimeType = vw_CircuitsGrid.CrimeType;
                circuitConfigurationViewModel.Cycle = vw_CircuitsGrid.CycleID;
                circuitConfigurationViewModel.CircuitStartDate = vw_CircuitsGrid.CircuitStartDate.ToShortDateString();

                foreach (var member in vw_CircuitsGrid.CircuitMembers)
                {
                    if (member.JudgePodiumType == (int)JudgePodiumType.HeadJudge)
                        circuitConfigurationViewModel.HeadJudge = member.UserID;
                    if (member.JudgePodiumType == (int)JudgePodiumType.LeftJudge)
                        circuitConfigurationViewModel.FirstJudge = member.UserID;
                    if (member.JudgePodiumType == (int)JudgePodiumType.RightJudge)
                        circuitConfigurationViewModel.SecondJudge = member.UserID;
                    if (member.JudgePodiumType == (int)JudgePodiumType.LeftLeftJudge)
                        circuitConfigurationViewModel.ThirdJudge = member.UserID;
                    if (member.JudgePodiumType == (int)JudgePodiumType.LeftLeftLeftJudge)
                        circuitConfigurationViewModel.FourthJudge = member.UserID;
                    if (member.JudgePodiumType == (int)JudgePodiumType.LeftLeftLeftLeftJudge)
                        circuitConfigurationViewModel.FifthJudge = member.UserID;
                    if (member.JudgePodiumType == (int)JudgePodiumType.LeftLeftLeftLeftLeftJudge)
                        circuitConfigurationViewModel.SixthJudge = member.UserID;
                    if (member.JudgePodiumType == (int)JudgePodiumType.OptionalJudge)
                        circuitConfigurationViewModel.alternativeJudge = member.UserID;

                }
                    ViewData["SessionEnded"] = false;
                    return CPartialView(circuitConfigurationViewModel);
            }
            catch (Exception ex)
            {
                return ErrorPage(ex);
            }
            }
            else
            {
                ViewData["SessionEnded"] = true;
                return CPartialView();
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Prefix = "Edit")]CircuitConfigurationViewModel CircuitConfigurationViewModel, [Bind(Prefix = "Edit")]int[] Edit_PoliceStation)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;

                try
                {
                if (CircuitConfigurationViewModel.JudgeCount == 4)
                {
                    if (CircuitConfigurationViewModel.ThirdJudge == null)
                        ModelState.AddModelError("Edit.ThirdJudge", Resources.RequiredErrorMessage);
                    if (CircuitConfigurationViewModel.FourthJudge == null)
                        ModelState.AddModelError("Edit.FourthJudge", Resources.RequiredErrorMessage);
                }
                else if (CircuitConfigurationViewModel.JudgeCount == 6)
                {
                    if (CircuitConfigurationViewModel.ThirdJudge == null)
                        ModelState.AddModelError("Edit.ThirdJudge", Resources.RequiredErrorMessage);
                    if (CircuitConfigurationViewModel.FourthJudge == null)
                        ModelState.AddModelError("Edit.FourthJudge", Resources.RequiredErrorMessage);
                    if (CircuitConfigurationViewModel.FifthJudge == null)
                        ModelState.AddModelError("Edit.FifthJudge", Resources.RequiredErrorMessage);
                    if (CircuitConfigurationViewModel.SixthJudge == null)
                        ModelState.AddModelError("Edit.SixthJudge", Resources.RequiredErrorMessage);
                }
                if (ModelState.IsValid)
                {

                    List<vw_CircuitsJudges> SelectedJudges = new List<vw_CircuitsJudges>();
                    SelectedJudges.Add(new vw_CircuitsJudges() { JudgeID = CircuitConfigurationViewModel.HeadJudge.Value, JudgePodiumType = (int)JudgePodiumType.HeadJudge });
                    SelectedJudges.Add(new vw_CircuitsJudges() { JudgeID = CircuitConfigurationViewModel.FirstJudge.Value, JudgePodiumType = (int)JudgePodiumType.RightJudge });
                    SelectedJudges.Add(new vw_CircuitsJudges() { JudgeID = CircuitConfigurationViewModel.SecondJudge.Value, JudgePodiumType = (int)JudgePodiumType.LeftJudge });
                    if (CircuitConfigurationViewModel.alternativeJudge.HasValue)
                        SelectedJudges.Add(new vw_CircuitsJudges() { JudgeID = CircuitConfigurationViewModel.alternativeJudge.Value, JudgePodiumType = (int)JudgePodiumType.OptionalJudge });

                    switch (CircuitConfigurationViewModel.JudgeCount)
                    {
                        case 4:

                            SelectedJudges.Add(new vw_CircuitsJudges() { JudgeID = CircuitConfigurationViewModel.ThirdJudge.Value, JudgePodiumType = (int)JudgePodiumType.LeftLeftJudge });
                            SelectedJudges.Add(new vw_CircuitsJudges() { JudgeID = CircuitConfigurationViewModel.FourthJudge.Value, JudgePodiumType = (int)JudgePodiumType.LeftLeftLeftJudge });

                            break;
                        case 6:

                            SelectedJudges.Add(new vw_CircuitsJudges() { JudgeID = CircuitConfigurationViewModel.ThirdJudge.Value, JudgePodiumType = (int)JudgePodiumType.LeftLeftJudge });
                            SelectedJudges.Add(new vw_CircuitsJudges() { JudgeID = CircuitConfigurationViewModel.FourthJudge.Value, JudgePodiumType = (int)JudgePodiumType.LeftLeftLeftJudge });
                            SelectedJudges.Add(new vw_CircuitsJudges() { JudgeID = CircuitConfigurationViewModel.FifthJudge.Value, JudgePodiumType = (int)JudgePodiumType.LeftLeftLeftLeftJudge });
                            SelectedJudges.Add(new vw_CircuitsJudges() { JudgeID = CircuitConfigurationViewModel.SixthJudge.Value, JudgePodiumType = (int)JudgePodiumType.LeftLeftLeftLeftLeftJudge });

                            break;
                    }

                    vw_CircuitData vw_Circuit = new vw_CircuitData()
                    {
                        AssistantSecretaryID = CircuitConfigurationViewModel.SecretaryAssistant.GetValueOrDefault(),
                        CenterJudgeID = CircuitConfigurationViewModel.HeadJudge.Value,
                        CircuitName = CircuitConfigurationViewModel.CircuitName,
                        CircuitStartDate = Convert.ToDateTime(CircuitConfigurationViewModel.CircuitStartDate),
                        CourtID = CurrentUser.CourtID.Value,
                        CrimeTypeID = CircuitConfigurationViewModel.CrimeType.Value,
                        CycleID = CircuitConfigurationViewModel.Cycle.Value,
                        ID = CircuitConfigurationViewModel.CircuitID.Value,
                        JudgeCount = CircuitConfigurationViewModel.JudgeCount.Value,
                        SecretaryID = CircuitConfigurationViewModel.SecretaryHead.Value,
                        JudgesID = SelectedJudges,
                        PoliceStations = (from a in CircuitConfigurationViewModel.PoliceStation select a).ToList(),

                    };
                    var AddCircuitResult = CircuitService.EditCircuit(vw_Circuit);
                    switch (AddCircuitResult)
                    {
                        case SaveCircuitStatus.Saved_Successfully:
                            return RedirectJS(Url.Action("Index")).WithSuccessMessages(Messages.OperationCompletedSuccessfully);

                        case SaveCircuitStatus.Saved_Before:
                            ModelState.AddModelError("CircuitName", JIC.Base.Resources.Messages.CircuitExists);
                            return CPartialView(GetMainData(CircuitConfigurationViewModel)).WithErrorMessages(Messages.CircuitExists).WithPrefix("Edit");
                        case SaveCircuitStatus.Judge_Used_Twice:
                            return CPartialView(GetMainData(CircuitConfigurationViewModel)).WithErrorMessages(Messages.JudgeUsedTwice).WithPrefix("Edit");
                        case SaveCircuitStatus.Secretary_Used_Twice:
                            return CPartialView(GetMainData(CircuitConfigurationViewModel)).WithErrorMessages(Messages.SecretaryUsedTwice).WithPrefix("Edit");
                        case SaveCircuitStatus.Failed_To_Save_Judges:
                            return CPartialView(CircuitConfigurationViewModel).WithErrorMessages(Messages.OperationNotCompleted).WithPrefix("Edit");
                        default:
                            return CPartialView(GetMainData(CircuitConfigurationViewModel)).WithErrorMessages(Messages.OperationNotCompleted).WithPrefix("Edit");
                    }

                }
                else
                {
                    
                    return CPartialView(GetMainData(CircuitConfigurationViewModel)).WithPrefix("Edit");
                }
                

            }
            catch (Exception ex)
            {
                return ErrorPage(ex);
            }
            }
            else
            {
                ViewData["SessionEnded"] = true;
                return CPartialView();
            }
        }
        #endregion
        #region Delete
        [HttpGet]
        public ActionResult Delete(int? ID)
        {
            try
            {

                vw_CircuitsGrid vw_CircuitsGrid = CircuitService.GetCircuitsFullDataByID(ID.Value);
                GetCircuitData getCircuitData = new GetCircuitData()
                {
                    CircuitName = vw_CircuitsGrid.CircuitName
                };
                return CPartialView(getCircuitData);
            }
            catch (Exception ex)
            {
                return ErrorPage(ex);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(GetCircuitData getCircuitData)
        {
            try
            {
                var DeleteCircuitResult = CircuitService.DeleteCircuit(getCircuitData.ID.Value);
                switch (DeleteCircuitResult)
                {
                    case DeleteCircuitStatus.CaseConnectedToCircuit:
                        return RedirectJS(Url.Action("Index")).WithErrorMessages(Messages.CaseConnectedToCircuit);
                    case DeleteCircuitStatus.CircuitStartDateBeforeToday:
                        return RedirectJS(Url.Action("Index")).WithErrorMessages(Messages.InvalidDateLessThanToday);
                    case DeleteCircuitStatus.Deleted:
                        return RedirectJS(Url.Action("Index")).WithSuccessMessages(Messages.OperationCompletedSuccessfully);
                    case DeleteCircuitStatus.NotDeleted:
                        return RedirectJS(Url.Action("Index")).WithErrorMessages(Messages.OperationNotCompleted);
                    default:
                        return CPartialView().WithErrorMessages(Messages.OperationNotCompleted);
                }
            }
            catch (Exception ex)
            {
                return ErrorPage(ex);
            }
        }
        #endregion
        #region Helpers
        public ActionResult GetPoliceStationsByCourtID(int CourtID)
        {
            var AllPoliceStation = lookupService.GetPoliceStationsByCourtID(CourtID);
            return Json(AllPoliceStation, JsonRequestBehavior.AllowGet);
        }
        public CircuitConfigurationViewModel GetMainData(CircuitConfigurationViewModel model = null)
        {
            if (model == null)
            {
                DateTime defaultdate = new DateTime(DateTime.Now.Year, 10, 1);
                model = new CircuitConfigurationViewModel();
                model.CircuitStartDate = defaultdate.ToShortDateString();
            }
            var allCourts = lookupService.GetCourts();
            if (IsAuthenticatied && CurrentUser.CourtID != null)
            {
                model.AllJudges = UserService.GetAllJudges(CurrentUser.CourtID.Value);
                model.AllSecretaries = UserService.GetAllSecretaries(CurrentUser.CourtID.Value);
                model.CourtName = allCourts.Where(x => x.ID == CurrentUser.CourtID.Value).Select(courtid => courtid.Name).Single();
                model.AllPoliceStation = lookupService.GetPoliceStationsByCourtID(CurrentUser.CourtID.Value);
            }

            else
                model.AllCourts = allCourts.ToList();

            model.Circuits = CircuitService.GetCircuits();
           
            model.AllCrimes = lookupService.GetCrimeTypes();
            model.CrimeType = (int)Enum_CrimeType.Normal;
            model.AllCycles = lookupService.GetAllCycles();



            return model;
        }
        //public ActionResult Save();
        #endregion
    }

}