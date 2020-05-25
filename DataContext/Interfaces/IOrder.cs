using DataContext.Model;
using DataContext.Model.Shopping_Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext.Interfaces
{
    public interface IOrder
    {
        void AddOrder(Order order);
        IEnumerable<Order> GetAll();
        Order GetOrder(int OrderId);
        List<ShoppingCartItem> GetShoppingCartItemsToOrder(string ShoppingCartId);
        int NextNumber();
        int GetLastId();
        void AddLine(OrderLine orderLine);
        IEnumerable<OrderLine> GetAllLines(int OrderId);
        IEnumerable<OrderLine> GetAllLines();

        void CalculateOrderTotalPrice(int OrderId);
        IEnumerable<Order> GetAllPaidOrders();
        IEnumerable<OrderLine> GetAllPaidOrderLines();
        void ChangeStatus(int OrderId, string Status);
        int GetIdByOrderNumber(int OrderNumber);

    }
}
