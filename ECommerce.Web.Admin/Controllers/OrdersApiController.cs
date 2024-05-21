using ECommerce.ApplicationCore.Interfaces;
using ECommerce.ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Web.User.Helpers;
using ECommerce.ApplicationCore.Enums;
using Microsoft.AspNetCore.SignalR;
using ECommerce.Web.User.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ECommerce.Web.Admin.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IHubContext<OrderHub> _hubContext;
        UserManager<IdentityUser> _userManager;



        public OrdersController(IOrderService orderService, IHubContext<OrderHub> hubContext, UserManager<IdentityUser> userManager)
        {
            _hubContext = hubContext;
            _orderService = orderService;
            _userManager = userManager;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _orderService.GetAllAsync();
            return orders.SerializeObject();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return order.SerializeObject();
        }

   

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] UpdateOrderStatusRequest request)
        {
            if (!Enum.TryParse<OrderStatus>(request.Status, out OrderStatus orderStatus))
            {
                return BadRequest("Invalid order status.");
            }
            IdentityUser user = await _userManager.GetUserAsync(User);

            await _orderService.UpdateOrderStatusAsync(id, orderStatus);
           
            await _hubContext.Clients.Group(id.ToString()).SendAsync("ReceiveNotification", $"Your order {id} status has been updated to {orderStatus}");


            return NoContent();
        }

    }

    public class UpdateOrderStatusRequest
    {
        public string Status { get; set; }
    }

}

