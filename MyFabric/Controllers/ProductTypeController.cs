using Core.DBModels;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFabric.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeRepository _productTypeRepository;
        public ProductTypeController(IProductTypeRepository productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productType = await _productTypeRepository.GetAllAsync();
            return Ok(productType);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var productType = await _productTypeRepository.FindByIdAsync(id);
            return Ok(productType);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductType productType)
        {
            await _productTypeRepository.AddAsync(productType);
            return Ok("Eklendi başarıyla");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductType product)
        {
            var tempproductTypeRepository = await _productTypeRepository.FindByIdAsync(product.ID);
            if (tempproductTypeRepository != null)
            {
                await _productTypeRepository.UpdateAsync(product);
                return Ok(product);
            }
            return BadRequest("Müşteri Bulunamadı");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var tempproductTypeRepository = await _productTypeRepository.FindByIdAsync(id);
            if (tempproductTypeRepository != null)
            {
                await _productTypeRepository.RemoveAsync(id);
                return NoContent();
            }
            return BadRequest("Müşteri Bulunamadı");
        }
    }
}
