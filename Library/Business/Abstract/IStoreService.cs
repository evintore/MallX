using Core.Dto;
using Core.Results;
using Entities.Dto;

namespace Business.Abstract
{
    public interface IStoreService
    {
        Task<Response<StoreDto>> CreateStoreAsync(StoreDto storeDto);

        Task<Response<StoreDto>> GetStoreByIdAsync(int id, bool? isActive = true);

        Task<Response<List<StoreDto>>> GetStoresAsync(PageQueryDto parameter);

        Task<Response<StoreDto>> UpdateStoreAsync(StoreDto storeDto);

        Task<Response<NoDataDto>> DeleteStoreAsync(int id);
    }
}
