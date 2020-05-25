using DataContext.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext.Interfaces
{
    public interface IProduct
    {
        void AddProduct();
        IEnumerable<Product> GetAll();
        Product GetProduct(int ProductId);
    }
}
