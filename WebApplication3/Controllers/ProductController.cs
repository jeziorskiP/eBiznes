using DataContext.Interfaces;
using DataContext.Model;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Product;

namespace WebApplication3.Controllers
{
    public class ProductController : Controller
    {
        private IProduct _product;


        public ProductController(IProduct product)
        {
            _product = product; 
        }

        public async Task<IActionResult> GetAll(int page = 1)
        {
            IOrderedQueryable<Product> query = _product.GetAllQuery();
            var model = await PagingList.CreateAsync(query, 6, page);
            /*
            var listingResult = _product.GetAll()
                .Select(result => new ProductIndexListingModel
                {
                    Id = result.Id,
                    Name = result.Name,
                    ProductNumber = result.ProductNumber,
                    Price = result.Price,
                    ImagePath = result.ImagePath
                });

                var model = new ProductIndexModel()
                {
                    Products = listingResult
                };
                */
                return View(model);
            
        }


        public async Task<IActionResult> Index(int page = 1)
        {
            IOrderedQueryable<Product> query = _product.GetAllQuery();
            var model = await PagingList.CreateAsync(query, 6, page);
            /*
            var listingResult = _product.GetAll()
                .Select(result => new ProductIndexListingModel
                {
                    Id = result.Id,
                    Name = result.Name,
                    ProductNumber = result.ProductNumber,
                    Price = result.Price,
                    ImagePath = result.ImagePath
                });

                var model = new ProductIndexModel()
                {
                    Products = listingResult
                };
                */
            return View(model);
            /*
            var listingResult = _product.GetAll()
            .Select(result => new ProductIndexListingModel
            {
                Id = result.Id,
                Name = result.Name,
                ProductNumber = result.ProductNumber,
                Price = result.Price,
                ImagePath = result.ImagePath
            });

            var model = new ProductIndexModel()
            {
                Products = listingResult
            };
     
            return View(model);
            */
        }

        public IActionResult ProductDetails(int ProductId)
        {
            var prod = _product.GetProduct(ProductId);
            ProductIndexListingModel model = new ProductIndexListingModel()
            {
                Id = prod.Id,
                Color = prod.Color,
                Height = prod.Height,
                ImagePath = prod.ImagePath,
                Name = prod.Name,
                ProductNumber = prod.ProductNumber,
                Purpose = prod.Purpose,
                Shape = prod.Shape,
                Size = prod.Size,
                Width = prod.Width,
                Price = prod.Price
            };

            return View(model);
        }
    }
}
