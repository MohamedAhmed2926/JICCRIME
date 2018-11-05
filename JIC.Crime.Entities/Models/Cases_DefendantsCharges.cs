namespace JIC.Crime.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cases_DefendantsCharges
    {
        public int ID { get; set; }

        public int ChargeID { get; set; }

        public long DefendantID { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        [StringLength(50)]
        public string LastModifiedBy { get; set; }

        public virtual Cases_CaseDefendants Cases_CaseDefendants { get; set; }

        public virtual Configurations_Lookups Configurations_Lookups { get; set; }
    }
}
