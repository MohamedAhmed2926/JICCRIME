namespace JIC.Fault.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cases_MasterCase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cases_MasterCase()
        {
            Cases_Cases = new HashSet<Cases_Cases>();
        }

        public int ID { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstLevelNumber { get; set; }

        [Required]
        [StringLength(4)]
        public string FirstLevelYear { get; set; }

        public int PoliceStationID { get; set; }

        [StringLength(100)]
        public string SecondLevelNumber { get; set; }

        [StringLength(4)]
        public string SecondLevelYear { get; set; }

        public int? ProsecutionID { get; set; }

        public int CaseTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string NationalID { get; set; }

        public int CrimeID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_Cases> Cases_Cases { get; set; }

        public virtual Configurations_Lookups Configurations_Lookups { get; set; }

        public virtual Configurations_CaseTypes Configurations_CaseTypes { get; set; }

        public virtual Configurations_PoliceStations Configurations_PoliceStations { get; set; }

        public virtual Configurations_Prosecutions Configurations_Prosecutions { get; set; }
    }
}
