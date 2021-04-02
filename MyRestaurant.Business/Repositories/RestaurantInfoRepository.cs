using AutoMapper;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Errors;
using MyRestaurant.Business.Repositories.Contracts;
using MyRestaurant.Models;
using MyRestaurant.Services;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MyRestaurant.Business.Repositories
{
    public class RestaurantInfoRepository : IRestaurantInfoRepository
    {
        private readonly IMapper _mapper;
        private readonly IRestaurantInfoService _restaurantInfo;
        public RestaurantInfoRepository(IMapper mapper, IRestaurantInfoService restaurantInfo)
        {
            _mapper = mapper;
            _restaurantInfo = restaurantInfo;
        }

        public async Task<GetRestaurantInfoDto> CreateRestaurantInfoAsync(CreateRestaurantInfoDto infoDto)
        {
            var restaurantInfo = _mapper.Map<RestaurantInfo>(infoDto);

            restaurantInfo = await _restaurantInfo.AddRestaurantInfoAsync(restaurantInfo);

            return _mapper.Map<GetRestaurantInfoDto>(restaurantInfo);
        }

        public async Task<IEnumerable<GetRestaurantInfoDto>> GetRestaurantInfosAsync()
        {
            var restaurantInfos = await _restaurantInfo.GetRestaurantInfosAsync();

            return _mapper.Map<IEnumerable<GetRestaurantInfoDto>>(restaurantInfos);
        }

        public async Task<GetRestaurantInfoDto> GetRestaurantInfoAsync(int id)
        {
            var restaurantInfo = await _restaurantInfo.GetRestaurantInfoAsync(d => d.Id == id);

            if (restaurantInfo == null)
                throw new RestException(HttpStatusCode.NotFound, "Restaurant information not found.");

            return _mapper.Map<GetRestaurantInfoDto>(restaurantInfo);
        }
    }
}
