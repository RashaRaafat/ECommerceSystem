using ECommerce.ApplicationCore.Interfaces;
using ECommerce.ApplicationCore.Models;
using ECommerce.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ECommerce.Services;

public class CartService: ICartService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string CartCookieName = "Cart";
    private readonly ApplicationDbContext _context;

    public CartService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;

    }

        public void AddToCart(int productId)
    {
        CartItem item = new CartItem();
        item.Quantity = 1;
        item.ProductId = productId;
        List<CartItem> cartItems = GetCartItemsFromCookies();
        if(!cartItems.Any(p=>p.ProductId==productId))
        {
            cartItems.Add(item);
            SaveCartItemsToCookies(cartItems);
        }
    }

    public void RemoveFromCart(int productId)
    {
        List<CartItem> cartItems = GetCartItemsFromCookies();
        cartItems.RemoveAll(item => item.ProductId == productId);
        SaveCartItemsToCookies(cartItems);
    }

    public List<CartItem> GetCartItemsFromCookies()
    {
        var cartCookie = _httpContextAccessor.HttpContext.Request.Cookies[CartCookieName];
        if (cartCookie != null)
        {
            List<CartItem> cartItems= JsonConvert.DeserializeObject<List<CartItem>>(cartCookie);
            foreach (var item in cartItems)
            {
                item.Product = _context.Products.FirstOrDefault(p => p.Id == item.ProductId);
            }

            return cartItems;
        }
        return new List<CartItem>();
    }
    
    public void ClearCartCookie()
    {
        _httpContextAccessor.HttpContext.Response.Cookies.Delete(CartCookieName);
    }
    private void SaveCartItemsToCookies(List<CartItem> cartItems)
    {
        var cartCookieValue = JsonConvert.SerializeObject(cartItems);
        _httpContextAccessor.HttpContext.Response.Cookies.Append(CartCookieName, cartCookieValue);
    }
}



