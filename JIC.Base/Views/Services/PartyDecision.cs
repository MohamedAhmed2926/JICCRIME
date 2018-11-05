using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views.Services
{
    [DataContract]
    public class PartyDecision
    {
        [DataMember]
        public int CasePartyID { get; set; }
        [DataMember]
        public int PartyDecisionTypeID { get; set; }
    }
}
