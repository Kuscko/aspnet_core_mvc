using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Registration.Models
{
    public class MinimumAgeAttribute : ValidationAttribute, IClientModelValidator
    {
        private int minYears;
        public MinimumAgeAttribute(int years) {
            minYears = years;
        }

        protected override ValidationResult IsValid(object value, ValidationContext ctx)
        {
            if (value is DateTime) {
                DateTime dateToCheck = (DateTime) value;
                dateToCheck = dateToCheck.AddYears(minYears);
                if (dateToCheck <= DateTime.Today) { 
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult(GetMsg(ctx.DisplayName));
        }

        public void AddValidation(ClientModelValidationContext ctx)
        {
            if (!ctx.Attributes.ContainsKey("data-val"))
                ctx.Attributes.Add("data-val", "true");
            ctx.Attributes.Add("data-val-minimumage-years", 
                minYears.ToString());
            ctx.Attributes.Add("data-val-minimumage",
                GetMsg(ctx.ModelMetadata.DisplayName ?? ctx.ModelMetadata.Name));
        }

        private string GetMsg(string name) => base.ErrorMessage ??
                $"{name} must be at least {minYears} years ago."; 
    }
}
