using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.Product
{
    public class ProductIndexModel
    {
        public IEnumerable<ProductIndexListingModel> Products { get; set; }
    }
}
