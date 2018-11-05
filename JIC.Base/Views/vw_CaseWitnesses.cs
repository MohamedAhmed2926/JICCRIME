using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
   public class vw_CaseWitnesses
    {
        public long ID { get; set; }

        public long PersonID { get; set; }

        public int CaseID { get; set; }
        public string WitnessDocument { get; set; }

        public byte[] FileDataDocument { get; set; }

        public bool IsActive { get; set; }

    }
}
