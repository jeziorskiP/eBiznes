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
    public class PackingNoteService : IPackingNote
    {
        private AppDbContext _dbContext;
        public PackingNoteService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddPackingNote(PackingNote packingNote)
        {
            _dbContext.PackingNotes.Add(packingNote);
            _dbContext.SaveChanges();
        }

        public void AddPackingNoteLine(PackingNoteLine packingNoteLine)
        {
            _dbContext.Add(packingNoteLine);
            _dbContext.SaveChanges();
        }

        public void ChangeOrderStatus(int OrderId, string Status)
        {
            _dbContext.Orders.FirstOrDefault(o => o.Id == OrderId).Status = Status;
            _dbContext.SaveChanges();
        }

        // Change status for all orders assign to given PackingNote
        public void ChangeOrderStatusByPN(int PackingNoteId, string Status)
        {
            var orders = _dbContext.PackingNoteLines
                            .Where(pn => pn.PackingNote.Id == PackingNoteId)
                            .GroupBy(o => o.OrderId)
                            .Select(x => new { OrderId = x.Key })
                            .ToList();

            for (int i = 0; i < orders.Count(); i++)
            {
                ChangeOrderStatus(orders[i].OrderId, Status);
            }

        }

        // Change Status in PackingNote AND in PackingNoteLINES
        public void ChangeStatus(int PackingNoteId, string Status)
        {
            GetPackingNote(PackingNoteId).Status = Status;
            var lines = GetAllLines(PackingNoteId).ToList();
            for (int i = 0; i < lines.Count(); i++)
            {
                lines[i].Status = Status;
            }
            _dbContext.SaveChanges();
        }

        // PackingNoteId = 0 (null)-> change only lines with given OrderId
        // OrderId = 0 (null) -> change whole lines assign to given PackingNoteId
        // OrderId + PackingNote -> Clear 
        public void ChangeStatus(int OrderId, int PackingNoteId, string Status)
        {
            // PackingNote = xxx
            if(OrderId <=0)
            {
                var lines = GetAllLines(PackingNoteId).ToList();
                for (int i = 0; i < lines.Count(); i++)
                {
                    lines[i].Status = Status;
                    _dbContext.SaveChanges();
                }
                GetPackingNote(PackingNoteId).Status = Status;
                _dbContext.SaveChanges();
            }
            // OrderId = xxx
            else if(PackingNoteId <=0)
            {
                var lines = GetAllLines(0).Where(o => o.OrderId == OrderId).ToList();
                for (int i = 0; i < lines.Count(); i++)
                {
                    lines[i].Status = Status;
                    _dbContext.SaveChanges();
                }
            }

        }

        public bool CheckPackingNote(int OrderId)
        {
            var isGood = GetAllLines(0).Where(p => p.OrderId == OrderId);
            if (isGood.Count() <=0)
                return false;
            else
                return true;
        }

        public void CheckPackingNote(IEnumerable<OrderLine> orderLines)
        {
            for (int i = 0; i < orderLines.Count(); i++)
            {
                var order = _dbContext.Orders.FirstOrDefault(o => o.Id == orderLines.ToList()[i].Order.Id);
                // if PN was created - change status
                if (CheckPackingNote(orderLines.ToList()[i].Order.Id))
                {
                    Console.WriteLine("UTWORZONO PN I ZMIENIAM STATUS");
                    if (order.Status != "Magazyn")
                        ChangeOrderStatus(order.Id,"Magazyn");
                }
                else
                    ChangeOrderStatus(order.Id, "Magazyn");
            }
        }

        public void CorrectPackingNoteStatus(int OrderId)
        {
            var pnID = GetAllLines(0)
                        .FirstOrDefault(o => o.OrderId == OrderId).PackingNote.Id;

            var pnl = GetAllLines(0)
                        .Where(pn => pn.PackingNote.Id == pnID)
                        .Where(p => p.Status != "Gotowe do wysyłki");
            if(pnl.Count() <= 0)
            {
                ChangeStatus(pnID, "Gotowe do wysyłki");
            }

        }

        public void CreatePackingNotes(IEnumerable<OrderLine> orderLines)
        {

            if(orderLines.Count() != 0)
            {
                PackingNote packingNote = new PackingNote()
                {
                    CreationDate = DateTime.Now,
                    PackingNoteNumber = NextNumber(),
                    Status = "Rozpoczęte"
                };
                AddPackingNote(packingNote);

                for (int i = 0; i < orderLines.Count(); i++)
                {
                    Console.WriteLine("CREATREING: " + i);
                    PackingNoteLine pnl = null;
                    pnl = new PackingNoteLine()
                    {
                        Amount = orderLines.ToList()[i].Amount,
                        Product = orderLines.ToList()[i].Product,
                        Order = orderLines.ToList()[i].Order,
                        OrderId = orderLines.ToList()[i].Order.Id,
                        Notes = orderLines.ToList()[i].Note,
                        PackingNote = packingNote,
                        Status = "Rozpoczęte"
                    };
                    AddPackingNoteLine(pnl);
                }
            }

            
    }


        public void DeletePackingNote()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PackingNote> GetAll()
        {
            return _dbContext.PackingNotes.Include(pnl => pnl.PackingNoteLines);
        }

        // PackingNoteId < 1000 or null -> getAllLines
        public IEnumerable<PackingNoteLine> GetAllLines(int PackingNoteId)
        {
            if( PackingNoteId == 0)
            {
                return _dbContext.PackingNoteLines
                .Include(p => p.Product)
                .Include(o => o.Order)
                .Include(pn => pn.PackingNote);
            }
            else
            {
                return _dbContext.PackingNoteLines
                .Include(p => p.Product)
                .Include(o => o.Order)
                .Include(pn => pn.PackingNote)
                .Where(ppn => ppn.PackingNote.Id == PackingNoteId);
            }
            
        }

        public PackingNote GetPackingNote(int PackingNoteId)
        {
            return GetAll().FirstOrDefault(pn => pn.Id == PackingNoteId);
        }

        public int NextNumber()
        {
            if (GetAll().Count() == 0)
                return 100000;
            else
                return GetAll().OrderBy(o => o.PackingNoteNumber).Last().PackingNoteNumber + 1;
        }

        public string PrepareList(int PackingNoteId)
        {
            var lines = GetAllLines(PackingNoteId);
            string str = "Zamówienie <br/><br/>";
            str +="Nr wewnetrzny: " + GetPackingNote(PackingNoteId).PackingNoteNumber+ "<br/>";
            var xxx = lines
                    .GroupBy(p => p.Product.ProductNumber)
                    .Select(x => new { ProdNumber = x.Key, ProdSum = x.Select(y => y.Amount).Sum() });

            

            foreach (var item in xxx)
            {
                str += item.ProdNumber + "  -  " + item.ProdSum + "<br/>";
            }
            str += "<br/><br/>";
            var spec = lines.Where(p => p.Notes != "");
            if (spec.Count() > 0)
                str += "Wsród wyżej wymienionych, znajduja sie specjalne zamowienia, opisane ponizej<br/><br/>";
            foreach (var item in spec)
            {
                str += item.Product.ProductNumber + "  -  " + item.Amount +"  -  " + item.Notes + "<br/>";
            }
            str += "<br/><br/>";


            return str;
        }
    }
}
