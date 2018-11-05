using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Validations
{
    public class ArabicAndNumber : RegularExpressionAttribute
    {
        public ArabicAndNumber()
            : base(GetRegex())
        { }

        private static string GetRegex()
        {
            // TODO: Go off and get your RegEx here
            return @"^[\u0621-\u064A ][\u0621-\u064A0-9+ ]+";
        }
        public override string FormatErrorMessage(string name)
        {
            return JIC.Base.Resources.Messages.InvalidCharacterField;
        }
    }
}

