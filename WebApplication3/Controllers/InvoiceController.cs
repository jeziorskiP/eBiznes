using DataContext.Interfaces;
using DataContext.Model;
using DinkToPdf;
using DinkToPdf.Contracts;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using Syncfusion.Drawing;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using WebApplication3.Models.Helper;
using WebApplication3.Models.Invoice;
using PdfDocument = Syncfusion.Pdf.PdfDocument;

namespace WebApplication3.Controllers
{
    public class InvoiceController : Controller
    {
        private IInvoice _invoice;
        private IOrder _order;
        private IHelper _helper;
        private IConverter _converter;


        public InvoiceController(IInvoice invoice, IOrder order, IConverter converter, IHelper helper)
        {
            _invoice = invoice;
            _order = order;
            _converter = converter;
            _helper = helper;
        }

        public IActionResult Index()
        {
            HtmlToPdfConverter converter = new HtmlToPdfConverter();
            WebKitConverterSettings settings = new WebKitConverterSettings();
            settings.WebKitPath = Path.Combine(Environment.CurrentDirectory, "QtBinariesWindows");

            converter.ConverterSettings = settings;
            string path = "https://localhost:44347/Invoice/ShowInvoice?OrderId=2015";
            PdfDocument document = converter.Convert("https://www.google.com");
            
            MemoryStream ms = new MemoryStream();
            document.Save(ms);

            document.Close(true);

            ms.Position = 0;
            FileStreamResult fileStreamResult = new FileStreamResult(ms, "application/pdf");
            fileStreamResult.FileDownloadName = "CHUJ.pdf";
            return fileStreamResult;

        }




        // Po orderId tylko
        //ActionID => 2 - GetAll incoice
        public IActionResult ShowInvoice(int InvoiceId, int OrderId, string email, int ActionId)
        {
            ViewBag.ActionId = ActionId;
            var InvoiceMod = _invoice.GetInvoiceByOrderId(OrderId);
            var OrderMod = _order.GetAll().FirstOrDefault(o => o.Id == OrderId);
            var InvoiceLinesMod = _invoice.GetInvoiceLines(InvoiceMod.Id);


            int C = 0;
            var listingResultIL = InvoiceLinesMod // _packingNote.GetAllLines(PackingNoteId)
                .Select(item =>
                    new InvoiceLineIndexListingModel
                    {
                        InvoiceLineId = item.Id,
                        ProductName = item.Product.Name,
                        Amount = item.Amount,
                        Price = item.Price,
                        PriceNetto = Math.Round( Decimal.Divide(item.Price,1.23m),2),
                        Tax = Math.Round(item.Price - (Math.Round(Decimal.Divide(item.Price, 1.23m), 2))   ,2),

                        TotalPrice = item.TotalPrice
                    });
            decimal sumA = 0.0m;    
            decimal sumT = 0.0m;
            decimal sumN = 0.0m;
            int cnt = 1;
            foreach (var item in listingResultIL)
            {
                sumA += item.TotalPrice;
                sumT += item.Tax;
                sumN += Math.Round(Decimal.Divide(item.Price, 1.23m), 2);
                item.LP = cnt;
                Console.WriteLine("DDDDDDDDDDDDDDDD " + item.LP);
                Console.WriteLine("DDDDDDDDDDDDDDDD " + cnt);
                cnt++;
            }


            Console.WriteLine("DDDDDDDDDDDDDDDD 222 " + listingResultIL.ToList()[0].LP );
            
            var model = new InvoiceLinesListingModel()
            {
                 InvoiceLines = listingResultIL
            };

            Console.WriteLine("ILOSC: " + listingResultIL.Count());


            var listingResult = // _packingNote.GetAllLines(PackingNoteId)

                new InvoiceIndexListingModel
                {
                    OrderId = OrderMod.Id,
                    OrderNumber = OrderMod.OrderNumber,

                    InvoiceNumber = InvoiceMod.InvoiceNumber,
                    InvoiceTime = InvoiceMod.InvoiceDate,

                    AddressId = OrderMod.Address.Id,
                    AddressStreet = OrderMod.Address.Street,
                    AddressStreetNumber = OrderMod.Address.StreetNumber,
                    AddressAppartmentNumber = OrderMod.Address.AppartmentNumber,
                    AddressPostCode = OrderMod.Address.PostCode,
                    AddressCity = OrderMod.Address.City,
                    AddressCountry = OrderMod.Address.Country,


                    ClientId = OrderMod.Client.Id,
                    ClientNumber = OrderMod.Client.ClientNumber,
                    ClientName = OrderMod.Client.Name,
                    ClientSurname = OrderMod.Client.Surname,
                    ClientPhoneNumer = OrderMod.Client.PhoneNumer,
                    Clientemail = OrderMod.Client.email,

                    Lines = model,
                    TotalPriceBrutto = sumA,
                    TotalTax = sumT,
                    TotalPriceNetto = sumN

                };
            
            
            StringReader sr = new StringReader( InvoicePdfTemplete.DO(listingResult) );

            MemoryStream memoryStream = new MemoryStream();

            Document document = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(document);
            
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();
            document.Add(new Paragraph("fdefedwf"));
            htmlparser.Parse(sr);
            document.Close();

            byte[] bytes = memoryStream.ToArray();
            memoryStream = new MemoryStream();
            memoryStream.Write(bytes, 0, bytes.Length);
            memoryStream.Position = 0;
            if(email != "admin")
                _helper.InvoiceEmailSender(listingResult.Clientemail, "Faktura "+ listingResult.InvoiceNumber, memoryStream);
            //memoryStream.Close();

           // return new FileStreamResult(memoryStream,"application/pdf");

            return View(listingResult);
        }
        public IActionResult InvoiceExport(int InvoiceId, int OrderId)
        {
            var InvoiceMod = _invoice.GetInvoiceByOrderId(OrderId);
            var OrderMod = _order.GetAll().FirstOrDefault(o => o.Id == OrderId);
            var InvoiceLinesMod = _invoice.GetInvoiceLines(InvoiceMod.Id);


            int C = 0;
            var listingResultIL = InvoiceLinesMod // _packingNote.GetAllLines(PackingNoteId)
                .Select(item =>
                    new InvoiceLineIndexListingModel
                    {
                        InvoiceLineId = item.Id,
                        ProductName = item.Product.Name,
                        Amount = item.Amount,
                        Price = item.Price,
                        PriceNetto = Math.Round(Decimal.Divide(item.Price, 1.23m), 2),
                        Tax = Math.Round(item.Price - (Math.Round(Decimal.Divide(item.Price, 1.23m), 2)), 2),
                        //PriceNettoSumValue = sumN,
                        // TaxSumValue = Math.Round( item.TotalPrice - sumN,2),

                        TotalPrice = item.TotalPrice
                    });
            decimal sumA = 0.0m;
            decimal sumT = 0.0m;
            decimal sumN = 0.0m;
            int cnt = 1;
            foreach (var item in listingResultIL)
            {
                sumA += item.TotalPrice;
                sumT += item.Tax;
                sumN += Math.Round(Decimal.Divide(item.Price, 1.23m), 2);
                item.LP = cnt;
                Console.WriteLine("DDDDDDDDDDDDDDDD " + item.LP);
                Console.WriteLine("DDDDDDDDDDDDDDDD " + cnt);
                cnt++;
            }

            Console.WriteLine("DDDDDDDDDDDDDDDD 222 " + listingResultIL.ToList()[0].LP);

            var model = new InvoiceLinesListingModel()
            {
                InvoiceLines = listingResultIL
            };

            Console.WriteLine("ILOSC: " + listingResultIL.Count());


            var listingResult = // _packingNote.GetAllLines(PackingNoteId)

                new InvoiceIndexListingModel
                {
                    OrderId = OrderMod.Id,
                    OrderNumber = OrderMod.OrderNumber,

                    InvoiceNumber = InvoiceMod.InvoiceNumber,
                    InvoiceTime = InvoiceMod.InvoiceDate,

                    AddressId = OrderMod.Address.Id,
                    AddressStreet = OrderMod.Address.Street,
                    AddressStreetNumber = OrderMod.Address.StreetNumber,
                    AddressAppartmentNumber = OrderMod.Address.AppartmentNumber,
                    AddressPostCode = OrderMod.Address.PostCode,
                    AddressCity = OrderMod.Address.City,
                    AddressCountry = OrderMod.Address.Country,


                    ClientId = OrderMod.Client.Id,
                    ClientNumber = OrderMod.Client.ClientNumber,
                    ClientName = OrderMod.Client.Name,
                    ClientSurname = OrderMod.Client.Surname,
                    ClientPhoneNumer = OrderMod.Client.PhoneNumer,
                    Clientemail = OrderMod.Client.email,

                    Lines = model,
                    TotalPriceBrutto = sumA,
                    TotalTax = sumT,
                    TotalPriceNetto = sumN

                };


            StringReader sr = new StringReader(InvoicePdfTemplete.DO(listingResult));

            MemoryStream memoryStream = new MemoryStream();

            Document document = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(document);

            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();
            htmlparser.Parse(sr);
            document.Close();

            byte[] bytes = memoryStream.ToArray();
            memoryStream = new MemoryStream();
            memoryStream.Write(bytes, 0, bytes.Length);
            memoryStream.Position = 0;

            //_helper.InvoiceEmailSender("pawel.jeziorski.97@gmail.com", "TEST ATT", memoryStream);

             return new FileStreamResult(memoryStream,"application/pdf");
        }


        public IActionResult PrintInvoice(int OrderNumber, string email, string SearchBy)
        {
            if(SearchBy != "" || SearchBy != null)
            {
                if(_invoice.CheckInvoice(OrderNumber, email))       // email=admin wiec przechodzi
                {
                    Console.WriteLine("WEWNATRZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ");
                    Console.WriteLine("SERACHBY "+SearchBy  );
                    Console.WriteLine("OrderNumber " + OrderNumber);
                    if(SearchBy == "OrderNumber")
                    {
                        var orderId = _order.GetIdByOrderNumber(OrderNumber);
                        return RedirectToAction("ShowInvoice", new { OrderId = orderId });
                    }
                    else if(SearchBy == "InvoiceNumber")
                    {
                        var orderId = _invoice.GetOrderIdByInvoiceNumber(OrderNumber);  //OrderNumber = InvoiceNumber
                        return RedirectToAction("ShowInvoice", new { OrderId = orderId });
                    }
                    else
                        return RedirectToAction("Index", "Home");
                }
                else
                {
                    var or = _invoice.GetOrderIdByInvoiceNumber(OrderNumber);
                    var Order = _order.GetOrder(or);
                    if(_invoice.CheckInvoice(Order.OrderNumber, email))
                    {
                        if (SearchBy == "OrderNumber")
                        {
                            var orderId = _order.GetIdByOrderNumber(OrderNumber);
                            return RedirectToAction("ShowInvoice", new { OrderId = orderId });
                        }
                        else if (SearchBy == "InvoiceNumber")
                        {
                            var orderId = _invoice.GetOrderIdByInvoiceNumber(OrderNumber);  //OrderNumber = InvoiceNumber
                            return RedirectToAction("ShowInvoice", new { OrderId = orderId });
                        }
                        else
                            return RedirectToAction("Index", "Home");


                    }

                }
                    return RedirectToAction("Index", "Home");

            }
            else if(_invoice.CheckInvoice(OrderNumber, email))
            {
                var orderId = _order.GetIdByOrderNumber(OrderNumber);
                return RedirectToAction("ShowInvoice", new { OrderId = orderId });
            }
            else
                return RedirectToAction("Index","Home");

        }



        public IActionResult InsertPrintInvoice()
        {
            return View();
        }
        public IActionResult InsertPrintInvoices()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            var listingResultIL = _invoice.GetAll() // _packingNote.GetAllLines(PackingNoteId)
                .Select(item => new InvoiceIndexListingModel
                {
                    InvoiceId = item.Id,
                    InvoiceNumber = item.InvoiceNumber,
                    InvoiceTime = item.InvoiceDate,
                    Post = item.Posted,
                    OrderNumber = item.Order.OrderNumber,
                    OrderId = item.OrderId,
                    TotalPriceBrutto  = item.TotalPrice
                });




            var model = new InvoiceListingModel()
            {
                Invoices = listingResultIL
            };

            return View(model);
            
        }

        public IActionResult Post(int InvoiceId)
        {
            _invoice.PostInvoice(InvoiceId);
            return RedirectToAction("GetAll");
        }



        public IActionResult InsertUpdate(int InvoiceNumber)
        {
            return View();
        }

        public IActionResult Update(int InvoiceNumber)
        {
            var invoice = _invoice.GetAll().FirstOrDefault(i => i.InvoiceNumber == InvoiceNumber);
            if(invoice != null)
            {
                var invoiceLines = _invoice.GetInvoiceLines(invoice.Id)
                    .Select(item => new InvoiceLineIndexListingModel
                    {
                        InvoiceLineId = item.Id,
                        ProductName = item.Product.Name,
                        Amount = item.Amount,
                        Price = item.Price,
                        TotalPrice = item.TotalPrice
                      
                    });
                var model = new InvoiceLinesListingModel()
                {
                    InvoiceLines = invoiceLines
                };
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult UpdateLine(int InvoiceLineId)
        {
            ViewBag.InvoiceLineId = InvoiceLineId;
            Console.WriteLine("Jestm w22222222!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            var invoiceLines = _invoice.GetInvoiceLine(InvoiceLineId);

                var model =  new InvoiceLineIndexListingModel
                {
                    InvoiceLineId = invoiceLines.Id,
                    Amount = invoiceLines.Amount,
                    Price = invoiceLines.Price,
                    TotalPrice = invoiceLines.TotalPrice
                };


            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateLine(InvoiceLineIndexListingModel model, int InvoiceLineId)
        {
            Console.WriteLine("Jestm w Update!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Console.WriteLine("model "+ model.Price);
            Console.WriteLine("model ID"+ model.InvoiceLineId);
            InvoiceLine il = new InvoiceLine()
            {
                Amount = model.Amount,
                Price = model.Price,
                TotalPrice = model.TotalPrice
            };

            _invoice.UpdateInvoiceLine(il, model.InvoiceLineId);

            return RedirectToAction("Index", "Home");
        }
    }
}
