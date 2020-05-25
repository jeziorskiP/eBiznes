using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.Product
{
    public class ProductIndexListingModel
    {
        public int Id { get; set; }
        public int ProductNumber { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string Shape { get; set; }
        public string Purpose { get; set; }
        public string ImagePath { get; set; }

        // DUE TO PN
        public int Amount { get; set; }

    }
}
