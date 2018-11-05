using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views.Services
{
    [DataContract]
    public abstract class CaseDecision
    {
        [DataMember]
        public int ReservationID { get; set; }

        [DataMember]
        public int CourtID { get; set; }

        [DataMember]
        public int CircleID { get; set; }

        [DataMember]
        public int BusinessCaseID { get; set; }

        [DataMember]
        public string DecisionText { get; set; }
        [DataMember]
        public string DecisionFileURL { get; set; }

    }
}
