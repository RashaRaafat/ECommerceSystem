using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECommerce.ApplicationCore.Interfaces;
using ECommerce.ApplicationCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ECommerce.Web.User.Hubs;
using Microsoft.AspNetCore.Authentication.Cookies;

[Authorize]
//[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]

public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    UserManager<IdentityUser> _userManager;
    private readonly ICartService _cartService;
    private readonly IHubContext<OrderHub> _hubContext;


    public OrderController(IOrderService orderService, UserManager<IdentityUser> userManager, ICartService cartService, IHubContext<OrderHub> hubContext)
    {
        _hubContext = hubContext;

        _orderService = orderService;
        _userManager = userManager;
        _cartService = cartService;

    }

    public async Task<IActionResult> Index()
    {
        IdentityUser user = await _userManager.GetUserAsync(User);

        var orders = await _orderService.GetOrdersForUserAsync(user.Id);
        return View(orders);
    }
   
    public async Task<IActionResult> Details(int id)
    {

        var order = await _orderService.GetOrderItemsAsync(id);
        if (order == null)
        {
            return NotFound();
        }
        return View(order);
    }

   
    public async Task<IActionResult> Create()
    {
        List<CartItem> items = _cartService.GetCartItemsFromCookies();
        Order order = new Order();
        IdentityUser user = await _userManager.GetUserAsync(User);

        order.UserId = user.Id;

        foreach (var item in items)
        {
            var orderItem = new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Product.Price,

            };

            order.OrderItems.Add(orderItem);
        }
        order.TotalPrice = order.CalculateTotalPrice();
       Order newOrder= await _orderService.CreateOrderAsync(order);
        _cartService.ClearCartCookie();

        //await _hubContext.Clients.Group("Admins").SendAsync("NewOrder", $"{user.UserName} created a new order number {newOrder.Id}.");
        await _hubContext.Clients.All.SendAsync("ReceiveNotification", $"{user.UserName} created a new order number {newOrder.Id}.");

        //await _hubContext.Clients.Group("Admins").SendAsync("ReceiveNotification", $"{user.UserName} created a new order: {order.Id}");

        return RedirectToAction(nameof(Index));
        //return RedirectToAction("Confirmation");
    }



}
