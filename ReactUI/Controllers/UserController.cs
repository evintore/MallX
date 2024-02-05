using Business.Abstract;
using Core.Dto;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using ReactUI.Controllers.Base;

namespace ReactUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : CustomBaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUser(int id, bool? isActive = true)
        {
            var result = await _userService.GetUserByIdAsync(id, isActive);

            return ActionResultInstance(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] PageQueryDto parameter)
        {
            var result = await _userService.GetUsersAsync(parameter);

            return ActionResultInstance(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserDto userDto)
        {
            var result = await _userService.CreateUserAsync(userDto);

            return ActionResultInstance(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserDto userDto)
        {
            var result = await _userService.UpdateUserAsync(userDto);

            return ActionResultInstance(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);

            return ActionResultInstance(result);
        }
    }
}
