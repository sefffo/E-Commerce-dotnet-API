using ECommerce.Presentation.Attributes;
using ECommerce.Services.Abstraction;
using ECommerce.SharedLibirary.DTO_s.OrderDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.Presentation.Controllers
{
    public class OrderController(IOrderService orderService) : ApiBaseController
    {
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDTO>> CreateOrder(OrderDTO orderDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await orderService.CreateOrderAsync(orderDto, email);
            return HandleResult(result); // ✅ unwraps Result<OrderToReturnDTO>
        }

        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDTO>>> GetDeliveryMethods()
        {
            var result = await orderService.GetDeliveryMethodsAsync();
            return HandleResult(result); // ✅ unwraps Result<IEnumerable<DeliveryMethodDTO>>
        }

        [HttpGet("AllOrders")]
        [RedisCache(60)]
        public async Task<ActionResult<IEnumerable<OrderToReturnDTO>>> GetAllOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await orderService.GetAllOrdersAsync(email);
            return HandleResult(result); // ✅ unwraps Result<IEnumerable<OrderToReturnDTO>>
        }

        [HttpGet("{orderId}")]
        [RedisCache(60)]
        public async Task<ActionResult<OrderToReturnDTO>> GetOrderById(Guid orderId)
        {
            var result = await orderService.GetOrderById(orderId);
            return HandleResult(result); // ✅ unwraps Result<OrderToReturnDTO>
        }
    }
}
