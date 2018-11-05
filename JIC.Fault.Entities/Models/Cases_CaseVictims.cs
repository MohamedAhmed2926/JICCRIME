namespace JIC.Fault.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cases_CaseVictims
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cases_CaseVictims()
        {
            Cases_VictimsCaseLog = new HashSet<Cases_VictimsCaseLog>();
            Cases_VictimsSessionsLog = new HashSet<Cases_VictimsSessionsLog>();
        }

        public long ID { get; set; }

        public long PersonID { get; set; }

        public int CaseID { get; set; }

        public bool IsCivilRightProsecutor { get; set; }

        public bool IsActive { get; set; }

        public virtual Cases_Cases Cases_Cases { get; set; }

        public virtual Configurations_Persons Configurations_Persons { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_VictimsCaseLog> Cases_VictimsCaseLog { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_VictimsSessionsLog> Cases_VictimsSessionsLog { get; set; }

        public virtual Service_CaseVictimProsecution Service_CaseVictimProsecution { get; set; }
    }
}
