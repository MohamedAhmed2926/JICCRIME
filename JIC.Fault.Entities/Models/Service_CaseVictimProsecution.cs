using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Fault.Entities.Models
{
    public class Service_CaseVictimProsecution
    {
        [Key, ForeignKey("Cases_CaseVictim")]
        public long CaseVictimID { get; set; }
        public int ProsecutionID { get; set; }
        public virtual Cases_CaseVictims Cases_CaseVictim { get; set; }
    }
}
