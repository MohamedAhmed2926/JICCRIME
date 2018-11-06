using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Crime.Entities.Models
{
    class Cases_WitnessSessionLog
    {

        public long WitnessID { get; set; }

        public int SessionID { get; set; }

        public int CaseID { get; set; }

        public bool PresenceStatus { get; set; }
        
        public string WitnessTestimony { get; set; }

        public byte[] TestimonyFileData { get; set; }

        public bool IsActive { get; set; }

        public virtual Cases_Cases Cases_Cases { get; set; }
        public virtual Cases_CaseWitnesses Cases_CaseWitnesses { get; set; }

    }
}
