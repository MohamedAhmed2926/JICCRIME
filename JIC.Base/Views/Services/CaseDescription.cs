using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views.Services
{


    [DataContract]
    public class CaseDescription
    {
        [DataMember]
        public string LawItems { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
