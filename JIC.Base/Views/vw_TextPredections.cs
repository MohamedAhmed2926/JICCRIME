using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
   public class vw_TextPredections
    {
        public int TextID { get; set; }
        //  [Display(Name = "CrimeType", ResourceType = typeof(Base.Resources.Resources))]
        //    [Required(ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        //    public int CrimeTypeID { get; set; }

        //  public string CrimeTypeName { get; set; }

        [Display(Name = "TextPredectionsDescription", ResourceType = typeof(Base.Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        public string TextPredectionsDescription { get; set; }

        [Display(Name = "TextTitle", ResourceType = typeof(Base.Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        public string TextTitle { get; set; }
        public int  UserID { get; set; }
        [Display(Name = "CircuitName", ResourceType = typeof(Base.Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        public int CircuitID { get; set; }
        [Display(Name = "CircuitName", ResourceType = typeof(Base.Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Base.Resources.Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        public string CircuitName { get; set; }

      
    }
}
