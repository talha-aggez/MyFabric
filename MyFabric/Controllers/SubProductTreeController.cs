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
    public class SubProductTreeController : ControllerBase
    {
        private readonly ISubProductTreeRepository _subProductTreeRepository;
        private readonly IProductRepository _productRepository;
        public SubProductTreeController(ISubProductTreeRepository operationRepository, IProductRepository productRepository)
        {
            _subProductTreeRepository = operationRepository;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productType = await _subProductTreeRepository.GetAllAsync();
            return Ok(productType);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var productType = await _subProductTreeRepository.FindByIdAsync(id);
            return Ok(productType);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(SubProductTree subProductTree)
        {
            await _subProductTreeRepository.AddAsync(subProductTree);
            return Ok("Eklendi başarıyla");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(SubProductTree subProductTree)
        {
            var tempproductTypeRepository = await _subProductTreeRepository.FindByIdAsync(subProductTree.ID);
            if (tempproductTypeRepository != null)
            {
                await _subProductTreeRepository.UpdateAsync(subProductTree);
                return Ok(subProductTree);
            }
            return BadRequest("Müşteri Bulunamadı");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var tempproductTypeRepository = await _subProductTreeRepository.FindByIdAsync(id);
            if (tempproductTypeRepository != null)
            {
                await _subProductTreeRepository.RemoveAsync(id);
                return Ok("Silme işlemi başarılı");
            }
            return BadRequest("Müşteri Bulunamadı");
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSubProductTreeWithAll()
        {
            var products = await _subProductTreeRepository.GetSubProductTreeWithAllAsync();
            
            List<SubProductWithAllDto> listProduct = new List<SubProductWithAllDto>();
            foreach (var item in products)
            {
                var temp =await _productRepository.FindByIdAsync(item.SubProductID);
               
                listProduct.Add(new SubProductWithAllDto { Amount=item.Amount,ProductId=item.ProductID,SubProductId=item.SubProductID,ProductName=item.Product.ProductName,SubProductName= temp.ProductName });
            }
            return Ok(listProduct);
        }
    }
}
