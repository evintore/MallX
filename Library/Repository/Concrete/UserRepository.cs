using Core.Entities;
using Data;
using Repository.Abstract;

namespace Repository.Concrete
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}
