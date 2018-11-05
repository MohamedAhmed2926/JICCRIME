namespace JIC.Crime.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cases_CaseDocuments
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cases_CaseDocuments()
        {
            Cases_CaseObtainments = new HashSet<Cases_CaseObtainments>();
            Cases_CaseRequests = new HashSet<Cases_CaseRequests>();
            Cases_CasesTransmissionRequests = new HashSet<Cases_CasesTransmissionRequests>();
            Cases_CaseObtainments1 = new HashSet<Cases_CaseObtainments>();
        }

        public Guid ID { get; set; }

        public int AttachmentTypeID { get; set; }

        [StringLength(100)]
        public string DocumentTitle { get; set; }

        public DateTime UploadDate { get; set; }

        public int UploadedBy { get; set; }

        [Required]
        [StringLength(500)]
        public string FileName { get; set; }

        [Required]
        public byte[] FileData { get; set; }

        public Guid? FolderID { get; set; }
        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        [StringLength(50)]
        public string LastModifiedBy { get; set; }

        public virtual Cases_CaseDocumentFolders Cases_CaseDocumentFolders { get; set; }

        public virtual Configurations_Lookups Configurations_Lookups { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseObtainments> Cases_CaseObtainments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseRequests> Cases_CaseRequests { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CasesTransmissionRequests> Cases_CasesTransmissionRequests { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseObtainments> Cases_CaseObtainments1 { get; set; }
    }
}
