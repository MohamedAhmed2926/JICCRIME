using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JIC.Base.views;
using JIC.Crime.View.Models;
using JIC.Base.Views;
using JIC.Base;
using JIC.Crime.View.Helpers;
using System.Text.RegularExpressions;

namespace JIC.Crime.View.Controllers
{
    [CAuthorize(SystemUserTypes.Secretary, SystemUserTypes.Judge)]

    public class MinutesOfSessionController : ControllerBase
    { 
        private IDefectsService DefectsService;
        private ISessionService SessionService;
        private ILookupService LookupService;
        private ITextPredectionsService TextPredectionsService;
        private ICrimeCaseService CaseService;
        private ICircuitMembersService CircuitMembersService;
        private ICircuitService CircuitService;

        public MinutesOfSessionController(ICircuitService CircuitService, IDefectsService DefectsService, ISessionService SessionService, ILookupService LookupService, ITextPredectionsService TextPredectionsService, ICrimeCaseService CaseService, ICircuitMembersService CircuitMembersService)
        {
            this.DefectsService = DefectsService;
            this.SessionService = SessionService;
            this.LookupService = LookupService;
            this.TextPredectionsService = TextPredectionsService;
            this.CaseService = CaseService;
            this.CircuitMembersService = CircuitMembersService;
            this.CircuitService = CircuitService;
        }

        private MinutesOfSessionCreateViewModel PrepareViewModel(MinutesOfSessionViewModel MinutesOfSession = null)
        {
            if (MinutesOfSession == null)
            {
                MinutesOfSession = new MinutesOfSessionViewModel();
            }
            //   var attendanctype = LookupService.GetLookupsByCategory(Base.LookupsCategories.PresenceStatuses);
            var defendents = DefectsService.GetDefectsByCaseID(MinutesOfSession.CaseID, MinutesOfSession.SessionID);
            var result = new MinutesOfSessionCreateViewModel()
            {

                AutoCompleteText = GetTextPrediction(MinutesOfSession.CircuitID),
                MinutesOfSession = MinutesOfSession,
                SessionData = SessionService.GetSessionData(MinutesOfSession.SessionID),
                CaseBasicData = CaseService.GetCaseBasicData(MinutesOfSession.CaseID),
                vw_CircuitsGrid = CircuitMembersService.GetCircuitMembersByCircuitID(MinutesOfSession.CircuitID),
                CaseDefectsData = defendents
                      .Select(def => new CaseDefectsDataViewModel
                      {
                          ID = def.ID,
                          Address = def.Address,
                          Age = CalculateAge(def.Birthdate.Value),
                          Birthdate = def.Birthdate,
                          CaseID = def.CaseID,
                          DefectID = def.ID,
                          DefectType = def.DefectType,
                          IsCivilRightProsecutor = def.IsCivilRightProsecutor,
                          JobName = def.JobName,
                          Name = def.Name,
                          NationalID = def.NationalID,
                          NationalityType = def.NationalityType,
                          Nationality = def.Nationality,
                          Order = def.Order,
                          PassportNumber = def.PassportNumber,
                          PersonID = def.PersonID,
                          Presence = def.Presence,
                          Status = def.Status,
                          
                      }).Where(e => e.DefectType == PartyTypes.Defendant).OrderBy(defect => defect.Order).ToList(),

            };
            foreach (var defent in result.CaseDefectsData)
            {
                string[] li = Regex.Split(defent.Address, "/");
                if (li[0] != null)
                    defent.Address = li[0];
            }

            result.MinutesOfSession.Text = SessionService.GetMinutesOfSession(MinutesOfSession.SessionID);
            result.MinutesOfSession.CourtID = CurrentUser.CourtID;
            result.MinutesOfSession.CurentUserID = CurrentUser.ID;
            return result;
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

        public MinutesOfSessionCreateViewModel DayNameChange(MinutesOfSessionCreateViewModel _AllViewModel)
        {
            switch (_AllViewModel.SessionData.SessionDate.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    _AllViewModel.SessionData.DayName = "السبت";
                    break;
                case DayOfWeek.Sunday:
                    _AllViewModel.SessionData.DayName = "الأحد";
                    break;
                case DayOfWeek.Monday:
                    _AllViewModel.SessionData.DayName = "الاثنين";
                    break;
                case DayOfWeek.Tuesday:
                    _AllViewModel.SessionData.DayName = "الثلاثاء";
                    break;
                case DayOfWeek.Wednesday:
                    _AllViewModel.SessionData.DayName = "الاربعاء";
                    break;
                case DayOfWeek.Thursday:
                    _AllViewModel.SessionData.DayName = "الخميس";
                    break;
                case DayOfWeek.Friday:
                    _AllViewModel.SessionData.DayName = "الجمعة";
                    break;

            }

            return _AllViewModel;
        }
        public IDictionary<string, string> GetTextPrediction(int CircuitID)
        {
            List<vw_KeyValue> list = CircuitService.GetCircuitsBySecretairyID(CurrentUser.ID).Where(e=>e.ID==CircuitID).ToList();

            var TextPred = new Dictionary<string, string>();
            TextPredectionsService.GetTextPredections(list).ToList()
                 .ForEach(text => TextPred[text.TextTitle] = text.TextPredectionsDescription);
            return TextPred;
        }

        public ActionResult TextOfSession(int id, int SessionID)
        {
            string textMinutesSession = "";
            var DefectsAfterSave = DefectsService.GetDefectsByCaseID(id, SessionID).Where(defect => defect.DefectType == PartyTypes.Defendant).OrderBy(defect => defect.Order).ToList();

            foreach (var def in DefectsAfterSave)
            {
                switch (def.Presence)
                {
                    case PresenceStatus.PersonalAttendance:
                    case PresenceStatus.AttornyAttendance:
                    case PresenceStatus.ConsederationAttendance:
                        textMinutesSession += JIC.Base.Resources.Resources.Attendance;
                        break;

                    case PresenceStatus.AbsenceAttendance:
                        textMinutesSession += JIC.Base.Resources.Resources.Absence;
                        break;
                }

            }
            return Json(textMinutesSession, JsonRequestBehavior.AllowGet);
        }
        //  GET: WriteMinutesOfSession

        [HttpGet]
        public ActionResult Create(int id, int SessionID, int CircuitID)
        {
            if (CurrentUser != null)
            {
                MinutesOfSessionViewModel MinutesOfSession = new MinutesOfSessionViewModel();
         
            MinutesOfSession.CaseID = id;
            MinutesOfSession.CircuitID = CircuitID;
            MinutesOfSession.SessionID = SessionID;
            MinutesOfSession.CourtID = CurrentUser.CourtID;
            MinutesOfSession.CurentUserID = CurrentUser.ID;
            MinutesOfSession.CrimeTypeID = CaseService.GetCaseBasicData(id).CrimeTypeID;
            MinutesOfSessionCreateViewModel CreateViewModel = PrepareViewModel(MinutesOfSession);
            CreateViewModel.SessionData.SessionDate = CreateViewModel.SessionData.SessionDate.Date;

            CreateViewModel = DayNameChange(CreateViewModel);

            // MinutesOfSessionViewModel members = new MinutesOfSessionViewModel();
            CreateViewModel.MinutesOfSession.JudgeCount = CreateViewModel.vw_CircuitsGrid.Count();
            foreach (var member in CreateViewModel.vw_CircuitsGrid)
            {
                if (member.JudgePodiumType == (int)JudgePodiumType.HeadJudge)
                    CreateViewModel.MinutesOfSession.HeadJudge = member.JudgeName;
                if (member.JudgePodiumType == (int)JudgePodiumType.RightJudge)
                    CreateViewModel.MinutesOfSession.FirstJudge = member.JudgeName;
                if (member.JudgePodiumType == (int)JudgePodiumType.LeftLeftJudge)
                    CreateViewModel.MinutesOfSession.ThirdJudge = member.JudgeName;
                if (member.JudgePodiumType == (int)JudgePodiumType.LeftLeftLeftJudge)
                    CreateViewModel.MinutesOfSession.FourthJudge = member.JudgeName;
                if (member.JudgePodiumType == (int)JudgePodiumType.LeftLeftLeftLeftJudge)
                    CreateViewModel.MinutesOfSession.FifthJudge = member.JudgeName;
                if (member.JudgePodiumType == (int)JudgePodiumType.LeftLeftLeftLeftLeftJudge)
                    CreateViewModel.MinutesOfSession.SixthJudge = member.JudgeName;
                if (member.JudgePodiumType == (int)JudgePodiumType.OptionalJudge)
                    CreateViewModel.MinutesOfSession.alternativeJudge = member.JudgeName;
                if (member.JudgePodiumType == (int)JudgePodiumType.LeftJudge)
                    CreateViewModel.MinutesOfSession.SecondJudge = member.JudgeName;
            }
                ViewData["SessionEnded"] = false;
                return PartialView(CreateViewModel);
            }

            else
            {
                ViewData["SessionEnded"] = true;
                return PartialView();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MinutesOfSessionCreateViewModel MinutesOfSessionViewModel)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                vw_MinutesOfSession vw_Minutes = new vw_MinutesOfSession()
                {
                    SessionID = MinutesOfSessionViewModel.MinutesOfSession.SessionID,
                    Text = MinutesOfSessionViewModel.MinutesOfSession.Text,

                };
            if (MinutesOfSessionViewModel.MinutesOfSession.Text == null)
            {
                return CPartialView(PrepareViewModel(MinutesOfSessionViewModel.MinutesOfSession)).WithErrorMessages("برجاء كتابة المحضر");

            }
            
            var result = SessionService.SaveMinutesOfSession(vw_Minutes);
                if (result == Base.SaveMinutesOfSessionStatus.Succeeded)
            {
                ViewBag.Saved = true;
                return CPartialView(PrepareViewModel(MinutesOfSessionViewModel.MinutesOfSession)).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);

                }
            else if (result == SaveMinutesOfSessionStatus.SessionSentToJudge)
                 {
                return CPartialView(PrepareViewModel(MinutesOfSessionViewModel.MinutesOfSession)).WithErrorMessages("لا يمكن تعديل المحضر بعد إرساله للقاضى للتصديق");

            }
            else if (result == SaveMinutesOfSessionStatus.SessionApprovedByJudge)
                {
                    return CPartialView(PrepareViewModel(MinutesOfSessionViewModel.MinutesOfSession)).WithErrorMessages("لايمكن الحفظ تم التصديق من القاضى");

                }
                else
                {
                    return CPartialView(PrepareViewModel(MinutesOfSessionViewModel.MinutesOfSession)).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);
                }
            }

            else
            {
                ViewData["SessionEnded"] = true;
                return CPartialView();
            }
        }

            }



}
