namespace JIC.Crime.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cases_DocumentsLog
    {
        public int ID { get; set; }

        public Guid SessionFolderID { get; set; }

        [Required]
        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [StringLength(50)]
        public string ActionType { get; set; }

        [Required]
        public string ActionDetails { get; set; }

        public Guid? FolderID { get; set; }

        public Guid? FileID { get; set; }
    }
}
