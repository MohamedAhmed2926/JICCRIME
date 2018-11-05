namespace JIC.Fault.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Configurations_CaseTypes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Configurations_CaseTypes()
        {
            Cases_MasterCase = new HashSet<Cases_MasterCase>();
            CourtConfigurations_CircuitDaysSetup = new HashSet<CourtConfigurations_CircuitDaysSetup>();
            CourtConfigurations_CircuitRolls = new HashSet<CourtConfigurations_CircuitRolls>();
            CourtConfigurations_TextPredections = new HashSet<CourtConfigurations_TextPredections>();
            Configurations_Courts = new HashSet<Configurations_Courts>();
        }

        public int ID { get; set; }

        [StringLength(10)]
        public string Name { get; set; }

        [Required]
        [StringLength(2)]
        public string Code { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_MasterCase> Cases_MasterCase { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourtConfigurations_CircuitDaysSetup> CourtConfigurations_CircuitDaysSetup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourtConfigurations_CircuitRolls> CourtConfigurations_CircuitRolls { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourtConfigurations_TextPredections> CourtConfigurations_TextPredections { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Configurations_Courts> Configurations_Courts { get; set; }
    }
}
