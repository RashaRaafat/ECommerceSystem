using ECommerce.ApplicationCore.Interfaces;
using ECommerce.ApplicationCore.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.User.Controllers
{
    [Authorize]
    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]

    public class CartController : Controller
    {
        private readonly ICartService _cartService;

      
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            List<CartItem> cartItems = _cartService.GetCartItemsFromCookies();
            return View(cartItems);
        }

        public IActionResult AddToCart(int productId)
        {
            _cartService.AddToCart(productId);
            return RedirectToAction("Index", "Cart");
        }


        public IActionResult RemoveFromCart(int productId)
        {
            _cartService.RemoveFromCart(productId);
            return RedirectToAction("Index", "Cart");
        }
       
    }

}
