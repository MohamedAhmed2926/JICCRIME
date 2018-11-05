using JIC.Base;
using JIC.Base.Resources;
using JIC.Base.Views;
using JIC.Crime.View.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace JIC.Crime.View.Models
{
    public class AttachmentViewModel
    {
        public List<vw_KeyValueStringID> FoldersList { get; set; }
        public List<vw_KeyValue> AttachmentTypes { get; set; }
        public Guid AttachmentID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "نوع الملف")]

        
        public int AttachmentTypeID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        public AttachmentTypes AttachmentType { get; set; }
        [Display(Name = "إسم الملف")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        public string Name { get; set; }
        [Display(Name = "الحافظة")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        public Guid? FolderID { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        public string filePath { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        //public string vpath { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        public string FileName { get; set; }
        public int CaseID { get; set; }
        public long? SessionID { get; set; }
        //[Required]
        //public HttpPostedFileBase attachment { get; set; }
    }


    public abstract class AttachmentBase
    {
        public Guid AttachmentID { get; set; }
        public virtual string Name { get; set; }
        public int? CaseID { get; set; }
        public long? SessionID { get; set; }

    }

    public class FileAttachment : AttachmentBase
    {
        public string AttachmentType { get; set; }
        public DateTime UploadDate { get; set; }
        public string AttachmentURl { get; set; }
      
    }

    public class FolderAttachment : AttachmentBase
    {
        public List<AttachmentBase> Attachments { get; set; } = new List<AttachmentBase>();
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        public int? FolderCount { get; set; }
    }

    public class AttachmentFolderViewModel
    {
        public List<FolderAttachment> Attachments { get; set; } = new List<FolderAttachment>();
        public int FolderCount { get; set; }
        public string AcordianTitle { get; set; }
        public int CaseID { get; set; }
        public long? SessionID { get; set; }
    }

    public class FolderViewModel : AttachmentBase
    {
        [Display(Name = "إسم الحافظة")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        public override string Name { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        [GreaterThan(0)]
        [Display(Name = "عدد المستندات")]
        public int? FolderCount { get; set; }
    }
    public class FullFolderViewModel
    {
        public List<FolderViewModel> FolderViewModel { get; set; }
        public FolderViewModel FolderModel { get; set; }
        public int CaseID { get; set; }
        public long? SessionID { get; set; }
    }

    public class AddAttachmentViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        public AttachmentTypes AttachmentType { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        public string filePath { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        public string vpath { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        public string FileName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "إسم الملف")]
        public string Name { get; set; }
        [Display(Name = "الحافظة")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredErrorMessage")]

        public Guid? FolderID { get; set; }
        public int UserTypeID { get; set; }
        public int CaseID { get; set; }
        public List<vw_KeyValueStringID> FoldersList { get; set; }
        public List<vw_KeyValue> AttachmentTypes { get; set; }
        public long? SessionID { get; set; }
        public int CircuitID { get; set; }
    }
    public static class ExtensionMethods
    {
        public static bool HasFile(this HttpPostedFileBase file)
        {
            return file != null && file.ContentLength > 0;
        }
    }
}