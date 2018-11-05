using JIC.Base;
using JIC.Base.Resources;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIC.Crime.View.Models
{
    public class CaseDefectsDataViewModel
    {
        public long ID { get; set; }
        public int CaseID { get; set; }
        public long DefectID { get; set; }
        public long PersonID { get; set; }
        public string Name { get; set; }
        public int? NationalityType { get; set; }
        public string NationalID { get; set; }
        public DateTime? Birthdate { get; set; }
        public long Age { get; set; }
        public PartyTypes DefectType { get; set; }
        public int Status { get; set; }
       
        public bool IsCivilRightProsecutor { get; set; }
        public string Address { get; set; }
        public string JobName { get; set; }
        public string PassportNumber { get; set; }
        public PresenceStatus Presence { get; set; }
        public int Order { get; set; }
        public string Nationality { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages),
ErrorMessageResourceName = "RequiredErrorMessage")]
        public List<SelectListItem> AtendanceType { get; set; }
        public int Attendence { get; set; }
        public int SessionId { get; set; }

    }
    public class CaseDefentsViewModel
    {
        public CaseDefentsViewModel()
        {
            Crimes = new List<vw_KeyValue>();
            caseDefects = new List<CaseDefectsDataViewModel>();
            //SessionData = new List<vw_SessionData>();
        }

        public List<vw_KeyValue> Crimes { get; set; }
        public List<CaseDefectsDataViewModel> caseDefects { get; set; }
        public vw_SessionData SessionData { get; set; }

        public CaseDefectsDataViewModel caseDefectsData { get; set; }
    }
   
}

