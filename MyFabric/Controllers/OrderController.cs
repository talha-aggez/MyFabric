using Core.DBModels;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFabric.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFabric.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        public OrderController(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productType = await _orderRepository.GetAllAsync();
            return Ok(productType);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetOrdersFromAppUserId(int id)
        {
            var orderList = await _orderRepository.GetOrdersFromAppUserIdAsync(id);
            var orderItemList = new List<OrderListDto>();
            for(var i = 0; i<orderList.Count; i++)
            {
                for (var j = 0;  j<orderList[i].OrderItems.Count; j++)
                    orderItemList.Add(new OrderListDto() { Amount = orderList[i].OrderItems[j].Amount,DeadLine = orderList[i].DeadLine,OrderDate= orderList[i].OrderDate,OrderID= orderList[i].ID,ProductID= orderList[i].OrderItems[j].ProductID,ProductName= orderList[i].OrderItems[j].Product.ProductName});
            }
            
            return Ok(orderItemList);
        }
        
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetOrderItemsFromOrderIdAsync(int id)
        {
            var orderItemList = await _orderItemRepository.GetOrderItemsFromOrderIdAsync(id);
            return Ok(orderItemList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var productType = await _orderRepository.FindByIdAsync(id);
            return Ok(productType);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CheckOutDto order)
        {
            List<OrderItem> orderItems = order.OrderItems;
            Order orderDetails = new Order();

            if (orderItems != null)
            {
                orderDetails.OrderDate = DateTime.Now;
                orderDetails.DeadLine = order.DeadLine;
                orderDetails.CustomerID = order.AppUserId;
                orderDetails.OrderItems = order.OrderItems;
                await _orderRepository.AddAsync(orderDetails);
            }
           
            return Ok("Eklendi başarıyla");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrder(Order order)
        {
            var tempproductTypeRepository = await _orderRepository.FindByIdAsync(order.ID);
            if (tempproductTypeRepository != null)
            {
                await _orderRepository.UpdateAsync(order);
                return Ok(order);
            }
            return BadRequest("Order Bulunamadı");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var tempproductTypeRepository = await _orderRepository.FindByIdAsync(id);
            if (tempproductTypeRepository != null)
            {
                await _orderRepository.RemoveAsync(id);
                return Ok("Silme işlemi başarılı");
            }
            return BadRequest("Order Bulunamadı");
        }




    }
}
