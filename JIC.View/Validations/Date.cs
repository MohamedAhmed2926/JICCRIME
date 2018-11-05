using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Validations
{
    public class Date : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value != null)
            {
                if (value.ToString().Length > 4 || value.ToString().Length < 4)
                    return new ValidationResult(JIC.Base.Resources.Messages.InValidYearFormat);
                else
                    return ValidationResult.Success;
            }
            else
                return ValidationResult.Success;
        }
    }
    public class DateNotLessThanToday : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime output = DateTime.Now;
            CultureInfo provider = CultureInfo.InvariantCulture;
            if (value != null && DateTime.TryParseExact(value.ToString(), JIC.Base.SystemConfigurations.DateTime_ShortDateFormat, provider, DateTimeStyles.None, out output))
            {
                if (output.Date < DateTime.Now.Date)
                    return new ValidationResult(JIC.Base.Resources.Messages.MinDateToday);
                else
                    return ValidationResult.Success;
            }
            else
                return ValidationResult.Success;
        }
    }
    public class StrDateBeforeToday : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime output = DateTime.Now;
            CultureInfo provider = CultureInfo.InvariantCulture;
            if (value != null && DateTime.TryParseExact(value.ToString(), JIC.Base.SystemConfigurations.DateTime_ShortDateFormat, provider, DateTimeStyles.None, out output))
            {
                if (output > DateTime.Now)
                    return new ValidationResult("لا بد من إدخال تاريخ سابق لتاريخ اليوم");
                else
                    return ValidationResult.Success;
            }
            return ValidationResult.Success;

        }
    }
    public class MaxNextYear : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                int Year, maxYear;
                DateTime SelectedDate = Convert.ToDateTime(value);
                DateTime dateNow = DateTime.Now;
                if (dateNow.Month > 9)
                    Year = dateNow.Year + 1;
                else
                    Year = dateNow.Year;
                maxYear = Year + 1;
                if (SelectedDate.Year > maxYear)
                    return new ValidationResult(JIC.Base.Resources.Messages.InValidDate);
                else if (SelectedDate.Year == maxYear && SelectedDate.Month > 9)
                    return new ValidationResult(JIC.Base.Resources.Messages.InvalidDateMaxNextYear);
                else
                    return ValidationResult.Success;
            }
            catch(Exception ex)
            {
                return new ValidationResult(JIC.Base.Resources.Messages.InValidDate);
            }
        }
    }
}