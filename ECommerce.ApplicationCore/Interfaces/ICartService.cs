using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.ApplicationCore.Models;

    namespace ECommerce.ApplicationCore.Interfaces
    {
    public interface ICartService
    {

        void AddToCart(int productId);
        void ClearCartCookie();
        void RemoveFromCart(int productId);
        List<CartItem> GetCartItemsFromCookies();


    }
    }


