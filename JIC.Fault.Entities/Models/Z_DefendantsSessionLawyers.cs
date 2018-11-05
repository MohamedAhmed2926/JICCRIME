namespace JIC.Fault.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Z_DefendantsSessionLawyers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Z_DefendantsSessionLawyers()
        {
            Z_LawyerAttorneys = new HashSet<Z_LawyerAttorneys>();
        }

        public long ID { get; set; }

        public long DefendantID { get; set; }

        public long SessionID { get; set; }

        public int LawyerID { get; set; }

        public virtual Cases_CaseDefendants Cases_CaseDefendants { get; set; }

        public virtual Cases_CaseSessions Cases_CaseSessions { get; set; }

        public virtual Z_Lawyers Z_Lawyers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Z_LawyerAttorneys> Z_LawyerAttorneys { get; set; }
    }
}
