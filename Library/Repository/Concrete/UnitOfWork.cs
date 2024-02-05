using Data;
using Repository.Abstract;

namespace Repository.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IUserRepository? _userRepository;
        private readonly IMallInfoRepository? _mallInfoRepository;
        private readonly IStoreRepository? _storeRepository;
        private readonly IBrandRepository? _brandRepository;
        private readonly ISnapshotRepository? _snapshotRepository;
        private readonly ICategoryRepository? _categoryRepository;
        private readonly ISubcategoryRepository? _subcategoryRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IUserRepository User => _userRepository ?? new UserRepository(_context);
        
        public IMallInfoRepository MallInfo => _mallInfoRepository ?? new MallInfoRepository(_context);
        public IStoreRepository Store => _storeRepository ?? new StoreRepository(_context);
        public IBrandRepository Brand => _brandRepository ?? new BrandRepository(_context);
        public ISnapshotRepository Snapshot => _snapshotRepository ?? new SnapshotRepository(_context);
        public ICategoryRepository Category => _categoryRepository ?? new CategoryRepository(_context);
        public ISubcategoryRepository Subcategory => _subcategoryRepository ?? new SubcategoryRepository(_context);

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
