using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
  public  class vw_unCompletCase
    {
        public int CaseId { get; set; }

        public string FirstNumber { get; set; }
        public string FirstYear { get; set; }
        public string SecondYear { get; set; }
        public long OverAll { get; set; }
        [Display(Name = "CrimeType", ResourceType = typeof(Base.Resources.Resources))]

        public string CrimName { get; set; }
        [Display(Name = "CaseTitle", ResourceType = typeof(Base.Resources.Resources))]

        public string CaseTitle { get; set; }
        public string SecondNumber { get; set; }
        public string FirstprosecutionName { get; set; }
        public string SecondprosecutionName { get; set; }

        public int? SecondprosecutionId { get; set; }
        public int? FirstprosecutionId { get; set; }
        // لا يوجد رقم شامل للقضية الغير مكتملة 
        //بعد الرجوع لـ user story وجدت أن من البيانات الظاهرة رقم الشامل
        // public string OverAll { get; set; }
        [Display(Name = "NotComplete", ResourceType = typeof(Base.Resources.Resources))]

        public List<NotCompleteStatus> NotCompleteStatus { get {
                List<NotCompleteStatus> list = new List<Base.NotCompleteStatus>();
                if (DefendantNotComplete)
                    list.Add(Base.NotCompleteStatus.Defendent);
                if (OrderOfAssignmentNotComplete)
                    list.Add(Base.NotCompleteStatus.OrderOfAssignment);
                if (CaseDocumentFoldersNotComplete)
                    list.Add(Base.NotCompleteStatus.Document);
                if (OverallNumbersNotComplete)
                    list.Add(Base.NotCompleteStatus.OverAllNumber);
                return list;
            }
          
        }
        [Display(Name = "NotComplete", ResourceType = typeof(Base.Resources.Resources))]
        public string ShowCaseStatus { get; set; }
        public bool DefendantNotComplete { get; set; }
        public bool OrderOfAssignmentNotComplete { get; set; }
        public bool CaseDocumentFoldersNotComplete { get; set; }
        public bool OverallNumbersNotComplete { get; set; }
    }
}
