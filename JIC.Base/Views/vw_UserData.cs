using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
    public class vw_UserData
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string  Password { get; set; }
        public long? PersonID { get; set; }
        public string PhoneNo { get; set; }
        public int UserTypeID { get; set; }
        public string UserType { get; set; }

        public int? CourtID { get; set; }
        public int? ProsecutionID { get; set; }
        public int? UserJudgeLevel { get; set; }
        public bool Active { get; set; }
        public string FullName { get; set; }
        public int AccessFailedCount { get; set; }
    }
}
