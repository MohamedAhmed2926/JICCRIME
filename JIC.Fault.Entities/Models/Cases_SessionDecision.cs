namespace JIC.Fault.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cases_SessionDecision
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cases_SessionDecision()
        {
            Cases_DefendantsDecision = new HashSet<Cases_DefendantsDecision>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long CaseSessionID { get; set; }

        [Required]
        public string DecisionText { get; set; }

        public short DecisionTypeID { get; set; }

        public long? FirstRollID { get; set; }

        public long? SecondRollID { get; set; }

        public bool? PaymentStatus { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PaymentDate { get; set; }

        public int? OldCircuitID { get; set; }

        public int? OldCaseTypeID { get; set; }

        public int? NewCircuitID { get; set; }

        public int? NewCaseTypeID { get; set; }

        public virtual Cases_CaseSessions Cases_CaseSessions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_DefendantsDecision> Cases_DefendantsDecision { get; set; }

        public virtual Configurations_DecisionTypes Configurations_DecisionTypes { get; set; }

        public virtual CourtConfigurations_CircuitRolls CourtConfigurations_CircuitRolls { get; set; }

        public virtual CourtConfigurations_CircuitRolls CourtConfigurations_CircuitRolls1 { get; set; }
    }
}
