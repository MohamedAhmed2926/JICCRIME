namespace JIC.Crime.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Configurations_PoliceStations
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Configurations_PoliceStations()
        {
            Cases_MasterCase = new HashSet<Cases_MasterCase>();
            CourtConfigurations_CircuitsPoliceStation = new HashSet<CourtConfigurations_CircuitsPoliceStation>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int ProsecutionID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_MasterCase> Cases_MasterCase { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourtConfigurations_CircuitsPoliceStation> CourtConfigurations_CircuitsPoliceStation { get; set; }

        public virtual Configurations_Prosecutions Configurations_Prosecutions { get; set; }
    }
}
