using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Product;

namespace WebApplication3.Models.PackingNotes
{
    public class PNDetailsIndexListingModel
    {
        public int PackingNoteId { get; set; }
        public int PackingNoteNumber { get; set; }
        public string PackingNoteStatus { get; set; }
       // public DateTime PackingNoteCreationDate { get; set; }
        public string Note { get; set; }


        public int OrderId { get; set; }
        public int OrderNumber { get; set; }
        public DateTime OrderCreationDate { get; set; }
        public string OrderStatus { get; set; }

        public int ProductNumber { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string Shape { get; set; }
        public string Purpose { get; set; }

        // DUE TO PN
        public int Amount { get; set; }

        public IEnumerable<ProductIndexListingModel> Products { get; set; }
    }
}
