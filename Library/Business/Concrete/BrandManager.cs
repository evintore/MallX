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
    public class BrandManager : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<BrandDto>> CreateBrandAsync(BrandDto brandDto)
        {
            Brand brandEntity = ObjectMapper.Mapper.Map<Brand>(brandDto);

            await _unitOfWork.Brand.InsertAsync(brandEntity);
            await _unitOfWork.SaveChangesAsync();

            return Response<BrandDto>.Success(ObjectMapper.Mapper.Map<BrandDto>(brandEntity), (int)HttpStatusCode.Created);
        }

        public async Task<Response<NoDataDto>> DeleteBrandAsync(int id)
        {
            var dbBrand = await _unitOfWork.Brand.GetByIdAsync(id);

            if (dbBrand is null)
                return Response<NoDataDto>.Fail("Brand is not found", (int)HttpStatusCode.NotFound, true);

            _unitOfWork.Brand.Delete(dbBrand);
            await _unitOfWork.SaveChangesAsync();

            return Response<NoDataDto>.Success((int)HttpStatusCode.NoContent);
        }

        public async Task<Response<BrandDto>> GetBrandByIdAsync(int id, bool? isActive = true)
        {
            var dbBrand = await _unitOfWork.Brand.GetByIdAsync(id);

            if (dbBrand is null || dbBrand.IsActive.Equals(!isActive))
                return Response<BrandDto>.Fail("Brand is not found", (int)HttpStatusCode.NotFound, true);

            return Response<BrandDto>.Success(ObjectMapper.Mapper.Map<BrandDto>(dbBrand), (int)HttpStatusCode.OK);
        }

        public async Task<Response<List<BrandDto>>> GetBrandsAsync(PageQueryDto parameter)
        {
            var dbBrandsQuery = _unitOfWork.Brand.GetAll()
                .Where(x => x.IsActive.Equals(parameter.IsActive));

            if (!string.IsNullOrEmpty(parameter.SearchKey))
            {
                dbBrandsQuery = dbBrandsQuery.Where(x => EF.Functions.ILike(x.BrandName, "%" + parameter.SearchKey + "%"));
            }

            dbBrandsQuery = string.IsNullOrEmpty(parameter.OrderBy)
                ? dbBrandsQuery.OrderByDescending(x => x.CreatedDate)
                : dbBrandsQuery.OrderBy(parameter.OrderBy);

            var dbBrandsQueryLast = dbBrandsQuery.Select(x => ObjectMapper.Mapper.Map<BrandDto>(x));

            var dbBrandList = await PagedList<BrandDto>.ToPagedListAsync(dbBrandsQueryLast.AsNoTracking(), parameter.PageId, parameter.PageSize);

            return Response<List<BrandDto>>.Success(ObjectMapper.Mapper.Map<List<BrandDto>>(dbBrandList), (int)HttpStatusCode.OK, dbBrandList.CurrentPage, dbBrandList.TotalCount);
        }

        public async Task<Response<BrandDto>> UpdateBrandAsync(BrandDto brandDto)
        {
            var dbBrand = await _unitOfWork.Brand.GetByIdAsync(brandDto.PkId);

            if (dbBrand is null)
                return Response<BrandDto>.Fail("Brand is not found", (int)HttpStatusCode.NotFound, true);

            ObjectMapper.Mapper.Map(brandDto, dbBrand);

            await _unitOfWork.SaveChangesAsync();

            return Response<BrandDto>.Success(ObjectMapper.Mapper.Map<BrandDto>(dbBrand), (int)HttpStatusCode.NoContent);
        }
    }
}
