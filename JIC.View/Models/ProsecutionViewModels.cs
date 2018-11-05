using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JIC.Crime.View.Models
{
   public class ProsecutionViewModels
    {
        public int ID { get; set; }
         [Display(Name = "Prosecution", ResourceType = typeof(Base.Resources.Resources))]
        public string ProsecutionName { get; set; }
       
    }
    public class ProsectuionModel
    {
        [Display(Name = "Prosecution", ResourceType = typeof(Base.Resources.Resources))]
        public int ProsecutionID { get; set; }

        public SelectList Prosecutions { get; set; }
    }
}
