using DataContext.Interfaces;
using DataContext.Model.Shopping_Cart;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Cart;

namespace WebApplication3.Controllers
{
    public class CartController : Controller
    {
        private readonly ShoppingCart _shoppingCart;

        private IProduct _product;

        public CartController(ShoppingCart shoppingCart, IProduct product)
        {
            _shoppingCart = shoppingCart;
            _product = product;
        }

        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.shoppingCartItems = items;

            var sCVM = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(sCVM);
        }

        public IActionResult AddToShoppingCart(int productId, int amount, string Note)
        {
            var selectedProduct = _product.GetAll().FirstOrDefault(p => p.Id == productId);
            
            Console.WriteLine("SELECTEDPRODUCT id : "+ selectedProduct.Id);
            if (selectedProduct != null)
            {
                _shoppingCart.AddToCart(selectedProduct, amount, Note);
            }
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int ProductId)
        {
            _shoppingCart.RemoveFromCart(ProductId);
            return RedirectToAction("Index");
        }

        public IActionResult ClearCart()
        {
            _shoppingCart.ClearCart();
            return RedirectToAction("Index");
        }





    }
}
