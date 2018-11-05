using JIC.Base;
using JIC.Base.Views;
using JIC.Crime.View.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Models
{
    public class UserViewModels
    {
        public string UserName { get; set; }
        public string UserType { get; set; }
        public string PhoneNo { get; set; }
        public int ID { get; internal set; }
        public bool Locked { get; set; }
    }

    public class UserCreateViewModel
    {
        public vw_UserDataModel UserData { get; set; }

        public List<vw_KeyValue> UserTypes { get; set; }
        public List<vw_KeyValue> Courts { get; set; }
        public List<vw_KeyValue> Prosecutions { get; set; }
        public List<vw_KeyValue> JudgeLevels { get; set; }
        public PersonViewModel PersonData { get; set; }
    }

    public class vw_UserDataModel
    {
        public int? ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Base.Resources.Resources), ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name ="UserName",ResourceType = typeof(JIC.Base.Resources.Resources))]
        [RegularExpression("^(?=.*[a-zA-Z\u0600-\u06ff])[a-zA-Z0-9\u0600-\u06ff~!@#$%^&*()]*", ErrorMessage = "هذا الحقل يجب ان يحتوى على حروف وأرقام فقط")]
        [UniqueUserName]
        public string UserName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Base.Resources.Resources), ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "UserTypes",ResourceType =typeof(JIC.Base.Resources.Resources))]
        public SystemUserTypes? UserType { get; set; }
        [Display(Name = "Court", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [RequiredIfCourtUser]
        public int? CourtID { get; set; }
        [Display(Name = "Prosecution", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [RequiredIfProsecutionUser]
        public int? ProsecutionID { get; set; }
        [Display(Name = "JudgLevel", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [RequiredIfCourtUser]
        [RequiredIfJudge]
        public int? UserJudgeLevel { get; set; }
        [Display(Name = "Phone", ResourceType = typeof(JIC.Base.Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Base.Resources.Resources), ErrorMessageResourceName = "RequiredErrorMessage")]
        [RegularExpression("(01)(0|1|2|5)[0-9]{8}", ErrorMessage ="قم بإدخال رقم تليفون صحيح")]
        [IsPhoneExist]
        public string PhoneNo { get; set; }

        internal vw_UserData ToVwUserData()
        {
            return new vw_UserData
            {
                Active = true,
                CourtID = CourtID,
                UserTypeID = (int)UserType,
                UserJudgeLevel = UserJudgeLevel,
                ProsecutionID = ProsecutionID,
                PhoneNo = PhoneNo,
                UserName = UserName,
                ID = ID.HasValue ? ID.Value : 0
            };
        }
    }

    public class vw_UserChangePassword
    {
        [Required(ErrorMessageResourceType = typeof(Base.Resources.Resources), ErrorMessageResourceName = "RequiredErrorMessage")]
        [NotEqualDefaultPassword(ErrorMessage = "كلمة المرور متكررة")]
        [Display(Name ="كلمة المرور")]
        [MinLength(6,ErrorMessage = "كلمة المرور لا يمكن ان تقل عن 6 أحرف")]
        [RegularExpression("^(?=.*[a-zA-Z\u0600-\u06ff])(?=.*[0-9])[a-zA-Z0-9\u0600-\u06ff~!@#$%^&*()]*", ErrorMessage = "كلمة المرور لابد ان تحتوى على حروف وأرقام")]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(Base.Resources.Resources), ErrorMessageResourceName = "RequiredErrorMessage")]
        [Compare("Password", ErrorMessageResourceName = "ValuesDoNotMatch",ErrorMessageResourceType = typeof(Base.Resources.Resources))]
        [Display(Name = "تاكيد كلمة المرور")]
        public string ConfirmPassword { get; set; }
    }
}