namespace JIC.Crime.Entities.Models
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
            CourtConfigurations_CircuitMembers = new HashSet<CourtConfigurations_CircuitMembers>();
            CourtConfigurations_CircuitRolls = new HashSet<CourtConfigurations_CircuitRolls>();
            CourtConfigurations_CircuitsPoliceStation = new HashSet<CourtConfigurations_CircuitsPoliceStation>();
            CourtConfigurations_TextPredictions = new HashSet<CourtConfigurations_TextPredictions>();

        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int CourtID { get; set; }

        public int SecretaryID { get; set; }

        public int? AssistantSecretaryID { get; set; }

        public int CrimeType { get; set; }

        [Column(TypeName = "date")]
        public DateTime CircuitStartDate { get; set; }

        public int CycleID { get; set; }

        public bool IsActive { get; set; }

        public bool IsFutureCircuit { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        [StringLength(50)]
        public string LastModifiedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_Cases> Cases_Cases { get; set; }

        public virtual Configurations_Courts Configurations_Courts { get; set; }

        public virtual Cases_CrimeTypes Cases_CrimeTypes { get; set; }

        public virtual Configurations_Lookups Configurations_Lookups1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourtConfigurations_CircuitMembers> CourtConfigurations_CircuitMembers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourtConfigurations_CircuitRolls> CourtConfigurations_CircuitRolls { get; set; }

        public virtual Security_Users Secretary_Users { get; set; }

        public virtual Security_Users Assistant_Secretary_Users { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourtConfigurations_CircuitsPoliceStation> CourtConfigurations_CircuitsPoliceStation { get; set; }


          [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
           public virtual ICollection<CourtConfigurations_TextPredictions> CourtConfigurations_TextPredictions { get; set; }

    }
}
