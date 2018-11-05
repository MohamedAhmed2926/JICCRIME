namespace JIC.Crime.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cases_VictimsCaseLog
    {
        public long ID { get; set; }

        public long VictimID { get; set; }

        public bool IsCivilRightProsecutor { get; set; }

        [Column(TypeName = "date")]
        public DateTime FromDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ToDate { get; set; }

        public virtual Cases_CaseVictims Cases_CaseVictims { get; set; }
    }
}
