using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
    public class vw_CrimeCaseBasicData : vw_CaseBasicData
    {
        public string CourtName { get; set; }
        public int FirstNumberInt { get { return int.Parse(FirstNumber); } set { FirstNumber = value.ToString(); } }
        public int FirstYearInt { get { return int.Parse(FirstYear); } set { FirstYear = value.ToString(); } }
        public int SecondNumberInt { get { return int.Parse(SecondNumber); } set { SecondNumber = value.ToString(); } }
        public int SecondYearInt { get { return int.Parse(SecondYear); } set { SecondYear = value.ToString(); } }
        public int CrimeTypeID { get; set; }
        public string CrimeTypeName { get; set; }
        public int? OverAllId { get; set; }
        public bool HasObtainment { get; set; }
        public bool IsComplete { get; set; }
        public string Obtainment { get; set; }
        public long? OverAllNumber { get; set; }
        public int? OverAllNumberProsecution { get; set; }
        public int? OverAllNumberYear { get; set; }
         
        public string overAllNumber { get; set; }
        public int CrimeNumber { get; set; }
        public string Status { get; set; }
        public string PoliceStationName { get; set; }
        public string CaseStatus { get; set; }
        public string MainCrimeName { get; set; }

        public string FirstprosecutionName { get; set; }
        public string SecoundProsecutionName { get; set; }
        public string OrderOfAssignment { get; set; }

    }
}
