using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Models
{
    public class CrimeTypes
    {
        public int CrimeTypeID { get; set; }
        [Display(Name = "CrimeType", ResourceType = typeof(Base.Resources.Resources))]

        public string CrimeName { get; set; }
    }
}