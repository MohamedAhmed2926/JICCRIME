namespace JIC.Fault.Entities.Models
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
            Cases_CaseTransfer = new HashSet<Cases_CaseTransfer>();
            Cases_CaseSessions = new HashSet<Cases_CaseSessions>();
            Cases_CaseVictims = new HashSet<Cases_CaseVictims>();
        }

        public int ID { get; set; }

        public int CircuitID { get; set; }

        public int MasterCaseID { get; set; }

        public int CaseLevelID { get; set; }

        public int ProcedureTypeID { get; set; }

        public int CaseStatusID { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        [StringLength(50)]
        public string LastModifiedBy { get; set; }

        public int? NewCaseStatusID { get; set; }

        public int? ProsecutionCaseID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseDefendants> Cases_CaseDefendants { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseDescription> Cases_CaseDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseDocumentFolders> Cases_CaseDocumentFolders { get; set; }

        public virtual Configurations_Lookups Configurations_Lookups { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseTransfer> Cases_CaseTransfer { get; set; }

        public virtual CourtConfigurations_Circuits CourtConfigurations_Circuits { get; set; }

        public virtual Configurations_Lookups Configurations_Lookups1 { get; set; }

        public virtual Configurations_Lookups Configurations_Lookups2 { get; set; }

        public virtual Cases_MasterCase Cases_MasterCase { get; set; }

        public virtual Cases_Resumption_Detail Cases_Resumption_Detail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseSessions> Cases_CaseSessions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseVictims> Cases_CaseVictims { get; set; }

        public virtual Service_CaseProsecution Service_CaseProsecution { get; set; }
    }
}
