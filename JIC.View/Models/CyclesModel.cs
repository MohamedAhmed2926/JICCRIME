using JIC.Base.Resources;
using JIC.Utilities.MvcHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Models
{
    public class CyclesModel
    {
        public int CourtID { get; set; }
        [Required]
        public ShowMonthOnly Month { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages),ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name ="From" , ResourceType = typeof(JIC.Base.Resources.Resources))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FirstFrom { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "To", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FirstTo { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "From", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SecondFrom { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "To", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SecondTo { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "From", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ThirdFrom { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "To", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ThirdTo { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "From", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FourthFrom { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "To", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FourthTo { get; set; }

        [Display(Name = "From", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FirstSeperatorFrom { get; set; }

        [Display(Name = "To", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FirstSeperatorTo { get; set; }

        [Display(Name = "From", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SecondSeperatorFrom { get; set; }

        [Display(Name = "To", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SecondSeperatorTo { get; set; }
    }
}