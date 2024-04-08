using MoonhotelsConnectorHub.Domain.Dto;

namespace MoonhotelsConnectorHub.Application.Services
{
    public class ProviderResponseAggregator
    {
        public HubSearchResponse AggregateResponses(List<HubSearchResponse?> providerResponses)
        {
            try
            {
                var aggregatedResponse = new HubSearchResponse();

                var groupedRooms = providerResponses
                   .SelectMany(response => response.Rooms)
                   .GroupBy(room => room.RoomId)
                   .ToList();

                foreach (var groupedRoom in groupedRooms)
                {
                    var room = new Room
                    {
                        RoomId = groupedRoom.Key,
                        Rates = groupedRoom
                            .SelectMany(room => room.Rates)
                            .GroupBy(rate => new { rate.MealPlanId, rate.IsCancellable })
                            .Select(grp => grp
                                .OrderBy(rate => rate.Price)
                                .First())
                            .ToList()
                    };

                    aggregatedResponse.Rooms.Add(room);
                }

                return aggregatedResponse;
            }catch (Exception ex)
            {
                throw new Exception($"Error while aggregating responses: {ex.Message}");
            }
            
        }
    }
}
