namespace JIC.Fault.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Configurations_Persons
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Configurations_Persons()
        {
            Cases_CaseDefendants = new HashSet<Cases_CaseDefendants>();
            Cases_CaseVictims = new HashSet<Cases_CaseVictims>();
            Configurations_OrganizationRepresentatives = new HashSet<Configurations_OrganizationRepresentatives>();
            Configurations_OrganizationRepresentatives1 = new HashSet<Configurations_OrganizationRepresentatives>();
            Z_Lawyers = new HashSet<Z_Lawyers>();
            Configurations_Prosecuters = new HashSet<Configurations_Prosecuters>();
            Z_SessionWitnesses = new HashSet<Z_SessionWitnesses>();
            Security_Users = new HashSet<Security_Users>();
        }

        public long ID { get; set; }

        [Required]
        [StringLength(255)]
        public string FullName { get; set; }

        [StringLength(14)]
        public string NationalID { get; set; }

        [StringLength(10)]
        public string PassportNumber { get; set; }

        public bool IsLegalPerson { get; set; }

        [StringLength(1000)]
        public string Address { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthdate { get; set; }

        [StringLength(1000)]
        public string JobTitle { get; set; }

        public bool IsEgyption { get; set; }

        public int? NationalityID { get; set; }

        public int? ImprisonStatusID { get; set; }

        [Required]
        [StringLength(255)]
        public string CleanFullName { get; set; }

        public int? ProsectuionPersonID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseDefendants> Cases_CaseDefendants { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseVictims> Cases_CaseVictims { get; set; }

        public virtual Configurations_Lookups Configurations_Lookups { get; set; }

        public virtual Configurations_Lookups Configurations_Lookups1 { get; set; }

        public virtual Configurations_OrganizationDetails Configurations_OrganizationDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Configurations_OrganizationRepresentatives> Configurations_OrganizationRepresentatives { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Configurations_OrganizationRepresentatives> Configurations_OrganizationRepresentatives1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Z_Lawyers> Z_Lawyers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Configurations_Prosecuters> Configurations_Prosecuters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Z_SessionWitnesses> Z_SessionWitnesses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Security_Users> Security_Users { get; set; }
    }
}
