using JIC.Base;
using JIC.Base.Views;
using JIC.Crime.View.Helpers;
using JIC.Crime.View.Models;
using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIC.Crime.View.Controllers
{
  //  [CAuthorize(SystemUserTypes.ElementaryCourtAdministrator, SystemUserTypes.Judge)]
    public class PersonInformationController : ControllerBase
    {
          private IPersonInformationService PersonInfoService;
          public PersonInformationController(IPersonInformationService PersonInformationService)
        {
           this.PersonInfoService = PersonInformationService;
        }
        // GET: PersonInformation
       
        [HttpGet]
        public ActionResult index(PersonInformatViewModel personInformat= null)
        {
            PersonInformatViewModel person = new PersonInformatViewModel();
            
            if (personInformat.Name != null && personInformat.NatNo !=null)
            {
                vw_InformationPerson info = new vw_InformationPerson();
                info = PersonInfoService.GetInformationPerson(personInformat.NatNo, personInformat.Name);
                if (info != null)
                {
                    person.address = info.address;
                    person.Age = CalculateAge(DateTime.Parse(info.BirthDate)).ToString();
                    person.BirthDate = info.BirthDate;
                    person.CaseName = info.CaseName;
                    person.CasesList = info.CasesList.Select(cas => new vw_casesViewModel()
                    {
                        CaseName = cas.CaseName,
                        OverallNumber = cas.OverallNumber,
                        Status = cas.Status
                    }).ToList();
                    person.CircuitName = info.CircuitName;
                    person.Cities = info.Cities;
                    person.Job = info.Job;
                    person.PersonName = info.Name;
                    person.Nationalities = info.Nationalities;
                    person.NationalNo = info.NatNo;
                    person.OverallNumber = info.OverallNumber;
                    person.PassportNo = info.PassportNo;
                    person.PhoneNo = info.PhoneNo;
                    person.PoliceStations = info.PoliceStations;
                    person.Status = info.Status;
                    person.UserTypes = info.UserTypes;
                }
                else
                {
                    return CPartialView(person).WithErrorMessages("لا يوجد شخص");
                }

                } 
            return View(person);
        }

            
        
        [HttpGet]
        public ActionResult create()
        {
            PersonInformatViewModel personInformatViewModel = new PersonInformatViewModel();
            return View(personInformatViewModel);
        }
        [HttpPost]
        public ActionResult create(PersonInformatViewModel personInformat)
        {

            return RedirectJS(Url.Action("Index", personInformat));
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