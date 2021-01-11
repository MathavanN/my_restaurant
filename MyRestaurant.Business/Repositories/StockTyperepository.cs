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
    public class StockTypeRepository: IStockTypeRepository
    {
        private readonly IMapper _mapper;
        private readonly IStockTypeServices _stockType;
        public StockTypeRepository(IMapper mapper, IStockTypeServices stockType)
        {
            _mapper = mapper;
            _stockType = stockType;
        }

        private async Task CheckStockTypeAsync(int id, string type)
        {
            var dbStockType = await _stockType.GetStockTypeAsync(d => d.Type == type && d.Id != id);
            if (dbStockType != null)
                throw new RestException(HttpStatusCode.Conflict, $"Stock Type {type} is already available.");
        }

        public async Task<GetStockTypeDto> CreateStockTypeAsync(CreateStockTypeDto stockTypeDto)
        {
            await CheckStockTypeAsync(0, stockTypeDto.Type);

            var stockType = _mapper.Map<StockType>(stockTypeDto);
            await _stockType.AddStockTypeAsync(stockType);

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
            var serviceTypes = await _stockType.GetStockTypesAsync();

            return _mapper.Map<IEnumerable<GetStockTypeDto>>(serviceTypes);
        }
        private async Task<StockType> GetStockTypeById(int id)
        {
            var stockType = await _stockType.GetStockTypeAsync(d => d.Id == id);

            if (stockType == null)
                throw new RestException(HttpStatusCode.NotFound, "Stock type not found.");

            return stockType;
        }
        public async Task UpdateStockTypeAsync(int id, EditStockTypeDto stockTypeDto)
        {
            await CheckStockTypeAsync(id, stockTypeDto.Type); 

            var stockType = await GetStockTypeById(id);

            stockType = _mapper.Map(stockTypeDto, stockType);

            await _stockType.UpdateStockTypeAsync(stockType);
        }
    }
}
