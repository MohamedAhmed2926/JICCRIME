using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
    public class vw_CircuitConfiguration
    {
        public int? CircuitID { get; set; }
        public int? Court { get; set; }
        public string CircuitName { get; set; }
        public DateTime? CircuitStartDate { get; set; }
        public int? Cycle { get; set; }
        public int? JudgeCount { get; set; }

        public List<vw_CircuitsJudges> CircuitJudges { get; set; }
        public int? SecretaryHead { get; set; }
        public int? SecretaryAssistant { get; set; }
        public int? CrimeType { get; set; }
        public List<int> PoliceStations { get; set; }
    }
}
