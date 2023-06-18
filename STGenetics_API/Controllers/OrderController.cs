using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STGenetics_API.Dto;
using STGenetics_API.Entities;
using STGenetics_API.Services.Contracts;

namespace STGenetics_API.Controllers
{
    [Authorize]
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService) => _orderService = orderService;

        [HttpPost("add-order")]
        public async Task<ActionResult<Order>> Add([FromBody] List<OrderRequestDto> orderRequests) 
        {
            if (_orderService.HasDuplicateAnimalIds(orderRequests))
                return BadRequest("ERROR : Found a duplicate animalId.");

            var order = await _orderService.AddOrder(orderRequests);    

            return Ok(order);
        }

    }
}
