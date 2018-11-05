using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Validations
{
    public class CRequired : RequiredAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return JIC.Base.Resources.Messages.RequiredErrorMessage;
        }
    }
}