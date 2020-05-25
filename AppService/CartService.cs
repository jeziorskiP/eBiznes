using DataContext;
using DataContext.Interfaces;
using DataContext.Model;
using DataContext.Model.Shopping_Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppService
{
    public class CartService : IShoppingCart
    {

        private AppDbContext _dbContext;
        public CartService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /*
        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            var context = services.GetService<AppDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }
        */
        public void AddToCart(Product product, int amount)
        {
            throw new NotImplementedException();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            throw new NotImplementedException();
        }

        public decimal GetShoppingCartTotal()
        {
            throw new NotImplementedException();
        }

        public void RemoveFromCart(Product product, int amount)
        {
            throw new NotImplementedException();
        }

    }
}
