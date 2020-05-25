using DataContext;
using DataContext.Interfaces;
using DataContext.Model;
using DataContext.Model.Shopping_Cart;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppService
{
    public class OrderService : IOrder
    {
        private AppDbContext _dbContext;
        public OrderService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddLine(OrderLine orderLine)
        {
            _dbContext.OrderLines.Add(orderLine);
            _dbContext.SaveChanges();
        }

        public void AddOrder(Order order)
        {
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
        }

        public void CalculateOrderTotalPrice(int OrderId)
        {
            decimal total = 0;
            var lines = GetAllLines(OrderId);
            foreach (var item in lines)
            {
                total = total + item.Price;
            }
            GetOrder(OrderId).TotalPrice = total;
            _dbContext.SaveChanges();

        }

        public void ChangeStatus(int OrderId, string Status)
        {
            GetOrder(OrderId).Status = Status;
            GetOrder(OrderId).StatusChangeDate = DateTime.Now;
            _dbContext.SaveChanges();

        }

        public IEnumerable<Order> GetAll()
        {
            var all = _dbContext.Orders
                        .Include(a => a.Address)
                        .Include(c => c.Client)
                        .Include(cc => cc.Complaints)
                        .Include(o => o.OrderLines);
                        //.Include(ol => ol.OrderLines.Select(p => p.Product));

            return all;
        }

        public IEnumerable<OrderLine> GetAllLines(int OrderId)
        {
            return _dbContext.OrderLines.Include(p => p.Product).Where(ol => ol.Order.Id == OrderId);
        }

        public IEnumerable<OrderLine> GetAllLines()
        {
            return _dbContext.OrderLines.Include(p => p.Product).Include(o => o.Order);
        }

        public IEnumerable<OrderLine> GetAllPaidOrderLines()
        {
            return GetAllLines().Where(p => p.Order.Status == "Zapłacono");
        }

        public IEnumerable<Order> GetAllPaidOrders()
        {
            return GetAll();
        }

        public int GetIdByOrderNumber(int OrderNumber)
        {
            return GetAll().FirstOrDefault(o => o.OrderNumber == OrderNumber).Id;
        }

        public int GetLastId()
        {
            return GetAll().OrderBy(o => o.Id).Last().Id;
        }

        public Order GetOrder(int OrderId)
        {
            return GetAll().FirstOrDefault(o => o.Id == OrderId);
        }

        public List<ShoppingCartItem> GetShoppingCartItemsToOrder(string ShoppingCartId)
        {
            return _dbContext.shoppingCartItems
                    .Where(c => c.ShoppingCartId == ShoppingCartId)
                    .Include(p => p.product)
                    .ToList();
        }

        public int NextNumber()
        {
            if (GetAll().Count() == 0)
                return 100000;
            else
                return GetAll().OrderBy(o => o.OrderNumber).Last().OrderNumber + 1;
        }
    }
}
