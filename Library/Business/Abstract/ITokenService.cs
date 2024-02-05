using Core.Dto;
using Entities.Dto;

namespace Business.Abstract
{
    public interface ITokenService
    {
        TokenDto CreateToken(UserDto userDto);
    }
}