using JIC.Base;
using JIC.Base.Views;
using JIC.Crime.View.Models;
using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIC.Crime.View.Controllers
{
    public class WitnessesController : ControllerBase
    {
        private IWitnessesService witnessrService;
        private ILookupService LookupService;

        public WitnessesController(IWitnessesService witnessrService, ILookupService LookupService)
        {
            this.witnessrService = witnessrService;
            this.LookupService = LookupService;
        }

        public WitnessViewModel PersonViewModel()
        {

            List<vw_KeyValue> PoliceStations = new List<vw_KeyValue>();
            return new WitnessViewModel
            {
                Cities = LookupService.GetAllCitites(),
                PoliceStations = LookupService.GetAllPoliceStations(),
                Nationalities = LookupService.GetLookupsByCategory(Base.LookupsCategories.Nationalities)
            };
        
          
        
        }


        // GET: Witnesses
        public ActionResult Index(int id)
        {
            if (CurrentUser != null)
            {

                ViewData["CaseID"] = id;
                try
                {
                    List<WitnessViewModel> Witnesses = witnessrService.GetWitnessesByCaseID(id)
                    .Select(pros => new WitnessViewModel
                    {
                        ID = pros.ID,
                        Name  = pros.Name,
                        NatNo   = pros.NationalID,
                        CaseID= id
                    }).ToList();
                    return View(Witnesses);
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

        public ActionResult Create(int id)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                ViewData["CaseID"] = id;
                WitnessViewModel WitnessCreate = PersonViewModel();
                WitnessCreate.CaseID = id;
                WitnessCreate.ID = 0;
                return PartialView(WitnessCreate);
            }
            else
            {
                ViewData["SessionEnded"] = true;

                return PartialView();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WitnessViewModel  WitnessModel)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                try
                {
                    if (ModelState.IsValid)
                    {
                       // int WitnessID;
                        Base.Views.vw_PersonData vw_Witness = new Base.Views.vw_PersonData()
                        {
                            ID = 0,
                            Name = WitnessModel.Name,
                            NatNo = WitnessModel.NatNo,
                            Job = WitnessModel.Job,
                            BirthDate = WitnessModel.GetBirthDate(),
                            NationalityID = WitnessModel.NationalityID,
                            PassportNo = WitnessModel.PassportNo,
                            address = (WitnessModel.address_address != null ? new Base.Views.vw_Address { address = WitnessModel.address_address, CityID = WitnessModel.address_CityID, PoliceStationID = WitnessModel.address_PoliceStationID } : null),
                            CleanFullName = Base.Utilities.RemoveSpaces(Base.Utilities.RemoveSpecialCharacters(WitnessModel.Name)),

                        };
                        var ResultAddWitness = witnessrService.AddNewWitness(vw_Witness,(int)WitnessModel.ID,0,null,CurrentUser.ID,(SystemUserTypes)CurrentUser.UserTypeID  );


                        if (ResultAddWitness == AddWitnessStatus.AddedSuccessfully )
                        {
                            //, "User", new { returnUrl = "/" }
                            return RedirectTo(Url.Action("Index", "Witnesses", new { id = (int)WitnessModel.ID })).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);
                            //  return RedirectJS(Url.Action("Index")).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);
                        }
                        else if (ResultAddWitness == AddWitnessStatus.FailedToAdd )
                        {
                            return RedirectTo(Url.Action("Index", "Witnesses", new { id = (int)WitnessModel.ID })).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);

                            //  return RedirectJS(Url.Action("Index")).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);

                        }
                        //else if (ResultAddProsecutor == ProsecutorStatus.ProsecuterHasSession)
                        //{
                        //    return CPartialView(PrepareViewModel(ProsecutorModel)).WithErrorMessages(JIC.Base.Resources.Messages.ProsecutorSessionExistEdite);

                        //}
                        //else if (ResultAddProsecutor == ProsecutorStatus.NationalNO_Exist_Before)
                        //
                        //    return CPartialView(PrepareViewModel(ProsecutorModel)).WithErrorMessages(JIC.Base.Resources.Messages.NatIDExist);

                        //}
                    }

                    return PartialView();

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



    }
}