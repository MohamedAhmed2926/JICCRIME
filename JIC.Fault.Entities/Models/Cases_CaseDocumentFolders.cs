namespace JIC.Fault.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cases_CaseDocumentFolders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cases_CaseDocumentFolders()
        {
            Cases_CaseDocuments = new HashSet<Cases_CaseDocuments>();
        }

        public Guid ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int CaseID { get; set; }

        public long? SessionID { get; set; }

        public Guid? ParentFolderID { get; set; }

        public DateTime Date { get; set; }

        public int? DocumentsCount { get; set; }

        public int? ComputedDocumentsCount { get; set; }

        public int? ComputedFoldersCount { get; set; }

        public virtual Cases_CaseSessions Cases_CaseSessions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseDocuments> Cases_CaseDocuments { get; set; }

        public virtual Cases_Cases Cases_Cases { get; set; }
    }
}
