using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Validators
{
    public class YearValidationAttribute : ValidationAttribute
    {
        public YearValidationAttribute(int year)
        {
            Year = year;
        }

        public int Year { get; set; }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var year = ((DateTime)value).Year;
            if(year < Year)
            {
                return new ValidationResult("Please enter a valid year");
            }
         
            return ValidationResult.Success;
            
        }
    }
}
