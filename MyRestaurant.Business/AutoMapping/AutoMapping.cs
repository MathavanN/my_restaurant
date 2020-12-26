using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Models;

namespace MyRestaurant.Business.AutoMapping
{
    public class AutoMapping : AutoMapper.Profile
    {
        public AutoMapping()
        {
            //map from payload to models
            CreateMap<CreateServiceTypeDto, ServiceType>();
            CreateMap<EditServiceTypeDto, ServiceType>();
            CreateMap<CreateRestaurantInfoDto, RestaurantInfo>();


            //map from models to dto
            CreateMap<ServiceType, GetServiceTypeDto>();
            CreateMap<RestaurantInfo, GetRestaurantInfoDto>()
                .ForMember(d => d.Address, opt => opt.MapFrom(src => $"{src.Address}, {src.City}, {src.Country}"));
        }
    }
}
