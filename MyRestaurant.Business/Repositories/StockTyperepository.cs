using AutoMapper;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Errors;
using MyRestaurant.Business.Repositories.Contracts;
using MyRestaurant.Models;
using MyRestaurant.Services;
using System.Net;

namespace MyRestaurant.Business.Repositories
{
    public class StockTypeRepository : IStockTypeRepository
    {
        private readonly IMapper _mapper;
        private readonly IStockTypeService _stockType;
        public StockTypeRepository(IMapper mapper, IStockTypeService stockType)
        {
            _mapper = mapper;
            _stockType = stockType;
        }

        private async Task CheckStockTypeAsync(int id, string type)
        {
            var dbStockType = await _stockType.GetStockTypeAsync(d => d.Type == type && d.Id != id);
            if (dbStockType != null)
                throw new RestException(HttpStatusCode.Conflict, $"Stock type \"{type}\" is already available.");
        }

        public async Task<GetStockTypeDto> CreateStockTypeAsync(CreateStockTypeDto stockTypeDto)
        {
            await CheckStockTypeAsync(0, stockTypeDto.Type);

            var stockType = _mapper.Map<StockType>(stockTypeDto);
            stockType = await _stockType.AddStockTypeAsync(stockType);

            return _mapper.Map<GetStockTypeDto>(stockType);
        }

        public async Task DeleteStockTypeAsync(int id)
        {
            var stockType = await GetStockTypeById(id);

            await _stockType.DeleteStockTypeAsync(stockType);
        }

        public async Task<GetStockTypeDto> GetStockTypeAsync(int id)
        {
            var stockType = await GetStockTypeById(id);

            return _mapper.Map<GetStockTypeDto>(stockType);
        }

        public async Task<IEnumerable<GetStockTypeDto>> GetStockTypesAsync()
        {
            var stockTypes = await _stockType.GetStockTypesAsync();

            return _mapper.Map<IEnumerable<GetStockTypeDto>>(stockTypes.OrderBy(d => d.Type));
        }
        private async Task<StockType> GetStockTypeById(int id)
        {
            var stockType = await _stockType.GetStockTypeAsync(d => d.Id == id);

            if (stockType == null)
                throw new RestException(HttpStatusCode.NotFound, "Stock type not found.");

            return stockType;
        }
        public async Task<GetStockTypeDto> UpdateStockTypeAsync(int id, EditStockTypeDto stockTypeDto)
        {
            var stockType = await GetStockTypeById(id);

            await CheckStockTypeAsync(id, stockTypeDto.Type);

            stockType = _mapper.Map(stockTypeDto, stockType);

            await _stockType.UpdateStockTypeAsync(stockType);

            return _mapper.Map<GetStockTypeDto>(stockType);
        }
    }
}
