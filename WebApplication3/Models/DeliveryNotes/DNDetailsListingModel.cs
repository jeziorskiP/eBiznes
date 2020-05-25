using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.DeliveryNotes
{
    public class DNDetailsListingModel
    {

        public IEnumerable<DNDetailsIndexListingModel>  DeliveryNotes { get; set; }
        
    }
}
