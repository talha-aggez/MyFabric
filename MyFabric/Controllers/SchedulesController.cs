﻿using Core.DBModels;
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
         public SchedulesController(IScheduleRepository scheduleRepository)
         {
            _scheduleRepository = scheduleRepository;
         }
        [HttpGet]
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
            var scheduleAllList =await _scheduleRepository.GetAllAsync();

            
            foreach (var item in schedules)
            {
                if(item.)
                await _scheduleRepository.AddAsync(new Schedule {OrderID=item.OrderID,ProductID=item.ProductID,WorkCenterID=item.WorkCenterID, Speed=item.Speed});
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