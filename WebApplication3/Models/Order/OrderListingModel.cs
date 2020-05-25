using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.Order
{
    public class OrderListingModel
    {
        public IEnumerable<OrderIndexListingModel> Orders { get; set; }
    }
}
