using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.Invoice
{
    public class InvoiceIndexListingModel
    {
        //Address
        public int AddressId { get; set; }
        public string AddressStreet { get; set; }
        public int AddressStreetNumber { get; set; }
        public int AddressAppartmentNumber { get; set; }
        public string AddressPostCode { get; set; }
        public string AddressCity { get; set; }
        public string AddressCountry { get; set; }

        //Client
        public int ClientId { get; set; }
        public int ClientNumber { get; set; }
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }
        public string ClientPhoneNumer { get; set; }
        public string Clientemail { get; set; }

        //order
        public int OrderId { get; set; }
        public int OrderNumber { get; set; }

        public InvoiceLinesListingModel Lines { get; set; }

        public int InvoiceId { get; set; }
        public int InvoiceNumber { get; set; }
        public bool Post { get; set; }
        public DateTime InvoiceTime { get; set; }

        public decimal TotalPriceBrutto { get; set; }
        public decimal TotalPriceNetto { get; set; }
        public decimal TotalTax { get; set; }


    }
}
