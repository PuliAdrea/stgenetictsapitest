using STGenetics_API.Contracts;
using STGenetics_API.Dto;
using STGenetics_API.Entities;
using STGenetics_API.Services.Contracts;

namespace STGenetics_API.Services.Impl
{
    public class OrderService : IOrderService
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderService(IAnimalRepository animalRepository, IOrderRepository orderRepository)
        {
            _animalRepository = animalRepository;
            _orderRepository = orderRepository;
        }

        public async Task<Order> AddOrder(List<OrderRequestDto> orderRequests)
        {          
            decimal totalOrder = 0;
            var totalAnimals = 0;
            var chargeFreigth = false;

            foreach (var item in orderRequests) 
            {
                var animal = await  _animalRepository.Get(item.AnimalId);

                var costOrder = animal.Price * item.Amount;

                if (item.Amount > 50) 
                {
                    decimal discountPercentage = 5.0m;
                    decimal discountAmount = costOrder * (discountPercentage / 100); 
                    costOrder = costOrder - discountAmount;
                }

                totalOrder = totalOrder + costOrder;
                totalAnimals = totalAnimals + item.Amount;
            }

            if (totalAnimals < 200) 
            {
                decimal discountPercentage = 3.0m;
                decimal discountAmount = totalOrder * (discountPercentage / 100);
                totalOrder = totalOrder - discountAmount;
            }

            if (totalAnimals < 300) 
            {
                chargeFreigth = true;
                totalOrder = totalOrder + 1000;
            }

            var order = new OrderDto 
            {
                Total = totalOrder,
                ChargeFreight = chargeFreigth
            };

            var result = await _orderRepository.Add(order);

            return result;
        }


        public bool HasDuplicateAnimalIds(List<OrderRequestDto> orderRequests)
        {
            HashSet<int> animalIds = new HashSet<int>();

            foreach (var orderRequest in orderRequests)
            {
                if (!animalIds.Add(orderRequest.AnimalId))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
