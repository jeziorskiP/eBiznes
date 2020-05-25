using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.Complaint
{
    public class ComplaintListingModel
    {
        public IEnumerable<ComplaintIndexListingModel> Complaints { get; set; }
    }
}
