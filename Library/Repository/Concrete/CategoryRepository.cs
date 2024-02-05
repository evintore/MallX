using Data;
using Entities.Concrete;
using Repository.Abstract;

namespace Repository.Concrete
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository

    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
