using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.Client
{
    public class ClientIndexListingModel
    {
        public int Id { get; set; }
        public int ClientNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumer { get; set; }
        public string email { get; set; }
    }
}
