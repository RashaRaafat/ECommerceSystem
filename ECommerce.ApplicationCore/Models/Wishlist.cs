using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.ApplicationCore.Models
{
        public class Wishlist
        {
            public int Id { get; set; }
            public string UserId { get; set; }
            public int ProductId { get; set; }

            // Navigation properties for many-to-one relationships with User and Product
           // public User User { get; set; }
            public Product Product { get; set; }
        }
}


