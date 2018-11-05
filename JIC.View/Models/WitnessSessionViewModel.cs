using JIC.Base.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Models
{
    public class WitnessSessionViewModel
    {
        public int WitnessID { get; set; }
        public int CaseID { get; set; }
        public int AttendanceID { get; set; }
        public int CircuitID { get; set; }
        public int SessionID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        public string WitnessTestimony { get; set; }
        public byte[] TestimonyFileData { get; set; }

       

    }
}