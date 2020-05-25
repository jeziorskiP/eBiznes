using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext.Model
{
    public class PackingNoteLine
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public PackingNote PackingNote { get; set; }  // one -> many
        public Product Product { get; set; }  // one -> many

    }
}
