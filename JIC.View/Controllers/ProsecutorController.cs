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

namespace JIC.Crime.View.Controllers
{
   [CAuthorize(SystemUserTypes.CriminalDepManager, SystemUserTypes.ElementaryCourtAdministrator)]
    public class ProsecutorController : ControllerBase
    {
        private IProsecutorService prosecutorService;
       private ILookupService LookupService;
        public ProsecutorController(IProsecutorService prosecutorService,ILookupService LookupService)
        {
            this.prosecutorService = prosecutorService;
            this.LookupService = LookupService;
        }
        private ProsecutorCreateViewModel PrepareViewModel(ProsecutorViewModels Prosecutions = null)
        {
            if (Prosecutions == null)
                Prosecutions = new ProsecutorViewModels();
            return new ProsecutorCreateViewModel
            {
                ListProsecutionModel = LookupService.GetProsecutions(IsAuthenticatied ? CurrentUser.CourtID : null)
             .Select(Pros => new ProsecutionViewModels
             {
                 ID = Pros.ID,
                 ProsecutionName = Pros.Name,
             }).ToList(),
                ProsecutorModel = Prosecutions,
            };

        }
        // GET: Prosecutor
        public ActionResult Index()
        {
            if (CurrentUser != null)
            { 
            try
            {
                List<ProsecutorViewModels> Prosecutors = prosecutorService.GetProsecutor(IsAuthenticatied ? CurrentUser.CourtID : null)
                .Select(pros => new ProsecutorViewModels
                {
                    ID = pros.ID,
                    ProsecutionName = pros.ProsecutionName,
                    ProcecutoerName = pros.ProcecutoerName,
                    NationalID = pros.NationalID
                }).ToList();
                return View(Prosecutors);
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
                ProsecutorCreateViewModel ProsecutorCreate = PrepareViewModel();
                return PartialView(ProsecutorCreate);
            }
            else
            {
                ViewData["SessionEnded"] = true;
              
                return PartialView();
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProsecutorViewModels ProsecutorModel)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                try
                {
                    if (ModelState.IsValid)
                    {
                        int ProsecutorID;
                        vw_ProcecuterData vw_ProcecuterData = new vw_ProcecuterData()
                        {
                            ID = ProsecutorModel.ID,
                            NationalID = ProsecutorModel.NationalID,
                            ProcecutionID = ProsecutorModel.ProcecutionID,
                            ProcecutoerName = ProsecutorModel.ProcecutoerName,
                            ProsecutionName = ProsecutorModel.ProsecutionName,
                        };
                        var ResultAddProsecutor = prosecutorService.AddProsecutor(vw_ProcecuterData, out ProsecutorID);


                        if (ResultAddProsecutor == ProsecutorStatus.Succeeded)
                        {
                            return RedirectJS(Url.Action("Index")).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);
                        }
                        else if (ResultAddProsecutor == ProsecutorStatus.Failed)
                        {
                            return CPartialView(PrepareViewModel(ProsecutorModel)).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);

                        }
                        else if (ResultAddProsecutor == ProsecutorStatus.ProsecuterHasSession)
                        {
                            return CPartialView(PrepareViewModel(ProsecutorModel)).WithErrorMessages(JIC.Base.Resources.Messages.ProsecutorSessionExistEdite);

                        }
                        else if (ResultAddProsecutor == ProsecutorStatus.NationalNO_Exist_Before)
                        {
                            return CPartialView(PrepareViewModel(ProsecutorModel)).WithErrorMessages(JIC.Base.Resources.Messages.NatIDExist);

                        }
                    }
                  
                    return PartialView(PrepareViewModel(ProsecutorModel));

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
                    vw_ProcecuterData Prosecutor = prosecutorService.GetProsecutorByID(id);
                    ProsecutorViewModels ProsecutorView = new ProsecutorViewModels
                    {
                        ID = Prosecutor.ID,
                        ProcecutionID = Prosecutor.ProcecutionID,
                        ProcecutoerName = Prosecutor.ProcecutoerName,
                        NationalID = Prosecutor.NationalID,
                    };

                    ViewData["SessionEnded"] = false;
                    return PartialView(PrepareViewModel(ProsecutorView));
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

        public ActionResult Edit(ProsecutorViewModels ProsecutorModel)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                try
                {
                    if (ModelState.IsValid)
                    {
                        vw_ProcecuterData vw_ProcecuterData = new vw_ProcecuterData()
                        {
                            ID = ProsecutorModel.ID,
                            NationalID = ProsecutorModel.NationalID,
                            ProcecutionID = ProsecutorModel.ProcecutionID,
                            ProcecutoerName = ProsecutorModel.ProcecutoerName,
                            ProsecutionName = ProsecutorModel.ProsecutionName,
                        };

                        var prosecutorResult = prosecutorService.EditProsecutor(vw_ProcecuterData);

                        if (prosecutorResult == ProsecutorStatus.Succeeded)
                        {
                            return RedirectJS(Url.Action("Index")).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);
                        }
                        else if (prosecutorResult == ProsecutorStatus.Failed)
                        {
                            return CPartialView(PrepareViewModel(ProsecutorModel)).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);

                        }
                        else if (prosecutorResult == ProsecutorStatus.ProsecuterHasSession)
                        {
                            return CPartialView(PrepareViewModel(ProsecutorModel)).WithErrorMessages(JIC.Base.Resources.Messages.ProsecutorSessionExistEdite);

                        }
                        else if (prosecutorResult == ProsecutorStatus.NationalNO_Exist_Before)
                        {
                            return CPartialView(PrepareViewModel(ProsecutorModel)).WithErrorMessages(JIC.Base.Resources.Messages.NatIDExist);

                        }

                    }

                    return PartialView(PrepareViewModel(ProsecutorModel));
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
        public ActionResult Delete(int? id)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                try
                {
                    vw_ProcecuterData Prosecutor = prosecutorService.GetProsecutorByID(id);

                    ProsecutorViewModels ProsecutorView = new ProsecutorViewModels
                    {
                        ID = Prosecutor.ID,

                        ProsecutionName = Prosecutor.ProsecutionName,
                        ProcecutoerName = Prosecutor.ProcecutoerName,
                        NationalID = Prosecutor.NationalID,
                    };


                    return PartialView(ProsecutorView);
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

        public ActionResult Delete(ProsecutorViewModels ProsecutorModel)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                try
                {
                    if (ModelState.IsValid)
                    {
                        vw_ProcecuterData ProsecutorToGetPersonID = new vw_ProcecuterData();
                        ProsecutorToGetPersonID = prosecutorService.GetProsecutorByID(ProsecutorModel.ID);
                        var prosecutorResult = prosecutorService.DeleteProsecutor(ProsecutorModel.ID);
                        if (prosecutorResult == ProsecutorStatus.Succeeded)
                        {
                            return RedirectJS(Url.Action("Index")).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);
                        }

                        else if (prosecutorResult == ProsecutorStatus.Failed)
                        {
                            return CPartialView(ProsecutorModel).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);

                        }
                        else if (prosecutorResult == ProsecutorStatus.ProsecuterHasSession)
                        {
                            return CPartialView(ProsecutorModel).WithErrorMessages(JIC.Base.Resources.Messages.ProsecutorSessionExistDelete);

                        }
                    }
                    return PartialView(ProsecutorModel);
                }
                catch (Exception ex)
                {
                    return ErrorPage(ex);
                }
            }
            else
            {
                ViewData["SessionEnded"] = false;
                return PartialView();
            }
        }
        
    }
}