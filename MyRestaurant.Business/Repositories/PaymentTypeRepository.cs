using AutoMapper;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Errors;
using MyRestaurant.Business.Repositories.Contracts;
using MyRestaurant.Models;
using MyRestaurant.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MyRestaurant.Business.Repositories
{
    public class PaymentTypeRepository : IPaymentTypeRepository
    {
        private readonly IMapper _mapper;
        private readonly IPaymentTypeService _paymentType;
        public PaymentTypeRepository(IMapper mapper, IPaymentTypeService paymentType)
        {
            _mapper = mapper;
            _paymentType = paymentType;
        }

        private async Task CheckPaymentTypeAsync(int id, string name)
        {
            var dbPaymentType = await _paymentType.GetPaymentTypeAsync(d => d.Name == name && d.Id != id);
            if (dbPaymentType != null)
                throw new RestException(HttpStatusCode.Conflict, $"Payment type {name} is already available.");
        }

        public async Task<GetPaymentTypeDto> CreatePaymentTypeAsync(CreatePaymentTypeDto paymentTypeDto)
        {
            await CheckPaymentTypeAsync(0, paymentTypeDto.Name);

            var paymentType = _mapper.Map<PaymentType>(paymentTypeDto);
            paymentType = await _paymentType.AddPaymentTypeAsync(paymentType);

            return _mapper.Map<GetPaymentTypeDto>(paymentType);
        }

        public async Task DeletePaymentTypeAsync(int id)
        {
            var paymentType = await GetPaymentTypeById(id);

            await _paymentType.DeletePaymentTypeAsync(paymentType);
        }

        public async Task<GetPaymentTypeDto> GetPaymentTypeAsync(int id)
        {
            var paymentType = await GetPaymentTypeById(id);

            return _mapper.Map<GetPaymentTypeDto>(paymentType);
        }

        public async Task<IEnumerable<GetPaymentTypeDto>> GetPaymentTypesAsync()
        {
            var paymentTypes = await _paymentType.GetPaymentTypesAsync();

            return _mapper.Map<IEnumerable<GetPaymentTypeDto>>(paymentTypes.OrderBy(d => d.Name));
        }
        private async Task<PaymentType> GetPaymentTypeById(int id)
        {
            var paymentType = await _paymentType.GetPaymentTypeAsync(d => d.Id == id);

            if (paymentType == null)
                throw new RestException(HttpStatusCode.NotFound, "Payment type not found.");

            return paymentType;
        }
        public async Task<GetPaymentTypeDto> UpdatePaymentTypeAsync(int id, EditPaymentTypeDto paymentTypeDto)
        {
            var paymentType = await GetPaymentTypeById(id);

            await CheckPaymentTypeAsync(id, paymentTypeDto.Name);

            paymentType = _mapper.Map(paymentTypeDto, paymentType);

            await _paymentType.UpdatePaymentTypeAsync(paymentType);

            return _mapper.Map<GetPaymentTypeDto>(paymentType);
        }
    }
}
