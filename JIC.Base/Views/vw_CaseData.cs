using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
    public class vw_CaseData
    {
        public vw_CrimeCaseBasicData CaseBasicData { get; set; }
     //   public vw_CaseDescription CaseDescription { get; set; }
        //  public List<vw_DefendantFullData> Defendants { get; set; }
        //  public List<vw_VictimFullData> Victims { get; set; }
        public vw_OrderOfAssignment  OrderOfAssignment { get; set; }
        public List<vw_CaseDefectsData > Defendants { get; set; }
        public List<vw_CaseDefectsData> Victims { get; set; }
        public List<vw_CaseDocuments> Documents { get; set; }
        public List<vw_CaseDecision> CaseDecision { get; set; }

        public List<vw_SessionData > CaseSessions { get; set; }
    }
}
