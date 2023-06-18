using STGenetics_API.Dto;
using STGenetics_API.Entities;

namespace STGenetics_API.Contracts
{
    public interface IOrderRepository
    {
        public Task<Order> Add(OrderDto order);
    }
}
