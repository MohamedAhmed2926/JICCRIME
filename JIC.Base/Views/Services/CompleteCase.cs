using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views.Services
{
    [DataContract]
    public class CompleteCase : CaseBase
    {
        [DataMember]
        public CaseSession FinalSession { get; set; }
        [DataMember]
        public List<DefendantDecision> DefendantDecisions { get; set; }

        public vw_FaultCaseBasicData MapToBasicData()
        {
            throw new NotImplementedException();
        }

        public vw_CaseConfiguration MapCaseSessionConfiguration(long sessionID, int caseID)
        {
            throw new NotImplementedException();
        }
    }
}
