using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Models
{
    public class WitnessesSessionLogViewModel
    {
        public long ID { get; set; }
        public long WitnessID { get; set; }
        public string WitnessDocument { get; set; }

        public byte[] FileDataDocument { get; set; }

        public bool AttendanceStatus { get; set; }
    }
}