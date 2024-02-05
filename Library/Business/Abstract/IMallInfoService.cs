using Core.Dto;
using Core.Results;
using Entities.Dto;

namespace Business.Abstract
{
    public interface IMallInfoService
    {
        Task<Response<MallInfoDto>> CreateMallInfoAsync(MallInfoDto mallInfoDto);

        Task<Response<MallInfoDto>> GetMallInfoByIdAsync(int id, bool? isActive = true);

        Task<Response<List<MallInfoDto>>> GetMallInfosAsync(PageQueryDto parameter);

        Task<Response<MallInfoDto>> UpdateMallInfoAsync(MallInfoDto mallInfoDto);

        Task<Response<MallInfoDto>> DeleteMallInfoAsync(int id);
    }
}
