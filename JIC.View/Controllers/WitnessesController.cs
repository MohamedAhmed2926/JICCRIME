using JIC.Base;
using JIC.Base.Views;
using JIC.Crime.View.Models;
using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace JIC.Crime.View.Controllers
{
    public class WitnessesController : ControllerBase
    {
        private IWitnessesService WitnessService;
        private ILookupService LookupService;
        private IPersonService PersonService;

        public WitnessesController(IWitnessesService witnessrService, ILookupService LookupService,IPersonService PersonService)
        {
            this.WitnessService = witnessrService;
            this.LookupService = LookupService;
            this.PersonService = PersonService;
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


        public JsonResult GetPersonData(string NatID)
        {
           RetrievedPersonData P = new RetrievedPersonData();
            Base.Views.vw_PersonData PD = PersonService.GetPersons(NatID).FirstOrDefault();
            if (PD != null)
            {
                P.ID = PD.ID;
                P.Name = PD.Name;
                P.NatNo = PD.NatNo;
                P.Job = PD.Job;
                P.BirthDate = PD.BirthDate.Value.ToShortDateString();
                P.NationalityID = PD.NationalityID;
                P.PassportNo = PD.PassportNo;
                P.CityID = PD.address.CityID;
                P.Address = PD.address.address;
                P.PoliceStationID = PD.address.PoliceStationID;

                string[] li = Regex.Split(PD.address.address.ToString(), "/");
                if (li[0] != null)
                    P.Address = li[0];
                if (li[1] != null)
                   P.CityID = int.Parse(li[1]);
                if (li[2] != null)
                    P.PoliceStationID = int.Parse(li[2]);


                //P.address = (PD.address_address != null ? new Base.Views.vw_Address { address = PD.address_address; CityID = PD.address_CityID; PoliceStationID = PD.address_PoliceStationID } : null);
                P.CleanFullName = Base.Utilities.RemoveSpaces(Base.Utilities.RemoveSpecialCharacters(PD.Name));

            }

            return Json(P, JsonRequestBehavior.AllowGet);
        }


        // GET: Witnesses
        public ActionResult Index(int id)
        {
            if (CurrentUser != null)
            {

                ViewData["CaseID"] = id;
                try
                {
                    List<WitnessViewModel> Witnesses = WitnessService.GetWitnessesByCaseID(id)
                    .Select(pros => new WitnessViewModel
                    {
                        ID = pros.ID,
                        Name  = pros.Name,
                        NatNo   = pros.NationalID,
                      
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

     //   [ChildActionOnly]
        public ActionResult Create(int id)
        {
            if (CurrentUser != null)
            {
                ViewData["SessionEnded"] = false;
                ViewData["CaseID"] = id;
                WitnessViewModel WitnessCreate = PersonViewModel();
                //WitnessCreate.PersonID = id;
                //WitnessCreate.ID = 0;
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
        public ActionResult Create(WitnessViewModel  WitnessModel,HttpPostedFileBase file)
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
                            ID = WitnessModel.PersonID,
                            Name = WitnessModel.Name,
                            NatNo = WitnessModel.NatNo,
                            Job = WitnessModel.Job,
                            BirthDate = WitnessModel.GetBirthDate(),
                            NationalityID = WitnessModel.NationalityID,
                            PassportNo = WitnessModel.PassportNo,
                            address = (WitnessModel.address_address != null ? new Base.Views.vw_Address { address = WitnessModel.address_address, CityID = WitnessModel.address_CityID, PoliceStationID = WitnessModel.address_PoliceStationID } : null),
                            CleanFullName = Base.Utilities.RemoveSpaces(Base.Utilities.RemoveSpecialCharacters(WitnessModel.Name)),
                    
                        };
                        byte[] FileData = null;
                        if (file != null && file.ContentLength > 0)
                        {
                            FileData = null;
                            using (var binaryReader = new BinaryReader(file.InputStream))
                            {
                                FileData = binaryReader.ReadBytes(file.ContentLength);
                            }
                        }
                         
                       // = new BinaryReader(WitnessModel.filePath.InputStream);//System.IO.File.ReadAllBytes(WitnessModel.filePath.);
                        var ResultAddWitness = WitnessService.AddNewWitness(vw_Witness,(int)WitnessModel.ID,0, FileData, CurrentUser.ID,(SystemUserTypes)CurrentUser.UserTypeID  );


                        if (ResultAddWitness == AddWitnessStatus.AddedSuccessfully )
                        {
                            //, "User", new { returnUrl = "/" }
                            return RedirectTo(Url.Action("Index", "Witnesses", new { id = (int)WitnessModel.ID })).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);
                           }
                        else if (ResultAddWitness == AddWitnessStatus.FailedToAdd )
                        {
                            return RedirectTo(Url.Action("Index", "Witnesses", new { id = (int)WitnessModel.ID })).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);
                        }
                        else if (ResultAddWitness == AddWitnessStatus.SavedBefore )
                        {
                            return RedirectTo(Url.Action("Index", "Witnesses", new { id = (int)WitnessModel.ID })).WithErrorMessages(JIC.Base.Resources.Messages.WitnessSavedBefore);
                        }
                        else if (ResultAddWitness == AddWitnessStatus.IsDefendant )
                        {
                            return RedirectTo(Url.Action("Index", "Witnesses", new { id = (int)WitnessModel.ID })).WithErrorMessages(JIC.Base.Resources.Messages.PartyAlreadyExists);
                        }
                        else if (ResultAddWitness == AddWitnessStatus.IsVictim)
                        {
                            return RedirectTo(Url.Action("Index", "Witnesses", new { id = (int)WitnessModel.ID })).WithErrorMessages(JIC.Base.Resources.Messages.PartyAlreadyExists);
                        }
                        else if (ResultAddWitness == AddWitnessStatus.IsLawyer )
                        {
                            return RedirectTo(Url.Action("Index", "Witnesses", new { id = (int)WitnessModel.ID })).WithErrorMessages(JIC.Base.Resources.Messages.PersonIsALawyer);
                        }
                      

                    }
                   // WitnessViewModel WitnessCreate = PersonViewModel();
                    return RedirectTo(Url.Action("Index", "Witnesses", new { id = (int)WitnessModel.ID })).WithSuccessMessages(JIC.Base.Resources.Messages.OperationNotCompleted);


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