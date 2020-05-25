using DataContext.Interfaces;
using DataContext.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Address;

namespace WebApplication3.Controllers
{
    public class AddressController : Controller
    {
        private IAddress _address;
        public AddressController(IAddress address)
        {
            _address = address;
        }

        public IActionResult FulfillAddress(int OrderId)
        {

            Console.WriteLine(" ????????????????????Najpierw???????????????????????????????//// ");

            Console.WriteLine("ORDER : " + OrderId);
            ViewBag.OrderId = OrderId;
            return View();
        }
        [HttpPost]
        public IActionResult FulfillAddress(AddressIndexListingModel model, int OrderId)
        {

            Console.WriteLine(" ????????????????????PONEIJZS???????????????????????????????//// ");
            Address address = new Address()
            {
                Street = model.Street,
                StreetNumber = model.StreetNumber,
                AppartmentNumber = model.AppartmentNumber,
                PostCode = model.PostCode,
                City = model.City,
                Country = model.Country
            };

            _address.AddAddress(address);
            _address.AddAddressToOrder(address.Id, OrderId);

            return RedirectToAction("FulfillClient", "Client", new { OrderId = OrderId });
            //return View();
        }



    }
}
