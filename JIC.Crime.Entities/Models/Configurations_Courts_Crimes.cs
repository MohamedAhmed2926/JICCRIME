namespace JIC.Crime.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Configurations_Courts_Crimes
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int CourtID { get; set; }

        public int CrimeTypeID { get; set; }

        public virtual Cases_CrimeTypes Cases_CrimeTypes { get; set; }

        public virtual Configurations_Courts Configurations_Courts { get; set; }
    }
}
