using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Models
{
    public class SearchGridViewModel
    {
        public int CaseID { get; set; }
        public string CrimeType { get; set; }

        public string OverAllNumber { get; set; }

        public string FirstNumber { get; set; }

        public string SecondNumber { get; set; }

        public string LastSessionDate { get; set; }

        public string LastDecesion { get; set; }

        public string PoliceStation { get; set; }

        public string MainCrimeType { get; set; }
    }
}