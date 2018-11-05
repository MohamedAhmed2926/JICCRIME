using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Models
{
    public enum CourtConfiguration
    {
        VacationDatesMode,
        VacationDaysMode,
        CyceDistributionMode
    }
    public class CourtConfigurationViewModel
    {
        public int CourtID { get; set; }
        public CourtConfiguration CourtConfigurationMode { get; set; }
    }
}