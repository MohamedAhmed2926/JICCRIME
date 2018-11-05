using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
    public class vw_CircuitData
    {
        public int ID { get; set; }
        public string CircuitName { get; set; }
        public int CenterJudgeID { get; set; }
        public string UserName { get; set; }
        public List<vw_CircuitsJudges> JudgesID { get; set; }
        public int SecretaryID { get; set; }
        public int? AssistantSecretaryID { get; set; }
        public int CrimeTypeID { get; set; }
        public DateTime CircuitStartDate { get; set; }
        public List<int> PoliceStations { get; set; }
        public int CourtID { get; set; }
        public int CycleID { get; set; }
        public bool IsActive { get; set; }
        public bool IsFutureCircuit { get; set; }
        public int? JudgeCount { get; set; }

        
    }
}
