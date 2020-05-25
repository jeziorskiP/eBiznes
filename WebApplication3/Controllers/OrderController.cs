using DataContext.Interfaces;
using DataContext.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Cart;
using WebApplication3.Models.Helper;
using WebApplication3.Models.Order;
using WebApplication3.Models.Product;

namespace WebApplication3.Controllers
{
    public class OrderController : Controller
    {
        private IOrder _order;
        private IInvoice _invoice;
        public OrderController(IOrder order, IInvoice invoice)
        {
            _order = order;
            _invoice = invoice;
        }

        public IActionResult CreateOrder(string CartId)
        {
            //var cartId = model.ShoppingCart.ShoppingCartId;
            var cartElements = _order.GetShoppingCartItemsToOrder(CartId);
            Order order = new Order()
            {
                OrderNumber = _order.NextNumber(),
                OrderedDate = DateTime.Now,
                Status = "Utworzono"
                
            };
            _order.AddOrder(order);

            //int OrderId = _order.GetLastId();
            foreach (var item in cartElements)
            {
                OrderLine orderLine = new OrderLine()
                {
                    Amount = item.Amount,
                    Price = item.product.Price,
                    Note = item.Note,
                    Product = item.product,
                    Order = order
                };
                _order.AddLine(orderLine);
            }
            _order.CalculateOrderTotalPrice(order.Id);
            return RedirectToAction("FulfillAddress", "Address", new { OrderId = order.Id });
            //return RedirectToAction("FulfillAddress", "Address");
        }

        //if action = 1 -> "confirm and pay" 
        //if action = 2 -> packingNoteDetails
        //if action = 3 -> chyba INCOICE
        //if action = 4 -> worker, only view
        public IActionResult OrderSummary(int OrderId, int ActionId, int TempId)
        {
            ViewBag.ActionId = ActionId;
            ViewBag.Temp = TempId;
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("ORderId " + OrderId);
            Console.WriteLine("Action " + ActionId);
            Console.WriteLine("Temp " + TempId);
            //OrderSummaryIndexListingModel
            var order = _order.GetOrder(OrderId);
            var orderLines = _order.GetAllLines(OrderId);

            var listingResult = orderLines
            .Select(item =>
                new ProductIndexListingModel
                {
                    Id = item.Product.Id,
                    Color = item.Product.Color,
                    Height = item.Product.Height,
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    ProductNumber = item.Product.ProductNumber,
                    Purpose = item.Product.Purpose,
                    Shape = item.Product.Shape,
                    Size = item.Product.Size,
                    Width = item.Product.Width
                });

            var model = new ProductIndexModel()
            {
                Products = listingResult
            };

            OrderSummaryIndexListingModel model1 = new OrderSummaryIndexListingModel()
            {
                AddressId = order.Address.Id,
                AppartmentNumber = order.Address.AppartmentNumber,
                City = order.Address.City,
                Country = order.Address.Country,
                PostCode = order.Address.PostCode,
                Street = order.Address.Street,
                StreetNumber = order.Address.StreetNumber,
                ClientId = order.Client.Id,
                ClientNumber = order.Client.ClientNumber,
                Name = order.Client.Name,
                Surname = order.Client.Surname,
                PhoneNumer = order.Client.PhoneNumer,
                email = order.Client.email,
                Products = listingResult,
                OrderId = order.Id,
                OrderNumber = order.OrderNumber,
                TotalPrice = order.TotalPrice
            };
            return View(model1);
        }

        public IActionResult Pay(int OrderId)
        {
            ViewBag.OrderId = OrderId;
            //swtorz fakture
            int InvoiceId = _invoice.PrepareInvoice(OrderId);
            ViewBag.InvoiceId = InvoiceId;
            ViewBag.Total = _invoice.CalculateTotalPrice(InvoiceId);
            return View();
        }

        public IActionResult PAYUPage(int OrderId)
        {
            _order.ChangeStatus(OrderId, "Zapłacono");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult GetAll(string filter, int OrderNumber)
        {
            if(OrderNumber != 0)
            {
                var listingResult = _order.GetAll().Where(o => o.OrderNumber == OrderNumber)
                .Select(item =>
                    new OrderIndexListingModel
                    {
                        Id = item.Id,
                        OrderNumber = item.OrderNumber,
                        Status = item.Status,
                        OrderedDate = item.OrderedDate,
                        StatusChangeDate = item.StatusChangeDate,
                        OrderCompletenessDate = item.OrderCompletenessDate,
                        TotalPrice = item.TotalPrice
                    });

                var model = new OrderListingModel()
                {
                    Orders = listingResult
                };

                return View(model);
            }
            else
            {
                var listingResult = _order.GetAll()
                .Select(item =>
                    new OrderIndexListingModel
                    {
                        Id = item.Id,
                        OrderNumber = item.OrderNumber,
                        Status = item.Status,
                        OrderedDate = item.OrderedDate,
                        StatusChangeDate = item.StatusChangeDate,
                        OrderCompletenessDate = item.OrderCompletenessDate,
                        TotalPrice = item.TotalPrice
                    });

                    var model = new OrderListingModel()
                    {
                        Orders = listingResult
                    };

                    return View(model);
            }


        }


        public IActionResult FilterOrder()
        {
            return View();
        }


    }
}
