using Data;
using Entities.Concrete;
using Repository.Abstract;

namespace Repository.Concrete
{
    public class StoreRepository : GenericRepository<Store>, IStoreRepository
    {
        public StoreRepository(AppDbContext context) : base(context)
        {
        }
    }
}
