namespace JIC.Crime.Entities.Models
{
    using JIC.Base.Entities.Models;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public partial class Security_Users : User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Security_Users()
        {
            Cases_CaseTransfer = new HashSet<Cases_CaseTransfer>();
            CourtConfigurations_CircuitMembers = new HashSet<CourtConfigurations_CircuitMembers>();
            CourtConfigurations_CircuitRolls = new HashSet<CourtConfigurations_CircuitRolls>();
            CourtConfigurations_Circuits = new HashSet<CourtConfigurations_Circuits>();
            CourtConfigurations_Circuits1 = new HashSet<CourtConfigurations_Circuits>();
            Security_UsersLoginFailure = new HashSet<Security_UsersLoginFailure>();
            Cases_CaseSessions = new HashSet<Cases_CaseSessions >();
        }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        public int UserTypeID { get; set; }

        public int? CourtID { get; set; }

        public int? TitleID { get; set; }

        public bool Active { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        [StringLength(50)]
        public string LastModifiedBy { get; set; }

        public int? FailedPWCount { get; set; }

        [StringLength(16)]
        public string MobileNo { get; set; }
        
        public long? PersonsId { get; set; }

        public bool? Locked { get; set; }
      //  public bool? MadeOperations { get; set; }

        public DateTime? ActiveDateFrom { get; set; }

        public DateTime? ActiveDateTo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_CaseTransfer> Cases_CaseTransfer { get; set; }

        public virtual Configurations_Courts Configurations_Courts { get; set; }

        public virtual Configurations_Persons Configurations_Persons { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourtConfigurations_CircuitMembers> CourtConfigurations_CircuitMembers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourtConfigurations_CircuitRolls> CourtConfigurations_CircuitRolls { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourtConfigurations_Circuits> CourtConfigurations_Circuits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourtConfigurations_Circuits> CourtConfigurations_Circuits1 { get; set; }
      //  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<CourtConfigurations_TextPredictions> CourtConfigurations_TextPredictions { get; set; }

        public virtual Security_UserTypes Security_UserTypes { get; set; }

        public virtual Security_UserTypes Security_UserTypes1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Security_UsersLoginFailure> Security_UsersLoginFailure { get; set; }
        public int? ProsecutionID { get; set; }
        public int? LevelID { get; set; }
     //   public virtual Cases_CaseSessions Cases_CaseSessions { get; set; }
        public virtual ICollection<Cases_CaseSessions> Cases_CaseSessions { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Security_Users, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
