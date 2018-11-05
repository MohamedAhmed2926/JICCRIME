using JIC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
    public class vw_UserSecurity
    {
        public string Username { get; set; }
 
        //public string Password { get; set; }

        public SystemUserTypes UserType { get; set; }

        public int? LevelID { get; set; }
        public int? ProsecutionID { get; set; }

        public int? CourtID { get; set; }
     
        public string MobileNo { get; set; }

        public int? PersonsId { get; set; }

        public string FullName { get; set; }

    }
}
