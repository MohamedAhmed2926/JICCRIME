using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Crime.View.Models
{
   public class ProsecutorViewModels
    {
        public int ID { get; set; }
        [Display(Name = "Prosecution", ResourceType = typeof(Base.Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        public int ProcecutionID { get; set; }

        [Display(Name = "ProsecuterName", ResourceType = typeof(Base.Resources.Resources))]

        [Required(ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        [RegularExpression(@"^[\u0621-\u064A\040]+$", ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "InvalidCharacterField")]

        public string ProcecutoerName { get; set; }
        [Display(Name = "NationalId", ResourceType = typeof(Base.Resources.Resources))]
        
        [RegularExpression(@"(2|3)[0-9][0-9][0-1][0-9][0-3][0-9](01|02|03|04|11|12|13|14|15|16|17|18|19|21|22|23|24|25|26|27|28|29|31|32|33|34|35|88)\d\d\d\d\d", ErrorMessageResourceType = typeof(Base.Resources.Resources), ErrorMessageResourceName = "NationalIdNotValid")]

        public string NationalID { get; set; }

        public int PersonID { get; set; }
        [Display(Name = "Prosecution", ResourceType = typeof(Base.Resources.Resources))]
         
        public string ProsecutionName { get; set; }

    }

    public class ProsecutorCreateViewModel
    {
      public  ProsecutorViewModels ProsecutorModel { get; set; }
      public List<ProsecutionViewModels> ListProsecutionModel { get; set; }

    }

    }
