using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Fault.Entities.Models
{
    public class Service_CaseProsecution
    {
        [Key, ForeignKey("Cases_Case")]
        public int CaseID { get; set; }
        public int ProsecutionID { get; set; }

        public virtual Cases_Cases Cases_Case { get; set; }
    }
}
