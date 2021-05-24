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
    public class OperationController : ControllerBase
    {
        private readonly IOperationRepository _operationRepository;
        public OperationController(IOperationRepository operationRepository)
        {
            _operationRepository = operationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productType = await _operationRepository.GetAllAsync();
            return Ok(productType);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var productType = await _operationRepository.FindByIdAsync(id);
            return Ok(productType);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Operation productType)
        {
            await _operationRepository.AddAsync(productType);
            return Ok("Eklendi başarıyla");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductWithProductType()
        {
            var operations = await _operationRepository.GetOperationWithProductTypeAsync();
            List<OperationWithProductTypeDto> listOperations = new List<OperationWithProductTypeDto>();
            foreach (var item in operations)
            {
                listOperations.Add(new OperationWithProductTypeDto { ID=item.ID,Name=item.Name,ProductTypeID=item.ProductTypeID,ProductTypeName=item.ProductType.Name });
            }
            return Ok(listOperations);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Operation product)
        {
            var tempproductTypeRepository = await _operationRepository.FindByIdAsync(product.ID);
            if (tempproductTypeRepository != null)
            {
                await _operationRepository.UpdateAsync(product);
                return Ok(product);
            }
            return BadRequest("Müşteri Bulunamadı");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var tempproductTypeRepository = await _operationRepository.FindByIdAsync(id);
            if (tempproductTypeRepository != null)
            {
                await _operationRepository.RemoveAsync(id);
                return Ok("Silme işlemi başarılı");
            }
            return BadRequest("Müşteri Bulunamadı");
        }
    }
}
