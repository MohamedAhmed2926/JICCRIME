using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Models
{
    public class WorkingDaysViewModels
    {
        public WorkingDaysViewModels()
        {
            IsFridaySelected = IsMondaySelected = IsSaturdaySelected = IsSundaySelected = IsThursdaySelected = IsTuesdaySelected = IsWednesdaySelected = true;
        }
        public bool IsSaturdaySelected { get; set; }
        public bool IsSundaySelected { get; set; }
        public bool IsMondaySelected { get; set; }
        public bool IsTuesdaySelected { get; set; }
        public bool IsWednesdaySelected { get; set; }
        public bool IsThursdaySelected { get; set; }
        public bool IsFridaySelected { get; set; }
    }
}