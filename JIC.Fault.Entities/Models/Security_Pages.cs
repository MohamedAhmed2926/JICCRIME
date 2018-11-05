namespace JIC.Fault.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Security_Pages
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Security_Pages()
        {
            Security_Actions = new HashSet<Security_Actions>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string Path { get; set; }

        public int ModuleID { get; set; }

        public bool ShowInMenu { get; set; }

        public int OrderIndex { get; set; }

        [Required]
        [StringLength(200)]
        public string RoutingName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Security_Actions> Security_Actions { get; set; }

        public virtual Security_Modules Security_Modules { get; set; }
    }
}
