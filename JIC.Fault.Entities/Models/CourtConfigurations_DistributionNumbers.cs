namespace JIC.Fault.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CourtConfigurations_DistributionNumbers
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RuleID { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte Number { get; set; }

        public virtual CourtConfigurations_CasesDistributionRules CourtConfigurations_CasesDistributionRules { get; set; }
    }
}
