using JIC.Base.Views;
using JIC.Crime.View.Models;
using JIC.Services.ServicesInterfaces;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
//using Microsoft.ApplicationInsights.Extensibility.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace JIC.Crime.View.Controllers
{
    public class AllOpenRollsController : ControllerBase
    {
        private ISessionService SessionService;
        private IUserService userService;
        private IRollService RollService;
        private ICircuitService CircuitService;
        private IProsecutorService ProsecutorService;
        private ILookupService LookupService;

        public AllOpenRollsController(IUserService userService, ISessionService SessionService, IRollService RollService, ICircuitService CircuitService, IProsecutorService ProsecutorService, ILookupService lookupService)
        {
            this.SessionService = SessionService;
            this.userService = userService;
            this.RollService = RollService;
            this.CircuitService = CircuitService;
            this.ProsecutorService = ProsecutorService;
            this.LookupService = lookupService;
        }
        // GET: AllOpenRolls
        public ActionResult Index()
        {
            if (CurrentUser !=null)
            { 
            List<vw_SessionData> GetOpenedRolls = RollService.GetRollsOpend(CurrentUser.ID,CurrentUser.UserTypeID);
            OpenRollCreateViewModel _AllViewModel = new OpenRollCreateViewModel();
            _AllViewModel.AllOpenRolls = GetOpenedRolls;
            return View(_AllViewModel);
        }

                else
                {
                    return RedirectTo(Url.Action("login", "User",new {returnUrl ="/"})).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");
                }
        }
        [HttpGet]
        public ActionResult CompleteOPenRoll(int SessionID)
        {
            if (CurrentUser != null)
            {
                OpenRollViewModel OPenRoll = new OpenRollViewModel()
            {
                SessionID = SessionID,

            };
            OpenRollCreateViewModel _AllViewModel = new OpenRollCreateViewModel();
            _AllViewModel.OPenRoll = OPenRoll;
            vw_SessionData SessionDate = new vw_SessionData();
            SessionDate.SessionDate = RollService.GetCasesINRoll(SessionID).Select(e => e.RollDate).FirstOrDefault();
            _AllViewModel.SessionData = SessionDate;

            _AllViewModel.ListCasesInRoll = RollService.GetCasesINRoll(SessionID)
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
                CircuitID=roll.CircuitID,
                
            }).ToList();

          
            _AllViewModel.ListCasesInRoll = PrepareOrder(_AllViewModel.ListCasesInRoll);
            _AllViewModel = DayNameChange(_AllViewModel);
            return View(_AllViewModel);
            }

            else
            {
                return RedirectTo(Url.Action("login", "User", new { returnUrl = "/" })).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");
            }
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
    }
}