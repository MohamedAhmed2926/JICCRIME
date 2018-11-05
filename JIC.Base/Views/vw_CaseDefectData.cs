using System.Collections.Generic;

namespace JIC.Base.Views
{
    public class vw_CaseDefectData
    {
        public long ID { get; set; }
        public int CaseID { get; set; }
        public long PersonID { get; set; }
        public PartyTypes DefectType { get; set; }
        public int? DefendantStatus { get; set; }
        public List<int> Crimes { get; set; }
        public bool IsCivilRightProsecutor { get; set; }
        public int Order { get; set; }
    }
}