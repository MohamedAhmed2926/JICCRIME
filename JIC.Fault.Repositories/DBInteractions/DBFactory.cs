using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JIC.Fault.Repositories.DBInteractions
{
    public class DBFactory
    {
        private static JICFaultContext JICFaultContext;
        public static JICFaultContext Get()
        {
            if(HttpContext.Current == null)
                return JICFaultContext ?? (JICFaultContext = new JICFaultContext());

            return (JICFaultContext)(HttpContext.Current.Items["DataContext"] ?? (HttpContext.Current.Items["DataContext"] = new JICFaultContext()));
        }
    }
}
