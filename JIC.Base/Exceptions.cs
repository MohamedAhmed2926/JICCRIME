using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base
{
    public class ValidationExceptions : Exception
    {
        public ValidationExceptions(string message)
            : base(message)
        {
        }
    }
}
