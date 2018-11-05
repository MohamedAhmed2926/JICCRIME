using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views.Services
{
    [DataContract]
    public class PostPoneDecision : CaseDecision
    {
        [DataMember]
        public DateTime NextSessionData { get; set; }
        [DataMember]
        public int DecisionTypeID { get; set; }
    }
}
