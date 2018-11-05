using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Crime.Entities.Models
{
    public partial class Cases_WitnessesSessionsLog
    {
        public long ID { get; set; }

        public long WitnessID { get; set; }
        public string WitnessDocument { get; set; }

        public byte[] FileDataDocument { get; set; }

        public bool AttendanceStatus{ get; set; }

        public virtual Cases_CaseWitnesses Cases_CaseWitnesses { get; set; }

    }
}
