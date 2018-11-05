using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Crime.Entities.Models
{
    public class CourtConfigurations_CourtHalls
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CourtConfigurations_CourtHalls()
        {
          
            CourtConfigurations_CircuitRolls = new HashSet<CourtConfigurations_CircuitRolls>();
        }
        public long ID { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }       
        public int CourtID { get; set; }

        public Configurations_Courts Court { get; set; }
        public virtual ICollection<CourtConfigurations_CircuitRolls> CourtConfigurations_CircuitRolls { get; set; }


    }
}
