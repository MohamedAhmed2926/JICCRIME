namespace JIC.Fault.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Configurations_Courts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Configurations_Courts()
        {
            CourtConfigurations_Circuits = new HashSet<CourtConfigurations_Circuits>();
            Configurations_Courts1 = new HashSet<Configurations_Courts>();
            Configurations_Prosecutions = new HashSet<Configurations_Prosecutions>();
            Security_Users = new HashSet<Security_Users>();
            Configurations_CaseTypes = new HashSet<Configurations_CaseTypes>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int? ParentID { get; set; }

        public int CourtLevelID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourtConfigurations_Circuits> CourtConfigurations_Circuits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Configurations_Courts> Configurations_Courts1 { get; set; }

        public virtual Configurations_Courts Configurations_Courts2 { get; set; }

        public virtual Configurations_Lookups Configurations_Lookups { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Configurations_Prosecutions> Configurations_Prosecutions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Security_Users> Security_Users { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Configurations_CaseTypes> Configurations_CaseTypes { get; set; }
    }
}
