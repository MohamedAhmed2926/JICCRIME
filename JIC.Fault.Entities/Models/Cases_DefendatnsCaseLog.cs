namespace JIC.Fault.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cases_DefendatnsCaseLog
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cases_DefendatnsCaseLog()
        {
            Cases_DefendantsCharges = new HashSet<Cases_DefendantsCharges>();
        }

        public long ID { get; set; }

        public long DefendantID { get; set; }

        public int PoliceStationStatusID { get; set; }

        [Column(TypeName = "date")]
        public DateTime FromDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ToDate { get; set; }

        public virtual Cases_CaseDefendants Cases_CaseDefendants { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_DefendantsCharges> Cases_DefendantsCharges { get; set; }

        public virtual Configurations_Lookups Configurations_Lookups { get; set; }
    }
}
