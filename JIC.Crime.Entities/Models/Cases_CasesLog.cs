namespace JIC.Crime.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cases_CasesLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int CaseID { get; set; }

        public int StepID { get; set; }

        public int CircuitID { get; set; }

        public int MasterCaseID { get; set; }

        public int CaseLevel { get; set; }

        public int ProcedureTypeID { get; set; }

        public int CaseStatusID { get; set; }

        public virtual Cases_Cases Cases_Cases { get; set; }
    }
}
