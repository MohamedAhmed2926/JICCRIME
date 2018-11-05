namespace JIC.Crime.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cases_MasterCase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cases_MasterCase()
        {
            Cases_CaseObtainments = new HashSet<Cases_CaseObtainments>();
            Cases_Cases = new HashSet<Cases_Cases>();
            Cases_MasterCasesLog = new HashSet<Cases_MasterCasesLog>();
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

        [Required]
        [StringLength(50)]
        public string NationalID { get; set; }

        public int CrimeID { get; set; }

        public int? OverallID { get; set; }

        public bool HasObtainments { get; set; }

        public int CrimeType { get; set; }

        public byte? NotificationActionID { get; set; }
        [DefaultValue("false")]
        public bool IsComplete { get; set; }

        public string OrderOfAssignment { get; set; }

        public virtual CaseAction CaseAction { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseObtainments> Cases_CaseObtainments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_Cases> Cases_Cases { get; set; }

        public virtual Configurations_Lookups MainCrimeLookup { get; set; }

        public virtual Cases_CrimeTypes CrimeTypeLookup { get; set; }

        public virtual Configurations_OverallNumbers Configurations_OverallNumbers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_MasterCasesLog> Cases_MasterCasesLog { get; set; }

        public virtual Configurations_PoliceStations Configurations_PoliceStations { get; set; }

        public virtual Configurations_Prosecutions Configurations_Prosecutions { get; set; }
    }
}
