using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext.Model
{
    public class Invoice
    {
        public int Id { get; set; }
        public int InvoiceNumber { get; set; }
        //public int OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public bool Posted { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime CreateDate { get; set; }
        //public int UserId { get; set; }



        public int OrderId { get; set; }
        public Order Order { get; set; }

        public IList<InvoiceLine> InvoiceLines{ get; set; }



    }
}
