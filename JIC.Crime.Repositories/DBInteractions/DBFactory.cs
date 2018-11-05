using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JIC.Crime.Repositories.DBInteractions
{
    public class DBFactory
    {
        public static JICCrimeContext Get()
        {
            return (JICCrimeContext)(HttpContext.Current.Items["DataContext"] ?? (HttpContext.Current.Items["DataContext"] = new JICCrimeContext()));
        }
    }
}
