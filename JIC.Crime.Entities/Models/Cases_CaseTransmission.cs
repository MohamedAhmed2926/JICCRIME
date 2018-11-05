namespace JIC.Crime.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cases_CaseTransmission
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TransmissionID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CaseID { get; set; }

        [Key]
        [Column(Order = 2)]
        public bool IsActive { get; set; }

        public virtual Cases_Cases Cases_Cases { get; set; }

        public virtual Cases_CasesTransmissionRequests Cases_CasesTransmissionRequests { get; set; }
    }
}
