using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Models
{
    public class CaseWitnessesViewModel
    {
        public long ID { get; set; }

        public long PersonID { get; set; }

        public int CaseID { get; set; }
        public string WitnessDocument { get; set; }

        public byte[] FileDataDocument { get; set; }

        public bool IsActive { get; set; }

        public string WitnessName { get; set; }

        public string NationalID { get; set; }
    }
}