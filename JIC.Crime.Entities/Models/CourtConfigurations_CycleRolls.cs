namespace JIC.Crime.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CourtConfigurations_CycleRolls
    {
        public int ID { get; set; }

        public int CycleID { get; set; }
        [Index("CycleRolls_Uniq_Index", 2, IsUnique = true)]
        [Column(TypeName = "date")]
        public DateTime RollDate { get; set; }
        [Index("CycleRolls_Uniq_Index", 1, IsUnique = true)]
        public int CourtID { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        [StringLength(50)]
        public string LastModifiedBy { get; set; }

        public virtual Configurations_Courts Configurations_Courts { get; set; }

        public virtual CourtConfigurations_Cycles CourtConfigurations_Cycles { get; set; }
    }
}
