using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
    public class vw_AddSessionSearchResult
    {
        public int CaseID { get; set; }
        public int CrimeID { get; set; }
        public string FirstLevelNumber { get; set; }
        public string SecondLevelNumber { get; set; }
        public string CrimeType { get; set; }
        public string PoliceStation { get; set; }
        public string MainCrime { get; set; }
        public string CaseStatus { get; set; }
        public string CircuitName { get; set; }
        public DateTime SessionDate { get; set; }

    }
}
