namespace JIC.Fault.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Configurations_Prosecutions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Configurations_Prosecutions()
        {
            Cases_MasterCase = new HashSet<Cases_MasterCase>();
            Configurations_PoliceStations = new HashSet<Configurations_PoliceStations>();
            Configurations_Prosecuters = new HashSet<Configurations_Prosecuters>();
            Configurations_Prosecutions1 = new HashSet<Configurations_Prosecutions>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int? ParentID { get; set; }

        public int CourtID { get; set; }

        [Required]
        [StringLength(2)]
        public string Code { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_MasterCase> Cases_MasterCase { get; set; }

        public virtual Configurations_Courts Configurations_Courts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Configurations_PoliceStations> Configurations_PoliceStations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Configurations_Prosecuters> Configurations_Prosecuters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Configurations_Prosecutions> Configurations_Prosecutions1 { get; set; }

        public virtual Configurations_Prosecutions Configurations_Prosecutions2 { get; set; }
    }
}
