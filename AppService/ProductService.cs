using DataContext;
using DataContext.Interfaces;
using DataContext.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppService
{
    public class ProductService : IProduct
    {

        private AppDbContext _dbContext;

        public ProductService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddProduct()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            return _dbContext.Products;
        }

        public IOrderedQueryable<Product> GetAllQuery()
        {
           return  _dbContext.Products.AsNoTracking().OrderBy(o => o.ProductNumber);
        }

        public Product GetProduct(int ProductId)
        {
            return GetAll().FirstOrDefault(p => p.Id == ProductId);
        }
    }
}
