namespace JIC.Crime.Entities.Models
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
            Cases_CaseWitnesses = new HashSet<Cases_CaseWitnesses>();
            Configurations_OrganizationRepresentatives = new HashSet<Configurations_OrganizationRepresentatives>();
            Configurations_OrganizationRepresentatives1 = new HashSet<Configurations_OrganizationRepresentatives>();
            //Lawyers = new HashSet<Lawyers>();
            Security_Users = new HashSet<Security_Users>();
            CaseLawyers = new HashSet<CaseLawyers>();
            Case_Lawyer = new HashSet<Case_Lawyer>();
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseDefendants> Cases_CaseDefendants { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseVictims> Cases_CaseVictims { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseWitnesses> Cases_CaseWitnesses { get; set; }

        public virtual Configurations_Lookups Configurations_Lookups { get; set; }

        public virtual Configurations_Lookups Configurations_Lookups1 { get; set; }

        public virtual Configurations_OrganizationDetails Configurations_OrganizationDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Configurations_OrganizationRepresentatives> Configurations_OrganizationRepresentatives { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Configurations_OrganizationRepresentatives> Configurations_OrganizationRepresentatives1 { get; set; }

  
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Security_Users> Security_Users { get; set; }

       // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
       // public virtual ICollection<Lawyers> Lawyers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CaseLawyers> CaseLawyers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Case_Lawyer> Case_Lawyer { get; set; }
    }
}
