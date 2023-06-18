using Dapper;
using STGenetics_API.Context;
using STGenetics_API.Contracts;
using STGenetics_API.Dto;
using STGenetics_API.Entities;

namespace STGenetics_API.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DapperContext _context;

        public OrderRepository(DapperContext context) => _context = context;
        public async Task<Order> Add(OrderDto order)
        {
            var query = @"INSERT INTO orders (total, charge_freight) VALUES (@total, @charge_freight);
                        SELECT SCOPE_IDENTITY() AS Id;";

            var parameters = new DynamicParameters();
            parameters.Add("@total", order.Total);
            parameters.Add("@charge_freight", order.ChargeFreight);

            using (var connection = _context.CreateConnection()) 
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdOrder = new Order
                {
                    Id = id,
                    Total = order.Total,
                    ChargeFreight = order.ChargeFreight,
                };

                return createdOrder;
            }
        }
    }
}
