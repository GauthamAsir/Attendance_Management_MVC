using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PresentationMVC.Validations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DateValidator : ValidationAttribute
    {

        private const string InvalidDate = "Please enter a valid date";

        private const string GreaterDate = "Your age should be greater than or equal to 18 years";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                DateTime dateTime = Convert.ToDateTime(value);

                if (dateTime >= DateTime.Now.AddYears(-18))
                {
                    return new ValidationResult(GreaterDate);
                }

                return ValidationResult.Success;

            }
            catch
            {
                return new ValidationResult(InvalidDate);
            }
        }

    }
}