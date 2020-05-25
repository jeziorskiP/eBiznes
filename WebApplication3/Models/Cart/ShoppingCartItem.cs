using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Product;

namespace WebApplication3.Models.Cart
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }
        public ProductIndexListingModel product { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
        public string Note { get; set; }

    }
}
