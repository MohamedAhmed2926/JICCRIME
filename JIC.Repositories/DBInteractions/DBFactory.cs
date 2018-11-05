using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JIC.Repositories.DBInteractions
{
    public class DBFactory
    {
        public static JICContext Get()
        {
            return (JICContext)(HttpContext.Current.Items["DataContext"] ?? (HttpContext.Current.Items["DataContext"] = new JICContext()));
        }
    }
}
