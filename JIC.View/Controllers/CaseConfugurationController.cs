using JIC.Services.ServicesInterfaces;
using JIC.Base.Views;
using JIC.Crime.View.Models;
using JIC.Crime.View.TestInterfaces;
using JIC.Crime.View.TestService;
using JIC.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JIC.Base;
using JIC.Crime.View.Helpers;

namespace JIC.Crime.View.Controllers
{
    //[CAuthorize(SystemUserTypes.CourtHead)]
    [CAuthorize(SystemUserTypes.ElementaryCourtAdministrator)]
    public class CaseConfugurationController : ControllerBase
    {
        private ILookupService lookupService;
        private IRollService RollService;
        private ICircuitService circuitService;
        private ICaseConfiguration caseSessionsService;
        private ISearchCasesService searchCasesService;
        private ISessionService SessionService;
        public CaseConfugurationController(ILookupService lookupService, ICircuitService circuitService, ICaseConfiguration caseSessionsService, ISearchCasesService searchCasesService, IRollService RollService, ISessionService SessionService)
        {
            this.lookupService = lookupService;
            this.circuitService = circuitService;
            this.caseSessionsService = caseSessionsService;
            this.searchCasesService = searchCasesService;
            this.RollService = RollService;
             this.SessionService= SessionService;
    }
        #region Index
        // GET: CaseConfuguration
        [HttpGet]

        //[ValidateAntiForgeryToken]
        public ActionResult Index()
        {
            if (CurrentUser != null)
            {
                return View(FillLists());
            }

            else
            {
                return RedirectTo(Url.Action("login", "User", new { returnUrl = "/" })).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");
            }
        }

        [HttpPost]

        //[ValidateAntiForgeryToken]
        public ActionResult Index(SessionsSearchViewModel model)
        {
            if (CurrentUser != null)
            {
                vw_AddSessionsSearchData sessionsSearchData = new vw_AddSessionsSearchData
            {
                CaseStatusID = model.CaseStatusID,
                CircuitID = model.CircuitID,
                CrimeID = model.CrimeID,
                CourtID = CurrentUser.CourtID,
                CrimeType = model.CrimeType,
                DefendantStatus = model.DefendantStatusID,
                FirstLevelNumber = model.FirstLevelNumber,
                FirstLevelProsecutionID = model.FirstLevelProsecutionID,
                FirstLevelYear = model.FirstLevelYear,
                PoliceStationID = model.PoliceStationID,
                SecondLevelNumber = model.SecondLevelNumber,
                SecondLevelProsecutionID = model.SecondLevelProsecutionID,
                SecondLevelYear = model.SecondLevelYear
            };
            List<vw_AddSessionSearchResult> SearchResult = searchCasesService.AddSessionsSearch(sessionsSearchData);
            model = FillLists();
            model.cases.AddRange(SearchResult.Select(x => new SessionsSearchGridViewModel
            {
                CaseID = x.CaseID,
                CaseStatus = x.CaseStatus,
                CrimeType = x.CrimeType,
                FirstLevelNumber = x.FirstLevelNumber,
                MainCrime = x.MainCrime,
                PoliceStation = x.PoliceStation,
                SecondLevelNumber = x.SecondLevelNumber
            }));
            return View(model);
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
        public ActionResult Create(string[] Cases , int CrimeID = 0)
        {
            if (CurrentUser != null)
            {
                CaseDataViewModels caseDataViewModels = new CaseDataViewModels();
            CaseConfigurationViewModel caseConfigurationViewModel = new CaseConfigurationViewModel();
            CaseConfigurationData caseConfigurationData = new CaseConfigurationData();
            caseDataViewModels.caseConfigurationData = caseConfigurationData;
            caseDataViewModels.caseConfigurationData.Circuits = circuitService.GetCircuitsByCrime(CrimeID,CurrentUser.CourtID.Value);
            caseDataViewModels.caseConfigurationData.caseConfiguration = caseConfigurationViewModel;
            List<int> CasesIDs = new List<int>();
            if (Cases != null)
            {
                foreach (var _case in Cases)
                {
                    CasesIDs.Add(Convert.ToInt32(_case));
                }
            }
            caseDataViewModels.caseConfigurationData.caseConfiguration.Cases = CasesIDs;

            //TempData["cases"] = Cases;
            System.Web.HttpContext.Current.Session["Cases"] = Cases;
                ViewData["SessionEnded"] = false;
                return CPartialView(caseDataViewModels);
            }

            else
            {
                ViewData["SessionEnded"] = true;
                return CPartialView();
            }
        }
        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult Create(CaseDataViewModels model)
        {
            if (CurrentUser != null)
            {
                try
            {

                string[] cases = (string[])Session["Cases"];
                List<int> CasesIDs = new List<int>();
                if (cases != null)
                {
                    foreach (var _case in cases)
                    {
                        CasesIDs.Add(Convert.ToInt32(_case));
                    }
                    model.caseConfigurationData.caseConfiguration.Cases = CasesIDs;
                }
                if (ModelState.IsValid)
                {
                    List<vw_KeyValueLongID> Sessions = caseSessionsService.GetCircuitRolls(model.caseConfigurationData.caseConfiguration.CircuitID).Select(x =>
               new vw_KeyValueLongID()
               {
                   ID = x.ID,
                   Name = x.Date.ToShortDateString()
               }).ToList();

                    CaseConfigurationViewModel caseConfigurationViewModel = new CaseConfigurationViewModel();

                    caseConfigurationViewModel.CircuitID = model.caseConfigurationData.caseConfiguration.CircuitID;
                    caseConfigurationViewModel.SessionID = model.caseConfigurationData.caseConfiguration.SessionID;
                    caseConfigurationViewModel.SessionDate = Sessions.Where(x => x.ID == model.caseConfigurationData.caseConfiguration.SessionID).Select(z => Convert.ToDateTime(z.Name)).SingleOrDefault();
                    caseConfigurationViewModel.Cases = CasesIDs;
                    int? rollid = RollService.GetRollID(caseConfigurationViewModel.CircuitID, caseConfigurationViewModel.SessionDate);
                    vw_CaseConfiguration caseConfiguration = GetConfigurationData(caseConfigurationViewModel);
                    caseConfiguration.SessionID = rollid;
                    if (caseSessionsService.AddCaseConfiguration(caseConfiguration))
                        return JavaScript("$(document).trigger('SessionCreate:Saved')");
                    else
                        return CPartialView().WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);
                }
                model.caseConfigurationData.Circuits = circuitService.GetCircuitsByCourtID(CurrentUser.CourtID.Value).ToList();
                if (Session["Sessions"] != null)
                    model.caseConfigurationData.Sessions = (List<vw_KeyValueLongID>)Session["Sessions"];
                    ViewData["SessionEnded"] = false;
                    return CPartialView(model);

            }
            catch (Exception ex)
            {
                return (ErrorPage(ex));
            }
            }

            else
            {
                ViewData["SessionEnded"] = true;
                return CPartialView();
            }

        }

        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult Case(string Cases)
        {
            return Json(Cases, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region Edit
        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult EditSessions(CaseDataViewModels model)
        {
            if (CurrentUser != null)
            {
                try
            {
                string[] cases = (string[])Session["Cases"];
                List<int> CasesIDs = new List<int>();
                if (cases != null)
                {
                    foreach (var _case in cases)
                    {
                        CasesIDs.Add(Convert.ToInt32(_case));
                    }
                    model.caseConfigurationData.caseConfiguration.Cases = CasesIDs;
                }
                if (ModelState.IsValid)
                {
                    List<vw_KeyValueLongID> Sessions = caseSessionsService.GetCircuitRolls(model.caseConfigurationData.caseConfiguration.CircuitID).Select(x =>
               new vw_KeyValueLongID()
               {
                   ID = x.ID,
                   Name = x.Date.ToShortDateString()
               }).ToList();

                    CaseConfigurationViewModel caseConfigurationViewModel = new CaseConfigurationViewModel();

                    caseConfigurationViewModel.CircuitID = model.caseConfigurationData.caseConfiguration.CircuitID;
                    caseConfigurationViewModel.SessionID = model.caseConfigurationData.caseConfiguration.SessionID;
                    caseConfigurationViewModel.SessionDate = Sessions.Where(x => x.ID == model.caseConfigurationData.caseConfiguration.SessionID).Select(z => Convert.ToDateTime(z.Name)).SingleOrDefault();
                    caseConfigurationViewModel.Cases = CasesIDs;
                    int? rollid= RollService.GetRollID(caseConfigurationViewModel.CircuitID, caseConfigurationViewModel.SessionDate);

                    vw_CaseConfiguration caseConfiguration = GetConfigurationData(caseConfigurationViewModel);
                    caseConfiguration.SessionID = rollid;
                    if (caseSessionsService.EditCaseConfiguration(caseConfiguration))
                    {
                        return JavaScript("$(document).trigger('Session:Saved')");
                        //PartialView(FillLists());
                    }


                    else
                        return CPartialView().WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);
                }
                model.caseConfigurationData.Circuits = circuitService.GetCircuitsByCourtID(CurrentUser.CourtID.Value).ToList();
                if (Session["Sessions"] != null)
                    model.caseConfigurationData.Sessions = (List<vw_KeyValueLongID>)Session["Sessions"];
                    ViewData["SessionEnded"] = false;
                    return CPartialView(model);

            }
            catch (Exception ex)
            {
                return (ErrorPage(ex));
            }
            }

            else
            {
                ViewData["SessionEnded"] = true;
                return CPartialView();
            }

        }
        [HttpGet]
        public ActionResult EditSessions(string[] Cases , int CrimeID =0)
        {
            if (CurrentUser != null)
            {
                CaseDataViewModels caseDataViewModels = new CaseDataViewModels();
            CaseConfigurationViewModel caseConfigurationViewModel = new CaseConfigurationViewModel();
            CaseConfigurationData caseConfigurationData = new CaseConfigurationData();
            caseDataViewModels.caseConfigurationData = caseConfigurationData;
            //caseDataViewModels.caseConfigurationData.Circuits = caseSessionsService.Circuits(CurrentUser.CourtID.Value);
            caseDataViewModels.caseConfigurationData.Circuits = circuitService.GetCircuitsByCrime(CrimeID, CurrentUser.CourtID.Value);
            caseDataViewModels.caseConfigurationData.caseConfiguration = caseConfigurationViewModel;
            List<int> CasesIDs = new List<int>();
            if (Cases != null)
            {
                foreach (var _case in Cases)
                {
                    CasesIDs.Add(Convert.ToInt32(_case));
                }
            }
            caseDataViewModels.caseConfigurationData.caseConfiguration.Cases = CasesIDs;

            Session["Cases"] = Cases;
            ViewData["SessionEnded"] = false;
            return CPartialView(caseDataViewModels);
        }

            else
            {
                ViewData["SessionEnded"] = true;
                return CPartialView();
    }
}
        [HttpGet]
        public ActionResult Edit()
        {
            //ViewData.TemplateInfo.HtmlFieldPrefix = "Edit";
            return CPartialView(FillLists()).WithPrefix("Edit");
        }
        [HttpPost]

        public ActionResult Edit([Bind(Prefix = "Edit")]SessionsSearchViewModel model)
        {
            if (CurrentUser != null)
            {
                //ViewData.TemplateInfo.HtmlFieldPrefix = "Edit";
                vw_AddSessionsSearchData sessionsSearchData = new vw_AddSessionsSearchData
            {
                CaseStatusID = model.CaseStatusID,
                CircuitID = model.CircuitID,
                CrimeID = model.CrimeID,
                CourtID = CurrentUser.CourtID,
                CrimeType = model.CrimeType,
                DefendantStatus = model.DefendantStatusID,
                FirstLevelNumber = model.FirstLevelNumber,
                FirstLevelProsecutionID = model.FirstLevelProsecutionID,
                FirstLevelYear = model.FirstLevelYear,
                PoliceStationID = model.PoliceStationID,
                SecondLevelNumber = model.SecondLevelNumber,
                SecondLevelProsecutionID = model.SecondLevelProsecutionID,
                SecondLevelYear = model.SecondLevelYear
            };
            List<vw_AddSessionSearchResult> SearchResult = searchCasesService.EditSessionSearch(sessionsSearchData);
            model = FillLists();
            model.cases.AddRange(SearchResult.Select(x => new SessionsSearchGridViewModel
            {
                CaseID = x.CaseID,
                CaseStatus = x.CaseStatus,
                CrimeType = x.CrimeType,
                FirstLevelNumber = x.FirstLevelNumber,
                MainCrime = x.MainCrime,
                PoliceStation = x.PoliceStation,
                SecondLevelNumber = x.SecondLevelNumber,
                SessionDate=x.SessionDate.ToShortDateString(),
               CrimeID = x.CrimeID,
               CircuitName=x.CircuitName,
            }));
            
            model.tabName = "#NotSendcases";
                ViewData["SessionEnded"] = false;
                return CPartialView(model).WithPrefix("Edit");
                //return RedirectTo(Url.Action("Index")).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);
            }

            else
            {
                ViewData["SessionEnded"] = true;
                return CPartialView();
            }
        }
        #endregion
        #region Helpers
        public ActionResult GetCircuitsByCrime(int CrimeID)
        {
            if (CurrentUser != null)
            {
                var Circuits = circuitService.GetCircuitsByCrime(CrimeID, CurrentUser.CourtID.Value);
            return Json(Circuits, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return RedirectTo(Url.Action("login", "User", new { returnUrl = "/" })).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");
            }

        }
        private vw_CaseConfiguration GetConfigurationData(CaseConfigurationViewModel caseConfigurationData)
        {
            return new vw_CaseConfiguration()
            {
                CasesIDs = caseConfigurationData.Cases,
                CircuitID = caseConfigurationData.CircuitID,
                SessionID = caseConfigurationData.SessionID,
                SessionDate = caseConfigurationData.SessionDate
            };
        }
        public ActionResult GetSessions(int CircuitID)
        {
            CaseConfigurationData caseConfiguration = new CaseConfigurationData
            {
                Sessions = caseSessionsService.GetCircuitRolls(CircuitID).Select(x =>
                new vw_KeyValueLongID()
                {
                    ID = x.ID,
                    Name = x.Date.ToShortDateString()
                }).OrderBy(z => DateTime.Parse(z.Name)).Where(y => DateTime.Parse(y.Name) > DateTime.Now).ToList()
            };
            Session["Sessions"] = caseConfiguration.Sessions;
            return CPartialView("SessionDropDown", new CaseDataViewModels
            {
                caseConfigurationData = caseConfiguration
            });
        }
        public ActionResult GetCircuitSessions(int CircuitID)
        {
            SessionsSearchViewModel SessionsSearch = new SessionsSearchViewModel
            {
                Sessions = caseSessionsService.GetCircuitRolls(CircuitID).Select(x =>
                new vw_KeyValueLongID()
                {
                    ID = x.ID,
                    Name = x.Date.ToShortDateString()
                }).OrderBy(z => DateTime.Parse(z.Name)).Where(y => DateTime.Parse(y.Name) > DateTime.Now).ToList()
            };
            return CPartialView("Sessions", new SessionsSearchViewModel
            {
                Sessions = SessionsSearch.Sessions
            });


        }
        SessionsSearchViewModel FillLists()
        {
            SessionsSearchViewModel SearchObj = new SessionsSearchViewModel
            {
                CourtID = CurrentUser.CourtID,
                Crimes = lookupService.GetCrimeTypes(),
                FirstLevelProsecutions = lookupService.GetIntialProsecutionsByCourtID(CurrentUser.CourtID.Value).ToList(),
                SecondLevelProsecutions = lookupService.GetElementaryProsecutions(CurrentUser.CourtID.Value).ToList(),
                PoliceStations = lookupService.GetPoliceStationsByCourtID(CurrentUser.CourtID.Value).ToList(),
                Circuits = circuitService.GetCircuitsByCrime((int)Enum_CrimeType.Normal, CurrentUser.CourtID.Value).ToList(),

                MainCrimes = lookupService.GetLookupsByCategory(Base.LookupsCategories.Crimes),
                CaseStatues = lookupService.GetLookupsByCategory(Base.LookupsCategories.CaseStatuses),
                DefendantsStautes = lookupService.GetLookupsByCategory(Base.LookupsCategories.PoliceStationDefendantsStatuses),


            };
            return SearchObj;



        }
        #endregion
        [HttpGet]
        public ActionResult AllCases()
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                return CPartialView(FillLists());
            }

            else
            {
                ViewData["SessionEnded"] = true;
                return CPartialView();
            }
        }
        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult AllCases(SessionsSearchViewModel model)
        {
            if (CurrentUser != null)
            {
                try
            {
                vw_AddSessionsSearchData sessionsSearchData = new vw_AddSessionsSearchData
                {
                    CaseStatusID = model.CaseStatusID,
                    CircuitID = model.CircuitID,
                    CrimeID = model.CrimeID,
                    CourtID = CurrentUser.CourtID,
                    CrimeType = model.CrimeType,
                    DefendantStatus = model.DefendantStatusID,
                    FirstLevelNumber = model.FirstLevelNumber,
                    FirstLevelProsecutionID = model.FirstLevelProsecutionID,
                    FirstLevelYear = model.FirstLevelYear,
                    PoliceStationID = model.PoliceStationID,
                    SecondLevelNumber = model.SecondLevelNumber,
                    SecondLevelProsecutionID = model.SecondLevelProsecutionID,
                    SecondLevelYear = model.SecondLevelYear
                };
                List<vw_AddSessionSearchResult> SearchResult = searchCasesService.AddSessionsSearch(sessionsSearchData);
                model = FillLists();
                model.cases.AddRange(SearchResult.Select(x => new SessionsSearchGridViewModel
                {
                    CaseID = x.CaseID,
                    CaseStatus = x.CaseStatus,
                    CrimeType = x.CrimeType,
                    FirstLevelNumber = x.FirstLevelNumber,
                    MainCrime = x.MainCrime,
                    PoliceStation = x.PoliceStation,
                    SecondLevelNumber = x.SecondLevelNumber,
                    CrimeID = x.CrimeID
                }));
                    ViewData["SessionEnded"] = false;
                    return CPartialView(model);

            }
            catch (Exception ex)
            {
                return (ErrorPage(ex));
            }
            }

            else
            {
                ViewData["SessionEnded"] = true;
                return CPartialView();
            }

        }

        public ActionResult CaseDetails(int id)
        {
            CaseDataViewModels model = new CaseDataViewModels();
            CaseBasicDataViewModel caseBasic = new CaseBasicDataViewModel();
            caseBasic.CaseID = id;
            model.CaseBasicDataViewModel = caseBasic;



            //CaseBasicDataViewModel model = new CaseBasicDataViewModel()
            //{
            //    CaseID = id
            //};
            return CPartialView(model);
        }

    }
}