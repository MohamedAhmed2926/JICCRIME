namespace JIC.Crime.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CaseAction_UserType
    {
        public int ID { get; set; }

        public int UserTypeID { get; set; }

        public byte CaseActionID { get; set; }

        public int ActionTypeID { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        [StringLength(50)]
        public string LastModifiedBy { get; set; }

        public virtual CaseAction CaseAction { get; set; }

        public virtual Configurations_Lookups Configurations_Lookups { get; set; }

        public virtual Security_UserTypes Security_UserTypes { get; set; }
    }
}
