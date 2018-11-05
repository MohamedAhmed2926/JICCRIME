using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views.Services
{
    [DataContract]
    public class CompleteDecision : CaseDecision
    {
        [DataMember]
        public PartyDecision[] PartiesDecisions { get; set; }
    }
}
