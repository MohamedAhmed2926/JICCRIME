namespace JIC.Crime.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cases_CrimeTypes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cases_CrimeTypes()
        {
            Configurations_Courts_Crimes = new HashSet<Configurations_Courts_Crimes>();
            Cases_MasterCases = new HashSet<Cases_MasterCase>();
            CourtConfigurations_Circuits = new HashSet<CourtConfigurations_Circuits>();
         //   CourtConfigurations_TextPredictions = new HashSet<CourtConfigurations_TextPredictions>();
        }

        public int ID { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(2)]
        public string Code { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Configurations_Courts_Crimes> Configurations_Courts_Crimes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_MasterCase> Cases_MasterCases { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourtConfigurations_Circuits> CourtConfigurations_Circuits { get; set; }

    //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
   //     public virtual ICollection<CourtConfigurations_TextPredictions> CourtConfigurations_TextPredictions { get; set; }
    }
}
