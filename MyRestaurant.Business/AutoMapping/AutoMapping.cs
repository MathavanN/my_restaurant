using MyRestaurant.Business.Dtos.V1.ServiceTypeDtos;
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


            //map from models to dto
            CreateMap<ServiceType, GetServiceTypeDto>();
        }
    }
}
