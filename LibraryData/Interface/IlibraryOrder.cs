using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Interface
{
    public interface IlibraryOrder
    {

        IEnumerable<Order> GetOrder(int OrderID);
        bool UpdateOrder(int id,Order order);
        bool AddOrder(Order order);
        bool DeleteOrder(int OrderID);

        IEnumerable<Order> GetAllOrder();
        IEnumerable<Order> GetAllOrdersForUser(int userID);
    }
}
