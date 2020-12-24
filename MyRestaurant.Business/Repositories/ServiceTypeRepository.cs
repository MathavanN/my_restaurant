using AutoMapper;
using MyRestaurant.Business.Dtos.V1.ServiceTypeDtos;
using MyRestaurant.Business.Errors;
using MyRestaurant.Business.Repositories.Contracts;
using MyRestaurant.Models;
using MyRestaurant.Services.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MyRestaurant.Business.Repositories
{
    public class ServiceTypeRepository : IServiceTypeRepository
    {
        private readonly IMapper _mapper;
        private readonly IServiceTypeService _serviceType;
        public ServiceTypeRepository(IMapper mapper, IServiceTypeService serviceType)
        {
            _mapper = mapper;
            _serviceType = serviceType;
        }

        public async Task<GetServiceTypeDto> CreateServiceTypeAsync(CreateServiceTypeDto serviceTypeDto)
        {
            var serviceType = _mapper.Map<ServiceType>(serviceTypeDto);
            await _serviceType.AddServiceTypeAsync(serviceType);

            return _mapper.Map<GetServiceTypeDto>(serviceType);
        }

        public async Task DeleteServiceTypeAsync(int id)
        {
            var serviceType = await _serviceType.GetServiceTypeAsync(id);

            if (serviceType == null)
                throw new RestException(HttpStatusCode.NotFound, "ServiceType Not Found");

            await _serviceType.DeleteServiceTypeAsync(serviceType);
        }

        public async Task<IEnumerable<GetServiceTypeDto>> GetServicesTypesAsync()
        {
            var serviceTypes = await _serviceType.GetServiceTypesAsync();

            return _mapper.Map<IEnumerable<GetServiceTypeDto>>(serviceTypes);
        }

        public async Task<GetServiceTypeDto> GetServiceTypeAsync(int id)
        {
            var serviceType = await _serviceType.GetServiceTypeAsync(id);

            if (serviceType == null)
                throw new RestException(HttpStatusCode.NotFound, "ServiceType Not Found");

            return _mapper.Map<GetServiceTypeDto>(serviceType);
        }

        public async Task UpdateServiceTypeAsync(int id, EditServiceTypeDto serviceTypeDto)
        {
            var serviceType = await _serviceType.GetServiceTypeAsync(id);

            if (serviceType == null)
                throw new RestException(HttpStatusCode.NotFound, "ServiceType Not Found");

            serviceType = _mapper.Map(serviceTypeDto, serviceType);

            await _serviceType.UpdateServiceTypeAsync(serviceType);
        }
    }
}
