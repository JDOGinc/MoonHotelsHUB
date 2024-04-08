using MoonhotelsConnectorHub.Domain.Dto;
using System.ComponentModel.DataAnnotations;

namespace MoonhotelsConnectorHub.Domain.Validators
{
    public class CheckOutDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var request = (HubSearchRequest)validationContext.ObjectInstance;

            if (request.CheckOut < request.CheckIn)
            {
                return new ValidationResult("The checkOut date must be after the checkIn date");
            }

            return ValidationResult.Success;
        }

    }

    public class CheckInDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var checkInDate = (DateTime)value;

            if (checkInDate.Date < DateTime.Today)
            {
                return new ValidationResult("The checkIn date cannot be less than today.");
            }

            return ValidationResult.Success;
        }
    }


}
