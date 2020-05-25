using DataContext.Model;
using DataContext.Model.Shopping_Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext.Interfaces
{
    public interface IShoppingCart
    {
        void AddToCart(Product product, int amount);
        void RemoveFromCart(Product product, int amount);
        List<ShoppingCartItem> GetShoppingCartItems();
        decimal GetShoppingCartTotal();
    }
}
