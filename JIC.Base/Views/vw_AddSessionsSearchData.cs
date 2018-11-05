using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
   public  class vw_AddSessionsSearchData
    {
        public int? CourtID { get; set; }

        public int? FirstLevelNumber { get; set; }

        public int? FirstLevelYear { get; set; }

        public int? PoliceStationID { get; set; }

        public int? SecondLevelNumber { get; set; }

        public int? SecondLevelYear { get; set; }

        public int? SecondLevelProsecutionID { get; set; }

        public int? FirstLevelProsecutionID { get; set; }
        public int? CircuitID { get; set; }

        public int? CrimeType { get; set; } // np3 el genaya

        public int? CrimeID { get; set; } // el tohma el ra2esya

        public int? CaseStatusID { get; set; } // no3 el 2deya
        public int? DefendantStatus { get; set; }
        public DateTime? SessionDate { get; set; }

    }
}
