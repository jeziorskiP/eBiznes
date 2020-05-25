using DataContext;
using DataContext.Interfaces;
using DataContext.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppService
{
    public class InvoiceService : IInvoice
    {
        private AppDbContext _dbContext;
        public InvoiceService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddInvoice(Invoice invoice)
        {
            _dbContext.Add(invoice);
            _dbContext.SaveChanges();
        }

        public void AddInvoiceLine(InvoiceLine invoiceLine)
        {
            _dbContext.Add(invoiceLine);
            _dbContext.SaveChanges();
        }

        public decimal CalculateTotalPrice(IEnumerable<OrderLine> orderLines)
        {
            decimal total = 0;
            foreach (var item in orderLines)
            {
                total = total +(item.Amount * item.Price);
            }
            return total;
        }

        public decimal CalculateTotalPrice(int InvoiceId)
        {
            return _dbContext.Invoices.FirstOrDefault(i => i.Id == InvoiceId).TotalPrice;
        }

        public bool CheckInvoice(int OrderNumber, string email)
        {
            // na podany orderId jest fakutra
            var In = GetAll().FirstOrDefault(o => o.Order.OrderNumber == OrderNumber);
            if (In != null)
            {
                var client = _dbContext.Clients.FirstOrDefault(c => c.email == email);
                if (client != null)
                {
                    Console.WriteLine("CLIENTID " + client.Id);
                    Order obj = _dbContext.Orders.Include(c => c.Client).FirstOrDefault(o => o.OrderNumber == OrderNumber && o.Client.Id == client.Id);

                    if (obj != null)
                        return true;
                    else
                        return false;
                }
                else if(email == "admin")
                {
                    return true;
                }
                else 
                    return false;
            }
            else
                return false;


        }

        public IEnumerable<Invoice> GetAll()
        {
            return _dbContext.Invoices.Include(i => i.InvoiceLines).Include(d =>d.Order);
        }

        public Invoice GetInvoice(int InvoiceId)
        {
            return GetAll().FirstOrDefault(i => i.Id == InvoiceId);
        }

        public Invoice GetInvoiceByOrderId(int OrderId)
        {
            return GetAll()
                .FirstOrDefault(o => o.OrderId == OrderId);
        }

        public InvoiceLine GetInvoiceLine(int InvoiceLineId)
        {
            return _dbContext.InvoiceLines.FirstOrDefault(il => il.Id == InvoiceLineId);
        }

        public IEnumerable<InvoiceLine> GetInvoiceLines(int InvoiceId)
        {
            return _dbContext.InvoiceLines
                .Include(p=>p.Product)
                .Where(i => i.Invoice.Id == InvoiceId);
        }

        public int GetOrderIdByInvoiceNumber(int InvoiceNumber)
        {
            return GetAll().FirstOrDefault(i => i.InvoiceNumber == InvoiceNumber).OrderId;
        }

        public int NextInvoiceNumber()
        {
            if (GetAll().Count() == 0)
                return 1000000;
            else
                return GetAll().OrderBy(o => o.InvoiceNumber).Last().InvoiceNumber + 1;
        }

        public void PostInvoice(int InvoiceId)
        {
            GetInvoice(InvoiceId).Posted = true;
            _dbContext.SaveChanges();
        }

        public int PrepareInvoice(int OrderId)
        {
            Order order = _dbContext.Orders.FirstOrDefault(o => o.Id == OrderId);

            IEnumerable<OrderLine> allLines = _dbContext.OrderLines.Include(p => p.Product).Where(ol => ol.Order.Id == OrderId);

            Invoice invoice = new Invoice()
            {
                InvoiceDate = DateTime.Now,
                Posted = false,
                CreateDate = DateTime.Now,
                InvoiceNumber = NextInvoiceNumber(),
                OrderId = OrderId,
                Order = order,
                TotalPrice = 0
                //InvoiceLines = xxxxx
            };

            AddInvoice(invoice);
            int InvoiceId = invoice.Id;
            //IList<InvoiceLine> lines = null;
            for (int i = 0; i < allLines.Count(); i++)
            {
                InvoiceLine il = new InvoiceLine()
                {
                    Product = allLines.ToList()[i].Product,
                    Amount = allLines.ToList()[i].Amount,
                    Price = allLines.ToList()[i].Price,
                    TotalPrice = allLines.ToList()[i].Amount * allLines.ToList()[i].Price,
                    Invoice = invoice
                };
                AddInvoiceLine(il);

            }
            /*
            foreach (var item in allLines)
            {
                InvoiceLine il = new InvoiceLine()
                {
                    Product = item.Product,
                    Amount = item.Amount,
                    Price = item.Price,
                    TotalPrice = item.Amount * item.Price,
                    Invoice = invoice
                };
                AddInvoiceLine(il);
                //lines.Add(il);
            }
            //invoice.InvoiceLines = lines;
            */
            UpdateTotalPrice(invoice.Id, CalculateTotalPrice(allLines));
            return InvoiceId;
        }

        public void SaveToPdf(int InvoiceId)
        {
           
        }

        public void UpdateInvoice(Invoice invoice)
        {
            var old = _dbContext.Invoices.FirstOrDefault(i => i.Id == invoice.Id);
            if(old != null)
            {
                old.TotalPrice = invoice.TotalPrice;
            }
            _dbContext.SaveChanges();
        }

        public void UpdateInvoiceLine(InvoiceLine invoiceLine, int InvoiceLineId)
        {
            var old = _dbContext.InvoiceLines.FirstOrDefault(il => il.Id == InvoiceLineId);
            if(old != null)
            {

                Console.WriteLine("sdasd");
                old.Price = invoiceLine.Price;
                old.Amount = invoiceLine.Amount;
                old.TotalPrice = invoiceLine.TotalPrice;
            }
            _dbContext.SaveChanges();
        }

        public void UpdateTotalPrice(int InvoiceId, decimal totalPrice)
        {
            GetAll().FirstOrDefault(i => i.Id == InvoiceId).TotalPrice = totalPrice;
            _dbContext.SaveChanges();
        }
    }
}
