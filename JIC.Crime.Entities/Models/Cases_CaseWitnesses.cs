namespace JIC.Crime.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cases_CaseWitnesses
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cases_CaseWitnesses()
        {
            Cases_WitnessesCaseLog = new HashSet<Cases_WitnessesCaseLog>();
        }

        public long ID { get; set; }

        public long PersonID { get; set; }

        public int CaseID { get; set; }

        public byte[] TestimonyFileData { get; set; }

        public bool IsActive { get; set; }

        public int UserID { get; set; }

        public virtual Cases_Cases Cases_Cases { get; set; }

        public virtual Configurations_Persons Configurations_Persons { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cases_WitnessesCaseLog> Cases_WitnessesCaseLog { get; set; }
    }
}
