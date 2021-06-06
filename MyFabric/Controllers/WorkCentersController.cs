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
    public class WorkCentersController : ControllerBase
    {
        private readonly IWorkCenterRepository _workCenterRepository;
        public WorkCentersController(IWorkCenterRepository workCenterRepository)
        {
            _workCenterRepository = workCenterRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var workCenter = await _workCenterRepository.GetAllAsync();
            return Ok(workCenter);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetWorkCenterTotalCount()
        {
            var workCenterCount =  _workCenterRepository.GetWorkCenterTotalCount();
            return Ok(workCenterCount);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> c()
        {
            var workCenterCount = _workCenterRepository.GetActiveWorkCenterTotalCount();
            return Ok(workCenterCount);
        }
        [HttpGet("[action]/{productId}")]
        public async Task<IActionResult> GetWorkCenterWithProductId(int productId)
        {
            var workCenterList = await _workCenterRepository.GetWorkCenterWithProductIdAsync(productId);
            var models = new List<WorkCenterListDto>();
            foreach (var item in workCenterList)
            {
                var speed = item.WorkCenterOperations.Where(p => p.WorkCenterID == item.ID).FirstOrDefault().Speed;
                var model = new WorkCenterListDto { WorkCenterId = item.ID, WorkCenterName = item.WorkCenterName , Speed = speed };
                models.Add(model);
            }
            return Ok(models);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var workCenter = await _workCenterRepository.FindByIdAsync(id);
            return Ok(workCenter);
        }
        [HttpPost]
        public async Task<IActionResult> CreateWorkCenter(WorkCenter workCenter)
        {
            await _workCenterRepository.AddAsync(workCenter);
            return Ok("Eklendi başarıyla");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateWorkCenter(WorkCenter workCenter)
        {
            var tempproductTypeRepository = await _workCenterRepository.FindByIdAsync(workCenter.ID);
            if (tempproductTypeRepository != null)
            {
                await _workCenterRepository.UpdateAsync(workCenter);
                return Ok(workCenter);
            }
            return BadRequest("Makine Bulunamadı");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var workCenter = await _workCenterRepository.FindByIdAsync(id);
            if (workCenter != null)
            {
                await _workCenterRepository.RemoveAsync(id);
                return NoContent();
            }
            return BadRequest("Makine Bulunamadı");
        }
    }
}
