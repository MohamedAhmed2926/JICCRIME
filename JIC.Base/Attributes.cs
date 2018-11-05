using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base
{ 
    public sealed class AuthenticationAttribute : Attribute
    {
        readonly bool _RequiresAuthentication;

        // This is a positional argument
        public AuthenticationAttribute(bool RequiresAuthentication)
        {
            this._RequiresAuthentication = RequiresAuthentication;
        }

        public bool RequiresAuthentication
        {
            get { return _RequiresAuthentication; }
        }
    }

}
