using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
    public class vw_SearchData
    {
        public int? CourtID { get; set; }

        public int? FirstLevelNumber { get; set; }

        public int? FirstLevelYear { get; set; }

        public int? PoliceStationID { get; set; }

        public SessionSearchMode? SessionDateType { get; set; }
        public DateTime? SessionDate { get; set; }

        public int? SecondLevelNumber { get; set; }

        public int? SecondLevelYear { get; set; }

        public int? SecondLevelProsecutionID { get; set; }

        public int? FirstLevelProsecutionID { get; set; }

        public DateTime? JudgeDate { get; set; }

        public int? JudgeType { get; set; }

        public long? OverAllNumber { get; set; }

        public int? OverAllNumberProsecution { get; set; }

        public int? OverAllNumberYear { get; set; }

        public int? PartyType { get; set; }

        public String PartyName { get; set; }

        public ObtainmentStatus? HasObtainment { get; set; }

        public int? CrimeType { get; set; }

        public int? CircuitID { get; set; }

        public bool? IsPendingSessionReservation { get; set; }
    }
}

