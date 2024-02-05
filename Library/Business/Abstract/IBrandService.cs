using Core.Dto;
using Core.Results;
using Entities.Dto;

namespace Business.Abstract
{
    public interface IBrandService
    {
        Task<Response<BrandDto>> CreateBrandAsync(BrandDto brandDto);

        Task<Response<BrandDto>> GetBrandByIdAsync(int id, bool? isActive = true);

        Task<Response<List<BrandDto>>> GetBrandsAsync(PageQueryDto parameter);

        Task<Response<BrandDto>> UpdateBrandAsync(BrandDto brandDto);

        Task<Response<NoDataDto>> DeleteBrandAsync(int id);
    }
}
