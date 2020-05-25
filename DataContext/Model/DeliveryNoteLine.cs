using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext.Model
{
    public class DeliveryNoteLine
    {
        public int Id { get; set; }
        public int Amount { get; set; }

        public string Notes { get; set; }
        public DeliveryNote DeliveryNote { get; set; }  // one -> many
        public Product Product { get; set; }  // one -> many

    }
}
