using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
    public class vw_CaseConfiguration
    {
        public List<int> CasesIDs { get; set; }
        public int CircuitID { get; set; }
        public long? SessionID { get; set; }
        public DateTime SessionDate { get; set; }
    }
}
