using Core.Dto;
using Core.Results;
using Entities.Dto;

namespace Business.Abstract
{
    public interface ISnapshotService
    {
        Task<Response<SnapshotDto>> CreateSnapshotAsync(SnapshotDto snapshotDto);

        Task<Response<SnapshotDto>> GetSnapshotByIdAsync(int id, bool? isActive = true);

        Task<Response<List<SnapshotDto>>> GetSnapshotsAsync(PageQueryDto parameter);

        Task<Response<SnapshotDto>> UpdateSnapshotAsync(SnapshotDto snapshotDto);

        Task<Response<NoDataDto>> DeleteSnapshotAsync(int id);
    }
}
