namespace JIC.Crime.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Configurations_CaseTypes
    {
        public int ID { get; set; }

        [StringLength(10)]
        public string Name { get; set; }

        [Required]
        [StringLength(2)]
        public string Code { get; set; }
    }
}
