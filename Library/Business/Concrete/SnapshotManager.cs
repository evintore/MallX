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
    public class SnapshotManager : ISnapshotService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SnapshotManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<SnapshotDto>> CreateSnapshotAsync(SnapshotDto snapshotDto)
        {
            snapshotDto.SnapshotDate = DateTime.UtcNow;

            Snapshot snapshotEntity = ObjectMapper.Mapper.Map<Snapshot>(snapshotDto);

            await _unitOfWork.Snapshot.InsertAsync(snapshotEntity);
            await _unitOfWork.SaveChangesAsync();

            return Response<SnapshotDto>.Success(ObjectMapper.Mapper.Map<SnapshotDto>(snapshotEntity), (int)HttpStatusCode.Created);
        }

        public async Task<Response<NoDataDto>> DeleteSnapshotAsync(int id)
        {
            var dbSnapshot = await _unitOfWork.Snapshot.GetByIdAsync(id);

            if (dbSnapshot is null)
                return Response<NoDataDto>.Fail("Snapshot is not found", (int)HttpStatusCode.NotFound, true);

            _unitOfWork.Snapshot.Delete(dbSnapshot);
            await _unitOfWork.SaveChangesAsync();

            return Response<NoDataDto>.Success((int)HttpStatusCode.NoContent);
        }

        public async Task<Response<SnapshotDto>> GetSnapshotByIdAsync(int id, bool? isActive = true)
        {
            var dbSnapshot = await _unitOfWork.Snapshot.GetByIdQueryableAsync(id)
                .Include(x => x.Store)
                    .ThenInclude(x => x.MallInfo)
                .FirstOrDefaultAsync(x => x.PkId == id);

            if (dbSnapshot is null || dbSnapshot.IsActive.Equals(!isActive))
                return Response<SnapshotDto>.Fail("Snapshot is not found", (int)HttpStatusCode.NotFound, true);

            return Response<SnapshotDto>.Success(ObjectMapper.Mapper.Map<SnapshotDto>(dbSnapshot), (int)HttpStatusCode.OK);
        }

        public async Task<Response<List<SnapshotDto>>> GetSnapshotsAsync(PageQueryDto parameter)
        {
            var dbSnapshotsQuery = _unitOfWork.Snapshot.GetAll()
                .Where(x => x.IsActive.Equals(parameter.IsActive));

            if (!string.IsNullOrEmpty(parameter.SearchKey))
            {
                dbSnapshotsQuery = dbSnapshotsQuery.Where(x => EF.Functions.ILike(x.Store.MallInfo.MallName, "%" + parameter.SearchKey + "%") || EF.Functions.ILike(x.Store.StoreName, "%" + parameter.SearchKey + "%"));
            }

            var dbSnapshotsQueryLast = dbSnapshotsQuery
                .Include(x => x.Store)
                .ThenInclude(x => x.MallInfo)
                //.Select(x => ObjectMapper.Mapper.Map<SnapshotDto>(x));
                .Select(x => new SnapshotDto()
                {
                    PkId = x.PkId,
                    CustomerCount = x.CustomerCount,
                    WorkerCount = x.WorkerCount,
                    CustomerInSalesCount = x.CustomerInSalesCount,
                    SnapshotDate = x.SnapshotDate,
                    StoreId = x.StoreId,
                    StoreName = x.Store.StoreName,
                    MallInfoId = x.Store.MallInfoId,
                    MallInfoName = x.Store.MallInfo.MallName,
                    CreatedDate = x.CreatedDate
                });

            dbSnapshotsQueryLast = string.IsNullOrEmpty(parameter.OrderBy)
                ? dbSnapshotsQueryLast.OrderByDescending(x => x.CreatedDate)
                : dbSnapshotsQueryLast.OrderBy(parameter.OrderBy);

            var dbSnapshotList = await PagedList<SnapshotDto>.ToPagedListAsync(dbSnapshotsQueryLast.AsNoTracking(), parameter.PageId, parameter.PageSize);


            return Response<List<SnapshotDto>>.Success(dbSnapshotList, (int)HttpStatusCode.OK, dbSnapshotList.CurrentPage, dbSnapshotList.TotalCount);
        }

        public async Task<Response<SnapshotDto>> UpdateSnapshotAsync(SnapshotDto snapshotDto)
        {
            var dbSnapshot = await _unitOfWork.Snapshot.GetByIdAsync(snapshotDto.PkId);

            if (dbSnapshot is null)
                return Response<SnapshotDto>.Fail("Snapshot is not found", (int)HttpStatusCode.NotFound, true);

            ObjectMapper.Mapper.Map(snapshotDto, dbSnapshot);

            await _unitOfWork.SaveChangesAsync();

            return Response<SnapshotDto>.Success(ObjectMapper.Mapper.Map<SnapshotDto>(dbSnapshot), (int)HttpStatusCode.NoContent);
        }
    }
}
