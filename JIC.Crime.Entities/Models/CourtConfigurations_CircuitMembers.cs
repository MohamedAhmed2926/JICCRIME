namespace JIC.Crime.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CourtConfigurations_CircuitMembers
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public int CircuitID { get; set; }

        [Column(TypeName = "date")]
        public DateTime FromDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ToDate { get; set; }

        public int? JudgeType { get; set; }

        public virtual Configurations_Lookups Configurations_Lookups { get; set; }

        public virtual Security_Users Security_Users { get; set; }

        public virtual CourtConfigurations_Circuits CourtConfigurations_Circuits { get; set; }
    }
}
