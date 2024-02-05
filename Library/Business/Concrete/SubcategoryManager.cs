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
    public class SubcategoryManager : ISubcategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubcategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<SubcategoryDto>> CreateSubcategoryAsync(SubcategoryDto subcategoryDto)
        {
            Subcategory subcategoryEntity = ObjectMapper.Mapper.Map<Subcategory>(subcategoryDto);
            await _unitOfWork.Subcategory.InsertAsync(subcategoryEntity);
            await _unitOfWork.SaveChangesAsync();

            return Response<SubcategoryDto>.Success(ObjectMapper.Mapper.Map<SubcategoryDto>(subcategoryEntity), (int)HttpStatusCode.Created);
        }

        public async Task<Response<SubcategoryDto>> DeleteSubcategoryAsync(int id)
        {
            var dbSubcategory = await _unitOfWork.Subcategory.GetByIdAsync(id);

            if (dbSubcategory is null)
                return Response<SubcategoryDto>.Fail("Subcategory is not found", (int)HttpStatusCode.NotFound, true);

            _unitOfWork.Subcategory.Delete(dbSubcategory);
            await _unitOfWork.SaveChangesAsync();

            return Response<SubcategoryDto>.Success((int)HttpStatusCode.NoContent);
        }

        public async Task<Response<List<SubcategoryDto>>> GetSubcategoriesAsync(SubcategoryQueryDto parameter)
        {
            var dbSubcategoriesQuery = _unitOfWork.Subcategory.GetAll()
                .Where(x => x.IsActive.Equals(parameter.IsActive) && x.CategoryId == parameter.CategoryId);

            if (!string.IsNullOrEmpty(parameter.SearchKey))
            {
                dbSubcategoriesQuery = dbSubcategoriesQuery.Where(x => EF.Functions.ILike(x.SubcategoryName, "%" + parameter.SearchKey + "%"));
            }

            dbSubcategoriesQuery = string.IsNullOrEmpty(parameter.OrderBy)
                ? dbSubcategoriesQuery.OrderByDescending(x => x.CreatedDate)
                : dbSubcategoriesQuery.OrderBy(parameter.OrderBy);

            var dbSubcategoriesQueryLast = dbSubcategoriesQuery.Select(x => ObjectMapper.Mapper.Map<SubcategoryDto>(x));

            var dbSubcategoryList = await PagedList<SubcategoryDto>.ToPagedListAsync(dbSubcategoriesQueryLast.AsNoTracking(), parameter.PageId, parameter.PageSize);

            return Response<List<SubcategoryDto>>.Success(ObjectMapper.Mapper.Map<List<SubcategoryDto>>(dbSubcategoryList), (int)HttpStatusCode.OK, dbSubcategoryList.CurrentPage, dbSubcategoryList.TotalCount);
        }

        public async Task<Response<SubcategoryDto>> GetSubcategoryByIdAsync(int id, bool? isActive = true)
        {
            var dbSubcategory = await _unitOfWork.Subcategory.GetByIdAsync(id);

            if (dbSubcategory is null || dbSubcategory.IsActive.Equals(!isActive))
                return Response<SubcategoryDto>.Fail("Subcategory is not found", (int)HttpStatusCode.NotFound, true);

            return Response<SubcategoryDto>.Success(ObjectMapper.Mapper.Map<SubcategoryDto>(dbSubcategory), (int)HttpStatusCode.OK);
        }

        public async Task<Response<SubcategoryDto>> UpdateSubcategoryAsync(SubcategoryDto subcategoryDto)
        {
            var dbSubcategory = await _unitOfWork.Subcategory.GetByIdAsync(subcategoryDto.PkId);

            if (dbSubcategory is null)
                return Response<SubcategoryDto>.Fail("subcategory is not found", (int)HttpStatusCode.NotFound, true);

            ObjectMapper.Mapper.Map(subcategoryDto, dbSubcategory);

            await _unitOfWork.SaveChangesAsync();

            return Response<SubcategoryDto>.Success(ObjectMapper.Mapper.Map<SubcategoryDto>(dbSubcategory), (int)HttpStatusCode.NoContent);
        }
    }
}
