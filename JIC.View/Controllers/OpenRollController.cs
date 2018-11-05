using JIC.Base;
using JIC.Services.ServicesInterfaces;
using JIC.Base.Views;
using JIC.Crime.View.Helpers;
using JIC.Crime.View.Models;
using JIC.Crime.View.Services;
using JIC.Crime.View.TestInterfaces;
using JIC.Crime.View.TestService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIC.Crime.View.Controllers
{
    [CAuthorize(SystemUserTypes.Secretary)]
    public class OpenRollController : ControllerBase
    {
        private ISessionService SessionService;
        private IUserService userService;
        private IRollService RollService;
        private ICircuitService CircuitService;
        private IProsecutorService ProsecutorService;
        private ILookupService LookupService;
    
        public OpenRollController(IUserService userService, ISessionService SessionService, IRollService RollService, ICircuitService CircuitService, IProsecutorService ProsecutorService, ILookupService lookupService)
        {
            this.SessionService = SessionService;
            this.userService = userService;
            this.RollService = RollService;
            this.CircuitService = CircuitService;
            this.ProsecutorService = ProsecutorService;
            this.LookupService = lookupService;
        }

        private OpenRollCreateViewModel PrepareViewModel(OpenRollViewModel OPenRoll = null)
        {
            if (OPenRoll == null)
            {
                OPenRoll = new OpenRollViewModel();
            }
            OpenRollCreateViewModel model=  new OpenRollCreateViewModel()
            {
                
               ListCasesInRoll = RollService.GetRollCasesForOpening(OPenRoll.CircuitID, CurrentUser.ID)
                   .Select(roll => new OpenRollViewModel
                   {
                       SessionID = roll.SessionID,
                       Order = roll.Order,
                       CaseID = roll.CaseID,
                       CaseStatus = roll.CaseStatus,
                       OverAllNumber = roll.OverAllNumber,
                       FirstLevelNumber = roll.FirstLevelNumber,
                       SecondLevelNumber = roll.SecondLevelNumber,
                       MainCrime = roll.MainCrime,
                       SecretaryID = roll.SecretaryID,
                       RollID=roll.RollID,
                       //CircuitID=roll.CircuitID,
                    //   rollStatus=roll.rollStatus,
                   }).ToList(),
                //  SessionData = SessionService.GetSessionData(OPenRoll.CircuitID),
                Circuits = CircuitService.GetCircuitsBySecretairyID(CurrentUser.ID, CurrentUser.CourtID),
               
               OPenRoll = OPenRoll,
                Prosecutors = ProsecutorService.GetProsecutor(CurrentUser.CourtID)
                .Select(pros => new ProsecutorViewModels
                {
                    ID = pros.ID,
                    ProcecutoerName = pros.ProcecutoerName,
                }).ToList(),
                Halls = LookupService.GetCourtHalls(IsAuthenticatied ? CurrentUser.CourtID : null),
            };
           
            if (model.ListCasesInRoll.Count() != 0)
                model.OPenRoll.RollID = model.ListCasesInRoll.Select(e => e.RollID).FirstOrDefault();
            if (model.ListCasesInRoll.Any(e => e.Order == 0))
            {
                model.OPenRoll.rollStatus = RollStatus.NotSorted;
            }
            model.ListCasesInRoll = PrepareOrder(model.ListCasesInRoll);
            vw_SessionData SessionDate = new vw_SessionData();
            SessionDate.SessionDate = DateTime.Now;
            model.SessionData = SessionDate;
            model.OPenRoll.CourtID = CurrentUser.CourtID;
            model.OPenRoll.CurentUserID = CurrentUser.ID;
            return model;
        }
        private List<OpenRollViewModel> PrepareOrder(List<OpenRollViewModel> OpenCases)
        {
            int count = 1;
            foreach (var Cases in OpenCases.OrderBy(roll => roll.Order))
            {
                Cases.Order = count;
                count++;
            };
            return OpenCases;

        }
        // GET: OpenRoll
        public ActionResult Index()
        {
            if (CurrentUser != null)
            {
                //  List<vw_RollCases> GetOpenedRolls = RollService.GetOpenedRolls(CurrentUser.ID);

                OpenRollCreateViewModel model = new OpenRollCreateViewModel()
                {
                    Circuits = CircuitService.GetCircuitsBySecretairyID(CurrentUser.ID, CurrentUser.CourtID)

                };

                if (model.Circuits.Count==0)
                {
                    return RedirectTo(Url.Action("NoSession")).WithErrorMessages("لا يوجد جلسات لفتحها اليوم");

                }
                return View(model);
            }

            else
            {
                return RedirectTo(Url.Action("login", "User", new { returnUrl = "/" })).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");
            }
        }
        public OpenRollCreateViewModel DayNameChange(OpenRollCreateViewModel _AllViewModel)
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
    [HttpGet]
        public ActionResult Create(int CircuitID)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                OpenRollViewModel OPenRoll = new OpenRollViewModel()
                {
                    CircuitID = CircuitID,
                    CourtID = CurrentUser.CourtID,
                    CurentUserID = CurrentUser.ID
                };
                OpenRollCreateViewModel _AllViewModel = PrepareViewModel(OPenRoll);
                _AllViewModel.ListCasesInRoll = PrepareOrder(_AllViewModel.ListCasesInRoll);
                //if (_AllViewModel.ListCasesInRoll.First().rollStatus == RollStatus.NotStarted)
                //{
                //    _AllViewModel.OPenRoll.CircuitID = _AllViewModel.ListCasesInRoll.First().CircuitID;
                //    ViewBag.Saved = true;
                //}
                _AllViewModel = DayNameChange(_AllViewModel);

                return PartialView(_AllViewModel);
            }
            else
            {
                ViewData["SessionEnded"] = true;
                return PartialView();
            }
            }
        [HttpPost]
        public ActionResult Create(OpenRollViewModel OPenRoll)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                if (ModelState.IsValid)
            {

                var OpenSessionRollResult = RollService.OpenSessionRoll(OPenRoll.prosecutorID, OPenRoll.HallID, OPenRoll.RollID);
               OPenRoll.rollStatus= OpenSessionRollResult;
                switch (OpenSessionRollResult)
                {
                    case RollStatus.NotSorted:
                        return CPartialView(PrepareViewModel(OPenRoll)).WithErrorMessages("لا يمكن فتح رول الجلسة لأنه لم يتم ترتيب الرول");
                    case RollStatus.PreviousRollNotClosed:
                        return CPartialView(PrepareViewModel(OPenRoll)).WithErrorMessages("لا يمكن فتح رول الجلسة لأنه لم يتم غلق الرول السابق");
                    case RollStatus.InProgress:
                        ViewBag.Saved = true;
                        
                    return CPartialView(PrepareViewModel(OPenRoll)).WithSuccessMessages("تم فتح الرول");
                    case RollStatus.OtherRollOpenForSecretary:
                        return CPartialView(PrepareViewModel(OPenRoll)).WithErrorMessages("نفس الرول مفتوح من امين سر اخر ");
                    case RollStatus.EmptyRoll:
                        return CPartialView(PrepareViewModel(OPenRoll)).WithErrorMessages("لا يوجد قضايا فى الرول");

                }
            }
             
            return PartialView(PrepareViewModel(OPenRoll));
            }

            else
            {
                ViewData["SessionEnded"] = true;
                return PartialView();
            }

        }
        public ActionResult OPenRoll(int CircuitID)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                OpenRollViewModel OPenRoll = new OpenRollViewModel()
            {
                CircuitID = CircuitID,
                CourtID = CurrentUser.CourtID,
                CurentUserID = CurrentUser.ID
            };
            OpenRollCreateViewModel _AllViewModel = new OpenRollCreateViewModel();
            _AllViewModel.OPenRoll = OPenRoll;
            vw_SessionData SessionDate = new vw_SessionData();
            SessionDate.SessionDate = RollService.GetOpenedRolls(CurrentUser.ID).Select(e => e.RollDate).First();
            _AllViewModel.SessionData = SessionDate;

            _AllViewModel.ListCasesInRoll = RollService.GetOpenedRolls(CurrentUser.ID)
            .Select(roll => new OpenRollViewModel
            {
                SessionID = roll.SessionID,
                Order = roll.Order,
                CaseID = roll.CaseID,
                CaseStatus = roll.CaseStatus,
                OverAllNumber = roll.OverAllNumber,
                FirstLevelNumber = roll.FirstLevelNumber,
                SecondLevelNumber = roll.SecondLevelNumber,
                MainCrime = roll.MainCrime,
                SecretaryID = roll.SecretaryID,

            }).ToList();


            _AllViewModel.ListCasesInRoll = PrepareOrder(_AllViewModel.ListCasesInRoll);
            _AllViewModel = DayNameChange(_AllViewModel);
            return PartialView(_AllViewModel);
            }

            else
            {
                ViewData["SessionEnded"] = true;
                return PartialView();
            }

        }
        
        public ActionResult NoSession()
        {

            return View();
        }
        
        }

}