using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Validations
{
    public class PhoneNumber : RegularExpressionAttribute
    {
        public PhoneNumber() : base("^[0-9]*$")
        {
        }
        public override string FormatErrorMessage(string name)
        {
            return JIC.Base.Resources.Messages.NumbersOnly;
        }
    }
    public class Arabic : RegularExpressionAttribute
    {
        public Arabic() : base("^[\u0621-\u064A ][\u0621-\u064A0-9+ ]+")
        {
        }
        public override string FormatErrorMessage(string name)
        {
            return JIC.Base.Resources.Messages.InvalidCharacterField;
        }
    }
}