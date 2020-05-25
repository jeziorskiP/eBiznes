using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataContext.Model
{
    public class Client
    {
        public int Id { get; set; }
        public int ClientNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [StringLength(9)]
        public string PhoneNumer { get; set; }
        public string email { get; set; }
        public IList<Order> Orders { get; set; }

    }
}
