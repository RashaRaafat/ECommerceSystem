using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.ApplicationCore.Models
{
        public class OrderItem
        {
            public int Id { get; set; }
            public int OrderId { get; set; } // Id of the order
            public int ProductId { get; set; } // Id of the product
            public int Quantity { get; set; } // Quantity of the product in the order
            public decimal Price { get; set; } // Price of the product at the time of purchase

            // Navigation properties
            public Order Order { get; set; } // The order this item belongs to
            public Product Product { get; set; } // The product in this item
        }

}
