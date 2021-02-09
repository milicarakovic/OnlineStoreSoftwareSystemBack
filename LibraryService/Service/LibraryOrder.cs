using LibraryData;
using LibraryData.Interface;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryService.Service
{
    public class LibraryOrder : IlibraryOrder
    {
        private ApplicationDbContext _db;

        public LibraryOrder(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool AddOrder(Order order)
        {
            try
            {
                var user = _db.User.SingleOrDefault(u => u.ID == order.User.ID);

                if (user == null) return false;
                var items = AddOrderItems(order.OrderItems);

                order.User = user;
                order.OrderItems = items;

                _db.Add(order);
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public IEnumerable<OrderItem> AddOrderItems(IEnumerable<OrderItem> items)
        {

            foreach (OrderItem item in items)
            {
                var product = item.Product is null ? (null) : _db.Product.SingleOrDefault(p => p.ID == item.Product.ID);
                item.Product = product;

            }

            return items;
        }

        public bool DeleteOrder(int OrderID)
        {
            try
            {
                var order = _db.Order.SingleOrDefault(o => o.ID == OrderID);
                if (order == null) return false;

                _db.Entry(order).State = EntityState.Deleted;
                _db.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public Order GetOrder(int OrderID)
        {
            try
            {
                return _db.Order                    
                    .Include(u => u.User)
                    .Include(i => i.OrderItems).ThenInclude(p => p.Product).SingleOrDefault(x=>x.ID == OrderID);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<Order> GetAllOrder()
        {
            try
            {
                return _db.Order
                    .Include(u => u.User)
                    .Include(i => i.OrderItems).ThenInclude(p => p.Product);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public IEnumerable<Order> GetAllOrdersForUser(int userID)
        {
            try
            {
                return _db.Order.Where(o => o.User.ID == userID);
            }
            catch (Exception)
            {
                return null;
            }
        }



        public bool UpdateOrder(int OrderID, Order order)
        {
            try
            {
                var user = _db.User.SingleOrDefault(u => u.ID == order.User.ID);

                var oldOrder = GetOrder(OrderID);
                if (oldOrder == null || user == null) return false;

                
                               
                _db.Entry(oldOrder).CurrentValues.SetValues(order);
                _db.SaveChanges();
                return true;
                
            }
            catch (Exception e)
            {
                return false;
            }
        }

        IEnumerable<Order> IlibraryOrder.GetOrder(int OrderID)
        {
            throw new NotImplementedException();
        }
    }

}
