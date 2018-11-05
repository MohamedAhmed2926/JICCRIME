namespace JIC.Crime.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cases_CaseTransfer
    {
        public int ID { get; set; }

        public int CaseID { get; set; }

        public long OldRollID { get; set; }

        public long NewRollID { get; set; }

        public int TransferTypeID { get; set; }

        public bool? Approved { get; set; }

        public int TransferedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        [StringLength(50)]
        public string LastModifiedBy { get; set; }

        public virtual Cases_Cases Cases_Cases { get; set; }

        public virtual Configurations_Lookups Configurations_Lookups { get; set; }

        public virtual CourtConfigurations_CircuitRolls CourtConfigurations_CircuitRolls { get; set; }

        public virtual CourtConfigurations_CircuitRolls CourtConfigurations_CircuitRolls1 { get; set; }

        public virtual Security_Users Security_Users { get; set; }
    }
}
