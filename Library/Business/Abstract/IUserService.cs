using Core.Dto;
using Core.Results;
using Entities.Dto;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<Response<UserDto>> CreateUserAsync(UserDto userDto);

        Task<Response<UserViewDto>> GetUserByIdAsync(int id, bool? isActive = true);

        Task<Response<List<UserViewDto>>> GetUsersAsync(PageQueryDto parameter);

        Task<Response<UserDto>> UpdateUserAsync(UserDto userDto);

        Task<Response<NoDataDto>> DeleteUserAsync(int id);
    }
}
