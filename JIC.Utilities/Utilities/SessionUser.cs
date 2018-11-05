using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Utilities.Utilities
{
    public class SessionUser
    {
        public int UserID { get; set; }

        public string SessionID { get; set; }

        public DateTime LogInTime { get; set; }

        public string Username { get; set; }
    }
}
