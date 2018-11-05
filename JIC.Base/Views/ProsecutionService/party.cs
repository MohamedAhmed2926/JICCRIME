using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views.ProsecutionService
{

    [DataContract]
    public class party
    {
        [DataMember]
        public int partyId { get; set; }

        [DataMember]
        public int courtType { get; set; }
    }
}
