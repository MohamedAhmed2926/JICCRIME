using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views.Services
{
    [DataContract]
    public class Document
    {
        [DataMember]
        public int DocumentTypeID { get; set; }
        [DataMember]
        public string DocumentTitle { get; set; }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public string FileURL { get; set; }
    }
}
