using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JIC.Services.ServicesInterfaces;
using JIC.Crime.View.Models;
using JIC.Base.Views;
using System.Data;
using JIC.Base;
using JIC.Base.views;
using JIC.Crime.View.Helpers;
using System.IO;

namespace JIC.Crime.View.Controllers
{
    [CAuthorize(SystemUserTypes.JICAdmin)]
    public class LawyerController : ControllerBase
    {
       

        private ILawyerService LawyerService;
        private ILookupService LookupService;

        public LawyerController(ILawyerService lawyerService, ILookupService lookupService)
        {
            this.LawyerService = lawyerService;
            this.LookupService = lookupService;
        }

        private LawyerCreateViewModel PrepareViewModel(LawyerViewModels Lawyers = null)
        {
            if (Lawyers == null)
                Lawyers = new LawyerViewModels();
            return new LawyerCreateViewModel
            {
                //List < vw_KeyValue > 
                ListLawyerLevelModel = LookupService.GetLookupsByCategory(LookupsCategories.LawyerLevel)
             .Select(Pros => new vw_KeyValue
             {
                 ID = Pros.ID,
                 Name = Pros.Name,
             }).ToList(),
                LawyerModel = Lawyers,
            };

        }



        // GET: Lawyer
        public ActionResult Index()
        {
            if ( CurrentUser != null)
            {
                try
                {
                    List<LawyerViewModels> Lawyerss = LawyerService.GetLawyers()
                    .Select(pros => new LawyerViewModels
                    {
                        ID = pros.ID,
                        LawyerLevelName = pros.LawyerLevelName,
                        LawyerName = pros.LawyerName,
                        NationalID = pros.NationalID,
                        LawyerCardNumber = pros.LawyerCardNumber,
                        Address = pros.Address,
                        DateOfBirth = pros.DateOfBirth,
                        PersonID = pros.PersonID
                        
                    }).ToList();
                    LawyerCreateViewModel lawyerCreateView = new LawyerCreateViewModel();
                    lawyerCreateView.Lawyers = Lawyerss;
                    return View(lawyerCreateView); 
                }
                catch (Exception ex)
                {
                    return ErrorPage(ex);
                }
            }
            else
            {
                return RedirectTo(Url.Action("login", "User", new { returnUrl = "/" })).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");

            }

        }
        [HttpPost]
        public ActionResult Index(LawyerCreateViewModel lawyerCreateView)
        {
            if (CurrentUser != null)
            {
                try
                {
                    List<LawyerViewModels> Lawyerss = LawyerService.GetLawyers()
                       .Select(pros => new LawyerViewModels
                       {
                           ID = pros.ID,
                           LawyerLevelName = pros.LawyerLevelName,
                           LawyerName = pros.LawyerName,
                           NationalID = pros.NationalID,
                           LawyerCardNumber = pros.LawyerCardNumber,
                           Address = pros.Address,
                           DateOfBirth = pros.DateOfBirth,
                           PersonID = pros.PersonID

                       }).ToList();
                    lawyerCreateView.Lawyers = Lawyerss.Where(e => e.LawyerCardNumber == lawyerCreateView.LawyerModel.LawyerCardNumber).ToList() ;
                    return View(lawyerCreateView);
                }
                catch (Exception ex)
                {
                    return ErrorPage(ex);
                }
            }
            else
            {
                return RedirectTo(Url.Action("login", "User", new { returnUrl = "/" })).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");

            }

        }

        [HttpGet]
        public ActionResult Create()
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                LawyerCreateViewModel LawyerCreate = PrepareViewModel();
                return PartialView(LawyerCreate);
            }
            else
            {
                ViewData["SessionEnded"] = true;

                return PartialView();
            }

        }
        [HttpPost]
     //   [ValidateAntiForgeryToken]
        public ActionResult Create(LawyerViewModels LawyerModel /*, HttpPostedFileBase file*/)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                try
                {
                    if (ModelState.IsValid)
                    {
                        int LawyerId;
                        if (LawyerModel.NationalID != null)
                        {
                            string year = LawyerModel.NationalID.Substring(1, 2);
                            string month = LawyerModel.NationalID.Substring(3, 2);
                            string day = LawyerModel.NationalID.Substring(5, 2);
                            string first = LawyerModel.NationalID.Substring(0, 1);
                            if (first == "2")
                            {
                                year = "19" + year;
                            }
                            else
                            {
                                year = "20" + year;
                            }
                            DateTime dob = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
                            LawyerModel.DateOfBirth = dob;
                        }
                        byte[] FileDataa = null;
                        if (LawyerModel.LawyerFile != null && LawyerModel.LawyerFile.ContentLength > 0)
                        {
                            //FileDataa = null;
                            using (var binaryReader = new BinaryReader(LawyerModel.LawyerFile.InputStream))
                            {
                                FileDataa = binaryReader.ReadBytes(LawyerModel.LawyerFile.ContentLength);
                            }
                            
                        }
                        vw_LawyerData vw_LawyerData = new vw_LawyerData()
                        {
                            ID = LawyerModel.ID,
                            LawyerLevelID = LawyerModel.LawyerLevelID,
                            LawyerName = LawyerModel.LawyerName,
                            LawyerLevelName = LawyerModel.LawyerLevelName,
                            LawyerCardNumber = LawyerModel.LawyerCardNumber,
                            NationalID = LawyerModel.NationalID,
                           
                            Address = LawyerModel.Address,
                            DateOfBirth = LawyerModel.DateOfBirth,
                           //DateOfBirth = null,
                            PersonID = LawyerModel.PersonID,
                            LawyerFileData = FileDataa,

                        };
                      
                        var ResultAddLawyer = LawyerService.AddLawyer(vw_LawyerData, out LawyerId);


                        if (ResultAddLawyer == LawyerStatus.Succeeded)
                        {
                            return RedirectJS(Url.Action("Index")).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);
                        }
                        else if (ResultAddLawyer == LawyerStatus.Failed)
                        {
                            return CPartialView(PrepareViewModel(LawyerModel)).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);

                        }
                        
                        else if (ResultAddLawyer == LawyerStatus.CardNumber_Exist_Before)
                        {
                            return CPartialView(PrepareViewModel(LawyerModel)).WithErrorMessages(JIC.Base.Resources.Messages.exist);

                        }
                        else if (ResultAddLawyer == LawyerStatus.CardNumber_Exist_Before)
                        {
                            return CPartialView(PrepareViewModel(LawyerModel)).WithErrorMessages(JIC.Base.Resources.Messages.NatIDExist);

                        }

                    }

                    return PartialView(PrepareViewModel(LawyerModel));

                }
                catch (Exception ex)
                {
                    return ErrorPage(ex);
                }
            }
            else
            {
                // return RedirectTo(Url.Action("login", "User", new { returnUrl = "/" })).WithErrorMessages("تم الخروج بشكل تلقائى لعدم التفاعل اكثر من 15 دقيقة");

                ViewData["SessionEnded"] = true;
                return CPartialView();
            }
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (CurrentUser != null)
            {
                try
                {
                    vw_LawyerData Lawyer = LawyerService.GetLawyerByID(id);
                    LawyerViewModels LawyerView = new LawyerViewModels
                    {
                        ID = Lawyer.ID,
                        LawyerLevelID = Lawyer.LawyerLevelID,
                        LawyerName = Lawyer.LawyerName,
                        LawyerLevelName = Lawyer.LawyerLevelName,
                        LawyerCardNumber = Lawyer.LawyerCardNumber,
                        NationalID = Lawyer.NationalID,
                        Address = Lawyer.Address,
                        DateOfBirth = Lawyer.DateOfBirth,
                        PersonID = Lawyer.PersonID,
                        LawyerFileData = Lawyer.LawyerFileData,
                    };

                    ViewData["SessionEnded"] = false;
                    return PartialView(PrepareViewModel(LawyerView));
                }
                catch (Exception ex)
                {
                    return ErrorPage(ex);
                }
            }
            else
            {
                ViewData["SessionEnded"] = true;
                return PartialView();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(LawyerViewModels LawyerModel/*, HttpPostedFileBase file*/)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (LawyerModel.LawyerFile!=null)
                        {
                           
                            if (LawyerModel.LawyerFile != null && LawyerModel.LawyerFile.ContentLength > 0)
                            {
                                //FileDataa = null;
                                using (var binaryReader = new BinaryReader(LawyerModel.LawyerFile.InputStream))
                                {
                                    LawyerModel.LawyerFileData = binaryReader.ReadBytes(LawyerModel.LawyerFile.ContentLength);
                                }

                            }
                        }
                        //int LawyerId;
                        if (LawyerModel.NationalID != null)
                        {
                            string year = LawyerModel.NationalID.Substring(1, 2);
                            string month = LawyerModel.NationalID.Substring(3, 2);
                            string day = LawyerModel.NationalID.Substring(5, 2);
                            string first = LawyerModel.NationalID.Substring(0, 1);
                            if (first == "2")
                            {
                                year = "19" + year;
                            }
                            else
                            {
                                year = "20" + year;
                            }
                            DateTime dob = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
                            LawyerModel.DateOfBirth = dob;
                        }

                       
                        vw_LawyerData vw_LawyerData = new vw_LawyerData()
                        {
                            ID = LawyerModel.ID,
                            LawyerLevelID = LawyerModel.LawyerLevelID,
                            LawyerName = LawyerModel.LawyerName,
                            LawyerLevelName = LawyerModel.LawyerLevelName,
                            LawyerCardNumber = LawyerModel.LawyerCardNumber,
                            NationalID = LawyerModel.NationalID,
                            Address = LawyerModel.Address,
                            DateOfBirth = LawyerModel.DateOfBirth,
                            PersonID = LawyerModel.PersonID,
                            LawyerFileData = LawyerModel.LawyerFileData,
                        };

                        var LawyerResult = LawyerService.EditLawyer(vw_LawyerData);

                        if (LawyerResult == LawyerStatus.Succeeded)
                        {
                            return RedirectJS(Url.Action("Index")).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);
                        }
                        else if (LawyerResult == LawyerStatus.Failed)
                        {
                            return CPartialView(PrepareViewModel(LawyerModel)).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);

                        }
                        
                        else if (LawyerResult == LawyerStatus.CardNumber_Exist_Before)
                        {
                            return CPartialView(PrepareViewModel(LawyerModel)).WithErrorMessages(JIC.Base.Resources.Messages.NatIDExist);

                        }

                    }

                    return PartialView(PrepareViewModel(LawyerModel));
                }
                catch (Exception ex)
                {
                    return ErrorPage(ex);
                }
            }
            else
            {
                ViewData["SessionEnded"] = true;
                return PartialView();
            }
        }

        [HttpGet]
        public ActionResult GetValidationNatNo(string NatNo)
        {
            bool validation = false ;
            if (NatNo != "" && NatNo.Length == 14)
            {
                long result = 0;
                result = (result
                         + (int.Parse(NatNo.Substring(0, 1)) * 2));
                result = (result
                            + (int.Parse(NatNo.Substring(1, 1)) * 7));
                result = (result
                            + (int.Parse(NatNo.Substring(2, 1)) * 6));
                result = (result
                            + (int.Parse(NatNo.Substring(3, 1)) * 5));
                result = (result
                            + (int.Parse(NatNo.Substring(4, 1)) * 4));
                result = (result
                            + (int.Parse(NatNo.Substring(5, 1)) * 3));
                result = (result
                            + (int.Parse(NatNo.Substring(6, 1)) * 2));
                result = (result
                            + (int.Parse(NatNo.Substring(7, 1)) * 7));
                result = (result
                            + (int.Parse(NatNo.Substring(8, 1)) * 6));
                result = (result
                            + (int.Parse(NatNo.Substring(9, 1)) * 5));
                result = (result
                            + (int.Parse(NatNo.Substring(10, 1)) * 4));
                result = (result
                            + (int.Parse(NatNo.Substring(11, 1)) * 3));
                result = (result
                            + (int.Parse(NatNo.Substring(12, 1)) * 2));
                result = (result % 11);
                result = (11 - result);
                if ((result > 9))
                {
                    result = (result % 10);
                }

                if (result == int.Parse(NatNo.Substring(13, 1)))
                {
                    validation = true;
                    return Json(validation, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    validation = false;
                    return Json(validation, JsonRequestBehavior.AllowGet);

                }
            }
            return Json(validation, JsonRequestBehavior.AllowGet);
        }

    }
}