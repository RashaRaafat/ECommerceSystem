using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.ApplicationCore.Enums;
using ECommerce.ApplicationCore.Models;

    namespace ECommerce.ApplicationCore.Interfaces
    {
    public interface IOrderService
    {
        Task<Order> GetByIdAsync(int id);
        Task<List<OrderItem>> GetOrderItemsAsync(int id);
        Task<List<Order>> GetOrdersForUserAsync(string userId);
        Task<Order> CreateOrderAsync(Order order); // Create an order with a list of order items



        

        Task AddOrderItemAsync(int orderId, OrderItem orderItem); // Add an order item to an existing order
        Task RemoveOrderItemAsync(int orderId, int orderItemId); // Remove an order item from an existing order
        
        //forAdmin
        
        Task UpdateOrderStatusAsync(int orderId, OrderStatus newStatus); // Update order status
        Task<List<Order>> GetAllAsync();

    }

}




