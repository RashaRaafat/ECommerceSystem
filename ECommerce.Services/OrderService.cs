using ECommerce.ApplicationCore.Enums;
using ECommerce.ApplicationCore.Interfaces;
using ECommerce.ApplicationCore.Models;
using ECommerce.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services;


// OrderService.cs
public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _context;

    public OrderService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task<List<Order>> GetOrdersForUserAsync(string userId)
    {
        return await _context.Orders.Where(o => o.UserId == userId).Include(p=>p.OrderItems).ToListAsync();
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        return await _context.Orders.Include(p => p.OrderItems).ThenInclude(p=>p.Product).Where(o => o.Id==id).FirstOrDefaultAsync();

    }

    public async Task<Order> CreateOrderAsync(Order order)
    {

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return order;
    }

    public async Task AddOrderItemAsync(int orderId, OrderItem orderItem)
    {
    //    var order = await _context.Orders.FindAsync(orderId);
    //    if (order == null)
    //    {
    //        throw new ArgumentException("Order not found");
    //    }

    //    order.OrderItems.Add(orderItem);
    //    await _context.SaveChangesAsync();
    }

    public async Task RemoveOrderItemAsync(int orderId, int orderItemId)
    {
    //    var order = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == orderId);
    //    if (order == null)
    //    {
    //        throw new ArgumentException("Order not found");
    //    }

    //    var orderItemToRemove = order.OrderItems.FirstOrDefault(oi => oi.Id == orderItemId);
    //    if (orderItemToRemove != null)
    //    {
    //        order.OrderItems.Remove(orderItemToRemove);
    //        await _context.SaveChangesAsync();
    //    }
    }

   public async Task UpdateOrderStatusAsync(int orderId, OrderStatus newStatus)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order == null)
        {
            throw new ArgumentException("Order not found");
        }

        order.Status = newStatus;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrderAsync(int orderId)
    {
    //    var order = await _context.Orders.FindAsync(orderId);
    //    if (order == null)
    //    {
    //        throw new ArgumentException("Order not found");
    //    }

    //    _context.Orders.Remove(order);
    //    await _context.SaveChangesAsync();
    }

    public async Task<List<OrderItem>> GetOrderItemsAsync(int id)
    {
        return await _context.OrderItems.Where(p=>p.OrderId==id).Include(p=>p.Product).ToListAsync();
    }
}
