namespace Repository.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IUserRepository User { get; }

        IMallInfoRepository MallInfo { get; }
        IStoreRepository Store { get; }
        IBrandRepository Brand { get; }
        ISnapshotRepository Snapshot { get; }
        ICategoryRepository Category { get; }
        ISubcategoryRepository Subcategory { get; }
        //object Category { get; }
        //object Subcategory { get; }

        Task<int> SaveChangesAsync();
    }
}
