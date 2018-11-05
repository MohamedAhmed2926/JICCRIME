namespace JIC.Fault.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Z_LawyerAttorneys
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Z_LawyerAttorneys()
        {
            Z_DefendantsSessionLawyers = new HashSet<Z_DefendantsSessionLawyers>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Number { get; set; }

        public int TypeID { get; set; }

        public int LawyerID { get; set; }

        [Required]
        public byte[] FileData { get; set; }

        public virtual Configurations_Lookups Configurations_Lookups { get; set; }

        public virtual Z_Lawyers Z_Lawyers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Z_DefendantsSessionLawyers> Z_DefendantsSessionLawyers { get; set; }
    }
}
