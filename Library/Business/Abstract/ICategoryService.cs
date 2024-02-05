using Core.Dto;
using Core.Results;
using Entities.Dto;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        Task<Response<CategoryDto>> CreateCategoryAsync(CategoryDto categoryDto);

        Task<Response<CategoryDto>> GetCategoryByIdAsync(int id, bool? isActive = true);

        Task<Response<List<CategoryDto>>> GetCategoriesAsync(PageQueryDto parameter);

        Task<Response<CategoryDto>> UpdateCategoryAsync(CategoryDto categoryDto);

        Task<Response<CategoryDto>> DeleteCategoryAsync(int id);
    }
}
