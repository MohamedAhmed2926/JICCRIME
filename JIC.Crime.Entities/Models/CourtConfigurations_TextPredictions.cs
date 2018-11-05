using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Crime.Entities.Models
{
    public partial class CourtConfigurations_TextPredictions
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Phrase { get; set; }

        //     public int UserID { get; set; }

        //  public int CrimeTypeID { get; set; }

          public int CircuitID { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        [StringLength(50)]
        public string LastModifiedBy { get; set; }

        //  public virtual Cases_CrimeTypes Cases_CrimeTypes { get; set; }

        //  public virtual Security_Users Security_Users { get; set; }
        public virtual CourtConfigurations_Circuits CourtConfigurations_Circuits { get; set; }

    }
}
