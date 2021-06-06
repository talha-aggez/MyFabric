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
        private readonly ISubProductTreeRepository _subProductTreeRepository;
        private readonly IProductRepository _productRepository;
        public OrderController(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, ISubProductTreeRepository subProductTreeRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _subProductTreeRepository = subProductTreeRepository;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orderList = await _orderRepository.GetOrdersWithAllAsync();
           
            var orderItemList = new List<OrderListDto>();
            for (var i = 0; i < orderList.Count; i++)
            {
                for (var j = 0; j < orderList[i].OrderItems.Count; j++) 
                    orderItemList.Add(new OrderListDto() { CustomerName =orderList[i].AppUser.Name, Amount = orderList[i].OrderItems[j].Amount, DeadLine = orderList[i].DeadLine, OrderDate = orderList[i].OrderDate, OrderID = orderList[i].ID, ProductID = orderList[i].OrderItems[j].ProductID, ProductName = orderList[i].OrderItems[j].Product.ProductName });
            }

            return Ok(orderItemList);
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetOrderItemsAndSubProductsByOrderId(int id)
        {
            var orderItemList = await _orderItemRepository.GetOrderItemsFromOrderIdAsync(id);
            var tempList = new List<OrderWithSubProductsDto>();
            var subProducts = new List<SubProductDto>();
            foreach (var item in orderItemList)
            {
               var temp= await _subProductTreeRepository.GetSubProductsByProductId(item.ProductID);
                subProducts = new List<SubProductDto>();
                foreach (var item2 in temp)
                {
                    var subProductName = await _productRepository.FindByIdAsync(item2.SubProductID);
                    subProducts.Add(new SubProductDto { SubProductId = item2.SubProductID, SubProductName = subProductName.ProductName , Amount = item2.Amount });
                }
                tempList.Add(new OrderWithSubProductsDto { ProductId = item.ProductID, ProductName = item.Product.ProductName , SubProducts = subProducts });
            }

            return Ok(tempList);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetOrdersFromAppUserId(int id)
        {

            var orderList = await _orderRepository.GetOrdersFromAppUserIdAsync(id);
            var orderItemList = new List<OrderListDto>();
            for(var i = 0; i<orderList.Count; i++)
            {
                for (var j = 0;  j<orderList[i].OrderItems.Count; j++)
                    orderItemList.Add(new OrderListDto() {Amount = orderList[i].OrderItems[j].Amount,DeadLine = orderList[i].DeadLine,OrderDate= orderList[i].OrderDate,OrderID= orderList[i].ID,ProductID= orderList[i].OrderItems[j].ProductID,ProductName= orderList[i].OrderItems[j].Product.ProductName});
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
                orderDetails.AppUserID = order.AppUserId;
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
        [HttpGet("[action]")]
        public async Task<IActionResult> GetTodayOrderCount()
        {
            var orderCountToday = await _orderRepository.GetTodayOrderCountAsync();
            return Ok(orderCountToday);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMostActive3Person()
        {
            var mostActiveCustomers = await _orderRepository.GetMostActive3PersonAsync();
            return Ok(mostActiveCustomers);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMostActive3Product()
        {
            var mostActiveProducts = await _orderRepository.GetMostActive3ProductAsync();
            return Ok(mostActiveProducts);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetTotalOrderCount()
        {
            var totalOrderCount =  _orderRepository.GetTotalOrderCountAsync();
            return Ok(totalOrderCount);
        }
        



    }
}
