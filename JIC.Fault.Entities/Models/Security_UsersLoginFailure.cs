namespace JIC.Fault.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Security_UsersLoginFailure
    {
        public long ID { get; set; }

        public int UserID { get; set; }

        public int? FallID { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        [StringLength(50)]
        public string LastModifiedBy { get; set; }

        public virtual Configurations_Lookups Configurations_Lookups { get; set; }

        public virtual Security_Users Security_Users { get; set; }
    }
}
