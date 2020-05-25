using DataContext.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.DeliveryNotes;

namespace WebApplication3.Controllers
{
    public class DeliveryNoteController : Controller
    {
        private IDeliveryNote _deliveryNote;
        private IOrder _order;

        public DeliveryNoteController(IDeliveryNote deliveryNote, IOrder order)
        {
            _deliveryNote = deliveryNote;
            _order = order;
        }

        public IActionResult Manager()
        {
            return View();
        }

        public IActionResult ReviewOrder(int OrderId)
        {
            return View();
        }

        // Show all DeliveryNotes + filtr
        public IActionResult ReviewDeliveryNotes()
        {
            var listingResult = _deliveryNote.GetAll()
             .Select(result => new DeliveryNoteIndexListingModel
             {
                 DeliveryNoteId = result.Id,
                 CreationTime = result.CreationTime,
                 DeliveryNoteNumber = result.DeliveryNoteNumber,
                 DeliveryNoteStatus = result.Status,
                 OrderId = result.OrderId,
                 OrderNumber = result.OrderNumber
             }) ;
            var model = new DeliveryNoteListingModel()
            {
                 DeliveryNotes = listingResult
            };


            return View(model);
        }



        // Show DeliveryNote's details + filtr
        public IActionResult DeliveryNoteDetails(int DeliveryNoteId)
        {
            var dn = _deliveryNote.GetOrder(DeliveryNoteId);
            Console.WriteLine("CLient" + dn.Client.ClientNumber);
            var cos = _deliveryNote.GetDeliveryNoteLines(DeliveryNoteId);
            var listingResult = cos // _packingNote.GetAllLines(PackingNoteId)
            .Select(item =>
                new DNDetailsIndexListingModel
                {
                    OrderId = dn.Id,
                    OrderNumber = dn.OrderNumber,
                    OrderCreationDate = dn.OrderedDate,
                    OrderStatus = dn.Status,


                    ProductName = item.Product.Name,
                    ProductNumber = item.Product.ProductNumber,
                    ProductAmount = item.Amount,

                    AddressId = dn.Address.Id,
                    AddressStreet = dn.Address.Street,
                    AddressStreetNumber = dn.Address.StreetNumber,
                    AddressAppartmentNumber = dn.Address.AppartmentNumber,
                    AddressPostCode = dn.Address.PostCode,
                    AddressCity = dn.Address.City,
                    AddressCountry = dn.Address.Country,


                    ClientId = dn.Client.Id,
                    ClientNumber = dn.Client.ClientNumber,
                    ClientName = dn.Client.Name,
                    ClientSurname = dn.Client.Surname,
                    ClientPhoneNumer = dn.Client.PhoneNumer,
                    Clientemail = dn.Client.email
                });

            var model = new DNDetailsListingModel()
            {
                DeliveryNotes = listingResult
            };

            return View(model);
        }


        public IActionResult ConfirmDelivery(int DeliveryNoteId, bool DeliveryNote)
        {
            if (DeliveryNote)
            {
                var dnID = _deliveryNote.GetDeliveryID(DeliveryNoteId); //DeliveryNOteId = DNNumber
                if(dnID != 0)
                {
                    _deliveryNote.ChangeStatus(dnID, "Wysyłka");
                    _order.ChangeStatus(_deliveryNote.GetOrder(dnID).Id, "Ukończono");
                    _deliveryNote.FinishOrder(dnID); //Complintessdate
                    return RedirectToAction("ReviewDeliveryNotes", "DeliveryNote");
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                _deliveryNote.ChangeStatus(DeliveryNoteId, "Wysyłka");
                _order.ChangeStatus(_deliveryNote.GetOrder(DeliveryNoteId).Id, "Ukończono");
                _deliveryNote.FinishOrder(DeliveryNoteId); //Complintessdate
                return RedirectToAction("ReviewDeliveryNotes", "DeliveryNote");
            }
        }

        public IActionResult InsertConfirmDelivery()
        {
            return View();
        }

        
    }
}
