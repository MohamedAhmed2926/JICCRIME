using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Models
{
    public class CaseDataViewModels
    {
        public CaseBasicDataViewModel CaseBasicDataViewModel { get; set; }
        public List<CasePartyViewModels> Parties { get; set; }
        public List<CasePartyViewModels> Victims { get; set; }
        public CaseDescriptionViewModels Description { get; set; }
        public OrderOfAssignmentViewModels OrderOfAssignment { get; set; }
        public List<DocumentsViewModels> Documents { get; set; }
        public List<DecisionViewModels> CaseDecision { get; set; }
        public IQueryable<CaseBasicDataViewModel> AllCaseBasicDataViewModel { get; set; }
        public CaseConfigurationData caseConfigurationData { get; set; }
    }


   
}