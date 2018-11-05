using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Models
{
    public class OrderOfAssignmentViewModels
    {
       
        public int CaseID { get; set; }
        [Display(Name = "OrderDescription", ResourceType = typeof(Base.Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        public string Description { get; set; }

    }
}