using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
  public  class vw_WitnessAttendance
    {

        public long WitnessID { get; set; }
        public int CaseID { get; set; }
        public int AttendanceID { get; set; }
        public int SessionID { get; set; }
        public string WitnessTestimony { get; set; }
        public byte[] TestimonyFileData { get; set; }
    }
}
