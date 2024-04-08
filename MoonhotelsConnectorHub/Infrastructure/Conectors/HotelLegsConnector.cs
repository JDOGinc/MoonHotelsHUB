using AutoMapper;
using MoonhotelsConnectorHub.Domain.Dto;
using MoonhotelsConnectorHub.Domain.Ports.Outgoing;
using MoonhotelsConnectorHub.Infrastructure.Dto.HotelLegs;
using System.Text.Json;

namespace MoonhotelsConnectorHub.Infrastructure.Conectors
{
    public class HotelLegsConnector : IProviderConnector
    {
       
        private const string MockJsonResponse = "{\"results\":[{\"room\":1,\"meal\":1,\"canCancel\":false,\"price\":123.48},{\"room\":1,\"meal\":1,\"canCancel\":true,\"price\":150},{\"room\":2,\"meal\":1,\"canCancel\":false,\"price\":148.25},{\"room\":2,\"meal\":1,\"canCancel\":false,\"price\":165.38}]}";

        public async Task<HubSearchResponse?> SearchAsync(HubSearchRequest request)
        {
            try {
                var hotelLegsRequest = MapHubRequestToHotelLegsRequest(request);
                var hotelLegsResponse = await SimulateHotelLegsApiCall(hotelLegsRequest);
                return MapHotelLegsResponseToHubResponse(hotelLegsResponse);
            } catch (Exception ex)
            {
                Console.WriteLine($"Error in HotelLegsConnector: {ex.Message}");
                return null;
            }
            
        }

        private static async Task<HotelLegsSearchResponse> SimulateHotelLegsApiCall(HotelLegsSearchRequest request)
        {
            await Task.Delay(1000);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var response = JsonSerializer.Deserialize<HotelLegsSearchResponse>(MockJsonResponse, options);
            return response ?? new HotelLegsSearchResponse();
        }
        private static HotelLegsSearchRequest MapHubRequestToHotelLegsRequest(HubSearchRequest request)
        {
            return new HotelLegsSearchRequest
            {
                Hotel = request.HotelId,
                CheckInDate = request.CheckIn.ToString("yyyy-MM-dd"),
                NumberOfNights = (request.CheckIn - request.CheckOut).Days,
                Guests = request.NumberOfGuests,
                Rooms = request.NumberOfRooms,
                Currency = request.Currency
            };
        }

        private static HubSearchResponse MapHotelLegsResponseToHubResponse(HotelLegsSearchResponse hotelLegsResponse)
        {
            var response = new HubSearchResponse
            {
                Rooms = new List<Room>()
            };

            var roomGroups = hotelLegsResponse.Results.GroupBy(r => r.Room);

            foreach (var group in roomGroups)
            {
                var room = new Room
                {
                    RoomId = group.Key,
                    Rates = group.Select(hotelLegsResult => new Rate
                    {
                        MealPlanId = hotelLegsResult.Meal,
                        IsCancellable = hotelLegsResult.CanCancel,
                        Price = hotelLegsResult.Price
                    }).ToList()
                };
                response.Rooms.Add(room);
            }

            return response;
        }



    }
}
