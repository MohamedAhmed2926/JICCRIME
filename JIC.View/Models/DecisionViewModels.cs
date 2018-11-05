using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Models
{
    public class DecisionViewModels
    {
       [Display(Name = "DecionDesc", ResourceType = typeof(Base.Resources.Resources))]
       public string DecionDesc { get; set; }
        [Display(Name = "DecisionDate", ResourceType = typeof(Base.Resources.Resources))]
  
        public DateTime DecisionDate { get; set; }
    }
}