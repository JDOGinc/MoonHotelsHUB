using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MoonhotelsConnectorHub.Domain.Validators
{
    public class CurrencyAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currencyCode = value as string;

            if (string.IsNullOrWhiteSpace(currencyCode))
            {
                return new ValidationResult("The currency code is required.");
            }
            var isCurrencyValid = CultureInfo
                .GetCultures(CultureTypes.SpecificCultures)
                .Select(culture => new RegionInfo(culture.LCID))
                .Any(region => region.ISOCurrencySymbol == currencyCode);

            if (!isCurrencyValid)
            {
                return new ValidationResult($"The currency code '{currencyCode}' is not valid.");
            }

            return ValidationResult.Success;
        }
    }
}
