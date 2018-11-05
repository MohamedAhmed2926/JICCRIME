using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
    public class vw_FaultCaseBasicData : vw_CaseBasicData
    {
        public int CaseLevelID { get; set; }
        public int CircuitID { get; set; }
        public int ProcedureTypeID { get; set; }
        public int CaseTypeID { get; set; }

        public int MasterCaseID { get; set; }
    }
}
