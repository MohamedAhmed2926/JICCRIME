namespace JIC.Fault.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Z_Lawyers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Z_Lawyers()
        {
            Z_DefendantsSessionLawyers = new HashSet<Z_DefendantsSessionLawyers>();
            Z_LawyerAttorneys = new HashSet<Z_LawyerAttorneys>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        public long PersonID { get; set; }

        [Required]
        [StringLength(50)]
        public string IDNumber { get; set; }

        public int DegreeID { get; set; }

        public virtual Configurations_Lookups Configurations_Lookups { get; set; }

        public virtual Configurations_Persons Configurations_Persons { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Z_DefendantsSessionLawyers> Z_DefendantsSessionLawyers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Z_LawyerAttorneys> Z_LawyerAttorneys { get; set; }
    }
}
