using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataContext.Model
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public int ProductNumber { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string Shape { get; set; }
        public string Purpose { get; set; }
        public string ImagePath { get; set; }


        public IList<OrderLine> OrderLines { get; set; }
        public IList<PackingNoteLine> PackingNoteLines { get; set; }
        public IList<DeliveryNoteLine> DeliveryNoteLines { get; set; }
        public IList<InvoiceLine> InvoiceLines { get; set; }
    }
}
