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
    public class StockItemRepository : IStockItemRepository
    {
        private readonly IMapper _mapper;
        private readonly IStockItemServices _stockItem;
        public StockItemRepository(IMapper mapper, IStockItemServices stockItem)
        {
            _mapper = mapper;
            _stockItem = stockItem;
        }

        private async Task CheckStockItemAsync(long id, string name, int typeId, int unitOfMeasureId, decimal itemUnit)
        {
            var dbStockItem = await _stockItem.GetStockItemAsync(d => d.Name == name 
                                                                    && d.TypeId == typeId && d.UnitOfMeasureId == unitOfMeasureId  
                                                                    && d.ItemUnit == itemUnit && d.Id != id);
            if (dbStockItem != null)
                throw new RestException(HttpStatusCode.Conflict, $"Stock Item is already available.");
        }

        public async Task<GetStockItemDto> CreateStockItemAsync(CreateStockItemDto stockItemDto)
        {
            await CheckStockItemAsync(0, stockItemDto.Name, stockItemDto.TypeId, stockItemDto.UnitOfMeasureId, stockItemDto.ItemUnit);

            var stockItem = _mapper.Map<StockItem>(stockItemDto);
            await _stockItem.AddStockItemAsync(stockItem);

            return _mapper.Map<GetStockItemDto>(stockItem);
        }

        public async Task DeleteStockItemAsync(long id)
        {
            var stockItem = await GetStockItemById(id);

            await _stockItem.DeleteStockItemAsync(stockItem);
        }

        private async Task<StockItem> GetStockItemById(long id)
        {
            var stockItem = await _stockItem.GetStockItemAsync(d => d.Id == id);

            if (stockItem == null)
                throw new RestException(HttpStatusCode.NotFound, "Stock Item Not Found");

            return stockItem;
        }

        public async Task<GetStockItemDto> GetStockItemAsync(long id)
        {
            var stockItem = await GetStockItemById(id);

            return _mapper.Map<GetStockItemDto>(stockItem);
        }

        public async Task<IEnumerable<GetStockItemDto>> GetStockItemsAsync()
        {
            var stockItems = await _stockItem.GetStockItemsAsync();

            return _mapper.Map<IEnumerable<GetStockItemDto>>(stockItems);
        }

        public async Task UpdateStockItemAsync(long id, EditStockItemDto stockItemDto)
        {
            await CheckStockItemAsync(id, stockItemDto.Name, stockItemDto.TypeId, stockItemDto.UnitOfMeasureId, stockItemDto.ItemUnit);

            var stockItem = await GetStockItemById(id);

            stockItem = _mapper.Map(stockItemDto, stockItem);

            await _stockItem.UpdateStockItemAsync(stockItem);
        }
    }
}
