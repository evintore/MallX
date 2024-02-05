using Core.Dto;
using Core.Results;
using Entities.Dto;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task<Response<TokenDto>> Login(UserLoginDto userLoginDto);

        Task<Response<TokenDto>> CheckLoggedIn(int userId);
    }
}