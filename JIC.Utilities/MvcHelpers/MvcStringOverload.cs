using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JIC.Utilities.MvcHelpers
{
    public class CMvcHtmlString : HtmlString
    {
        public CMvcHtmlString(string value) : base(value)
        {
        }

        public static CMvcHtmlString operator +(CMvcHtmlString c1, CMvcHtmlString c2)
        {
            return new CMvcHtmlString(c1.ToHtmlString() + c2.ToHtmlString());
        }
        public static CMvcHtmlString operator +(CMvcHtmlString c1, HtmlString c2)
        {
            return new CMvcHtmlString(c1.ToHtmlString() + c2.ToHtmlString());
        }
    }
}
