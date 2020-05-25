using DataContext.Interfaces;
using DataContext.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Client;

namespace WebApplication3.Controllers
{
    public class ClientController : Controller
    {
        private IClient _client;

        public ClientController(IClient client)
        {
            _client = client;
        }

        public IActionResult FulfillClient(int OrderId)
        {
            ViewBag.OrderId = OrderId;
            return View();
        }
        [HttpPost]
        public IActionResult FulfillClient(ClientIndexListingModel model, int OrderId)
        {
            Client client = new Client()
            {
                ClientNumber = _client.NextNumber(),
                Name = model.Name,
                Surname = model.Surname,
                PhoneNumer  = model.PhoneNumer,
                email = model.email
            };
            _client.AddClient(client);

            Console.WriteLine(" ??????????????????????????????????????????????????????????????//// ");

            Console.WriteLine("CLIENT  : " + client.Id);
            _client.AddClientToOrder(client.Id, OrderId);
            return RedirectToAction("OrderSummary","Order", new { OrderId = OrderId, ActionId = 1 });
        }
    }
}
