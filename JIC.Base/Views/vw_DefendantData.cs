using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
    public class vw_DefendantData
    {
        public long DefendantID { get; set; }
        public long PersonID { get; set; }
        public List<int> Crimes { get; set; }
        public bool IsCivilRights { get; set; }
        public int DefendantStatus { get; set; }
        public string Job { get; set; }
        public int Order { get; set; }
        public string PartyType { get; set; }
        public string Status { get; set; }

        public DecisionTypes DecisionType { get; set; }
        public List<vw_DefendantData> DefendantsListJudges { get; set; }
    }
}
