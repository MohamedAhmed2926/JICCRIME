namespace JIC.Fault.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CourtConfigurations_Circuits
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CourtConfigurations_Circuits()
        {
            Cases_Cases = new HashSet<Cases_Cases>();
            CourtConfigurations_CasesDistributionRules = new HashSet<CourtConfigurations_CasesDistributionRules>();
            CourtConfigurations_CircuitDaysSetup = new HashSet<CourtConfigurations_CircuitDaysSetup>();
            CourtConfigurations_CircuitMembers = new HashSet<CourtConfigurations_CircuitMembers>();
            CourtConfigurations_CircuitRolls = new HashSet<CourtConfigurations_CircuitRolls>();
            CourtConfigurations_CasesDistributionRules1 = new HashSet<CourtConfigurations_CasesDistributionRules>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int CourtID { get; set; }

        public bool IsActive { get; set; }

        public bool IsFutureCircuit { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_Cases> Cases_Cases { get; set; }

        public virtual Configurations_Courts Configurations_Courts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourtConfigurations_CasesDistributionRules> CourtConfigurations_CasesDistributionRules { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourtConfigurations_CircuitDaysSetup> CourtConfigurations_CircuitDaysSetup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourtConfigurations_CircuitMembers> CourtConfigurations_CircuitMembers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourtConfigurations_CircuitRolls> CourtConfigurations_CircuitRolls { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourtConfigurations_CasesDistributionRules> CourtConfigurations_CasesDistributionRules1 { get; set; }
    }
}
