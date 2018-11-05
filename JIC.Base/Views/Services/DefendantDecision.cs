using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views.Services
{
    [DataContract]
    public class DefendantDecision
    {
        [DataMember]
        public int CasePartyID { get; set; }
        [DataMember]
        public string PunishmentDetail { get; set; }
        [DataMember]
        public int? RestrictionNo { get; set; }
        [DataMember]
        public int? RestrictionYear { get; set; }
        [DataMember]
        public int? PunishmentType { get; set; }
        [DataMember]
        public bool IsGuilty { get; set; }

    }
}
