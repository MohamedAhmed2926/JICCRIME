using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIC.Crime.View.Models
{
    public class LawyerLevelViewModels
    {
        public int ID { get; set; }
        [Display(Name = "LawyerLevel", ResourceType = typeof(Base.Resources.Resources))]
        public string LawyerLevelName { get; set; }
    }

    public class LawyerLevelModel
    {
        [Display(Name = "LawyerLevel", ResourceType = typeof(Base.Resources.Resources))]
        public int LawyerLevelID { get; set; }

        public SelectList Levels { get; set; }
    }
}