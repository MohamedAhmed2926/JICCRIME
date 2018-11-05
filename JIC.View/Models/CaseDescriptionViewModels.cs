using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Models
{
    public class CaseDescriptionViewModels
    {
        [Display(Name = "CaseDescrption", ResourceType = typeof(Base.Resources.Resources))]

      public  string CaseDescrptionByCourts { get; set; }
        [Display(Name = "CaseLawItems", ResourceType = typeof(Base.Resources.Resources))]
      public string CaseLawItemsByCourts { get; set; }
        [Display(Name = "CaseDescrption", ResourceType = typeof(Base.Resources.Resources))]

      public  string CaseDescrptionByProsecution { get; set; }
        [Display(Name = "CaseLawItems", ResourceType = typeof(Base.Resources.Resources))]

     public   string CaseLawItemsByProsecution { get; set; }

        [Display(Name = "Date", ResourceType = typeof(Base.Resources.Resources))]
        public DateTime DateCourts { get; set; }
        [Display(Name = "Date", ResourceType = typeof(Base.Resources.Resources))]

        public DateTime DateProsecution { get; set; }
    }
}