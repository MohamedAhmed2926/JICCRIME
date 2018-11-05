namespace JIC.Crime.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cases_Cases
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cases_Cases()
        {
            Cases_CaseDefendants = new HashSet<Cases_CaseDefendants>();
            Cases_CaseDescription = new HashSet<Cases_CaseDescription>();
            Cases_CaseDocumentFolders = new HashSet<Cases_CaseDocumentFolders>();
            Cases_CaseNotes = new HashSet<Cases_CaseNotes>();
            Cases_CaseRequests = new HashSet<Cases_CaseRequests>();
            Cases_CasesLog = new HashSet<Cases_CasesLog>();
            Cases_CaseTransfer = new HashSet<Cases_CaseTransfer>();
            Cases_CaseTransmission = new HashSet<Cases_CaseTransmission>();
            Cases_CaseSessions = new HashSet<Cases_CaseSessions>();
            Cases_CaseVictims = new HashSet<Cases_CaseVictims>();
            Cases_CaseWitnesses = new HashSet<Cases_CaseWitnesses>();
        }

        public int ID { get; set; }

        public int? CircuitID { get; set; }

        public int CourtID { get; set; }

        public int MasterCaseID { get; set; }

        public int CaseLevelID { get; set; }

        public int ProcedureTypeID { get; set; }

        public int CaseStatusID { get; set; }

        public int? NewCaseStatusID { get; set; }

        public int? NoteStatusID { get; set; }
        
        public bool? IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        [StringLength(50)]
        public string LastModifiedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseDefendants> Cases_CaseDefendants { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseDescription> Cases_CaseDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseDocumentFolders> Cases_CaseDocumentFolders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseNotes> Cases_CaseNotes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseRequests> Cases_CaseRequests { get; set; }

        public virtual Configurations_Lookups Configurations_Lookups { get; set; }

        public virtual Configurations_Lookups Configurations_Lookups1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CasesLog> Cases_CasesLog { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseTransfer> Cases_CaseTransfer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseTransmission> Cases_CaseTransmission { get; set; }

        public virtual CourtConfigurations_Circuits CourtConfigurations_Circuits { get; set; }

        public virtual Configurations_Lookups Configurations_Lookups2 { get; set; }

        public virtual Configurations_Lookups Configurations_Lookups3 { get; set; }

        public virtual Cases_MasterCase Cases_MasterCase { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseSessions> Cases_CaseSessions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseVictims> Cases_CaseVictims { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseWitnesses> Cases_CaseWitnesses { get; set; }
        public virtual Configurations_Courts Configurations_Courts { get; set; }
    }
}
