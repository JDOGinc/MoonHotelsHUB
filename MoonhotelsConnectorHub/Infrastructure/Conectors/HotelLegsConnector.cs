using AutoMapper;
using MoonhotelsConnectorHub.Domain.Dto;
using MoonhotelsConnectorHub.Domain.Ports.Outgoing;
using MoonhotelsConnectorHub.Infrastructure.HotelLegs.Dto;
using System.Text.Json;

namespace MoonhotelsConnectorHub.Infrastructure.Conectors
{
    public class HotelLegsConnector : IProviderConnector
    {
        private readonly IMapper _mapper;
        private const string MockJsonResponse = "{\"results\":[{\"room\":1,\"meal\":1,\"canCancel\":false,\"price\":123.48},{\"room\":1,\"meal\":1,\"canCancel\":true,\"price\":150},{\"room\":2,\"meal\":1,\"canCancel\":false,\"price\":148.25},{\"room\":2,\"meal\":2,\"canCancel\":false,\"price\":165.38}]}";

        public HotelLegsConnector(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<HubSearchResponse> SearchAsync(HubSearchRequest request)
        {
            var hotelLegsRequest = _mapper.Map<HotelLegsSearchRequest>(request);
            var hotelLegsResponse = await SimulateHotelLegsApiCall(hotelLegsRequest);

            return _mapper.Map<HubSearchResponse>(hotelLegsResponse);
        }

        private async Task<HotelLegsSearchResponse> SimulateHotelLegsApiCall(HotelLegsSearchRequest request)
        {
            await Task.Delay(1000);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var response = JsonSerializer.Deserialize<HotelLegsSearchResponse>(MockJsonResponse, options);
            return response ?? new HotelLegsSearchResponse();
        }

        
    }
}
