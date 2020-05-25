using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Product;

namespace WebApplication3.Models.Helper
{
    public class OrderSummaryIndexListingModel
    {
        // Address
        public int AddressId { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public int AppartmentNumber { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        //Client
        public int ClientId { get; set; }
        public int ClientNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumer { get; set; }
        public string email { get; set; }

        //Products

        public IEnumerable<ProductIndexListingModel> Products { get; set; }


        //Order

        public int OrderId { get; set; }
        public int OrderNumber { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
