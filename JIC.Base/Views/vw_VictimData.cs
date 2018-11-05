using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
    public class vw_VictimData
    {
        public long VictimID { get; set; }
        public long PersonID { get; set; }
        public bool IsCivilRights { get; set; }
        public string Job { get; set; }
        public int Order { get; set; }
    }
}
