using JIC.Services.ServicesInterfaces;
using JIC.Base.Views;
using JIC.Crime.View.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Validations
{
    public class Unique : ValidationAttribute
    {
        private UniqueTypes Type;
        private IPersonService PersonService;
        public Unique(UniqueTypes Type)
        {
            this.Type = Type;
            this.PersonService = UnityConfig.GetService<IPersonService>();
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Models.vw_PersonData Person = validationContext.ObjectInstance as Models.vw_PersonData;
            long? ignoreID = (Person.ID == 0 ? (long?)null : Person.ID);
            switch (Type)
            {
                case UniqueTypes.NationalNo:
                    if (!String.IsNullOrEmpty(Person.NatNo) && PersonService.IsNationalNoExist(Person.NatNo, ignoreID))
                        return new ValidationResult("الرقم القومى مسجل من قبل");
                break;
                case UniqueTypes.PassportNo:
                    if (!String.IsNullOrEmpty(Person.PassportNo) && PersonService.IsPassportNoExist(Person.PassportNo, ignoreID))
                        return new ValidationResult("رقم جواز السفر مسجل من قبل");

                    break;
            }
            return ValidationResult.Success;
        }
    }
    public enum UniqueTypes
    {
        NationalNo,
        PassportNo
    }
}