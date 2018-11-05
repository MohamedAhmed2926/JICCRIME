using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views.Services
{
    [DataContract]
    public class ObjectionRequest
    {
        [DataMember]
        public string Business_Case_Id { get; set; }
        [DataMember]
        public int Court_ID { get; set; }
        [DataMember]
        public CaseSession FirstSession { get; set; }
    }
}
