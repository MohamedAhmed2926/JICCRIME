using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
    public class SearchResult
    {
        public int CaseID { get; set; }
        public string FirstLevelNumber { get; set; }
        public string SecondLevelNumber { get; set; }
        public string OverAllNumber { get; set; }
        public string CrimeType { get; set; }
        public string LastDecision { get; set; }
        public DateTime LastSessionDate { get; set; }
        public int MasterCase { get; set; }
    }
}
