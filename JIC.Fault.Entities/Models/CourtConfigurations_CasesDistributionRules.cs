namespace JIC.Fault.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CourtConfigurations_CasesDistributionRules
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CourtConfigurations_CasesDistributionRules()
        {
            CourtConfigurations_DistributionDates = new HashSet<CourtConfigurations_DistributionDates>();
            CourtConfigurations_DistributionNumbers = new HashSet<CourtConfigurations_DistributionNumbers>();
            CourtConfigurations_Circuits1 = new HashSet<CourtConfigurations_Circuits>();
        }

        public int ID { get; set; }

        public int DestinationCircuitID { get; set; }

        public bool IsContinuseCircuit { get; set; }

        public virtual CourtConfigurations_Circuits CourtConfigurations_Circuits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourtConfigurations_DistributionDates> CourtConfigurations_DistributionDates { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourtConfigurations_DistributionNumbers> CourtConfigurations_DistributionNumbers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourtConfigurations_Circuits> CourtConfigurations_Circuits1 { get; set; }
    }
}
