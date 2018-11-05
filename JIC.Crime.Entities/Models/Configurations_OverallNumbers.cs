namespace JIC.Crime.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Configurations_OverallNumbers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Configurations_OverallNumbers()
        {
            Cases_MasterCase = new HashSet<Cases_MasterCase>();
        }

        public int ID { get; set; }

        public long InclosiveSierial { get; set; }

        public int Year { get; set; }

        public int ProsecutionID { get; set; }

        public bool Active { get; set; }

        public int CourtID { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        [StringLength(50)]
        public string LastModifiedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_MasterCase> Cases_MasterCase { get; set; }

        public virtual Configurations_Prosecutions Configurations_Prosecutions { get; set; }
        public virtual Configurations_Courts Configurations_Courts { get; set; }
    }
}
