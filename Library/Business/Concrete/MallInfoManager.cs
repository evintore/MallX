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
    public class MallInfoManager : IMallInfoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAddressService _addressService;

        public MallInfoManager(IUnitOfWork unitOfWork, IAddressService addressService)
        {
            _unitOfWork = unitOfWork;
            _addressService = addressService;
        }

        public async Task<Response<MallInfoDto>> CreateMallInfoAsync(MallInfoDto mallInfoDto)
        {
            MallInfo mallInfoEntity = ObjectMapper.Mapper.Map<MallInfo>(mallInfoDto);

            mallInfoEntity.CountryName = _addressService.GetCountryName(mallInfoEntity.CountryCode).Data ?? "";
            mallInfoEntity.CityName = _addressService.GetCityName(mallInfoEntity.CityCode).Data ?? "";
            mallInfoEntity.TownName = _addressService.GetTownName(mallInfoEntity.TownCode).Data ?? "";

            await _unitOfWork.MallInfo.InsertAsync(mallInfoEntity);
            await _unitOfWork.SaveChangesAsync();

            return Response<MallInfoDto>.Success(ObjectMapper.Mapper.Map<MallInfoDto>(mallInfoEntity), (int)HttpStatusCode.Created);
        }

        public async Task<Response<MallInfoDto>> DeleteMallInfoAsync(int id)
        {
            var dbMallInfo = await _unitOfWork.MallInfo.GetByIdAsync(id);

            if (dbMallInfo is null)
                return Response<MallInfoDto>.Fail("MallInfo not found", (int)HttpStatusCode.NotFound, true);

            _unitOfWork.MallInfo.Delete(dbMallInfo);
            await _unitOfWork.SaveChangesAsync();

            return Response<MallInfoDto>.Success(ObjectMapper.Mapper.Map<MallInfoDto>(dbMallInfo), (int)HttpStatusCode.OK);
        }

        public async Task<Response<MallInfoDto>> GetMallInfoByIdAsync(int id, bool? isActive = true)
        {
            var dbMallInfo = await _unitOfWork.MallInfo.GetByIdAsync(id);

            if (dbMallInfo is null || dbMallInfo.IsActive.Equals(!isActive))
                return Response<MallInfoDto>.Fail("Mall info is not found", (int)HttpStatusCode.NotFound, true);

            return Response<MallInfoDto>.Success(ObjectMapper.Mapper.Map<MallInfoDto>(dbMallInfo), (int)HttpStatusCode.OK);
        }

        public async Task<Response<List<MallInfoDto>>> GetMallInfosAsync(PageQueryDto parameter)
        {
            var dbMallInfosQuery = _unitOfWork.MallInfo.GetAll()
                .Where(x => x.IsActive.Equals(parameter.IsActive));

            if (!string.IsNullOrEmpty(parameter.SearchKey))
            {
                dbMallInfosQuery = dbMallInfosQuery.Where(x => EF.Functions.ILike(x.MallName, "%" + parameter.SearchKey + "%"));
            }

            dbMallInfosQuery = string.IsNullOrEmpty(parameter.OrderBy)
                ? dbMallInfosQuery.OrderByDescending(x => x.CreatedDate)
                : dbMallInfosQuery.OrderBy(parameter.OrderBy);

            var dbMallInfosQueryLast = dbMallInfosQuery.Select(x => ObjectMapper.Mapper.Map<MallInfoDto>(x));

            var dbMallInfoList = await PagedList<MallInfoDto>.ToPagedListAsync(dbMallInfosQueryLast.AsNoTracking(), parameter.PageId, parameter.PageSize);

            // Resolve operation
            foreach (MallInfoDto mallInfoDto in dbMallInfoList)
            {
                mallInfoDto.CountryName = _addressService.GetCountryName(mallInfoDto.CountryCode).Data ?? "";
                mallInfoDto.CityName = _addressService.GetCityName(mallInfoDto.CityCode).Data ?? "";
                mallInfoDto.TownName = _addressService.GetTownName(mallInfoDto.TownCode).Data ?? "";
            }

            return Response<List<MallInfoDto>>.Success(dbMallInfoList, (int)HttpStatusCode.OK, dbMallInfoList.CurrentPage, dbMallInfoList.TotalCount);
        }

        public async Task<Response<MallInfoDto>> UpdateMallInfoAsync(MallInfoDto mallInfoDto)
        {
            var dbMallInfo = await _unitOfWork.MallInfo.GetByIdAsync(mallInfoDto.PkId);

            if (dbMallInfo is null)
                return Response<MallInfoDto>.Fail("Mall info is not found", (int)HttpStatusCode.NotFound, true);

            mallInfoDto.CountryName = _addressService.GetCountryName(mallInfoDto.CountryCode).Data ?? "";
            mallInfoDto.CityName = _addressService.GetCityName(mallInfoDto.CityCode).Data ?? "";
            mallInfoDto.TownName = _addressService.GetTownName(mallInfoDto.TownCode).Data ?? "";
            mallInfoDto.CreatedDate = dbMallInfo.CreatedDate;
            mallInfoDto.CreatedUserId = dbMallInfo.CreatedUserId;

            ObjectMapper.Mapper.Map(mallInfoDto, dbMallInfo);

            await _unitOfWork.SaveChangesAsync();

            return Response<MallInfoDto>.Success(ObjectMapper.Mapper.Map<MallInfoDto>(dbMallInfo), (int)HttpStatusCode.NoContent);
        }
    }
}