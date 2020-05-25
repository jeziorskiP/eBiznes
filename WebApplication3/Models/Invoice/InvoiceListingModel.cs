using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.Invoice
{
    public class InvoiceListingModel
    {
        public IEnumerable<InvoiceIndexListingModel> Invoices { get; set; }
    }
}
