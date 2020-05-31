using DataContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataContext.Interfaces
{
    public interface IProduct
    {
        void AddProduct();
        IEnumerable<Product> GetAll();
        IOrderedQueryable<Product> GetAllQuery();
        Product GetProduct(int ProductId);
    }
}
