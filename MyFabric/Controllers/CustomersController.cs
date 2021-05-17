using Core.DBModels;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFabric.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<IActionResult>  GetAll()
        {
            var customers  = await _customerRepository.GetAllAsync();
            return Ok(customers);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var customers = await _customerRepository.FindByIdAsync(id);
            return Ok(customers);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(Customer customer)
        {
            await _customerRepository.AddAsync(customer);
            return Ok("Eklendi başarıyla");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(Customer customer)
        {
            var tempCustomer = await _customerRepository.FindByIdAsync(customer.ID);
            if(tempCustomer != null)
            {
                await _customerRepository.UpdateAsync(customer);
                return Ok(customer);
            }
            return BadRequest("Müşteri Bulunamadı");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var tempCustomer = await _customerRepository.FindByIdAsync(id);
            if (tempCustomer != null)
            {
                await _customerRepository.RemoveAsync(id);
                return NoContent();
            }
            return BadRequest("Müşteri Bulunamadı");
        }
    }
}
