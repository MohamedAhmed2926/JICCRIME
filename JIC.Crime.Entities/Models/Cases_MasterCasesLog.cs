namespace JIC.Crime.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cases_MasterCasesLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int MasterCaseID { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstLevelNumber { get; set; }

        [Required]
        [StringLength(4)]
        public string FirstLevelYear { get; set; }

        public int PoliceStationID { get; set; }

        [Required]
        [StringLength(100)]
        public string SecondLevelNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string SecondLevelYear { get; set; }

        public int ProsecutionID { get; set; }

        public int CrimeID { get; set; }

        public int CrimeType { get; set; }

        public int OverallID { get; set; }

        public virtual Cases_MasterCase Cases_MasterCase { get; set; }
    }
}
