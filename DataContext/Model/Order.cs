using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext.Model
{
    public class Order
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        //Client
        public DateTime OrderedDate { get; set; }
        public DateTime OrderCompletenessDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public DateTime StatusChangeDate { get; set; }

        public Address Address { get; set; }  // one -> many
        public Client Client { get; set; }  // one -> many
        public Invoice Invoice { get; set; }  // one -> ONE
        public IList<PackingNoteLine> PackingNoteLine { get; set; } 
        public DeliveryNote DeliveryNote { get; set; }  // one -> ONE

        public IList<Complaint> Complaints { get; set; }
        public IList<OrderLine> OrderLines { get; set; }
    }
}
