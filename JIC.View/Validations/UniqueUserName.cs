using JIC.Services.ServicesInterfaces;
using JIC.Crime.View.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Validations
{
    public class UniqueUserName : ValidationAttribute
    {
        private IUserService userService;
        public UniqueUserName()
        {
            userService = UnityConfig.GetService<IUserService>();
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var User = (vw_UserDataModel)validationContext.ObjectInstance;
            if (userService.IsUserNameExist(User.UserName, User.ID))
                return new ValidationResult(JIC.Base.Resources.Messages.ExistUserName);
            return ValidationResult.Success;
        }
    }

    //public class IsPassporeExist : ValidationAttribute
    //{
    //    private IUserService userService;
    //    public IsPassporeExist()
    //    {
    //        userService = UnityConfig.GetService<IUserService>();
    //    }
    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {

    //        var person = (vw_PersonData)validationContext.ObjectInstance;
    //      //  var User = (vw_UserDataModel)validationContext.ObjectInstance;


    //        if (userService.IsPassporeExist(person.PassportNo, person.))
    //            return new ValidationResult("رقم جواز السفر موجود من قبل");

    //        return ValidationResult.Success;
    //    }
    //}

    public class IsPhoneExist : ValidationAttribute
    {
        private IUserService userService;
        public IsPhoneExist()
        {
            userService = UnityConfig.GetService<IUserService>();
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var User = (vw_UserDataModel)validationContext.ObjectInstance;

            if (userService.IsPhoneExist(User.PhoneNo, User.ID))
                return new ValidationResult("رقم التليفون المحمول موجود من قبل");

            return ValidationResult.Success;
        }
    }
}