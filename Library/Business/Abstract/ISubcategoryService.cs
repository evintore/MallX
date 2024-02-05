using Core.Dto;
using Core.Results;
using Entities.Dto;

namespace Business.Abstract
{
    public interface ISubcategoryService
    {
        Task<Response<SubcategoryDto>> CreateSubcategoryAsync(SubcategoryDto subcategoryDto);

        Task<Response<SubcategoryDto>> GetSubcategoryByIdAsync(int id, bool? isActive = true);

        Task<Response<List<SubcategoryDto>>> GetSubcategoriesAsync(SubcategoryQueryDto parameter);

        Task<Response<SubcategoryDto>> UpdateSubcategoryAsync(SubcategoryDto subcategoryDto);

        Task<Response<SubcategoryDto>> DeleteSubcategoryAsync(int id);
    }
}
