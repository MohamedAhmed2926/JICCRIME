namespace JIC.Crime.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Configurations_Lookups
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Configurations_Lookups()
        {
            CaseAction_UserType = new HashSet<CaseAction_UserType>();
            Cases_CaseDefendants = new HashSet<Cases_CaseDefendants>();
            Cases_CaseDocuments = new HashSet<Cases_CaseDocuments>();
            Cases_Cases = new HashSet<Cases_Cases>();
            Cases_Cases1 = new HashSet<Cases_Cases>();
            Cases_Cases2 = new HashSet<Cases_Cases>();
            Cases_Cases3 = new HashSet<Cases_Cases>();
            Cases_CaseTransfer = new HashSet<Cases_CaseTransfer>();
            Cases_DefendantsCharges = new HashSet<Cases_DefendantsCharges>();
            Cases_DefendantsDecision = new HashSet<Cases_DefendantsDecision>();
            Cases_DefendatnsCaseLog = new HashSet<Cases_DefendatnsCaseLog>();
            Cases_DefendatnsSessionsLog = new HashSet<Cases_DefendatnsSessionsLog>();
            Cases_DefendatnsSessionsLog1 = new HashSet<Cases_DefendatnsSessionsLog>();
            Cases_MasterCase = new HashSet<Cases_MasterCase>();
            Cases_VictimsSessionsLog = new HashSet<Cases_VictimsSessionsLog>();
            Configurations_Courts = new HashSet<Configurations_Courts>();
            Configurations_OrganizationDetails = new HashSet<Configurations_OrganizationDetails>();
            CourtConfigurations_CircuitMembers = new HashSet<CourtConfigurations_CircuitMembers>();
            CourtConfigurations_Circuits1 = new HashSet<CourtConfigurations_Circuits>();
            Configurations_OrganizationDetails1 = new HashSet<Configurations_OrganizationDetails>();
            Configurations_Persons = new HashSet<Configurations_Persons>();
            Configurations_Persons1 = new HashSet<Configurations_Persons>();
            Security_UsersLoginFailure = new HashSet<Security_UsersLoginFailure>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int CategoryID { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        [StringLength(50)]
        public string LastModifiedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CaseAction_UserType> CaseAction_UserType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseDefendants> Cases_CaseDefendants { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseDocuments> Cases_CaseDocuments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_Cases> Cases_Cases { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_Cases> Cases_Cases1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_Cases> Cases_Cases2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_Cases> Cases_Cases3 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseTransfer> Cases_CaseTransfer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_DefendantsCharges> Cases_DefendantsCharges { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_DefendantsDecision> Cases_DefendantsDecision { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_DefendatnsCaseLog> Cases_DefendatnsCaseLog { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_DefendatnsSessionsLog> Cases_DefendatnsSessionsLog { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_DefendatnsSessionsLog> Cases_DefendatnsSessionsLog1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_MasterCase> Cases_MasterCase { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_VictimsSessionsLog> Cases_VictimsSessionsLog { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Configurations_Courts> Configurations_Courts { get; set; }

        public virtual Configurations_LookupCategories Configurations_LookupCategories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Configurations_OrganizationDetails> Configurations_OrganizationDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourtConfigurations_CircuitMembers> CourtConfigurations_CircuitMembers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourtConfigurations_Circuits> CourtConfigurations_Circuits1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Configurations_OrganizationDetails> Configurations_OrganizationDetails1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Configurations_Persons> Configurations_Persons { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Configurations_Persons> Configurations_Persons1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Security_UsersLoginFailure> Security_UsersLoginFailure { get; set; }
     //   [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
     //   public virtual ICollection<CourtConfigurations_TextPredictions> CourtConfigurations_TextPredictions { get; set; }
    }
}
