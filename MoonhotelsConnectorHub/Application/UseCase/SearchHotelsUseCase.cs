using MoonhotelsConnectorHub.Application.Services;
using MoonhotelsConnectorHub.Domain.Dto;
using MoonhotelsConnectorHub.Domain.Ports.Incoming;
using MoonhotelsConnectorHub.Domain.Ports.Outgoing;

namespace MoonhotelsConnectorHub.Application.UseCase
{
    public class SearchHotelsUseCase : ISearchService
    {
        private readonly IEnumerable<IProviderConnector> _providerConnectors;
        private readonly ProviderResponseAggregator _aggregator;

        public SearchHotelsUseCase(IEnumerable<IProviderConnector> providerConnectors, ProviderResponseAggregator aggregator)
        {
            _providerConnectors = providerConnectors;
            _aggregator = aggregator;
        }

        public async Task<HubSearchResponse> PerformSearchAsync(HubSearchRequest request)
        {
            try { 
                var searchTasks = _providerConnectors.Select(connector => connector.SearchAsync(request));
                var responses = await Task.WhenAll(searchTasks);
                var validResponses = responses.Where(response => response != null).ToList();

                return _aggregator.AggregateResponses(validResponses);
            } catch (Exception ex)
            {
                throw new Exception($"Error while searching hotels: {ex.Message}");
            }
            
        }
    }
}
