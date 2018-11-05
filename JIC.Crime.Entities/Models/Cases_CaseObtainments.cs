namespace JIC.Crime.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cases_CaseObtainments
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cases_CaseObtainments()
        {
            Cases_CaseDocuments = new HashSet<Cases_CaseDocuments>();
            Cases_CaseDocuments1 = new HashSet<Cases_CaseDocuments>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string ObtainmentName { get; set; }

        public int ObtainmentNumber { get; set; }

        public int ObtainmentYear { get; set; }

        public int ProsecutionID { get; set; }

        [Required]
        [StringLength(75)]
        public string ObtainmentPerson { get; set; }

        [Required]
        [StringLength(50)]
        public string ObtainmentPersonTitle { get; set; }

        public int SafeNumber { get; set; }

        public int SafeYear { get; set; }

        public int MasterCaseID { get; set; }

        public long? CaseSessionID { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        [StringLength(50)]
        public string LastModifiedBy { get; set; }

        public virtual Cases_CaseSessions Cases_CaseSessions { get; set; }

        public virtual Cases_MasterCase Cases_MasterCase { get; set; }

        public virtual Configurations_Prosecutions Configurations_Prosecutions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseDocuments> Cases_CaseDocuments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseDocuments> Cases_CaseDocuments1 { get; set; }
    }
}
