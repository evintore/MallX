using Data;
using Entities.Concrete;
using Repository.Abstract;

namespace Repository.Concrete
{
    public class SnapshotRepository : GenericRepository<Snapshot>, ISnapshotRepository
    {
        public SnapshotRepository(AppDbContext context) : base(context)
        {
        }
    }
}
