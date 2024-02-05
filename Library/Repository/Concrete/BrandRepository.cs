using Data;
using Entities.Concrete;
using Repository.Abstract;

namespace Repository.Concrete
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepository(AppDbContext context) : base(context)
        {
        }
    }
}
