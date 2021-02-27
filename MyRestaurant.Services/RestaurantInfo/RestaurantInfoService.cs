using MyRestaurant.Core;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{
    public class RestaurantInfoService : IRestaurantInfoService
    {
        private readonly IMyRestaurantContext _context;
        public RestaurantInfoService(IMyRestaurantContext context) => _context = context;

        public async Task<RestaurantInfo> AddRestaurantInfoAsync(RestaurantInfo info)
        {
            _context.Create(info);
            await _context.CommitAsync();
            return info;
        }

        public async Task DeleteRestaurantInfoAsync(RestaurantInfo info)
        {
            _context.Delete(info);
            await _context.CommitAsync();
        }

        public async Task<RestaurantInfo> GetRestaurantInfoAsync(Expression<Func<RestaurantInfo, bool>> expression) => await _context.GetFirstOrDefaultAsync(expression);

        public async Task<IEnumerable<RestaurantInfo>> GetRestaurantInfosAsync() => await _context.GetAllAsync<RestaurantInfo>();

        public async Task UpdateRestaurantInfoAsync(RestaurantInfo info)
        {
            _context.Modify(info);
            await _context.CommitAsync();
        }
    }
}
