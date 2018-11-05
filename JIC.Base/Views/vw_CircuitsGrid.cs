using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
    public class vw_CircuitsGrid
    {
        public int ID { get; set; }
        public string CircuitName { get; set; }
        public string CenterJudgeName { get; set; }
        public int CenterJudgeID { get; set; }
        public DateTime CircuitStartDate { get; set; }
        public List<vw_CircuitsJudges>  CircuitMembers { get; set; }
        public List<vw_KeyValue> PoliceStations { get; set; }
        public int SecretaryHead { get; set; }

        public int? SecretaryAssistant { get; set; }
        public int CrimeType { get; set; }
        public string CrimeTypeName { get; set; }
        public int CycleID { get; set; }
        public string CycleName { get; set; }
        public int JudgeCount { get; set; }
    }
}
