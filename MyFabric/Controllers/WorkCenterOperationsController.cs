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
    public class WorkCenterOperationsController : ControllerBase
    {
        private readonly IWorkCenterOperationRepository _workCenterOperationRepository;
        public WorkCenterOperationsController(IWorkCenterOperationRepository workCenterOperationRepository)
        {
            _workCenterOperationRepository = workCenterOperationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var workCenterOperation = await _workCenterOperationRepository.GetAllAsync();
            return Ok(workCenterOperation);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetWithAll()
        {
            var workCenterOperation = await _workCenterOperationRepository.GetWorkCenterOperationWithAllAsync();
            List<WorkCenterOperationWithAllDto> listWorkCenterOperation = new List<WorkCenterOperationWithAllDto>();
            foreach (var item in workCenterOperation)
            {
                listWorkCenterOperation.Add(new WorkCenterOperationWithAllDto { ID=item.ID,OperationName=item.Operation.Name,WorkCenterName=item.WorkCenter.WorkCenterName, OperationID=item.OperationID,Speed=item.Speed,WorkCenterID=item.WorkCenterID});
            }
            return Ok(listWorkCenterOperation);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var workCenter = await _workCenterOperationRepository.FindByIdAsync(id);
            return Ok(workCenter);
        }
        [HttpPost]
        public async Task<IActionResult> CreateWorkCenter(WorkCenterOperation workCenterOperation)
        {
            await _workCenterOperationRepository.AddAsync(workCenterOperation);
            return Ok("Eklendi başarıyla");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateWorkCenterOperation(WorkCenterOperation workCenterOperation)
        {
            var tempproductTypeRepository = await _workCenterOperationRepository.FindByIdAsync(workCenterOperation.ID);
            if (tempproductTypeRepository != null)
            {
                await _workCenterOperationRepository.UpdateAsync(workCenterOperation);
                return Ok(workCenterOperation);
            }
            return BadRequest("Makine Bulunamadı");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkCenterOperation(int id)
        {
            var workCenter = await _workCenterOperationRepository.FindByIdAsync(id);
            if (workCenter != null)
            {
                await _workCenterOperationRepository.RemoveAsync(id);
                return NoContent();
            }
            return BadRequest("Makine Bulunamadı");
        }
    }
}
