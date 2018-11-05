namespace JIC.Fault.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cases_DefendantsDecision
    {
        public long ID { get; set; }

        public long CaseDefendantId { get; set; }

        public long SessionDessionId { get; set; }

        public bool IsGuilty { get; set; }

        public string PunishmentDetails { get; set; }

        public int? RestrictionNo { get; set; }

        public int? RestrictionYear { get; set; }

        public int? PunishmentType { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        [StringLength(50)]
        public string LastModifiedBy { get; set; }

        public virtual Cases_CaseDefendants Cases_CaseDefendants { get; set; }

        public virtual Configurations_Lookups Configurations_Lookups { get; set; }

        public virtual Cases_SessionDecision Cases_SessionDecision { get; set; }
    }
}
