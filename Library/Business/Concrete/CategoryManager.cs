using Business.Abstract;
using Business.Helpers;
using Business.ModelMapping.AutoMapper;
using Core.Dto;
using Core.Results;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.EntityFrameworkCore;
using Repository.Abstract;
using System.Net;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<CategoryDto>> CreateCategoryAsync(CategoryDto categoryDto)
        {
            Category categoryEntity = ObjectMapper.Mapper.Map<Category>(categoryDto);
            await _unitOfWork.Category.InsertAsync(categoryEntity);
            await _unitOfWork.SaveChangesAsync();

            return Response<CategoryDto>.Success(ObjectMapper.Mapper.Map<CategoryDto>(categoryEntity), (int)HttpStatusCode.Created);
        }

        public async Task<Response<CategoryDto>> DeleteCategoryAsync(int id)
        {
            var dbCategory = await _unitOfWork.Category.GetByIdAsync(id);

            if (dbCategory is null)
                return Response<CategoryDto>.Fail("Category is not found", (int)HttpStatusCode.NotFound, true);

            _unitOfWork.Category.Delete(dbCategory);
            await _unitOfWork.SaveChangesAsync();

            return Response<CategoryDto>.Success((int)HttpStatusCode.NoContent);
        }

        public async Task<Response<List<CategoryDto>>> GetCategoriesAsync(PageQueryDto parameter)
        {
            var dbCategoriesQuery = _unitOfWork.Category.GetAll()
                .Include(x => x.Subcategories)
                .Where(x => x.IsActive.Equals(parameter.IsActive));

            if (!string.IsNullOrEmpty(parameter.SearchKey))
            {
                dbCategoriesQuery = dbCategoriesQuery.Where(x => EF.Functions.ILike(x.CategoryName, "%" + parameter.SearchKey + "%"));
            }

            dbCategoriesQuery = string.IsNullOrEmpty(parameter.OrderBy)
                ? dbCategoriesQuery.OrderByDescending(x => x.CreatedDate)
                : dbCategoriesQuery.OrderBy(parameter.OrderBy);

            var dbCategoriesQueryLast = dbCategoriesQuery.Select(x => ObjectMapper.Mapper.Map<CategoryDto>(x));

            var dbCategoryList = await PagedList<CategoryDto>.ToPagedListAsync(dbCategoriesQueryLast.AsNoTracking(), parameter.PageId, parameter.PageSize);

            return Response<List<CategoryDto>>.Success(ObjectMapper.Mapper.Map<List<CategoryDto>>(dbCategoryList), (int)HttpStatusCode.OK, dbCategoryList.CurrentPage, dbCategoryList.TotalCount);
        }

        public async Task<Response<CategoryDto>> GetCategoryByIdAsync(int id, bool? isActive = true)
        {
            var dbCategory = await _unitOfWork.Category.GetByIdAsync(id);

            if (dbCategory is null || dbCategory.IsActive.Equals(!isActive))
                return Response<CategoryDto>.Fail("Category is not found", (int)HttpStatusCode.NotFound, true);

            return Response<CategoryDto>.Success(ObjectMapper.Mapper.Map<CategoryDto>(dbCategory), (int)HttpStatusCode.OK);
        }

        public async Task<Response<CategoryDto>> UpdateCategoryAsync(CategoryDto categoryDto)
        {
            var dbCategory = await _unitOfWork.Category.GetByIdAsync(categoryDto.PkId);

            if (dbCategory is null)
                return Response<CategoryDto>.Fail("Category is not found", (int)HttpStatusCode.NotFound, true);

            ObjectMapper.Mapper.Map(categoryDto, dbCategory);

            await _unitOfWork.SaveChangesAsync();

            return Response<CategoryDto>.Success(ObjectMapper.Mapper.Map<CategoryDto>(dbCategory), (int)HttpStatusCode.NoContent);
        }
    }
}
