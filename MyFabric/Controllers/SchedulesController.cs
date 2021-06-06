using Core.DBModels;
using Core.Interfaces;
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
    public class SchedulesController : ControllerBase
    {

        private readonly IScheduleRepository _scheduleRepository;
        private readonly IWorkCenterRepository _workCenterRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ISubProductTreeRepository _subProductTreeRepository;
        public SchedulesController(IScheduleRepository scheduleRepository, IWorkCenterRepository workCenterRepository, IOrderRepository orderRepository, ISubProductTreeRepository subProductTreeRepository)
        {
            _scheduleRepository = scheduleRepository;
            _workCenterRepository = workCenterRepository;
            _orderRepository = orderRepository;
            _subProductTreeRepository = subProductTreeRepository;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetScheduleByOrderIdAndProductId(int orderId, int productId)
        {
            var schedules = await _scheduleRepository.GetScheduleByOrderIdAndProductIdAsync(orderId, productId);
            return Ok(schedules);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var workCenter = await _scheduleRepository.GetAllAsync();
            return Ok(workCenter);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var workCenter = await _scheduleRepository.FindByIdAsync(id);
            return Ok(workCenter);
        }
        [HttpPost]
        public async Task<IActionResult> CreateWorkCenter(Schedule workCenter)
        {
            await _scheduleRepository.AddAsync(workCenter);
            return Ok("Eklendi başarıyla");
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateScheduleByScheduleList(List<ScheduleDto> schedules)
        {
            var scheduleAllList = await _scheduleRepository.GetAllAsync();
            var tempTime = 0;

            foreach (var item2 in schedules)
            {
                var temp = scheduleAllList.Where(p => p.OrderID == item2.OrderID && p.ProductID == item2.ProductID).ToList();
                if (temp.Count == 0)
                {
                    await _scheduleRepository.AddAsync(new Schedule { OrderID = item2.OrderID, ProductID = item2.ProductID, WorkCenterID = item2.WorkCenterID, Speed = item2.Speed });
                    var order = await _orderRepository.FindByIDWithOrderItemsAsync(item2.OrderID);
                    var orderItem = order.OrderItems.Where(p => p.ProductID == item2.UstUrun).FirstOrDefault();
                    if (orderItem != null)
                    {
                        var test = await _subProductTreeRepository.GetSubProductsByProductId(item2.UstUrun);
                        var test2 = test.Where(p => p.SubProductID == item2.ProductID).FirstOrDefault();
                        tempTime += orderItem.Amount * (int)item2.Speed * test2.Amount;
                    }
                    var workCenter = await _workCenterRepository.FindByIdAsync(item2.WorkCenterID);
                    workCenter.Active = true;
                    await _workCenterRepository.UpdateAsync(workCenter);
                }
                else
                {
                    await _scheduleRepository.UpdateAsync(new Schedule { ID = temp.FirstOrDefault().ID, OrderID = item2.OrderID, ProductID = item2.ProductID, WorkCenterID = item2.WorkCenterID, Speed = item2.Speed });
                }
            }
            var tempOrder = await _orderRepository.FindByIdAsync(schedules[0].OrderID);
            tempOrder.DeadLine = DateTime.Now.AddMinutes(tempTime);
            await _orderRepository.UpdateAsync(tempOrder);
            return Ok("Eklendi başarıyla");
        }
        //deneme
        [HttpPut]
        public async Task<IActionResult> UpdateWorkCenter(Schedule workCenter)
        {
            var tempproductTypeRepository = await _scheduleRepository.FindByIdAsync(workCenter.ID);
            if (tempproductTypeRepository != null)
            {
                await _scheduleRepository.UpdateAsync(workCenter);
                return Ok(workCenter);
            }
            return BadRequest("Makine Bulunamadı");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var workCenter = await _scheduleRepository.FindByIdAsync(id);
            if (workCenter != null)
            {
                await _scheduleRepository.RemoveAsync(id);
                return NoContent();
            }
            return BadRequest("Makine Bulunamadı");
        }

    }
}
