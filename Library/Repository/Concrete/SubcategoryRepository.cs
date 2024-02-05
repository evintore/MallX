using Data;
using Entities.Concrete;
using Repository.Abstract;

namespace Repository.Concrete
{
    public class SubcategoryRepository : GenericRepository<Subcategory>, ISubcategoryRepository
    {
        public SubcategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
