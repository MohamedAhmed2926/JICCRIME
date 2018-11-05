namespace JIC.Fault.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CourtConfigurations_CircuitRolls
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CourtConfigurations_CircuitRolls()
        {
            Cases_CaseSessions = new HashSet<Cases_CaseSessions>();
            Cases_CaseTransfer = new HashSet<Cases_CaseTransfer>();
            Cases_CaseTransfer1 = new HashSet<Cases_CaseTransfer>();
            Cases_SessionDecision = new HashSet<Cases_SessionDecision>();
            Cases_SessionDecision1 = new HashSet<Cases_SessionDecision>();
        }

        public long ID { get; set; }

        public int CircuitID { get; set; }

        [Column(TypeName = "date")]
        public DateTime SessionDate { get; set; }

        public int CaseTypeID { get; set; }

        public byte RollStatusID { get; set; }

        public bool? ApprovedByJudge { get; set; }

        public int? SecretaryID { get; set; }

        public bool GeneratedBySystem { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseSessions> Cases_CaseSessions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseTransfer> Cases_CaseTransfer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseTransfer> Cases_CaseTransfer1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_SessionDecision> Cases_SessionDecision { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_SessionDecision> Cases_SessionDecision1 { get; set; }

        public virtual Configurations_CaseTypes Configurations_CaseTypes { get; set; }

        public virtual CourtConfigurations_Circuits CourtConfigurations_Circuits { get; set; }

        public virtual Security_Users Security_Users { get; set; }
    }
}
