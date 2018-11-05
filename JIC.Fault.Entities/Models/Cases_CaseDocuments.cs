namespace JIC.Fault.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cases_CaseDocuments
    {
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

        public virtual Cases_CaseDocumentFolders Cases_CaseDocumentFolders { get; set; }

        public virtual Configurations_Lookups Configurations_Lookups { get; set; }
    }
}
