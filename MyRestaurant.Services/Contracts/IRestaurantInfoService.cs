using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyRestaurant.Services.Contracts
{
    public interface IRestaurantInfoService
    {
        Task<IEnumerable<RestaurantInfo>> GetRestaurantInfosAsync();
        Task<RestaurantInfo> GetRestaurantInfoAsync(Expression<Func<RestaurantInfo, bool>> expression);
        Task AddRestaurantInfoAsync(RestaurantInfo info);
        Task UpdateRestaurantInfoAsync(RestaurantInfo info);
        Task DeleteRestaurantInfoAsync(RestaurantInfo info);
    }
}
