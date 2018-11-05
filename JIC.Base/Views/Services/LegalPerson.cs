using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views.Services
{
    [DataContract]
    public class LegalPerson
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Type { get; set; }
        [DataMember]
        public string RegistrationNo { get; set; }
        [DataMember]
        public string RepName { get; set; }
    }
}
