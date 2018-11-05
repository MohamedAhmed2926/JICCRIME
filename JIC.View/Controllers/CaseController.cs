using JIC.Base;
using JIC.Base.Resources;
using JIC.Services.ServicesInterfaces;
using JIC.Base.Views;
using JIC.Crime.View.Helpers;
using JIC.Crime.View.Models;
using JIC.Crime.View.TestService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIC.Crime.View.Controllers
{
    [CAuthorize(SystemUserTypes.schedualEmployee, SystemUserTypes.ElementaryCourtAdministrator)]
    public class CaseController : ControllerBase
    {
        private ILookupService lookupService;
        private ICrimeCaseService CaseService;

        public CaseController(ILookupService lookupService, ICrimeCaseService CaseService)
        {
            this.lookupService = lookupService;
            this.CaseService = CaseService;
        }
        #region Index
        public ActionResult Index()
        {
            return View();
        }
        #endregion
        #region Create
        [HttpGet]
        public ActionResult Create()
        {
            if (CurrentUser != null)
            {
                CaseBasicDataViewModel caseBasicDataViewModel = new CaseBasicDataViewModel();
            caseBasicDataViewModel = GetCaseBasicData();
            return View(caseBasicDataViewModel);
        }

                else
                {
                    return RedirectTo(Url.Action("login", "User",new {returnUrl ="/"})).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");
}
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CaseBasicDataViewModel model)
        {
            if (CurrentUser != null)
            {

                try
                {
                CaseBasicDataViewModel _CaseBasicDataViewModel = GetCaseBasicData();
                model.Courts = _CaseBasicDataViewModel.Courts;
                model.CrimesTypes = _CaseBasicDataViewModel.CrimesTypes;
                model.AllMainCrimeType = _CaseBasicDataViewModel.AllMainCrimeType;
                model.Courts = lookupService.GetCourts().Where(x => x.ID == CurrentUser.CourtID.Value).ToList();
                if (model.CourtID != 0)
                    model.SecondLevelProsecutions = GetElementaryProsecutions(model.CourtID);
                if (model.SecondLevelProcID != 0)
                    model.FirstLevelProsecutions = lookupService.GetIntialProsecutions(model.SecondLevelProcID.Value).ToList();
                if (model.PoliceStationID != 0)
                    model.PoliceStations = lookupService.GetPoliceStations(model.FirstLevelProsecutionID.Value).ToList();
                if (ModelState.IsValid)
                {
                    int _CaseID = 0;
                    vw_CrimeCaseBasicData vw_CaseBasicData = new vw_CrimeCaseBasicData
                    {
                        CaseName = model.CaseName,
                        CourtID = model.CourtID,
                        MainCrimeID = model.MainCrimeID.Value,
                        CrimeTypeID = model.CrimeID,
                        FirstNumberInt = model.FirstNumber.Value,
                        FirstYearInt = model.FirstYear.Value,
                        HasObtainment = model.HasObtainment,
                        FirstPoliceStationID = model.PoliceStationID.Value,
                        SecondProsecutionID = model.SecondLevelProcID.Value,
                        SecondNumberInt = model.SecondNumber.Value,
                        SecondYearInt = model.SecondYear.Value,
                        FirstProsecutionID = model.FirstLevelProsecutionID.Value,
                        NationalID = model.CaseNationalID,
                        CaseStatusID = (int)CaseStatuses.New

                    };
                    int CaseID;

                    var AddCaseBasicData = CaseService.AddBasicData(vw_CaseBasicData, out CaseID);
                    switch (AddCaseBasicData)
                    {
                        case CaseSaveStatus.Saved:
                            _CaseID = CaseID;
                            return RedirectTo(Url.Action("Edit", new { id = _CaseID })).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);
                        case CaseSaveStatus.Failed:
                            ShowMessage(MessageTypes.Error, Messages.OperationNotCompleted, true);
                            break;
                        case CaseSaveStatus.Saved_Before:
                            ShowMessage(MessageTypes.Error, Messages.CaseIsExist, true);
                            break;
                        case CaseSaveStatus.SecondNumberExistBefore:
                            ShowMessage(MessageTypes.Error, Messages.SecondNumberExistBefore, true);
                           // ModelState.AddModelError("SecondNumber", Messages.SecondNumberExistBefore);
                            return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                return ErrorPage(ex);
            }
            return View(model);
            }

            else
            {
                return RedirectTo(Url.Action("login", "User", new { returnUrl = "/" })).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");
            }
        }
        #endregion
        #region Edit
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (CurrentUser != null)
            {
                try
            {
                CaseBasicDataViewModel caseBasicDataViewModel = new CaseBasicDataViewModel();
                caseBasicDataViewModel = GetCaseBasicData();
                vw_CrimeCaseBasicData vw_CaseData = CaseService.GetCaseBasicData(id);
                if (CurrentUser.UserTypeID != (int)SystemUserTypes.ElementaryCourtAdministrator && vw_CaseData.OverAllId != null)
                    return CPartialView("UnAuthorizedEdit");
                else
                {

                    caseBasicDataViewModel.CaseID = vw_CaseData.CaseID;
                    caseBasicDataViewModel.CaseName = vw_CaseData.CaseName;
                    caseBasicDataViewModel.CaseNationalID = vw_CaseData.NationalID;
                    caseBasicDataViewModel.CourtID = vw_CaseData.CourtID;
                    caseBasicDataViewModel.CourtName = vw_CaseData.CourtName;
                    caseBasicDataViewModel.SecondLevelProcID = vw_CaseData.SecondProsecutionID;
                    caseBasicDataViewModel.SecondLevelProsecutions =GetElementaryProsecutions(CurrentUser.CourtID.Value);
                    caseBasicDataViewModel.FirstLevelProsecutionID = vw_CaseData.FirstProsecutionID;
                    caseBasicDataViewModel.FirstLevelProsecutions = lookupService.GetIntialProsecutions(caseBasicDataViewModel.SecondLevelProcID.Value);
                    caseBasicDataViewModel.PoliceStations = lookupService.GetPoliceStations(caseBasicDataViewModel.FirstLevelProsecutionID.Value);
                    caseBasicDataViewModel.PoliceStationID = vw_CaseData.FirstPoliceStationID;
                    caseBasicDataViewModel.CrimeID = vw_CaseData.CrimeTypeID;
                    caseBasicDataViewModel.MainCrimeID = vw_CaseData.MainCrimeID;
                    caseBasicDataViewModel.FirstNumber = vw_CaseData.FirstNumberInt;
                    caseBasicDataViewModel.FirstYear = vw_CaseData.FirstYearInt;
                    caseBasicDataViewModel.HasObtainment = vw_CaseData.HasObtainment;
                    caseBasicDataViewModel.OverAllNumber = vw_CaseData.OverAllNumber;
                    caseBasicDataViewModel.OverAllNumberProsecution = vw_CaseData.OverAllNumberProsecution;
                    caseBasicDataViewModel.OverAllNumberYear = vw_CaseData.OverAllNumberYear;
                    caseBasicDataViewModel.MainCrimeID = vw_CaseData.MainCrimeID;
                    caseBasicDataViewModel.SecondNumber = vw_CaseData.SecondNumberInt;
                    caseBasicDataViewModel.SecondYear = vw_CaseData.SecondYearInt;
                    caseBasicDataViewModel.UserType = CurrentUser.UserTypeID;
                    caseBasicDataViewModel.OrderOfAssignment = vw_CaseData.OrderOfAssignment;
                    caseBasicDataViewModel.OverAllId = vw_CaseData.OverAllId;
                    caseBasicDataViewModel.IsComplete = vw_CaseData.IsComplete;
                    return View(caseBasicDataViewModel);
                }
            }
            catch (Exception ex)
            {
                return ErrorPage(ex);
            }
        }

                else
                {
                    return RedirectTo(Url.Action("login", "User",new {returnUrl ="/"})).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");
}
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CaseBasicDataViewModel model)
        {
            if (CurrentUser != null)
            {
                try
            {
                CaseBasicDataViewModel _CaseBasicDataViewModel = GetCaseBasicData();
                model.Courts = _CaseBasicDataViewModel.Courts;
                model.CrimesTypes = _CaseBasicDataViewModel.CrimesTypes;
                model.AllMainCrimeType = _CaseBasicDataViewModel.AllMainCrimeType;
                if (model.CourtID != 0)
                    model.SecondLevelProsecutions = GetElementaryProsecutions(model.CourtID);
                if (model.SecondLevelProcID != 0)
                    model.FirstLevelProsecutions = lookupService.GetIntialProsecutions(model.SecondLevelProcID.Value).ToList();
                if (model.PoliceStationID != 0)
                    model.PoliceStations = lookupService.GetPoliceStations(model.FirstLevelProsecutionID.Value).ToList();
                if (ModelState.IsValid)
                {
                    vw_CrimeCaseBasicData vw_CaseBasicData = new vw_CrimeCaseBasicData
                    {
                        CaseID = model.CaseID,
                        CaseName = model.CaseName,
                        CourtID = model.CourtID,
                        CrimeTypeID = model.CrimeID,
                        CourtName = model.CourtName,
                        FirstNumberInt = model.FirstNumber.Value,
                        FirstYearInt = model.FirstYear.Value,
                        HasObtainment = model.HasObtainment,
                        FirstPoliceStationID = model.PoliceStationID.Value,
                        SecondProsecutionID = model.SecondLevelProcID.Value,
                        SecondNumberInt = model.SecondNumber.Value,
                        SecondYearInt = model.SecondYear.Value,
                        FirstProsecutionID = model.FirstLevelProsecutionID.Value,
                        NationalID = model.CaseNationalID,
                        CaseStatusID = (int)CaseStatuses.New,
                        MainCrimeID = model.MainCrimeID.GetValueOrDefault(),
                        OrderOfAssignment = model.OrderOfAssignment,
                        OverAllId =model.OverAllId,
                        IsComplete=model.IsComplete,

                    };
                    var AddCaseBasicData = CaseService.UpdateBasicData(vw_CaseBasicData);
                    switch (AddCaseBasicData)
                    {
                        case CaseSaveStatus.Saved:

                            return RedirectTo(Url.Action("Edit", new { id = model.CaseID })).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);
                        case CaseSaveStatus.Failed:
                            ShowMessage(MessageTypes.Error, Messages.OperationNotCompleted, true);
                            break;
                        case CaseSaveStatus.Saved_Before:
                            ShowMessage(MessageTypes.Error, Messages.CaseIsExist, true);
                            break;
                        case CaseSaveStatus.SecondNumberExistBefore:
                            ModelState.AddModelError("SecondNumber", Messages.SecondNumberExistBefore);
                            if (this.IsAuthenticatied && this.CurrentUser.CourtID == null)
                                model.Courts = lookupService.GetCourts().ToList();
                            else
                                model.CourtName = lookupService.GetCourts().Where(x => x.ID == CurrentUser.CourtID.Value).Select(x => x.Name).Single();

                            return View(model);
                    }
                }

            }
            catch (Exception ex)
            {
                return ErrorPage(ex);
            }
            return View(model);
            }

            else
            {
                return RedirectTo(Url.Action("login", "User", new { returnUrl = "/" })).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");
            }
        }
        #endregion
        #region ViewCase
        public ActionResult View(int id)
        {
            try
            {
                CaseDataViewModels caseData = new CaseDataViewModels();
                vw_CaseData BasicData = CaseService.GetCaseData(id);
                CaseBasicDataViewModel caseBasicDataViewModel = new CaseBasicDataViewModel()
                {
                    
                    CourtName = BasicData.CaseBasicData.CourtName,
                    PoliceStationName = BasicData.CaseBasicData.PoliceStationName,
                    CaseID = BasicData.CaseBasicData.CaseID,
                    FirstNumber = BasicData.CaseBasicData.FirstNumberInt,
                    FirstYear = BasicData.CaseBasicData.FirstYearInt,
                    FirstLevelProsecutionID = BasicData.CaseBasicData.FirstProsecutionID,
                    SecondNumber = BasicData.CaseBasicData.SecondNumberInt,
                    SecondYear = BasicData.CaseBasicData.SecondYearInt,
                    SecondLevelProcID = BasicData.CaseBasicData.SecondProsecutionID,

                    OverAllNumber = BasicData.CaseBasicData.OverAllNumber,
                    OverAllNumberProsecution = BasicData.CaseBasicData.OverAllNumberProsecution,
                    OverAllNumberYear = BasicData.CaseBasicData.OverAllNumberYear,

                    CaseName = BasicData.CaseBasicData.CaseName,
                    MainCrime = BasicData.CaseBasicData.MainCrimeName,
                    HasObtainment = BasicData.CaseBasicData.HasObtainment,
                    FirstprosecutionName = BasicData.CaseBasicData.FirstprosecutionName,
                    SecoundProsecutionName = BasicData.CaseBasicData.SecoundProsecutionName,
                    CaseNationalID = BasicData.CaseBasicData.NationalID,
                    
                };
                if (caseBasicDataViewModel.HasObtainment)
                {
                    caseBasicDataViewModel.Obtainment = JIC.Base.Resources.Resources.HasObtainment;

                }
                else
                {
                    caseBasicDataViewModel.Obtainment = JIC.Base.Resources.Resources.NotHasObtainment;

                }
                return CPartialView(caseBasicDataViewModel);
            }
            catch (Exception ex)
            {
                return ErrorPage(ex);
            }
        }
        #endregion
        [HttpGet]
        public ActionResult Delete(int CaseID)
        {
            return CPartialView(new CaseBasicDataViewModel { CaseID = CaseID });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CaseBasicDataViewModel model)
        {
            if (ModelState.IsValid)
            {
                CaseService.DeleteBasicData(model.CaseID);
                return JavaScript("$(document).trigger('Case:DeleteSuccefull');");
            }
            return null;
        }


        #region Helpers
        //public List<vw_KeyValue> GetCrime(int CourtID)
        //{
        //    return 
        //}
        public CaseBasicDataViewModel GetCaseBasicData()
        {
            CaseBasicDataViewModel caseBasicDataViewModel = new CaseBasicDataViewModel();
            caseBasicDataViewModel.CrimesTypes = lookupService.GetCrimeTypes(CurrentUser.CourtID).ToList();
            caseBasicDataViewModel.FirstYear = caseBasicDataViewModel.SecondYear = DateTime.Now.Year;
            caseBasicDataViewModel.AllMainCrimeType = lookupService.GetLookupsByCategory(LookupsCategories.Crimes);
            if (this.IsAuthenticatied && this.CurrentUser.CourtID == null)
            {
                caseBasicDataViewModel.Courts = lookupService.GetCourts().ToList();
            }
            else
            {
                caseBasicDataViewModel.CourtID = CurrentUser.CourtID.Value;
                //caseBasicDataViewModel.Courts = lookupService.GetCourts().Where(x => x.ID == CurrentUser.CourtID.Value).ToList();
                caseBasicDataViewModel.CourtName = lookupService.GetCourts().Where(x => x.ID == CurrentUser.CourtID.Value).Select(x => x.Name).Single();
               caseBasicDataViewModel.SecondLevelProsecutions = GetElementaryProsecutions(CurrentUser.CourtID.Value);
               
            }
            return caseBasicDataViewModel;

        }

        public ActionResult GetCourtCrimeTypes(int CourtID)
        {
            return CPartialView("CrimeDropDown", new CaseBasicDataViewModel
            {
                CrimesTypes = lookupService.GetCrimeTypes(CourtID).ToList()
            }

            );

        }
        public List<vw_KeyValue> GetElementaryProsecutions(int CourtID)
        {
            return lookupService.GetElementaryProsecutions(CourtID).ToList();

        }
        public ActionResult GetPartialProsecutions(int CourtID)
        {
            var Prosecutions = lookupService.GetElementaryProsecutions(CourtID).ToList();
            return Json(Prosecutions, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetIntialProsecution(int ProsecutionID)
        {
            var IntialProsecution = lookupService.GetIntialProsecutions(ProsecutionID).ToList();
            return Json(IntialProsecution, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPoliceStation(int ProsecutionID)
        {
            var PoliceStations = lookupService.GetPoliceStations(ProsecutionID).ToList();
            return Json(PoliceStations, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

}