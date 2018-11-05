namespace JIC.Fault.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Configurations_OrganizationRepresentatives
    {
        public int ID { get; set; }

        public long OrganizationID { get; set; }

        public long PersonID { get; set; }

        [Column(TypeName = "date")]
        public DateTime FromDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ToDate { get; set; }

        public virtual Configurations_Persons Configurations_Persons { get; set; }

        public virtual Configurations_Persons Configurations_Persons1 { get; set; }
    }
}
