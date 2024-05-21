using ECommerce.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.ApplicationCore.Models
{
        public class Order
        {
            public int Id { get; set; }
            public string UserId { get; set; } 
            public decimal TotalPrice { get; set; }
            public DateTime OrderDate { get; set; }= DateTime.Now;
            public OrderStatus Status { get; set; } = OrderStatus.New;


            // Navigation property for the order items (products)
            public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public decimal CalculateTotalPrice()
        {
            return OrderItems.Sum(oi => oi.Price * oi.Quantity);
        }
    }

}
