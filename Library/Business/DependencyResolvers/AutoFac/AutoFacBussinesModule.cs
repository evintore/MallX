using Autofac;
using Business.Abstract;
using Business.Concrete;
using Repository.Abstract;
using Repository.Concrete;

namespace Business.DependencyResolvers.AutoFac
{
    public class AutoFacBussinesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<TokenManager>().As<ITokenService>();
            builder.RegisterType<AddressManager>().As<IAddressService>();
            builder.RegisterType<MallInfoManager>().As<IMallInfoService>();
            builder.RegisterType<StoreManager>().As<IStoreService>();
            builder.RegisterType<BrandManager>().As<IBrandService>();
            builder.RegisterType<SnapshotManager>().As<ISnapshotService>();
            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<SubcategoryManager>().As<ISubcategoryService>();

            builder.RegisterGenericComposite(typeof(GenericRepository<>), typeof(IGenericRepository<>));
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<MallInfoRepository>().As<IMallInfoRepository>();
            builder.RegisterType<StoreRepository>().As<IStoreRepository>();
            builder.RegisterType<BrandRepository>().As<IBrandRepository>();
            builder.RegisterType<SnapshotRepository>().As<ISnapshotRepository>();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>();
            builder.RegisterType<SubcategoryRepository>().As<ISubcategoryRepository>();
        }
    }
}
