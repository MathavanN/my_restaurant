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
    public class UnitOfMeasureRepository : IUnitOfMeasureRepository
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfMeasureServices _unitOfMeasure;
        public UnitOfMeasureRepository(IMapper mapper, IUnitOfMeasureServices unitOfMeasure)
        {
            _mapper = mapper;
            _unitOfMeasure = unitOfMeasure;
        }

        public async Task<GetUnitOfMeasureDto> CreateUnitOfMeasureAsync(CreateUnitOfMeasureDto unitOfMeasureDto)
        {
            var dbUnitOfMeasure = await _unitOfMeasure.GetUnitOfMeasureAsync(d => d.Code == unitOfMeasureDto.Code);
            if (dbUnitOfMeasure != null)
                throw new RestException(HttpStatusCode.Conflict, $"Unit Of Measure {unitOfMeasureDto.Code} is already available.");

            var unitOfMeasure = _mapper.Map<UnitOfMeasure>(unitOfMeasureDto);
            await _unitOfMeasure.AddUnitOfMeasureAsync(unitOfMeasure);

            return _mapper.Map<GetUnitOfMeasureDto>(unitOfMeasure);
        }

        private async Task<UnitOfMeasure> GetUnitOfMeasureId(long id)
        {
            var unitOfMeasure = await _unitOfMeasure.GetUnitOfMeasureAsync(d => d.Id == id);

            if (unitOfMeasure == null)
                throw new RestException(HttpStatusCode.NotFound, "Unit Of Measure Not Found");

            return unitOfMeasure;
        }

        public async Task DeleteUnitOfMeasureAsync(int id)
        {
            var unitOfMeasure = await GetUnitOfMeasureId(id);

            await _unitOfMeasure.DeleteUnitOfMeasureAsync(unitOfMeasure);
        }

        public async Task<GetUnitOfMeasureDto> GetUnitOfMeasureAsync(int id)
        {
            var unitOfMeasure = await GetUnitOfMeasureId(id);

            return _mapper.Map<GetUnitOfMeasureDto>(unitOfMeasure);
        }

        public async Task<IEnumerable<GetUnitOfMeasureDto>> GetUnitOfMeasuresAsync()
        {
            var unitOfMeasures = await _unitOfMeasure.GetUnitOfMeasuresAsync();

            return _mapper.Map<IEnumerable<GetUnitOfMeasureDto>>(unitOfMeasures);
        }

        public async Task UpdateUnitOfMeasureAsync(int id, EditUnitOfMeasureDto unitOfMeasureDto)
        {
            var unitOfMeasure = await GetUnitOfMeasureId(id);

            unitOfMeasure = _mapper.Map(unitOfMeasureDto, unitOfMeasure);

            await _unitOfMeasure.UpdateUnitOfMeasureAsync(unitOfMeasure);
        }
    }
}
