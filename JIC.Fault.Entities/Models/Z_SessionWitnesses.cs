namespace JIC.Fault.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Z_SessionWitnesses
    {
        public int ID { get; set; }

        public long PersonID { get; set; }

        public long SessionID { get; set; }

        public string Testimony { get; set; }

        public byte[] TestimonyFileData { get; set; }

        public virtual Cases_CaseSessions Cases_CaseSessions { get; set; }

        public virtual Configurations_Persons Configurations_Persons { get; set; }
    }
}
