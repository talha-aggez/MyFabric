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
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserRepository _appUserRepository;
        public AppUserController(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var appUser = await _appUserRepository.GetAllAsync();
            return Ok(appUser);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var appUser = await _appUserRepository.FindByIdAsync(id);
            return Ok(appUser);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAppUser(AppUser appUser)
        {
            await _appUserRepository.AddAsync(appUser);
            return Ok("Eklendi başarıyla");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAppUser(AppUser appUser)
        {
            var tempUser = await _appUserRepository.FindByIdAsync(appUser.ID);
            if (tempUser != null)
            {
                await _appUserRepository.UpdateAsync(tempUser);
                return Ok(tempUser);
            }
            return BadRequest("Müşteri Bulunamadı");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppUser(int id)
        {
            var appUser = await _appUserRepository.FindByIdAsync(id);
            if (appUser != null)
            {
                await _appUserRepository.RemoveAsync(id);
                return NoContent();
            }
            return BadRequest("Müşteri Bulunamadı");
        }
    }
}

