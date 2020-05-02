using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.CustomValidation
{
    public class EventDateCompareValidation : ValidationAttribute
    {
        public string DateEventFirst { get; set; }
        public DateTime DateEventSecond { get; set; }
        public EventDateCompareValidation(string firstDate) : base("{0} must be after {1}")
        {
            DateEventFirst = firstDate;
        }
        public string FormatErrorMessage(string name, string otherName)
        {
            return string.Format(ErrorMessageString, name, otherName);
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var secondDateComparable = value as IComparable;
            var firstDateComparable = GetFirstComparable(validationContext);
            if (firstDateComparable != null && secondDateComparable != null)
            {
                if (firstDateComparable.CompareTo(secondDateComparable) <= 0)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    string secondDateDisplayName = validationContext.ObjectInstance.GetType().GetProperty(DateEventFirst).Name;
                    string firstDateDisplayName = validationContext.DisplayName;

                    return new ValidationResult(
                        FormatErrorMessage(firstDateDisplayName, secondDateDisplayName));
                }
            }
            else
            {
                return new ValidationResult(
                        FormatErrorMessage("Something went wrong. Please make sure to select a valid date."));
            }
        }
        protected IComparable GetFirstComparable(
        ValidationContext validationContext)
        {
            var propertyInfo = validationContext
                                  .ObjectType
                                  .GetProperty(DateEventFirst);
            if (propertyInfo != null)
            {
                var firstDate = propertyInfo.GetValue(
                    validationContext.ObjectInstance, null);
                return firstDate as IComparable;
            }
            return null;
        }
    }
}
