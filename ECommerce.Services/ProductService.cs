using ECommerce.ApplicationCore.Interfaces;
using ECommerce.ApplicationCore.Models;
using ECommerce.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services;

// ProductService.cs
public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Product>> GetAllAsync(int?Categoryid)
    {
        if(Categoryid==null)
            return await _context.Products.Include(p=>p.Category).ToListAsync();
        else
            return await _context.Products.Where(p=>p.CategoryId== Categoryid).Include(p => p.Category).ToListAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }
}


