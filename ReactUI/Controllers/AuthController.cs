using Business.Abstract;
using Entities.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReactUI.Controllers.Base;
using System.Security.Claims;

namespace ReactUI.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class AuthController : CustomBaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var result = await _authService.Login(userLoginDto);

            return ActionResultInstance(result);
        }

        [HttpPost]
        public async Task<IActionResult> CheckLoggedIn()
        {
                string userId = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var result = await _authService.CheckLoggedIn(int.Parse(userId));

                if (result.Data is null)
                    HttpContext.Response.Cookies.Delete("access_token");

                return ActionResultInstance(result);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Logout(){
            HttpContext.Response.Cookies.Delete("access_token");
            
            return Ok();
        }
    }
}
