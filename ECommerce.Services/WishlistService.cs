using ECommerce.ApplicationCore.Interfaces;
using ECommerce.ApplicationCore.Models;
using ECommerce.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services;


public class WishlistService : IWishlistService
{
    private readonly ApplicationDbContext _context;

    public WishlistService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddToWishlistAsync(string userId, int productId)
    {
        var existingWishlistItem = await _context.Wishlists
            .FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == productId);

        if (existingWishlistItem != null)
        {
            throw new InvalidOperationException("Product is already in the wishlist.");
        }

        var wishlistItem = new Wishlist
        {
            UserId = userId,
            ProductId = productId
        };

        _context.Wishlists.Add(wishlistItem);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveFromWishlistAsync(string userId, int productId)
    {
        var wishlistItem = await _context.Wishlists
            .FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == productId);

        if (wishlistItem == null)
        {
            throw new InvalidOperationException("Product is not in the wishlist.");
        }

        _context.Wishlists.Remove(wishlistItem);
        await _context.SaveChangesAsync();
    }
    public async Task<List<Wishlist>> AllWishlistAsync(string userId)
    {
        return await _context.Wishlists.Where(p=>p.UserId==userId).Include(p=>p.Product).ToListAsync();
    }

    public async Task<bool> ExistInWishlist(string userId, int productId)
    {
        return await _context.Wishlists.Where(p => p.UserId == userId).AnyAsync(p => p.ProductId == productId);

    }
}


