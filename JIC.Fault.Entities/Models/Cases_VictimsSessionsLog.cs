namespace JIC.Fault.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cases_VictimsSessionsLog
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long VictimID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SessionID { get; set; }

        public int PresenceStatusID { get; set; }

        public virtual Cases_CaseSessions Cases_CaseSessions { get; set; }

        public virtual Cases_CaseVictims Cases_CaseVictims { get; set; }

        public virtual Configurations_Lookups Configurations_Lookups { get; set; }
    }
}
