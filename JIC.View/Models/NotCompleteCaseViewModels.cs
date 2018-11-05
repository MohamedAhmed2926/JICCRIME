using JIC.Base;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Models
{
    public class NotCompleteCaseViewModels  
    { 
       public int CaseId { get; set; }
        //رقم القضية الجزئى 
        [Display(Name = "FirstLevelNumber", ResourceType = typeof(@Base.Resources.Resources))]

        public string FirstNumber { get; set; }
        //رقم القضية الكلى 
        [Display(Name = "SecondLevelNumber", ResourceType = typeof(Base.Resources.Resources))]

        public string SecondNumber { get; set; }
        // لا يوجد رقم شامل للقضية الغير مكتملة 
        // public string OverAll { get; set; }
        [Display(Name = "CrimeType", ResourceType = typeof(Base.Resources.Resources))]

         public string CrimName { get; set; }
        [Display(Name = "CaseTitle", ResourceType = typeof(Base.Resources.Resources))]

        
        public string CaseTitle { get; set; }
        [Display(Name = "NotComplete", ResourceType = typeof(Base.Resources.Resources))]

         public List<NotCompleteStatus> NotCompleteStatus { get; set; }
         [Display(Name = "NotComplete", ResourceType = typeof(Base.Resources.Resources))]
        public string ShowCaseStatus { get; set; }

        
    }
}