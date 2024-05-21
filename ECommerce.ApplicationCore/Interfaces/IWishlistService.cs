using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.ApplicationCore.Models;

    namespace ECommerce.ApplicationCore.Interfaces
    {
        public interface IWishlistService
        {
            Task AddToWishlistAsync(string userId, int productId);
            Task RemoveFromWishlistAsync(string userId, int productId);
            Task<List<Wishlist>> AllWishlistAsync(string userId);
            Task<bool> ExistInWishlist(string userId, int productId);
        }
    }




