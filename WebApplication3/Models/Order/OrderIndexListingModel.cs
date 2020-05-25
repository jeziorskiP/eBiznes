using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.Order
{
    public class OrderIndexListingModel
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public DateTime OrderedDate { get; set; }
        public DateTime OrderCompletenessDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public DateTime StatusChangeDate { get; set; }
    }
}
