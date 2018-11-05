using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Crime.Entities.Models
{
   public class Cases_WitnessSessionLog
    {

     [Key]
        public int ID { get; set; }
        public long WitnessID { get; set; }
     
        public long SessionID { get; set; }
       
        public int CaseID { get; set; }
      
        public int PresenceStatus { get; set; }
        
        public string WitnessTestimony { get; set; }

        public byte[] TestimonyFileData { get; set; }

        //  public bool IsActive { get; set; }
        public virtual Cases_CaseSessions Cases_CaseSessions { get; set; }
        public virtual Cases_Cases Cases_Cases { get; set; }
        public virtual Cases_CaseWitnesses Cases_CaseWitnesses { get; set; }

    }
}


