using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext.Model
{
    public class DeliveryNote
    {
        public int Id { get; set; }
        public int DeliveryNoteNumber { get; set; }

        public int OrderId { get; set; }
        public int OrderNumber { get; set; }
        public Order Order { get; set; }

        public string Status { get; set; }
        public DateTime CreationTime { get; set; }
        public IList<DeliveryNoteLine> DeliveryNoteLines { get; set; }
    }
}
