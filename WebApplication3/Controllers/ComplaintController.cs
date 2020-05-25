using DataContext.Interfaces;
using DataContext.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Complaint;
using WebApplication3.Models.Helper;

namespace WebApplication3.Controllers
{
    public class ComplaintController : Controller
    {
        private IComplaint _complaint;
        private IOrder _order;
        public ComplaintController(IComplaint complaint, IOrder order)
        {
            _complaint = complaint;
            _order = order;
        }

        public IActionResult ComplaintManager()
        {
            return View();
        }


        public IActionResult SubmitComplaint()
        {
            return View();
        }



        public IActionResult CreateComplaint(int OrderNumber, string email)
        {
            var check = _complaint.CheckOrder(OrderNumber, email);
            Console.WriteLine("DDDDDDDDDDPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPP");
            Console.WriteLine("CHECK: " + check);
            if (check)
            {
                Console.WriteLine("INSEIDE");
                ViewBag.OrderId = _complaint.GetOrderId(OrderNumber);
                //int OrderId = _complaint.GetOrderId(OrderNumber);
                return View();
                //return RedirectToAction("CreateComplaint", new { OrderId = OrderId });
            }
            else
                return RedirectToAction("Index", "Home");
            //ViewBag.OrderId = OrderId;

        }
        [HttpPost]
        public IActionResult CreateComplaint(ComplaintIndexListingModel model, int OrderId)
        {
            Complaint complaint = new Complaint()
            {
                CreateDate = DateTime.Now,
                Description = model.Description,
                Order = _order.GetOrder(OrderId),
                OrderNumber = _order.GetOrder(OrderId).OrderNumber
            };
            _complaint.AddComplaint(complaint);

            var test = _complaint.GetComplaint(complaint.Id);
            if (test != null)
                _complaint.ChangeStatus(test.Id, "Złożone");
            else
                _complaint.ChangeStatus(test.Id, "Błąd");
            return RedirectToAction("SendComplaint", "Complaint", new { ComplaintId = test.Id });
        }

        public IActionResult SendComplaint(int ComplaintId)
        {
            var result = _complaint.GetComplaint(ComplaintId);
            ComplaintIndexListingModel model = new ComplaintIndexListingModel()
            {
                OrderNumber = result.OrderNumber,
                Status = result.Status,
                CreateDate = result.CreateDate,
                Description = result.Description
            };

            return View(model);
        }
        // Check complaint
        public IActionResult CheckComplaint()
        {
            return View();
        }


        // [HttpPost]
        public IActionResult ShowComplaint(int OrderNumber, string email)
        {
            var check = _complaint.CheckOrder(OrderNumber, email);
            Console.WriteLine("QQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQ");
            Console.WriteLine("CHECK: " + check);
            Console.WriteLine("OrderNumber: " + OrderNumber);
            Console.WriteLine("email: " + email);
            if (check)
            {
                //var complaint = _complaint.GetComplaintByOrder(OrderNumber);

                var listingResult = _complaint.GetAllByOrder(OrderNumber)
                    .Select(result =>
                    new ComplaintIndexListingModel
                    {
                        Id = result.Id,
                        OrderNumber = result.OrderNumber,
                        CreateDate = result.CreateDate,
                        Description = result.Description,
                        Status = result.Status,
                        Resolution = result.Resolution
                    });
                var model = new ComplaintListingModel()
                {
                    Complaints = listingResult
                };
                return View(model);
                //return RedirectToAction("CreateComplaint", "Complaint", new { OrderId = _complaint.GetOrderId(OrderNumber) });
            }
            else
                return RedirectToAction("Index", "Home");
        }


        public IActionResult ResolveComplaint(int ComplaintId)
        {
            ViewBag.ComplaintId = ComplaintId;
            var result = _complaint.GetComplaint(ComplaintId);
            ComplaintIndexListingModel model = new ComplaintIndexListingModel()
            {
                OrderNumber = result.OrderNumber,
                Status = result.Status,
                CreateDate = result.CreateDate,
                Description = result.Description,
                ChangeDate = result.ChangeDate,
                Resolution = result.Resolution
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult ResolveComplaint(ComplaintIndexListingModel model, int ComplaintId)
        {
            var Complaint = _complaint.GetComplaint(ComplaintId);
            if (Complaint != null)
            {
                Complaint.Resolution = model.Resolution;
                Complaint.Status = model.Status;
                Complaint.ChangeDate = DateTime.Now;
                _complaint.UpdateComplaint(Complaint);

            }

            return RedirectToAction("Index", "Home");



            //return View();
        }

        public IActionResult GetAll()
        {
            var complaints = _complaint.GetAll();
            if (complaints != null)
            {
                //var complaint = _complaint.GetComplaintByOrder(OrderNumber);

                var listingResult = complaints
                    .Select(result =>
                    new ComplaintIndexListingModel
                    {
                        Id = result.Id,
                        OrderNumber = result.OrderNumber,
                        CreateDate = result.CreateDate,
                        Description = result.Description,
                        Status = result.Status,
                        Resolution = result.Resolution
                    });
                var model = new ComplaintListingModel()
                {
                    Complaints = listingResult
                };
                return View(model);
            }
            else
                return RedirectToAction("Index", "Home");




        }
    }
}
