using MoonhotelsConnectorHub.Domain.Validators;
using System.ComponentModel.DataAnnotations;

namespace MoonhotelsConnectorHub.Domain.Dto
{
    public class HubSearchRequest
    {
        [Required]
        public int HotelId { get; set; }
        [Required]
        [CheckInDateAttribute]
        public DateTime CheckIn { get; set; }
        [Required]
        [CheckOutDateAttribute]
        public DateTime CheckOut { get; set; }
        [Range(1, 10, ErrorMessage = "The number of guests must be greater than zero.")]
        public int NumberOfGuests { get; set; }
        [Range(1, 10, ErrorMessage = "The number of rooms must be greater than zero.")]
        public int NumberOfRooms { get; set; }
        [Required(ErrorMessage = "The currency type can't be null")]
        [CurrencyAttribute]
        public string Currency { get; set; }
    }
}
