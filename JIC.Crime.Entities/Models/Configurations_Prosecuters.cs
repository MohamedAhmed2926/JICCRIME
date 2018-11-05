namespace JIC.Crime.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Configurations_Prosecuters
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Configurations_Prosecuters()
        {
            Cases_CaseSessions = new HashSet<Cases_CaseSessions>();
            CourtConfigurations_CircuitRolls = new HashSet<CourtConfigurations_CircuitRolls>();
        }

        public int ID { get; set; }

        public int ProsecutionID { get; set; }


        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(14)]
        public string NationalID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseSessions> Cases_CaseSessions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourtConfigurations_CircuitRolls> CourtConfigurations_CircuitRolls { get; set; }
        public virtual Configurations_Prosecutions Configurations_Prosecutions { get; set; }
    }
}
