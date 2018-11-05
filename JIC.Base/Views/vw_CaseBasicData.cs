using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
    public class vw_CaseBasicData
    {
        public int CaseID { get; set; }
        public int CourtID { get; set; }
        public string FirstNumber { get; set; }
        public string FirstYear { get; set; }
        public int FirstPoliceStationID { get; set; }
        public int FirstProsecutionID { get; set; }
        public string SecondNumber { get; set; }
        public string SecondYear { get; set; }
        public int? SecondProsecutionID { get; set; }
        public string CaseName { get; set; }
        public int CaseStatusID { get; set; }
        public int ProsecutionCaseID { get; set; }
        public string NationalID { get; set; }
        public int MainCrimeID { get; set; }
    }
}
