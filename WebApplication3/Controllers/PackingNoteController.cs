using DataContext.Interfaces;
using DataContext.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.PackingNotes;
using WebApplication3.Models.Product;

namespace WebApplication3.Controllers
{
    public class PackingNoteController : Controller
    {
        private IPackingNote _packingNote;
        private IOrder _order;
        private IDeliveryNote _deliveryNote;
        private IHelper _helper;
        public PackingNoteController(IPackingNote packingNote, IOrder order, IDeliveryNote deliveryNote, IHelper helper)
        {
            _packingNote = packingNote;
            _order = order;
            _deliveryNote = deliveryNote;
            _helper = helper;
        }

        public IActionResult Manager()
        {
            return View();
        }

        public IActionResult ReviewPackingNotes()
        {
            var listingResult = _packingNote.GetAll()
            .Select(item =>
                new PackingNoteIndexListingModel
                {
                    PackingNoteId = item.Id,
                    PackingNoteNumber = item.PackingNoteNumber,
                    CreationDate = item.CreationDate,
                    Status = item.Status,
                    CompletenessDate = item.CompletenessDate
                });

            var model = new PackingNoteListingModel()
            {
                PackingNotes = listingResult
            };
            return View(model);
        }

        public IActionResult PackingNoteDetails(int PackingNoteId)
        {
            var pnAll = _packingNote.GetAllLines(PackingNoteId);

            var listingResult1 = pnAll
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
                        Width = item.Product.Width,
                        Amount = item.Amount
                    });

            var listingResult = _packingNote.GetAllLines(PackingNoteId)
            .Select(item =>
                new PNDetailsIndexListingModel
                {  
                    OrderId = item.OrderId,
                    OrderNumber = item.Order.OrderNumber,
                    OrderCreationDate = item.Order.OrderedDate,
                    OrderStatus = item.Order.Status,
                    PackingNoteStatus = item.Status,
                    PackingNoteNumber = item.PackingNote.PackingNoteNumber,
                    Note = item.Notes,

                    Color = item.Product.Color,
                    Height = item.Product.Height,
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    ProductNumber = item.Product.ProductNumber,
                    Purpose = item.Product.Purpose,
                    Shape = item.Product.Shape,
                    Size = item.Product.Size,
                    Width = item.Product.Width,
                    Amount = item.Amount
                });
            
            var model = new PNDetailsListingModel()
            {
                 PackingNoteDetails= listingResult
            };
            
            return View(model);

        }

        // 1)
        public IActionResult Trigger()
        {
            _packingNote.CreatePackingNotes(_order.GetAllPaidOrderLines());
            _packingNote.CheckPackingNote(_order.GetAllPaidOrderLines());
            return View();
        }
        // 2)
        // DODAC WYSYŁANIE MAILA DO DOSTAWCY
        // Change status in PN and PNLe
        public IActionResult OrderGoods(int packingNoteId, bool PackingNote)
        {
            Console.WriteLine("PAKINGNOTE " + PackingNote);
            Console.WriteLine("PN ID " + packingNoteId);
            if (PackingNote)
            {
                Console.WriteLine("IF1111111111111");
                //packingNoteId = PackingNoteNUMBER
                var pn = _packingNote.GetAll().FirstOrDefault(p => p.PackingNoteNumber == packingNoteId);
                if(pn != null)
                {
                    Console.WriteLine("IF22222222222222222222");
                    _helper.WarehouseEmailSender("pawel.jeziorski.97@gmail.com",
                   _packingNote.GetPackingNote(pn.Id).PackingNoteNumber.ToString(),
                   _packingNote.PrepareList(pn.Id));

                    _packingNote.ChangeStatus(pn.Id, "Zlecono zamówienie");
                    return RedirectToAction("ReviewPackingNotes", "PackingNote");
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                if (_packingNote.GetPackingNote(packingNoteId) != null)
                {
                    _helper.WarehouseEmailSender("pawel.jeziorski.97@gmail.com",
                    _packingNote.GetPackingNote(packingNoteId).PackingNoteNumber.ToString(),
                    _packingNote.PrepareList(packingNoteId));

                    _packingNote.ChangeStatus(packingNoteId, "Zlecono zamówienie");
                    return RedirectToAction("ReviewPackingNotes", "PackingNote");
                }
                else

                    return RedirectToAction("Index", "Home");
            }
           

        }
        // 3)
        // Change status in PN and PNL
        public IActionResult PickUpGoods(int packingNoteId, bool PackingNote)
        {
            // PackingNoteId = packingNoteNumber
            if (PackingNote)
            {
                var pn = _packingNote.GetAll().FirstOrDefault(p => p.PackingNoteNumber == packingNoteId);
                if (pn != null)
                {
                    _packingNote.ChangeStatus(pn.Id, "Na magazynie");
                    return RedirectToAction("ReviewPackingNotes", "PackingNote");
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                _packingNote.ChangeStatus(packingNoteId, "Na magazynie");
                return RedirectToAction("ReviewPackingNotes", "PackingNote");
            }

        }

        // 4)
        public IActionResult GiveGoods(int PackingNoteId, int OrderId, string SearchBy, string Note)
        {
            Console.WriteLine("GIVEGOODS!");
            Console.WriteLine("OrderId: "+OrderId);
            // PackingNoteId = PackingNOteNumber
            if(SearchBy == "PackingNoteNumber")
            {
                var pn = _packingNote.GetAll().FirstOrDefault(p => p.PackingNoteNumber == PackingNoteId);
                if (pn != null)
                {
                    _packingNote.ChangeStatus(OrderId, pn.Id, "Gotowe do wysyłki"); //change all lines in PN
                    _packingNote.ChangeOrderStatusByPN(pn.Id, "Wysyłka");
                    _deliveryNote.CreateDeliveryNote(pn.Id, OrderId, Note);

                    return RedirectToAction("ReviewPackingNotes", "PackingNote");
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            else if(SearchBy == "OrderNumber")
            {   
                // GetAllLines(0) -> all lines
                // PackingNoteId = OrderNumber
                var pn = _packingNote.GetAllLines(0).FirstOrDefault(p => p.Order.OrderNumber == PackingNoteId);
                if (pn != null)
                {
                    _packingNote.ChangeStatus(pn.OrderId, 0, "Gotowe do wysyłki");//change selected lines in PN
                    _order.ChangeStatus(pn.OrderId, "Wysyłka");
                    _packingNote.CorrectPackingNoteStatus(pn.OrderId); //When piece of PN is preapre to Delivery
                    _deliveryNote.CreateDeliveryNote(0, pn.OrderId, Note);

                    return RedirectToAction("ReviewPackingNotes", "PackingNote");
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                _packingNote.ChangeStatus(OrderId, PackingNoteId, "Gotowe do wysyłki");
                _packingNote.ChangeOrderStatusByPN(PackingNoteId, "Wysyłka");
                _deliveryNote.CreateDeliveryNote(PackingNoteId, OrderId, "Brak");

                return RedirectToAction("ReviewPackingNotes", "PackingNote");
            }

        }

        // 4) - to wyzje jest uzywane jeszcze gdzies, wiec zmiana moglaby zepsuc wszytslo... 
        public IActionResult GiveGoods2(int PackingNoteNumber, int OrderNumber, string Note)
        {
            Console.WriteLine("GIVEGOODS!");
            Console.WriteLine("OrderNumber: " + OrderNumber);
            // PackingNOteNumber = XXX
            if (PackingNoteNumber != 0)
            {
                var pn = _packingNote.GetAll().FirstOrDefault(p => p.PackingNoteNumber == PackingNoteNumber);
                if (pn != null)
                {
                    _packingNote.ChangeStatus(OrderNumber, pn.Id, "Gotowe do wysyłki"); //change all lines in PN
                    _packingNote.ChangeOrderStatusByPN(pn.Id, "Wysyłka");
                    _deliveryNote.CreateDeliveryNote(pn.Id, OrderNumber, Note);

                    return RedirectToAction("ReviewPackingNotes", "PackingNote");
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            //OrderNumber = XXX
            else if (OrderNumber != 0)
            {
                // GetAllLines(0) -> all lines
                // PackingNoteId = OrderNumber
                var pn = _packingNote.GetAllLines(0).FirstOrDefault(p => p.Order.OrderNumber == OrderNumber);
                if (pn != null)
                {
                    _packingNote.ChangeStatus(pn.OrderId, 0, "Gotowe do wysyłki");//change selected lines in PN
                    _order.ChangeStatus(pn.OrderId, "Wysyłka");
                    _packingNote.CorrectPackingNoteStatus(pn.OrderId); //When piece of PN is preapre to Delivery
                    _deliveryNote.CreateDeliveryNote(0, pn.OrderId, Note);

                    return RedirectToAction("ReviewPackingNotes", "PackingNote");
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                /*
                _packingNote.ChangeStatus(OrderId, PackingNoteId, "Gotowe do wysyłki");
                _packingNote.ChangeOrderStatusByPN(PackingNoteId, "Wysyłka");
                _deliveryNote.CreateDeliveryNote(PackingNoteId, OrderId, "Brak");

                return RedirectToAction("ReviewPackingNotes", "PackingNote");
                */

                return RedirectToAction("Index", "Home");
            }

        }



        public IActionResult InsertOrderGoods()
        {
            return View();
        }

        public IActionResult InsertPickUpGoods()
        {
            return View();
        }
        public IActionResult InsertGiveGoods()
        {
            return View();
        }

        
        public IActionResult Review()
        {
            return View();
        }

        


        public IActionResult SpendTheGoods(int PackingNote)
        {
            return View();
        }




    }
}
