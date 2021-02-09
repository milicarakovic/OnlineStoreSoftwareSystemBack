using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryData.Interface;
using LibraryData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IlibraryOrder _order;
        public OrderController(IlibraryOrder order)
        {
            _order = order;
        }
        // GET: api/Order
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAllOrder()
        {
            var orders = _order.GetAllOrder();
            if (orders == null) return NotFound();
            return Ok(orders);
        }

        //[HttpGet("{userID}", Name = "GetOrderForUser")]
        //public ActionResult<IEnumerable<Order>> GetAllOrdersForUser(int userID)
        //{
        //    var orders = _order.GetAllOrdersForUser(userID);
        //    if (orders == null) return NotFound();
        //    return Ok(orders);
        //}
        // GET: api/Order/5
        [HttpGet("{id}", Name = "GetOrder")]
        public ActionResult GetOrder(int id)
        {
            var order = _order.GetOrder(id);
            if (order == null) return NotFound();
            return Ok(order);
        }


        // POST: api/Order
        [HttpPost]
        public ActionResult PostOrder([FromBody] Order order)
        {
            if (_order.AddOrder(order)) return Ok(order);
            return NotFound();
        }

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public ActionResult PutOrder(int id, [FromBody] Order order)
        {
            if (_order.UpdateOrder(id, order)) return Ok(order);
            return NotFound();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult DeleteOrder(int id)
        {
            if (_order.DeleteOrder(id)) return Ok();
            return NotFound();
        }
    }
}
