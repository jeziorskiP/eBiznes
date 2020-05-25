using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.Invoice
{
    public class InvoiceLineIndexListingModel
    {
        public int InvoiceLineId { get; set; }
        public int LP { get; set; }
        public decimal PriceNetto { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TaxSumValue { get; set; }
        public decimal Tax { get; set; }
        public decimal PriceNettoSumValue { get; set; }
        public string ProductName { get; set; }
    }
}
