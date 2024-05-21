using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ECommerce.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ECommerce.Web.User.Controllers
{
    [Authorize]
    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]

    public class WishListController : Controller
    {
        private readonly IWishlistService _wishlistService;
        UserManager<IdentityUser> _userManager;
        public WishListController(IWishlistService wishlistService, UserManager<IdentityUser> userManager)
        {
            _wishlistService = wishlistService ?? throw new ArgumentNullException(nameof(wishlistService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }
        public async Task<IActionResult> Index()
        {
            IdentityUser user = await _userManager.GetUserAsync(User);

            var userWishlist = await _wishlistService.AllWishlistAsync(user.Id);
            return View(userWishlist);
        }
        public async Task<IActionResult> AddToWishlist(int ProductId)
        {
            try
            {
                IdentityUser user = await _userManager.GetUserAsync(User);
                if (! await _wishlistService.ExistInWishlist(user.Id, ProductId))
                {
                    await _wishlistService.AddToWishlistAsync(user.Id, ProductId);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the product to the wishlist: {ex.Message}");
            }
        }

        public async Task<IActionResult> RemoveFromWishlist(int productId)
        {
            try
            {
                IdentityUser user = await _userManager.GetUserAsync(User);
                bool exists = await _wishlistService.ExistInWishlist(user.Id, productId);
                if (exists)
                {
                    await _wishlistService.RemoveFromWishlistAsync(user.Id, productId);
                }
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while removing the product from the wishlist: {ex.Message}");
            }
        }
    }
}



