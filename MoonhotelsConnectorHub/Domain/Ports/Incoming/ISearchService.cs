using MoonhotelsConnectorHub.Domain.Dto;

namespace MoonhotelsConnectorHub.Domain.Ports.Incoming
{
    public interface ISearchService
    {
        Task<HubSearchResponse> PerformSearchAsync(HubSearchRequest request);
    }
}
