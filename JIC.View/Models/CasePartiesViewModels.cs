using JIC.Base;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Models
{
    public class CasePartiesViewModel
    {
        public List<CasePartyViewModels> CaseParties { get; set; } = new List<CasePartyViewModels>();
        public int CaseID { get; set; }
    }
    public class CasePartyViewModels
    {
        public long id { get; set; }

        [Display(Name = "Order", ResourceType = typeof(Base.Resources.Resources))]
        public int PartiesOrder { get; set; }

        [Display(Name = "PartyName", ResourceType = typeof(Base.Resources.Resources))]
        public string PartyName { get; set; }

        [Display(Name = "NationalId", ResourceType = typeof(Base.Resources.Resources))]
        public string NationalID { get; set; }

        [Display(Name = "PartyStatus", ResourceType = typeof(Base.Resources.Resources))]
        public int? DefendantStatus { get; set; }

        [Display(Name = "PartyStatus", ResourceType = typeof(Base.Resources.Resources))]
        public string Status { get; set; }

        [Display(Name = "PartyType", ResourceType = typeof(Base.Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Base.Resources.Resources), ErrorMessageResourceName = "RequiredErrorMessage")]

        public PartyTypes PartyType { get; set; }

        public PartyTypes? OriginalPartyType { get; set; }

        [Display(Name = "IsCivilRightProsecutor", ResourceType = typeof(Base.Resources.Resources))]
        public bool IsCivilRightProsecutor { get; set; }

        public string IsCivilRights { get; set; }
        [Display(Name = "Crime", ResourceType = typeof(Base.Resources.Resources))]
        public List<int> CrimeTypes { get; set; }

        internal static CasePartyViewModels Map(vw_CaseDefectsData party)
        {
            if (party != null)
                return new CasePartyViewModels
                {
                    id = party.ID,
                    IsCivilRightProsecutor = party.IsCivilRightProsecutor,
                    NationalID = party.NationalID,
                    PartiesOrder = party.Order,
                    PartyName = party.Name,
                    PartyType = party.DefectType,
                    CrimeTypes = (party.Crimes != null ? party.Crimes.Select(crime => crime.ID).ToList() : new List<int>()),
                    OriginalPartyType = party.DefectType,
                    DefendantStatus = party.Status,
                    
                };
            return null;
        }

        public vw_CaseDefectData ToCaseDefectView()
        {
            return new vw_CaseDefectData
            {
               
                ID = this.id,
                IsCivilRightProsecutor = this.IsCivilRightProsecutor,
                //NationalID = this.NationalID,
                Order = this.PartiesOrder,
                //Name = PartyName,
                DefectType = PartyType,
                Crimes = this.CrimeTypes,
                DefendantStatus = this.DefendantStatus
            };
        }
    }

    public class FullCasePartiesViewModel
    {
        public CasePartyViewModels CasePartyViewModels { get; set; }
        public PersonViewModel PersonData { get; set; }
        public List<vw_KeyValue> PartyTypes { get; set; }
        public List<vw_KeyValue> DefendantStatus { get; set; }
        public List<vw_KeyValue> CrimeTypes { get; set; }
        public long CaseID { get; internal set; }
        public CaseStatus CaseInFlow { get; set; }
    }
    
    public class PartyViewModel
    {
        [Required]
        public int CaseID { get; set; }
        [Required]
        public long PartyID { get; set; }
        [Required]
        public int PartyType { get; set; }
        public int OriginalPartyType { get; set; }
    }

    //public class CasePartyPersonData : vw_PersonData
    //{
    //    [Required(ErrorMessageResourceType = typeof(Base.Resources.Resources), ErrorMessageResourceName = "RequiredErrorMessage")]
    //    public override string  Job { get; set; }
    //}
}