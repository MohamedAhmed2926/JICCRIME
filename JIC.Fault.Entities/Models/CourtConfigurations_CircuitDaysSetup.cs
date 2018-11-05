namespace JIC.Fault.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CourtConfigurations_CircuitDaysSetup
    {
        public long ID { get; set; }

        [Required]
        [StringLength(50)]
        public string DayOfWeek { get; set; }

        public int CircuitID { get; set; }

        public int CaseTypeID { get; set; }

        public int SecretaryID { get; set; }

        public virtual Configurations_CaseTypes Configurations_CaseTypes { get; set; }

        public virtual CourtConfigurations_Circuits CourtConfigurations_Circuits { get; set; }

        public virtual Security_Users Security_Users { get; set; }
    }
}
