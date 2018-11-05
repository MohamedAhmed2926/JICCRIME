using JIC.Base;
using JIC.Services.ServicesInterfaces;
using JIC.Base.Views;
using JIC.Crime.View.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using JIC.Crime.View.Helpers;

namespace JIC.Crime.View.Controllers
{
    [CAuthorize(SystemUserTypes.schedualEmployee, SystemUserTypes.ElementaryCourtAdministrator)]

    public class CasePartiesController : ControllerBase
    {
        private ICrimeCaseService CaseService;
        private IDefectsService PartiesService;
        public ILookupService LookupService;
        public IPersonService PersonService;

        public CasePartiesController(IDefectsService PartiesService,ICrimeCaseService CaseService, ILookupService LookupService,IPersonService PersonService)
        {
            this.PartiesService = PartiesService;
            this.CaseService = CaseService;
            this.LookupService = LookupService;
            this.PersonService = PersonService;
           
        }

        // GET: CaseParties
        public ActionResult Index(int CaseID)
        {
            return View(CaseID);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(List<AtendanceViewModel> attendance,int CaseID)
        {
            CasePartiesViewModel fullCasePartiesView = new CasePartiesViewModel
            {
                CaseID = attendance.Select(e => e.CaseID).First(),
                CaseParties= PartiesService.GetDefectsByCaseID(CaseID).Select(party => CasePartyViewModels.Map(party)).ToList()
           };

            foreach (var defendent in attendance.Where(e => e.DefectType == PartyTypes.Defendant))
            {
                var obj = fullCasePartiesView.CaseParties.Single(e => e.id == defendent.DefectID && e.PartyType == PartyTypes.Defendant);
                obj.PartiesOrder = defendent.OrderID;
            }
          
            foreach (var defendent in attendance.Where(e => e.DefectType == PartyTypes.Victim))
            {
                var obj = fullCasePartiesView.CaseParties.Single(e => e.id == defendent.DefectID && e.PartyType == PartyTypes.Victim);
                obj.PartiesOrder = defendent.OrderID;
            }
            List<vw_CaseDefectData> CaseDefectData = fullCasePartiesView.CaseParties
                .Select(defect => new vw_CaseDefectData
                {
                    Order = defect.PartiesOrder,
                    ID = defect.id,
                    DefectType=defect.PartyType

                }).ToList();
              var SaveDefects = PartiesService.SaveOrder(CaseDefectData);
            if (SaveDefects ==SavePartSOrder.SavedOrder)
            {
                return CPartialView("_OrderForm", CaseID).WithSuccessMessages("تم حفظ الترتيب");

            }
            else
            {
                return CPartialView("_OrderForm", CaseID).WithErrorMessages("لم يتم حفظ الترتيب");

            }
           
        }
        
        public ActionResult EditCaseParties(int CaseID)
        {
            CasePartiesViewModel fullCasePartiesView = new CasePartiesViewModel
            {
                CaseID = CaseID,
           CaseParties = PartiesService.GetDefectsByCaseID(CaseID).Select(party => CasePartyViewModels.Map(party)).ToList()

            };
            List<CasePartyViewModels> list = new List<CasePartyViewModels>();

            List<CasePartyViewModels> listDefendent = new List<CasePartyViewModels>();
            List<CasePartyViewModels> listVictim = new List<CasePartyViewModels>();
            listDefendent= fullCasePartiesView.CaseParties.Where(e => e.PartyType == PartyTypes.Defendant).OrderBy(e => e.PartiesOrder).ToList();
            listVictim = fullCasePartiesView.CaseParties.Where(e => e.PartyType == PartyTypes.Victim).OrderBy(e => e.PartiesOrder).ToList();
          
            list.AddRange(listDefendent);
            list.AddRange(listVictim);
            fullCasePartiesView.CaseParties = list;
            foreach (var parties in fullCasePartiesView.CaseParties.Where(e=>e.PartyType==PartyTypes.Defendant))
            {
                if (parties.DefendantStatus == 20)
                {
                    parties.Status = JIC.Base.Resources.Resources.Fugitive;
                }
                else if (parties.DefendantStatus == 19)
                    parties.Status = JIC.Base.Resources.Resources.Arrested;
                else if (parties.DefendantStatus == 21)
                    parties.Status = JIC.Base.Resources.Resources.UnWanted;

            }
           // fullCasePartiesView.CaseParties =PartiesService.GetDefectsByCaseID(CaseID).Select(party => CasePartyViewModels.Map(party)).OrderBy(e => e.PartiesOrder).ToList();

            // fullCasePartiesView.CaseParties= PartiesService.GetDefectsByCaseID(CaseID).Select(party => CasePartyViewModels.Map(party)).OrderBy(e=>e.PartiesOrder).ToList();

            return CPartialView("_EditCasePartiesGrid", fullCasePartiesView);
        }

        public ActionResult Create(int CaseID)
        {
            return CPartialView(PrepareCasePartiesViewModel(CaseID));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(int CaseID, Models.vw_PersonData PersonData, CasePartyViewModels CasePartyViewModels)
        {
            using (var Transaction = DataContext.Database.BeginTransaction())
            {
                if (ModelState.IsValid)
                {
                    long PersonID;
                    PersonStatus AddPerson;
                    if (PersonData.ID == 0)
                    {
                        AddPerson = PersonService.AddPerson(new Base.Views.vw_PersonData
                        {
                            ID = PersonData.ID,
                            Job = PersonData.Job,
                            BirthDate = PersonData.GetBirthDate(),
                            Name = PersonData.Name,
                            NationalityID = PersonData.NationalityID,
                            NatNo = PersonData.NatNo,
                            PassportNo = PersonData.PassportNo,
                            address = (PersonData.address_address != null ? new Base.Views.vw_Address { address = PersonData.address_address, CityID = PersonData.address_CityID, PoliceStationID = PersonData.address_PoliceStationID } : null),
                            CleanFullName = Base.Utilities.RemoveSpaces(Base.Utilities.RemoveSpecialCharacters(PersonData.Name)),
                        }, out PersonID);
                    }
                    else
                    {
                        PersonID = PersonData.ID;
                        AddPerson = PersonStatus.SuccefullSave;
                    }
                    if(PartiesService.IsPersonInCase(PersonID, CaseID))
                    {
                        return CPartialView(PrepareCasePartiesViewModel(CaseID, PersonData, CasePartyViewModels)).WithErrorMessages("الخصم مسجل فى القضيه من قبل");
                    }
                    else if (AddPerson == Base.PersonStatus.SuccefullSave)
                    {
                        var AddStatus = PartiesService.AddCaseDefect(new vw_CaseDefectData
                        {
                            CaseID = CaseID,
                            Crimes = CasePartyViewModels.CrimeTypes,
                            DefectType = CasePartyViewModels.PartyType,
                            IsCivilRightProsecutor = CasePartyViewModels.IsCivilRightProsecutor,
                            PersonID = PersonID,
                            DefendantStatus = CasePartyViewModels.DefendantStatus ?? 0,
                        });
                        switch (AddStatus)
                        {
                            case Base.SaveDefectsStatus.Saved:
                                Transaction.Commit();
                                ViewBag.SavedCreate = true;
                                //    JavaScript("$(document).trigger('Parties:SavedSuccefull')");
                                return CPartialView(PrepareCasePartiesViewModel(CaseID, PersonData, CasePartyViewModels)).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);
                        }
                    }
                }
                return CPartialView(PrepareCasePartiesViewModel(CaseID, PersonData, CasePartyViewModels));
            }
        }
        public ActionResult Edit(int CaseID, long PartyID, int PartyType)
        {
            if (CurrentUser != null)
            {
                vw_CaseDefectsData CaseParty = PartiesService.GetCaseDefect(CaseID, PartyID, (PartyTypes)PartyType);
                var ViewModel = PrepareCasePartiesViewModel(CaseID, Models.vw_PersonData.Map(CaseParty), CasePartyViewModels.Map(CaseParty));

                string[] li = Regex.Split(ViewModel.PersonData.PersonData.address_address.ToString(), "/");
                if (li[0] != null)
                    ViewModel.PersonData.PersonData.address_address = li[0];
                if (li[1] != null)
                    ViewModel.PersonData.PersonData.address_CityID = int.Parse(li[1]);
                if (li[2] != null)
                    ViewModel.PersonData.PersonData.address_PoliceStationID = int.Parse(li[2]);
                ViewModel.PersonData.PersonData.Age = CalculateAge(CaseParty.Birthdate.Value);
                ////لايمكن التعديل اذا تحركت القضية فى flow  الا بموافقة مدير النظام
                if (PartiesService.CaseInFlow(CaseID) == CaseStatus.GoInFlow)
                {
                    ViewModel.CaseInFlow = CaseStatus.GoInFlow;
                }
                if (CurrentUser.UserTypeID == (int)SystemUserTypes.ElementaryCourtAdministrator)
                {
                    ViewModel.CaseInFlow = CaseStatus.NotGoInFlow;
                }
                ViewData["SessionEnded"] = true;
                return CPartialView(ViewModel);
            }
            else
            {
                ViewData["SessionEnded"] = true;
                return CPartialView();
            }
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int CaseID, [Bind(Prefix = "PersonData")] Models.vw_PersonData PersonData, [Bind(Prefix = "Edit.CasePartyViewModels")] CasePartyViewModels CasePartyViewModels)
        {
            if (!CasePartyViewModels.OriginalPartyType.HasValue)
                ModelState.AddModelError("OriginalPartyType", JIC.Base.Resources.Messages.RequiredErrorMessage);
            if (!ModelState.IsValid)
                return CPartialView(PrepareCasePartiesViewModel(CaseID, PersonData, CasePartyViewModels));

            if (CasePartyViewModels.PartyType != CasePartyViewModels.OriginalPartyType)
            {
                using (var Transaction = this.DataContext.Database.BeginTransaction())
                {
                    var PartyData = PartiesService.GetCaseDefect(CaseID, CasePartyViewModels.id, CasePartyViewModels.OriginalPartyType);
                    var DeleteStatus = PartiesService.DeleteCaseDefect(CaseID, CasePartyViewModels.id, CasePartyViewModels.OriginalPartyType);
                    vw_CaseDefectData defect =new vw_CaseDefectData()
                    {    CaseID=CaseID,
                        ID = CasePartyViewModels.id,
                        IsCivilRightProsecutor = CasePartyViewModels.IsCivilRightProsecutor,
                        //NationalID = this.NationalID,
                        Order = CasePartyViewModels.PartiesOrder,
                        //Name = PartyName,
                        DefectType = CasePartyViewModels.PartyType,
                        Crimes = CasePartyViewModels.CrimeTypes,
                        DefendantStatus = CasePartyViewModels.DefendantStatus
                    };
                    var AddStatus = PartiesService.AddCaseDefect(new vw_CaseDefectData
                    {
                        CaseID = CaseID,
                        Crimes = CasePartyViewModels.CrimeTypes,
                        DefectType = CasePartyViewModels.PartyType,
                        IsCivilRightProsecutor = CasePartyViewModels.IsCivilRightProsecutor,
                        PersonID = PartyData.PersonID,
                        DefendantStatus = CasePartyViewModels.DefendantStatus ?? 0,
                    });
                    //var AddStatus = PartiesService.AddCaseDefect(defect);
                    if (DeleteStatus == DeleteDefectStatus.Deleted && AddStatus == SaveDefectsStatus.Saved)
                        Transaction.Commit();
                    ViewBag.SavedEdite = true;
                    return CPartialView(PrepareCasePartiesViewModel(CaseID, PersonData, CasePartyViewModels)).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);

                }
            }
            else
            {

                using (var Transaction = this.DataContext.Database.BeginTransaction())
                {
                    //Get PartyOriginalData
                    var CasePartyData = PartiesService.GetCaseDefect(CaseID, CasePartyViewModels.id, CasePartyViewModels.PartyType);

                    //UpdatePersonData
                    Base.Views.vw_PersonData personData = PersonData.ToPersonData();
                    personData.ID = CasePartyData.PersonID;
                    var PersonStatus = PersonService.EditPerson(personData);

                    //Update Case Parties Data
                    var CaseDefect = CasePartyViewModels.ToCaseDefectView();
                    CaseDefect.CaseID = CaseID;
                    CaseDefect.PersonID = personData.ID;
                    var DefectStatus = PartiesService.EditCaseDefect(CaseDefect);

                    //If Party Exist in both Defendant and Victim we Needs to Update the Other As well
                    var PersonParties = PartiesService.GetDefectsByCaseID(CaseID).Where(party => party.PersonID == CasePartyData.PersonID && party.DefectType != CasePartyViewModels.PartyType).ToList();
                    foreach (var party in PersonParties)
                    {
                        PartiesService.EditCaseDefect(new vw_CaseDefectData
                        {
                            CaseID = party.CaseID,
                            Crimes = party.Crimes.Select(crime=>crime.ID).ToList(),
                            DefectType = party.DefectType,
                            DefendantStatus = party.Status,
                            IsCivilRightProsecutor = party.IsCivilRightProsecutor,
                            Order = party.Order,
                            PersonID = personData.ID,
                            ID = party.ID
                        });
                    }
                    if (PersonStatus == PersonStatus.SuccefullSave && DefectStatus == SaveDefectsStatus.Saved)
                    {
                        Transaction.Commit();
                        ViewBag.SavedEdite = true;
                        return CPartialView(PrepareCasePartiesViewModel(CaseID, PersonData, CasePartyViewModels)).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);

                    }
                    else
                    {
                        return CPartialView(PrepareCasePartiesViewModel(CaseID, PersonData, CasePartyViewModels)).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);

                    }

                }
            }
           // JavaScript("$(document).trigger('Parties:UpdateSuccefull');");
 }

        public ActionResult Delete(int CaseID, long PartyID, int PartyType)
        {
            return CPartialView(new PartyViewModel { CaseID = CaseID, PartyID = PartyID, PartyType = PartyType });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PartyViewModel PartyViewModel)
        {
            if (ModelState.IsValid)
            {
               var deletresult= PartiesService.DeleteCaseDefect(PartyViewModel.CaseID, PartyViewModel.PartyID, (PartyTypes)PartyViewModel.PartyType);
                //     return JavaScript("$(document).trigger('Parties:DeleteSuccefull');");
                if (deletresult == DeleteDefectStatus.Deleted) {
                    ViewBag.SavedDelete = true;
                    return CPartialView(PartyViewModel).WithSuccessMessages(JIC.Base.Resources.Messages.OperationCompletedSuccessfully);

                }
                else
                    return CPartialView(PartyViewModel).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);
            }
            return CPartialView(PartyViewModel).WithErrorMessages(JIC.Base.Resources.Messages.OperationNotCompleted);
        }

        private FullCasePartiesViewModel PrepareCasePartiesViewModel(int CaseID, Models.vw_PersonData PersonData = null, CasePartyViewModels CasePartyViewModels = null)
        {
            if (PersonData == null)
                PersonData = new Models.vw_PersonData();
            return new FullCasePartiesViewModel
            {
                PersonData = new PersonViewModel(PersonData, LookupService,PersonService, CasePartyViewModels != null && CasePartyViewModels.id != 0 ? Modes.Update : Modes.Add),
                CasePartyViewModels = CasePartyViewModels ?? new CasePartyViewModels(),
                CrimeTypes = LookupService.GetLookupsByCategory(Base.LookupsCategories.Crimes),
                PartyTypes = LookupService.GetPartyTypes().Where(partyType => partyType.ID != 0).ToList(),
                DefendantStatus = LookupService.GetLookupsByCategory(Base.LookupsCategories.PoliceStationDefendantsStatuses),
                CaseID = CaseID
            };
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