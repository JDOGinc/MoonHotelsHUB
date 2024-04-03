using AutoMapper;
using MoonhotelsConnectorHub.Domain.Dto;
using MoonhotelsConnectorHub.Infrastructure.HotelLegs.Dto;

namespace MoonhotelsConnectorHub.Infrastructure.MapperConfig
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HubSearchRequest, HotelLegsSearchRequest>()
            .ForMember(dest => dest.Hotel, opt => opt.MapFrom(src => src.HotelId))
            .ForMember(dest => dest.CheckInDate, opt => opt.MapFrom(src => src.CheckIn.ToString("yyyy-MM-dd")))
            .ForMember(dest => dest.NumberOfNights, opt => opt.MapFrom(src => (src.CheckOut - src.CheckIn).Days))
            .ForMember(dest => dest.Guests, opt => opt.MapFrom(src => src.NumberOfGuests))
            .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.NumberOfRooms));



            CreateMap<HotelLegsSearchResponse, HubSearchResponse>()
            .AfterMap((src, dest) => {
                var roomGroups = src.Results.GroupBy(r => r.Room).ToList();
                foreach (var group in roomGroups)
                {
                    var room = new Room
                    {
                        RoomId = group.Key,
                        Rates = group.Select(result => new Rate
                        {
                            MealPlanId = result.Meal,
                            IsCancellable = result.CanCancel,
                            Price = result.Price
                        }).ToList()
                    };
                    dest.Rooms.Add(room);
                }
            });
        }
    }
}
