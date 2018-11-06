using JIC.Base;
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
    [CAuthorize(SystemUserTypes.schedualEmployee, SystemUserTypes.ElementaryCourtAdministrator)]
    public class LawyersDefectsController : ControllerBase
    {
        private ICrimeCaseService CaseService;
        private IDefectsService PartiesService;
        public ILookupService LookupService;
        public IPersonService PersonService;


        public LawyersDefectsController (IDefectsService PartiesService,ICrimeCaseService CaseService, ILookupService LookupService,IPersonService PersonService)
        {
            this.PartiesService = PartiesService;
            this.CaseService = CaseService;
            this.LookupService = LookupService;
            this.PersonService = PersonService;
           
        }
    // GET: LawyersDefects
    public ActionResult Index(int CaseID)
        {
            CasePartiesForLawyersViewModel fullCasePartiesView = new CasePartiesForLawyersViewModel
            {
                CaseID = CaseID,
                CaseParties = PartiesService.GetDefectsByCaseID(CaseID).Select(party => CasePartyViewModels.Map(party)).ToList()
            };

            foreach (var parties in fullCasePartiesView.CaseParties.Where(e => e.PartyType == PartyTypes.Defendant))
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
            return View();
        }
    }
}