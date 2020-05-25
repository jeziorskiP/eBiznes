using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataContext.Model.Shopping_Cart
{
    public class ShoppingCart
    {
        private AppDbContext _dbContext;
        public ShoppingCart(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> shoppingCartItems { get; set; }


        
        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            var context = services.GetService<AppDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }
      
        public void AddToCart(Product product, int amount, string Note)
        {
            var shoppingCartItem = _dbContext.shoppingCartItems.SingleOrDefault(
                s => s.product.Id == product.Id && s.ShoppingCartId == ShoppingCartId);
            if( shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    product = product,
                    Amount = amount,
                    Note = Note
                };
                _dbContext.shoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount =+ amount;//++;
            }
            _dbContext.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return shoppingCartItems ?? (shoppingCartItems = 
                _dbContext.shoppingCartItems
                    .Where(c => c.ShoppingCartId == ShoppingCartId)
                    .Include(p => p.product)
                    .ToList());
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _dbContext.shoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.product.Price * c.Amount).Sum();
            return total;
        }

        public void ClearCart()
        {
            var cartItems = _dbContext.shoppingCartItems
                .Where(c => c.ShoppingCartId == ShoppingCartId);
            _dbContext.shoppingCartItems.RemoveRange(cartItems);
            _dbContext.SaveChanges();
        }

        public int RemoveFromCart(int ProductId)
        {
            var shoppingCartItem = _dbContext.shoppingCartItems
                .SingleOrDefault(p => p.product.Id == ProductId && p.ShoppingCartId == ShoppingCartId);
            var localAmount = 0;
            if(shoppingCartItem != null)
            {
                if(shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _dbContext.shoppingCartItems.Remove(shoppingCartItem);
                }

            }

            _dbContext.SaveChanges();
            return localAmount;
        }





    }
}
