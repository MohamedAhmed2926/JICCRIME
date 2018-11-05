using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace JIC.Base.Views.Services
{
    [DataContract]
    public class Response
    {
        [DataMember]
        public bool Result
        {
            get
            {
                return ErrorCodes.Count == 0;
            }
            set
            {
                //Wcf isn't happy if the set function doesn't exit 
                //it will try to set any value just ignore it
            }
        }
        [DataMember]
        public List<int> ErrorCodes { get; set; } = new List<int>();

        public static Response Success
        {
            get
            {
                return new Response();
            }
        }

        public static Response Failed
        {
            get
            {
                return new Response
                {
                    ErrorCodes = new List<int> { ErrorCode.Failed }
                };

            }
        }
    }
    
}