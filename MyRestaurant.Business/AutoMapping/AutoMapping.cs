using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Models;
using MyRestaurant.Services;

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
            CreateMap<RegisterAdminDto, User>()
                .ForMember(d => d.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<RegisterNormalDto, User>()
                .ForMember(d => d.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<CreateSupplierDto, Supplier>();
            CreateMap<EditSupplierDto, Supplier>();
            CreateMap<CreateUnitOfMeasureDto, UnitOfMeasure>();
            CreateMap<EditUnitOfMeasureDto, UnitOfMeasure>();
            CreateMap<CreateStockTypeDto, StockType>();
            CreateMap<EditStockTypeDto, StockType>();
            CreateMap<CreateStockItemDto, StockItem>();
            CreateMap<EditStockItemDto, StockItem>();

            //map from models to dto
            CreateMap<ServiceType, GetServiceTypeDto>();
            CreateMap<RestaurantInfo, GetRestaurantInfoDto>()
                .ForMember(d => d.Address, opt => opt.MapFrom(src => $"{src.Address}, {src.City}, {src.Country}"));
            CreateMap<CurrentUser, CurrentUserDto>()
                .ForMember(d => d.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            CreateMap<Supplier, GetSupplierDto>();
            CreateMap<UnitOfMeasure, GetUnitOfMeasureDto>();
            CreateMap<StockType, GetStockTypeDto>();
            CreateMap<StockItem, GetStockItemDto>()
                .ForMember(d => d.StockType, opt => opt.MapFrom(src => src.Type.Type))
                .ForMember(d => d.UnitOfMeasureCode, opt => opt.MapFrom(src => src.UnitOfMeasure.Code));

        }
    }
}
