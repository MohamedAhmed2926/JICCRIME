using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JIC.Repositories.DBInteractions
{
    internal class Utitlities
    {
        internal static string CurrentUsername
        {
            get { return (HttpContext.Current.Session["CurrentUsername"] ?? "Unknown").ToString(); }
        }
    }
}
