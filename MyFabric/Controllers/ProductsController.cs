using Core.DBModels;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductWithProductType()
        {
            var products = await _productRepository.GetProductWithProductTypeAsync();
            List<ProductWithProductTypeDto> listProduct = new List<ProductWithProductTypeDto>();
            foreach (var item in products)
            {
                listProduct.Add(new ProductWithProductTypeDto { ProductName=item.ProductName,IsSalable=item.IsSalable,ProductId=item.ID,ProductTypeID=item.ProductTypeID,ProductTypeName=item.ProductType.Name});
            }
            return Ok(listProduct);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var products = await _productRepository.FindByIdAsync(id);
            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            await _productRepository.AddAsync(product);
            return Ok("Eklendi başarıyla");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            var tempProduct = await _productRepository.FindByIdAsync(product.ID);
            if (tempProduct != null)
            {
                await _productRepository.UpdateAsync(product);
                return Ok(product);
            }
            return BadRequest("Müşteri Bulunamadı");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var tempProduct = await _productRepository.FindByIdAsync(id);
            if (tempProduct != null)
            {
                await _productRepository.RemoveAsync(id);
                return Ok("Silme işlemi başarılı");
            }
            return BadRequest("Müşteri Bulunamadı");
        }


    }
}
