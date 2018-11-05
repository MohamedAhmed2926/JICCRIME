using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Crime.Entities.Models
{
    public class CaseLawyers
    {
        [Required]
        [Key]
       
        public int ID { get; set; }
        [Required]

        public int LevelID { get; set; }
        [Index(IsUnique = true)]
        public long PersonID { get; set; }

        [Required]
        
        public string CardNumber { get; set; }


        public virtual Configurations_Persons Configurations_Personss { get; set; }

        public virtual Configurations_Lookups Configurations_Lookupss { get; set; }
    }
}
