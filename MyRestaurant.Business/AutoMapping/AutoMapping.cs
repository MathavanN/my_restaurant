using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Models;
using MyRestaurant.Services;
using System.Linq;

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
            CreateMap<CreatePurchaseOrderDto, PurchaseOrder>();
            CreateMap<EditPurchaseOrderDto, PurchaseOrder>();
            CreateMap<ApprovalPurchaseOrderDto, PurchaseOrder>();
            CreateMap<CreatePurchaseOrderItemDto, PurchaseOrderItem>();
            CreateMap<EditPurchaseOrderItemDto, PurchaseOrderItem>();
            CreateMap<CreatePaymentTypeDto, PaymentType>();
            CreateMap<EditPaymentTypeDto, PaymentType>();
            CreateMap<CreateGoodsReceivedNoteDto, GoodsReceivedNote>();
            CreateMap<EditGoodsReceivedNoteDto, GoodsReceivedNote>();
            CreateMap<CreateGoodsReceivedNoteItemDto, GoodsReceivedNoteItem>();
            CreateMap<EditGoodsReceivedNoteItemDto, GoodsReceivedNoteItem>();
            CreateMap<CreateGoodsReceivedNoteFreeItemDto, GoodsReceivedNoteFreeItem>();
            CreateMap<EditGoodsReceivedNoteFreeItemDto, GoodsReceivedNoteFreeItem>();

            //map from models to dto
            CreateMap<ServiceType, GetServiceTypeDto>();
            CreateMap<RestaurantInfo, GetRestaurantInfoDto>()
                .ForMember(d => d.Address, opt => opt.MapFrom(src => $"{src.Address}, {src.City}, {src.Country}"));
            CreateMap<User, GetUserDto>()
                .ForMember(d => d.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(e => e.Role.Name).ToList()));
            CreateMap<CurrentUser, CurrentUserDto>()
                .ForMember(d => d.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            CreateMap<Supplier, GetSupplierDto>();
            CreateMap<UnitOfMeasure, GetUnitOfMeasureDto>();
            CreateMap<StockType, GetStockTypeDto>();
            CreateMap<PaymentType, GetPaymentTypeDto>();
            CreateMap<StockItem, GetStockItemDto>()
                .ForMember(d => d.StockType, opt => opt.MapFrom(src => src.Type.Type))
                .ForMember(d => d.UnitOfMeasureCode, opt => opt.MapFrom(src => src.UnitOfMeasure.Code));
            CreateMap<PurchaseOrder, GetPurchaseOrderDto>()
                .ForMember(d => d.SupplierName, opt => opt.MapFrom(src => src.Supplier.Name))
                .ForMember(d => d.RequestedUserName, opt => opt.MapFrom(src => $"{src.RequestedUser.FirstName} {src.RequestedUser.LastName}" ))
                .ForMember(d => d.RequestedUserId, opt => opt.MapFrom(src => src.RequestedUser.Id))
                .ForMember(d => d.ApprovedUserName, opt => opt.MapFrom(src => $"{src.ApprovedUser.FirstName} {src.ApprovedUser.LastName}"))
                .ForMember(d => d.ApprovedUserId, opt => opt.MapFrom(src => src.ApprovedUser.Id));
            CreateMap<PurchaseOrderItem, GetPurchaseOrderItemDto>()
                .ForMember(d => d.ItemTypeId, opt => opt.MapFrom(src => src.Item.Type.Id))
                .ForMember(d => d.ItemTypeName, opt => opt.MapFrom(src => src.Item.Type.Type))
                .ForMember(d => d.ItemName, opt => opt.MapFrom(src => src.Item.Name))
                .ForMember(d => d.ItemUnit, opt => opt.MapFrom(src => src.Item.ItemUnit))
                .ForMember(d => d.UnitOfMeasureCode, opt => opt.MapFrom(src => src.Item.UnitOfMeasure.Code));
            CreateMap<GoodsReceivedNote, GetGoodsReceivedNoteDto>()
                .ForMember(d => d.PurchaseOrderNumber, opt => opt.MapFrom(src => src.PurchaseOrder.OrderNumber))
                .ForMember(d => d.PaymentTypeName, opt => opt.MapFrom(src => src.PaymentType.Name))
                .ForMember(d => d.CreatedUserId, opt => opt.MapFrom(src => src.CreatedUser.Id))
                .ForMember(d => d.CreatedUserName, opt => opt.MapFrom(src => $"{src.CreatedUser.FirstName} {src.CreatedUser.LastName}"))
                .ForMember(d => d.ReceivedUserId, opt => opt.MapFrom(src => src.ReceivedUser.Id))
                .ForMember(d => d.ReceivedUserName, opt => opt.MapFrom(src => $"{src.ReceivedUser.FirstName} {src.ReceivedUser.LastName}"));
            CreateMap<GoodsReceivedNoteItem, GetGoodsReceivedNoteItemDto>()
                .ForMember(d => d.ItemTypeId, opt => opt.MapFrom(src => src.Item.Type.Id))
                .ForMember(d => d.ItemTypeName, opt => opt.MapFrom(src => src.Item.Type.Type))
                .ForMember(d => d.ItemName, opt => opt.MapFrom(src => src.Item.Name))
                .ForMember(d => d.ItemUnit, opt => opt.MapFrom(src => src.Item.ItemUnit))
                .ForMember(d => d.UnitOfMeasureCode, opt => opt.MapFrom(src => src.Item.UnitOfMeasure.Code));
            CreateMap<GoodsReceivedNoteFreeItem, GetGoodsReceivedNoteFreeItemDto>()
                .ForMember(d => d.ItemTypeId, opt => opt.MapFrom(src => src.Item.Type.Id))
                .ForMember(d => d.ItemTypeName, opt => opt.MapFrom(src => src.Item.Type.Type))
                .ForMember(d => d.ItemName, opt => opt.MapFrom(src => src.Item.Name))
                .ForMember(d => d.ItemUnit, opt => opt.MapFrom(src => src.Item.ItemUnit))
                .ForMember(d => d.UnitOfMeasureCode, opt => opt.MapFrom(src => src.Item.UnitOfMeasure.Code));
        }
    }
}
