using JIC.Services.ServicesInterfaces;
using JIC.Crime.View.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace JIC.Crime.View.Controllers
{
    public class PersonController : ControllerBase
    {
        public IPersonService PersonService;
        private ILookupService LookupService;
        public PersonController(IPersonService PersonService,ILookupService lookupService)
        {
            this.PersonService = PersonService;
            this.LookupService = lookupService;
        }
        // GET: Person
        public ActionResult ViewPerson(PersonViewModel personViewModel)
        {
            return PartialView();
        }
        
        public ActionResult GetPersons(string id)
        {
            List<Base.Views.vw_PersonData> PersonData = PersonService.GetPersons(id);
            return Json(PersonData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetPersonDetails(string NatID, vw_PersonData PersonData = null, [Bind(Prefix = "Edit.PersonData")]vw_PersonData EditPersonData = null, bool enableEdit = false)
        {
            if (Request.Form.AllKeys.Any(key => key.StartsWith("Edit.")))
            {
                PersonData = EditPersonData;
                ViewData.TemplateInfo.HtmlFieldPrefix = "Edit";
            }
            ModelState.Clear();
            var DBPersonData = PersonService.GetPersons(NatID).FirstOrDefault();
            if (DBPersonData != null)
            {
                if (DBPersonData.BirthDate != null)
                {
                    DBPersonData.Age = CalculateAge(DBPersonData.BirthDate.Value);
                }
                PersonData = vw_PersonData.Map(DBPersonData);
                //enableEdit = false;
            }
            else
                PersonData.ID = 0;
            if (Request.Form.AllKeys.Any(key => key.StartsWith("Edit.")))
            {
                if (PersonService.NumberOfAttachedCases(PersonData.ID) <= 1)
                    enableEdit = true;
            }
            else if (PersonService.NumberOfAttachedCases(PersonData.ID) == 0)
            {
                enableEdit = true;
            }
            if (DBPersonData != null)
            {
                enableEdit = false;
            }
           var PersonViewModel = new PersonViewModel(PersonData, LookupService, PersonService, PersonData.ID != 0 ? Base.Modes.Update : Base.Modes.Add);
            if (DBPersonData==null) {

                vw_PersonData persondata = new vw_PersonData();
                 PersonViewModel = new PersonViewModel(persondata, LookupService, PersonService, PersonData.ID != 0 ? Base.Modes.Update : Base.Modes.Add);

            }
           
                // PersonViewModel.PersonData.Age = CalculateAge(PersonData.BirthDateT.Value);
            PersonViewModel.IsLocked = !enableEdit;
            string[] li;
            if (PersonViewModel.PersonData.address_address != null)
            {
                li = Regex.Split(PersonViewModel.PersonData.address_address.ToString(), "/");
                 
                    if (li[0] != null)
                        PersonViewModel.PersonData.address_address = li[0];
                    if (li[1] != null)
                        PersonViewModel.PersonData.address_CityID = int.Parse(li[1]);
                    if (li[2] != null)
                        PersonViewModel.PersonData.address_PoliceStationID = int.Parse(li[2]);
                 
            }
           
            return PartialView("Person/_ViewPersonDetail", PersonViewModel);
        }

        public ActionResult ViewPersonData(vw_PersonData personViewModel)
        {
            //if(TempData["CRenderActionModelState"] != null)
            //{
            //    ModelStateDictionary TempModelData = (ModelStateDictionary)TempData["CRenderActionModelState"];
            //    for (int index = 0; index < TempModelData.Values.Count; index++)
            //    {
            //        var key = TempModelData.Keys.ToArray()[index];
            //        foreach (var error in TempModelData.Values.ToArray()[index].Errors)
            //        {
            //            ModelState.AddModelError(key, error.ErrorMessage);
            //        }
            //    }
            //}
            return CPartialView("Person/_ViewPerson", PrepareViewModel(personViewModel));
        }
        public PersonViewModel PrepareViewModel(vw_PersonData personData,bool clearModelState = false)
        {
            bool enableEdit = false;
            if (Request.Form.AllKeys.Any(key => key.StartsWith("Edit.")))
            {
                ViewData.TemplateInfo.HtmlFieldPrefix = "Edit";
            }
            if(clearModelState)
                ModelState.Clear();

            if (Request.Form.AllKeys.Any(key => key.StartsWith("Edit.")))
            {
                if (PersonService.NumberOfAttachedCases(personData.ID) <= 1)
                    enableEdit = true;
            }
            else if (PersonService.NumberOfAttachedCases(personData.ID) == 0)
            {
                enableEdit = true;
            }
            var PersonViewModel = new PersonViewModel(personData, LookupService, PersonService, personData.ID != 0 ? Base.Modes.Update : Base.Modes.Add);
            PersonViewModel.IsLocked = !enableEdit;
            return PersonViewModel;
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
    }
}