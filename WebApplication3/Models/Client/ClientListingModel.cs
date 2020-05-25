using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.Client
{
    public class ClientListingModel
    {
        public IEnumerable<ClientIndexListingModel> Clients { get; set; }
    }
}
