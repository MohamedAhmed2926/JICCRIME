using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
    public class vw_RollCases
    {
        public int Order { get; set; }
        public int CaseID { get; set; }
        public string OverAllNumber { get; set; }
        public string FirstLevelNumber { get; set; }
        public string SecondLevelNumber { get; set; }
        public string CaseStatus { get; set; }
        public string MainCrime { get; set; }
        public int? SecretaryID { get; set; }
        public long SessionID { get; set; }
        public string Title { get; set; }
       public int RollID { get; set; }
        public MinutesStatus MinutesOfSession {get; set;}
        public RollStatus rollStatus { get; set; }
        public int CircuitID { get; set; }
       public DateTime RollDate { get; set; }
    }
}
