using MoonhotelsConnectorHub.Domain.Dto;

namespace MoonhotelsConnectorHub.Domain.Ports.Outgoing
{
    public interface IProviderConnector
    {
        Task<HubSearchResponse> SearchAsync(HubSearchRequest request);
    }
}
