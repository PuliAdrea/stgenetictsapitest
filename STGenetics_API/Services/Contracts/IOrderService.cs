using STGenetics_API.Dto;
using STGenetics_API.Entities;

namespace STGenetics_API.Services.Contracts
{
    public interface IOrderService
    {
        public Task<Order> AddOrder(List<OrderRequestDto> orderRequests);
        bool HasDuplicateAnimalIds(List<OrderRequestDto> orderRequests);
    }
}
