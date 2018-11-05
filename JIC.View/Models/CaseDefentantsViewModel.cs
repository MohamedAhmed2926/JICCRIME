using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIC.Crime.View.Models
{
    public class CaseDefentantsViewModel
    {
        public CaseDefentantsViewModel()
        {
            DefentantJudments = new List<SelectListItem>();
        }
        public int CaseID { get; set; }
        public int ID { get; set; }
        [Display(Name = "Name", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public string Name { get; set; }
        [Display(Name = "Nationality", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public string Nationality { get; set; }
        [Display(Name = "NationalId", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public string NationalID { get; set; }
        [Display(Name = "Birthday", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public DateTime? Birthday { get; set; }
        [Display(Name = "Age", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public long Age { get; set; }
        [Display(Name = "Job", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public string Job { get; set; }
        [Display(Name = "PassportNo", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public string PassportNumber { get; set; }
        [Display(Name = "Address", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public string Address { get; set; }
        public List<SelectListItem> DefentantJudments { get; set; }
        public int casejudgmentID { get; set; }
        
    }
}