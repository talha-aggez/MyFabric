

using Core.DBModels;
using Core.Interfaces;
using Infrastructure.JWTUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFabric.DTO;
using System.Linq;
using System.Threading.Tasks;

namespace MyFabric.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IAppUserRoleRepository _appUserRoleRepository;
        private readonly IAppRoleRepository _appRoleRepository;

        public AuthController(IJwtService jwtService, IAppUserRepository appUserRepository, IAppUserRoleRepository appUserRoleRepository, IAppRoleRepository appRoleRepository)
        {
            _jwtService = jwtService;
            _appUserRepository = appUserRepository;
            _appUserRoleRepository = appUserRoleRepository;
            _appRoleRepository = appRoleRepository;



        }
        [HttpPost("[action]")]
     
        public async Task<IActionResult> Login(AppUserLoginDto userLoginDto)
        {

            var appUser = await _appUserRepository.FindByUserName(userLoginDto.Name);
            if (appUser == null)
            {
                return BadRequest("Kullanıcı adı veya şifre hatalı");
            }
            else
            {
                if (await _appUserRepository.CheckPassword(userLoginDto.Name,userLoginDto.Password))
                {
                    var roles = await _appUserRepository.GetRolesByUserName(userLoginDto.Name);
                    var tempToken = _jwtService.GenerateJWTToken(appUser, roles);
                    return Created("", new AppUserDto(){Name=userLoginDto.Name,Password=userLoginDto.Password,Token=tempToken,Roles = roles });
                }
                return BadRequest("Kullanıcı adı veya şifre hatalı");
            }

        }
        [HttpPost("[action]")]
    
        public async Task<IActionResult> SignUp(AppUserLoginDto appUserDto)
        {
            var appUser = await _appUserRepository.FindByUserName(appUserDto.Name);
            if (appUser != null)
            {
                return BadRequest("Bu kullanıcı adı kullanılmaktadır.Lütfen farklı bir kullanıcı adı deneyiniz");
            }
            await _appUserRepository.AddAsync(new AppUser {Name= appUserDto.Name,Password= appUserDto.Password});

            var user = await _appUserRepository.FindByUserName(appUserDto.Name);
            var role =await _appRoleRepository.FindByRoleName("Customer");

            await _appUserRoleRepository.AddAsync(
            new AppUserRole
            {
                AppUserId=user.ID,
                AppRoleId=role.Id
            });
            var roles = await _appUserRepository.GetRolesByUserName(appUserDto.Name);
            var tempToken = _jwtService.GenerateJWTToken(user, roles);
            return Created("", new AppUserDto() { Name = appUserDto.Name, Password = appUserDto.Password, Token = tempToken,Roles=roles });

        }
        [HttpGet("[action]")]
        [Authorize]       
        public async Task<IActionResult> ActiveUser()
        {
            var user= await _appUserRepository.FindByUserName(User.Identity.Name);
            var roles = await _appUserRepository.GetRolesByUserName(User.Identity.Name);

            AppUserWithRolesDto appUserWithRolesDto = new AppUserWithRolesDto
            {
                Name = user.Name,
                Password = user.Password,
                Roles = roles.Select(I => I.Name).ToList()
            };
            return Ok(appUserWithRolesDto);
            
        }
    }
}
