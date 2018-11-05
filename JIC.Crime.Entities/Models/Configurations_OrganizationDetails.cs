namespace JIC.Crime.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Configurations_OrganizationDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ID { get; set; }

        public int OrganizationTypeID { get; set; }

        public int? OrganizationCategoryID { get; set; }

        [StringLength(50)]
        public string RegistrationNumber { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }

        public virtual Configurations_Lookups Configurations_Lookups { get; set; }

        public virtual Configurations_Lookups Configurations_Lookups1 { get; set; }

        public virtual Configurations_Persons Configurations_Persons { get; set; }
    }
}
