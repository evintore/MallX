using Data;
using Entities.Concrete;
using Repository.Abstract;

namespace Repository.Concrete
{
    public class MallInfoRepository : GenericRepository<MallInfo>, IMallInfoRepository
    {
        public MallInfoRepository(AppDbContext context) : base(context)
        {
        }
    }
}