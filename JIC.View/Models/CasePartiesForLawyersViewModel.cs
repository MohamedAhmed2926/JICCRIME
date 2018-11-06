using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Models
{
    public class CasePartiesForLawyersViewModel
    {
        public int CaseID { get; set; }
        public List<CasePartyViewModels> CaseParties { get; set; } = new List<CasePartyViewModels>();
    }

}