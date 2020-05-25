using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext.Model
{
    public class InvoiceLine
    {
        public int Id { get; set; }
        //public int InvoiceId { get; set; }
        //public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public decimal TotalPrice { get; set; }

        public Invoice Invoice { get; set; }  // one -> many
        public Product Product { get; set; }  // one -> many
    }
}
