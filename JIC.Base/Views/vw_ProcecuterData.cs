using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.views
{
    public class vw_ProcecuterData
    {
        public int ID { get; set; }
        [Display(Name = "Prosecution", ResourceType = typeof(Base.Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "EnterProsecution")]

        public int ProcecutionID { get; set; }



        [Display(Name = "ProsecuterName", ResourceType = typeof(Base.Resources.Resources))]

        [Required(ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "EnterProsecutor")]
        [RegularExpression(@"^[\u0621-\u064A\040]+$", ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "wrongName")]

        public string ProcecutoerName { get; set; }
        [Display(Name = "NationalId", ResourceType = typeof(Base.Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "EnterNationalID")]

        [RegularExpression(@"(2|3)[0-9][1-9][0-1][1-9][0-3][1-9](01|02|03|04|11|12|13|14|15|16|17|18|19|21|22|23|24|25|26|27|28|29|31|32|33|34|35|88)\d\d\d\d\d", ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "NationalIDNotValid")]

        public string NationalID { get; set; }
 
        public string ProsecutionName { get; set; }
 
    }
}