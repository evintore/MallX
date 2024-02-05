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
    public class StoreManager : IStoreService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StoreManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<StoreDto>> CreateStoreAsync(StoreDto storeDto)
        {

            Store storeEntity = ObjectMapper.Mapper.Map<Store>(storeDto);
            await _unitOfWork.Store.InsertAsync(storeEntity);
            await _unitOfWork.SaveChangesAsync();

            return Response<StoreDto>.Success(ObjectMapper.Mapper.Map<StoreDto>(storeEntity), (int)HttpStatusCode.Created);
        }

        public async Task<Response<NoDataDto>> DeleteStoreAsync(int id)
        {
            var dbStore = await _unitOfWork.Store.GetByIdAsync(id);

            if (dbStore is null)
                return Response<NoDataDto>.Fail("Store is not found", (int)HttpStatusCode.NotFound, true);

            _unitOfWork.Store.Delete(dbStore);
            await _unitOfWork.SaveChangesAsync();

            return Response<NoDataDto>.Success((int)HttpStatusCode.NoContent);
        }

        public async Task<Response<StoreDto>> GetStoreByIdAsync(int id, bool? isActive = true)
        {
            var dbStore = await _unitOfWork.Store.GetByIdAsync(id);

            if (dbStore is null || dbStore.IsActive.Equals(!isActive))
                return Response<StoreDto>.Fail("Store is not found", (int)HttpStatusCode.NotFound, true);

            return Response<StoreDto>.Success(ObjectMapper.Mapper.Map<StoreDto>(dbStore), (int)HttpStatusCode.OK);
        }

        public async Task<Response<List<StoreDto>>> GetStoresAsync(PageQueryDto parameter)
        {
                var dbStoresQuery = _unitOfWork.Store.GetAll()
                                .Where(x => x.IsActive.Equals(parameter.IsActive));

                if (!string.IsNullOrEmpty(parameter.SearchKey))
                {
                    dbStoresQuery = dbStoresQuery.Where(x => EF.Functions.ILike(x.MallInfo.MallName, "%" + parameter.SearchKey + "%") || EF.Functions.ILike(x.Brand.BrandName, "%" + parameter.SearchKey + "%"));
                }



                var dbStoresQueryLast = dbStoresQuery
                    .Include(x => x.MallInfo)
                    .Include(x => x.Brand)
                    //.Select(x => ObjectMapper.Mapper.Map<StoreDto>(x));
                    .Select(x => new StoreDto()
                    {
                        PkId = x.PkId,
                        BrandId = x.BrandId,
                        BrandName = x.Brand.BrandName,
                        MallInfoId = x.MallInfoId,
                        MallInfoName = x.MallInfo.MallName,
                        StoreName = x.StoreName,   
                        Floor = x.Floor,
                        CreatedDate = x.CreatedDate,    
                        
                    });

                dbStoresQueryLast = string.IsNullOrEmpty(parameter.OrderBy)
                    ? dbStoresQueryLast.OrderByDescending(x => x.CreatedDate)
                    : dbStoresQueryLast.OrderBy(parameter.OrderBy);

                var dbStoreList = await PagedList<StoreDto>.ToPagedListAsync(dbStoresQueryLast.AsNoTracking(), parameter.PageId, parameter.PageSize);

                return Response<List<StoreDto>>.Success(dbStoreList, (int)HttpStatusCode.OK, dbStoreList.CurrentPage, dbStoreList.TotalCount);

            
          
        }

        public async Task<Response<StoreDto>> UpdateStoreAsync(StoreDto storeDto)
        {
            var dbStore = await _unitOfWork.Store.GetByIdAsync(storeDto.PkId);

            if (dbStore is null)
                return Response<StoreDto>.Fail("Store is not found", (int)HttpStatusCode.NotFound, true);

            ObjectMapper.Mapper.Map(storeDto, dbStore);

            await _unitOfWork.SaveChangesAsync();

            return Response<StoreDto>.Success(ObjectMapper.Mapper.Map<StoreDto>(dbStore), (int)HttpStatusCode.NoContent);
        }
    }
}
