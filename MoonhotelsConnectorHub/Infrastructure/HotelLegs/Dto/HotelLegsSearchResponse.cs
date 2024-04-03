namespace MoonhotelsConnectorHub.Infrastructure.HotelLegs.Dto
{
    public class HotelLegsSearchResponse
    {
        public List<HotelLegsRoomResult> Results { get; set; } = new List<HotelLegsRoomResult>();
    }

    public class HotelLegsRoomResult
    {
        public int Room { get; set; }
        public int Meal { get; set; }
        public bool CanCancel { get; set; }
        public decimal Price { get; set; }
    }
}
