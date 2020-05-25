using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.DeliveryNotes
{
    public class DeliveryNoteIndexListingModel
    {
        public int DeliveryNoteId { get; set; }
        public int DeliveryNoteNumber { get; set; }

        public int OrderId { get; set; }
        public int OrderNumber { get; set; }

        public string DeliveryNoteStatus { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
