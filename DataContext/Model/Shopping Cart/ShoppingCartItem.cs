using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext.Model.Shopping_Cart
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }
        public Product product { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
        public string Note { get; set; }
    }
}
