using JIC.Base;
using JIC.Base.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Models
{
    public class AtendanceViewModel
    {
        public long DefectID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages),ErrorMessageResourceName = "RequiredErrorMessage")]
        public PresenceStatus Presence { get; set; }
        public int? WitnessPresence { get; set; }
        public PartyTypes DefectType { get; set; }
        public int OrderID {get; set; }
        public int CaseID { get; set; }
        public int SessionId { get; set; }

    }
}