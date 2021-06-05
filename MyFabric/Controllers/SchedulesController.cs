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
        public SchedulesController(IScheduleRepository scheduleRepository,IWorkCenterRepository workCenterRepository)
        {
            _scheduleRepository = scheduleRepository;
            _workCenterRepository = workCenterRepository;
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


            foreach (var item2 in schedules)
            {
                var temp = scheduleAllList.Where(p => p.OrderID == item2.OrderID && p.ProductID == item2.ProductID).ToList();
                if (temp.Count == 0)
                {
                    await _scheduleRepository.AddAsync(new Schedule { OrderID = item2.OrderID, ProductID = item2.ProductID, WorkCenterID = item2.WorkCenterID, Speed = item2.Speed });
                    var workCenter = await _workCenterRepository.FindByIdAsync(item2.WorkCenterID);
                    workCenter.Active = true;
                    await _workCenterRepository.UpdateAsync(workCenter);
                }
                else
                {
                    await _scheduleRepository.UpdateAsync(new Schedule { ID = temp.FirstOrDefault().ID ,OrderID =  item2.OrderID, ProductID = item2.ProductID, WorkCenterID = item2.WorkCenterID, Speed = item2.Speed  });
                }
            }

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
