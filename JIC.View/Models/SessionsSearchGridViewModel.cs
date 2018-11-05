using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Models
{
    public class SessionsSearchGridViewModel
    {
        public int CaseID { get; set; }
        public string FirstLevelNumber { get; set; }
        public string SecondLevelNumber { get; set; }
        public string CrimeType { get; set; }
        public int CrimeID { get; set; }
        public string PoliceStation { get; set; }
        public string MainCrime { get; set; }
        public string CaseStatus { get; set; }
        public string CircuitName { get; set; }
        public string SessionDate { get; set; }
    }
}