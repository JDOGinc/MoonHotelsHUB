using MoonhotelsConnectorHub.Domain.Dto;

namespace MoonhotelsConnectorHub.Application.Services
{
    public class ProviderResponseAggregator
    {
        public HubSearchResponse AggregateResponses(IEnumerable<HubSearchResponse> providerResponses)
        {
            var aggregatedResponse = new HubSearchResponse();

            foreach (var response in providerResponses)
            {
                aggregatedResponse.Rooms.AddRange(response.Rooms);
            }

            return aggregatedResponse;
        }
    }
}
