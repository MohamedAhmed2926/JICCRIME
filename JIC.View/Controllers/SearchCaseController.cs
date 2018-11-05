using JIC.Base;
using JIC.Services.ServicesInterfaces;
using JIC.Base.Views;
using JIC.Crime.View.Models;
using JIC.Crime.View.TestInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JIC.Crime.View.Helpers;

namespace JIC.Crime.View.Controllers
{
    public class SearchCaseController : ControllerBase
    {
        public ISearchCasesService SearchServiceObj;
        private ILookupService lookupService;
        private ICircuitService circuitService;
        private ICrimeCaseService  caseService;

        public SearchCaseController(ISearchCasesService SearchServiceObj, ILookupService lookupService, ICircuitService circuitService,ICrimeCaseService caseService)
        {
            this.SearchServiceObj = SearchServiceObj;
            this.lookupService = lookupService;
            this.circuitService = circuitService;
            this.caseService = caseService;
        }

        public List<SearchGridViewModel> GetGridData(vw_SearchData SearchObj)
        {
            List<SearchGridViewModel> GridList_ = new List<SearchGridViewModel>();


            List<SearchResult> GridList = SearchServiceObj.Search(SearchObj);
            string SessionDate;
            foreach (SearchResult o in GridList)
            {
                if (o.LastSessionDate == null || o.LastSessionDate.Year == 1)
                {
                    SessionDate = "";
                }
                else
                {
                    SessionDate = o.LastSessionDate.ToShortDateString();
                }
                GridList_.Add(new SearchGridViewModel { CrimeType = o.CrimeType, CaseID = o.CaseID, FirstNumber = o.FirstLevelNumber, SecondNumber = o.SecondLevelNumber, OverAllNumber = o.OverAllNumber.ToString(), LastSessionDate = SessionDate, LastDecesion = o.LastDecision });

            }
            //  GridList_.Add(new SearchGridViewModel { CrimeType = "Crime1", CaseID = 1, FirstNumber ="2", SecondNumber = "3", OverAllNumber = "4", LastSessionDate = "30/12/2017", LastDecesion = "jj" });


            return GridList_;
        }

        //// GET: SearchCase
        [CAuthorize(SystemUserTypes.CourtHead, SystemUserTypes.InitialCourtAdministrator, SystemUserTypes.ElementaryCourtAdministrator,
           SystemUserTypes.CriminalDepManager, SystemUserTypes.schedualEmployee, SystemUserTypes.InquiriesEmployee,
         SystemUserTypes.ImplementationEmployee, SystemUserTypes.InquiriesEmployee, SystemUserTypes.Secretary, SystemUserTypes.Judge)]

        public ActionResult Search()
        {
            if (CurrentUser != null)
            {
                //Load Lists and default values here
                SearchViewModel Obj = FillSearchObject();
                if (CurrentUser.UserTypeID == (int)SystemUserTypes.JICAdmin)
                { ViewData["isJICAdmin"] = true; }
                else
                { ViewData["isJICAdmin"] = false; }
                return View(Obj);
            }
            else
            {
                return RedirectTo(Url.Action("login", "User", new { returnUrl = "/" })).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");
            }

        }

        [ValidateAntiForgeryToken]
        [HttpPost ]
        public ActionResult Search(SearchViewModel SearchObj)
        {
            if (CurrentUser != null)
            {

                FillLists(SearchObj);


            SearchObj.CourtID = CurrentUser.CourtID;


            ViewData["GridObj"] = SearchObj;

            if ((SearchObj.PartyName == "" || SearchObj.PartyName == null) && SearchObj.PartyType != null)
            {
                ModelState.AddModelError("PartyName", "يجب إدخال اسم الخصم");
                //  return CPartialView("SearchGrid", GridList).WithErrorMessages("يجب ادخال اسم الخصم");
            }
            if (CurrentUser.UserTypeID == (int)SystemUserTypes.JICAdmin)
            { ViewData["isJICAdmin"] = true; }
            else
            { ViewData["isJICAdmin"] = false; }
     
            return View(SearchObj);
        }
            else
            {
                return RedirectTo(Url.Action("login", "User", new { returnUrl = "/" })).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");
}
        }


        public SearchViewModel FillSearchObject()
        {
            SearchViewModel SearchObj = new SearchViewModel();
            FillLists(SearchObj);
            int CourtID =0 ;
            if (this.CurrentUser.CourtID != null)
            {
                CourtID = (int)this.CurrentUser.CourtID;
                SearchObj.CourtName = lookupService.GetCourts().Where(z => z.ID == CourtID).Select(k => k.Name).FirstOrDefault();
        }
            else
            {
                CourtID = lookupService.GetCourts().FirstOrDefault().ID;

            }
    SearchObj.CourtID = CourtID;
            SearchObj.SessionDate = null;
            SearchObj.JudgeDate = null;
            SearchObj.SessionDateType = (int)SessionSearchMode.All;
            SearchObj.HasObtainment = (int)ObtainmentStatus.All;
            return SearchObj;
        }

        void FillLists(SearchViewModel SearchObj)
        {
            int CourtID = 0;
           

            if (this.CurrentUser.CourtID == null)
            {
                
                SearchObj.Courts = lookupService.GetCourts();
                CourtID = SearchObj.Courts.FirstOrDefault().ID;
            }
            else
            {
                CourtID =(int) this.CurrentUser.CourtID;
            }
            SearchObj.CrimesTypes = lookupService.GetCrimeTypes().ToList();
            SearchObj.JudgeTypes = lookupService.GetJudgeTypes().ToList();
            SearchObj.FirstLevelProsecutions = lookupService.GetIntialProsecutionsByCourtID(CourtID).ToList();
            SearchObj.SecondLevelProsecutions =  lookupService.GetElementaryProsecutions (CourtID).ToList();
            SearchObj.PoliceStations = lookupService.GetPoliceStationsByCourtID(CourtID).ToList();
            SearchObj.Circuits = circuitService.GetCircuitsByCourtID(CourtID).ToList();
            SearchObj.SessionDateTypes = lookupService.GetSessionsDateTypes().ToList();
            SearchObj.PartyTypes = lookupService.GetPartyTypes().ToList();
            SearchObj.ObtainmentStatuses  = lookupService.GetObtainmentStatuses().ToList();
        }

 
        public ActionResult GetElementaryProsecutions(int CourtID)
        {
            var IntialProsecution = lookupService.GetElementaryProsecutions(CourtID).ToList();
            return Json(IntialProsecution, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetIntialProsecution(int CourtID)
        {
            var IntialProsecution = lookupService.GetIntialProsecutionsByCourtID(CourtID).ToList();
            return Json(IntialProsecution, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPoliceStation(int CourtID)
        {
            var PoliceStations = lookupService.GetPoliceStationsByCourtID(CourtID).ToList();
            return Json(PoliceStations, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCircuits(int CourtID)
        {
            var Circuits =circuitService.GetCircuitsByCourtID(CourtID).ToList();

            return Json(Circuits, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SearchGrid(int CourtID, int? FirstYear, int? FirstNumber, int? FirstLevelProsecutionID, int? SecondYear, int?
       SecondNumber, int? SecondLevelProsecutionID, int? PartyType, string PartyName, int? JudgeType, string
       JudgeDate, int? CircuitID, int? CrimeType, int? OverAllNumber, int?
       OverAllNumberYear, int? OverAllNumberProsecution, string SessionDate, SessionSearchMode? SessionDateType, int? PoliceStationID, ObtainmentStatus? HasObtainment)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;

                //if (this.CurrentUser.CourtID != null)
                //{ CourtID = (int)this.CurrentUser.CourtID; }

                vw_SearchData SearchDataObject = new vw_SearchData();
                List<SearchGridViewModel> GridList = new List<SearchGridViewModel>();
                SearchDataObject.CircuitID = CircuitID;
                SearchDataObject.CourtID = CourtID;// (int)this.CurrentUser.CourtID;  // ToDo: take court value from user session

                if (JudgeType == null)
                { SearchDataObject.JudgeType = JudgeType; }
                else
                { SearchDataObject.JudgeType = (int)JudgeType; }

                if (SessionDate == null || SessionDate == "")
                { SearchDataObject.SessionDate = null; }
                else
                { SearchDataObject.SessionDate = Convert.ToDateTime(SessionDate); }

                if (JudgeDate == null || JudgeDate == "")
                { SearchDataObject.JudgeDate = null; }
                else
                { SearchDataObject.JudgeDate = Convert.ToDateTime(JudgeDate); }

                SearchDataObject.CrimeType = CrimeType;

                if (SessionDateType == null)
                { SearchDataObject.SessionDateType = 0; }
                else
                { SearchDataObject.SessionDateType = SessionDateType; }

                //  SearchDataObject.SessionDateType = SessionDateType;
                SearchDataObject.FirstLevelNumber = FirstNumber;
                SearchDataObject.FirstLevelYear = FirstYear;
                SearchDataObject.HasObtainment = HasObtainment;
                SearchDataObject.PoliceStationID = PoliceStationID;
                SearchDataObject.SecondLevelProsecutionID = SecondLevelProsecutionID;
                SearchDataObject.SecondLevelNumber = SecondNumber;
                SearchDataObject.SecondLevelYear = SecondYear;
                SearchDataObject.FirstLevelProsecutionID = FirstLevelProsecutionID;
                SearchDataObject.OverAllNumber = OverAllNumber;
                SearchDataObject.OverAllNumberYear = OverAllNumberYear;
                SearchDataObject.OverAllNumberProsecution = OverAllNumberProsecution;
                SearchDataObject.PartyName = PartyName;
                SearchDataObject.PartyType = PartyType;




                GridList = GetGridData(SearchDataObject);
                if (GridList != null)
                { ViewData["Count"] = GridList.Count; }
                else
                { ViewData["Count"] = 0; }

                ViewData["UserID"] = CurrentUser.UserTypeID;

                if (CurrentUser.UserTypeID == (int)SystemUserTypes.ElementaryCourtAdministrator)
                {
                    ViewData["showDelete"] = true;
                }
                else
                { ViewData["showDelete"] = false; }

                if (CurrentUser.UserTypeID == (int)SystemUserTypes.schedualEmployee || CurrentUser.UserTypeID == (int)SystemUserTypes.ElementaryCourtAdministrator)
                {
                    ViewData["showEdit"] = true;
                }
                else
                {
                    ViewData["showEdit"] = false;
                }

                return PartialView("SearchGrid", GridList);

            }
            else
            {
                ViewData["SessionEnded"] = true;
                return PartialView();
            }
        }

        public ActionResult Delete(int CaseID)
        {
            return CPartialView(new SearchGridViewModel {  CaseID= CaseID });
        }
        [HttpPost]
        public ActionResult Delete(SearchGridViewModel CaseModel)
        {

            DeleteStatus DC = caseService.DeleteBasicData(CaseModel.CaseID);
            if (DC == DeleteStatus.Deleted)
            {
                return RedirectTo (Url.Action("Search")).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);

            }
            else
            {

                return RedirectTo(Url.Action("Search")).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);


            }


        }
    }
}