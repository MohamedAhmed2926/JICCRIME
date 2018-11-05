namespace JIC.Crime.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cases_CaseSessions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cases_CaseSessions()
        {
            Cases_CaseDescription = new HashSet<Cases_CaseDescription>();
            Cases_CaseDocumentFolders = new HashSet<Cases_CaseDocumentFolders>();
            Cases_CaseObtainments = new HashSet<Cases_CaseObtainments>();
            Cases_DefendatnsSessionsLog = new HashSet<Cases_DefendatnsSessionsLog>();
            Cases_VictimsSessionsLog = new HashSet<Cases_VictimsSessionsLog>();
            Cases_WitnessSessionLog = new HashSet<Cases_WitnessSessionLog>();

        }

        public long ID { get; set; }

        public int CaseID { get; set; }

        public long RollID { get; set; }

        public bool DoneByDefaultCircuit { get; set; }

        public int RollIndex { get; set; }

        public int? ProsecuterID { get; set; }
        public int? HallID { get; set; }
        public int? SecretaryID { get; set; }
        public string MunitesOfSession { get; set; }

        public bool ApprovedByJudge { get; set; }

        public bool? IsTransferedFromSession { get; set; }

        public bool? IsPendingOnTransfer { get; set; }
        public bool? IsSentToJudge { get; set; }
        public bool? IsTransferedApproved { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseDescription> Cases_CaseDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseDocumentFolders> Cases_CaseDocumentFolders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseObtainments> Cases_CaseObtainments { get; set; }

        public virtual Cases_Cases Cases_Cases { get; set; }

        public virtual CourtConfigurations_CircuitRolls CourtConfigurations_CircuitRolls { get; set; }

        public virtual Configurations_Prosecuters Configurations_Prosecuters { get; set; }
        public virtual Security_Users Security_Users { get; set; }
     //   public virtual ICollection<Security_Users> Security_Users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_DefendatnsSessionsLog> Cases_DefendatnsSessionsLog { get; set; }

        public virtual Cases_SessionDecision Cases_SessionDecision { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_VictimsSessionsLog> Cases_VictimsSessionsLog { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_WitnessSessionLog> Cases_WitnessSessionLog { get; set; }
    }
}
