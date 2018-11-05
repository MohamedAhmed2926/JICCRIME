namespace JIC.Fault.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cases_Resumption_Detail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cases_Resumption_Detail()
        {
            Configurations_Lookups1 = new HashSet<Configurations_Lookups>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CaseID { get; set; }

        public int? SequenceNo { get; set; }

        public int? SequenceYear { get; set; }

        public int? ProsecutionResumptionCause { get; set; }

        [Column(TypeName = "text")]
        public string ProsecutionReport { get; set; }

        [Column(TypeName = "date")]
        public DateTime ResumptionSubmitDate { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        [StringLength(50)]
        public string LastModifiedBy { get; set; }

        public virtual Cases_Cases Cases_Cases { get; set; }

        public virtual Configurations_Lookups Configurations_Lookups { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Configurations_Lookups> Configurations_Lookups1 { get; set; }
    }
}
