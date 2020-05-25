using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.DeliveryNotes
{
    public class DNDetailsIndexListingModel
    {


        //Address
        public int AddressId { get; set; }
        public string AddressStreet { get; set; }
        public int AddressStreetNumber { get; set; }
        public int AddressAppartmentNumber { get; set; }
        public string AddressPostCode { get; set; }
        public string AddressCity { get; set; }
        public string AddressCountry { get; set; }

        //Client
        public int ClientId { get; set; }
        public int ClientNumber { get; set; }
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }
        public string ClientPhoneNumer { get; set; }
        public string Clientemail { get; set; }

        //Order
        public int OrderId { get; set; }
        public int OrderNumber { get; set; }
        public DateTime OrderCreationDate { get; set; }
        public string OrderStatus { get; set; }

        //Product
        public int ProductNumber { get; set; }
        public string ProductName { get; set; }

        // DUE TO PN
        public int ProductAmount { get; set; }




    }
}
