using JIC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
    public class vw_SystemActions
    {
        public long ActionID { get; set; }
        public SystemUserTypes UserTypeID { get; set; }
        public string PagePath { get; set; }
        public string RoutingName { get; set; }
    }
}
