namespace JIC.Crime.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cases_CaseDescription
    {
        public long ID { get; set; }

        public int CaseID { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string LawItems { get; set; }

        [Column(TypeName = "date")]
        public DateTime FromDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ToDate { get; set; }

        public long? SessionID { get; set; }

        public virtual Cases_Cases Cases_Cases { get; set; }

        public virtual Cases_CaseSessions Cases_CaseSessions { get; set; }
    }
}
