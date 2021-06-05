using BuisnessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAPI.Controllers.V2
{
    [ApiController]
    [Route("api/v2/[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBL orderBL;
        public OrderController(IOrderBL orderBL)
        {
            this.orderBL = orderBL;
        }

        private static List<Order> OrderList=new List<Order>();

        /// <summary>
        /// This is adding a Order into the Database
        /// </summary>
        /// <param name="orderItems"></param>
        /// <returns>true if successfull</returns>
        [HttpPost]
        public IActionResult AddOrderItem(Order orderItems)
        {
            //OrderList.Add(new Order { CustomerId = orderItems.CustomerId,ItemName=orderItems.ItemName,Quantity=orderItems.Quantity });
            var getOrders = orderBL.AddOrders(orderItems);
            OrderList.AddRange(getOrders);

            if (OrderList.Count > 0)
                return Ok(true);
            else
                return Ok("No item in List");
        }

        /// <summary>
        /// Get individual Order details
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns>order detsils for the specific customer</returns>
        /// <response code="401">Returns Order details not found</response>
        [HttpGet]
        public IActionResult GetOrderItem(Guid CustomerId)
        {
            var getOrderList = orderBL.GetOrderListBasedOnCustomerId(CustomerId, OrderList);
            if (getOrderList.Count > 0)
                return Ok(getOrderList);
            else
                return NotFound();
        }
    }
}
