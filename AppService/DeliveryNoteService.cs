using DataContext;
using DataContext.Interfaces;
using DataContext.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppService
{
    public class DeliveryNoteService : IDeliveryNote
    {
        private AppDbContext _dbContext;

        public DeliveryNoteService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddDeliveryNote(DeliveryNote deliveryNote)
        {
            _dbContext.DeliveryNotes.Add(deliveryNote);
            _dbContext.SaveChanges();
        }

        public void AddDeliveryNoteLine(DeliveryNoteLine deliveryNoteLine)
        {
            _dbContext.DeliveryNoteLines.Add(deliveryNoteLine);
            _dbContext.SaveChanges();
        }

        public void ChangeStatus(int DeliveryNoteId, string Status)
        {
            GetDeliveryNote(DeliveryNoteId).Status = Status;
            _dbContext.SaveChanges();
        }

        // PackingNoteId OR OrderId
        public void CreateDeliveryNote(int PackingNoteId, int OrderId, string Note)
        {
            var data = _dbContext.PackingNoteLines
                .Include(p => p.Product)
                .Include(o => o.Order)
                .Include(pn => pn.PackingNote)
                .Where(ppp => ppp.Status == "Gotowe do wysyłki");

            // OrderId = xxx
            if (PackingNoteId == 0)
            {
                var order = _dbContext.Orders.FirstOrDefault(o => o.Id == OrderId);
                var pnl = data.Where(o => o.OrderId == OrderId).ToList();
                DeliveryNote dn = new DeliveryNote()
                {
                    CreationTime = DateTime.Now,
                    OrderId = OrderId,
                    Order = order,
                    OrderNumber = order.OrderNumber,
                    DeliveryNoteNumber = NextNumber(),
                    Status = "Rozpoczęto"
                };
                AddDeliveryNote(dn);
 
                for (int i = 0; i < pnl.Count(); i++)
                {
                    DeliveryNoteLine dnl = new DeliveryNoteLine()
                    {
                        DeliveryNote = dn,
                        Amount = pnl[i].Amount,
                        Product = pnl[i].Product,
                        Notes = Note
                    };
                    AddDeliveryNoteLine(dnl);
                }
            }

            // PackingNote = xxx
            else if(OrderId == 0)
            {
                var pnl = data.Where(pn => pn.PackingNote.Id == PackingNoteId).ToList();

                // OrderId 
                var orders = from s in pnl
                                 group s by s.OrderId into g
                                 select new
                                 {
                                     OrderId = (int) g.Key
                                 };

                List<int> temp = new List<int>();
                foreach (var item in orders)
                {
                    temp.Add(item.OrderId);
                }

                Console.WriteLine("LICZBA" + temp.Count());
                for (int i = 0; i < temp.Count(); i++)
                {
                    Console.WriteLine("Petla: " + i);
                    var order = _dbContext.Orders.FirstOrDefault(o => o.Id == temp[i]);
                    DeliveryNote dn = new DeliveryNote()
                    {
                        CreationTime = DateTime.Now,
                        DeliveryNoteNumber = NextNumber(),
                        Order = order,
                        OrderId = order.Id,
                        OrderNumber = order.OrderNumber,
                        Status = "Rozpoczęte"
                    };
                    int j = 0;
                    var pnlO = pnl.Where(o => o.OrderId == order.Id).ToList();
                    for ( j = 0; j < pnlO.Count(); j++)
                    {
                        DeliveryNoteLine dnl = new DeliveryNoteLine()
                        {
                            Notes = Note,
                            Product = pnlO[j].Product,
                            Amount = pnlO[j].Amount,
                            DeliveryNote = dn
                        };
                        AddDeliveryNoteLine(dnl);
                    }

                }

            }
        }

        public void DeleteDeliveryNote()
        {
            throw new NotImplementedException();
        }

        public void FinishOrder(int DeliveryNoteId)
        {
            var Order = GetOrder(DeliveryNoteId);
            Order.OrderCompletenessDate = DateTime.Now;
            _dbContext.SaveChanges();
        }

        public IEnumerable<DeliveryNote> GetAll()
        {
            return _dbContext.DeliveryNotes
                .Include(dnl => dnl.DeliveryNoteLines);
        }



        public IEnumerable<Order> GetAllOrders()
        {
            var all = _dbContext.Orders
            .Include(a => a.Address)
            .Include(c => c.Client)
            .Include(cc => cc.Complaints)
            .Include(o => o.OrderLines);

            return all;
        }

        public int GetDeliveryID(int DeliveryNumber)
        {
            return GetAll().FirstOrDefault(dn => dn.DeliveryNoteNumber == DeliveryNumber).Id;
        }

        public DeliveryNote GetDeliveryNote(int DeliveryNoteId)
        {
            return GetAll().FirstOrDefault(dn => dn.Id == DeliveryNoteId);
        }

        public IEnumerable<DeliveryNoteLine> GetDeliveryNoteLines(int DeliveryNoteId)
        {
            return _dbContext.DeliveryNoteLines
                .Include(p => p.Product)
                .Include(dn => dn.DeliveryNote)
                .Where(dnl => dnl.DeliveryNote.Id == DeliveryNoteId);
        }

        public Order GetOrder(int DeliveryNoteId)
        {
            int OrderId = GetDeliveryNote(DeliveryNoteId).OrderId;
            return GetAllOrders().FirstOrDefault(o => o.Id == OrderId);
        }

        public int NextNumber()
        {
            if (GetAll().Count() == 0)
                return 100000;
            else
                return GetAll().OrderBy(o => o.DeliveryNoteNumber).Last().DeliveryNoteNumber + 1;
        }
    }
}
