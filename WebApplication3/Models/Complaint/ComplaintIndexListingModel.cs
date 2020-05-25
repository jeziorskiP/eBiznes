using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.Complaint
{
    public class ComplaintIndexListingModel
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ChangeDate { get; set; }
        //public int UserId { get; set; }
        public string Description { get; set; }
        public string Resolution { get; set; }
    }
}
