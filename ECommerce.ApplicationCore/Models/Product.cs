﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.ApplicationCore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ?ImagePath { get; set; }
        public int CategoryId { get; set; }

        // Navigation property for many-to-one relationship with Category
        public Category Category { get; set; }
    }
}


