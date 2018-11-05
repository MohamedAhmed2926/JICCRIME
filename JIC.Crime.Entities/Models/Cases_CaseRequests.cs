namespace JIC.Crime.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cases_CaseRequests
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cases_CaseRequests()
        {
            Cases_CaseDocuments = new HashSet<Cases_CaseDocuments>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int CaseID { get; set; }

        public int RequestType { get; set; }

        public int RequestNumber { get; set; }

        public int RequestYear { get; set; }

        public int CourtID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RequestDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime ProsecutionNoteDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RevocationDate { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        [StringLength(50)]
        public string LastModifiedBy { get; set; }

        public virtual Cases_Cases Cases_Cases { get; set; }

        public virtual Configurations_Courts Configurations_Courts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseDocuments> Cases_CaseDocuments { get; set; }
    }
}
