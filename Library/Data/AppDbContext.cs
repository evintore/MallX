using Core.Entities;
using Core.Entities.Base;
using Core.Enums;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Data
{
    public class AppDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FluentAPI
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.Entries().ToList().ForEach(e =>
            {
                if (e.Entity.GetType() == typeof(User))
                {
                    switch (e.State)
                    {
                        case EntityState.Added:
                            ((User)e.Entity).CreatedDate = DateTime.UtcNow;
                            ((User)e.Entity).ModifiedDate = DateTime.UtcNow;
                            ((User)e.Entity).IsActive = true;
                            ((User)e.Entity).Status = UserStatus.Active;
                            break;
                        case EntityState.Modified:
                            ((User)e.Entity).ModifiedDate = DateTime.UtcNow;
                            break;
                        case EntityState.Deleted:
                            ((User)e.Entity).IsActive = false;
                            ((User)e.Entity).Status = UserStatus.Deactive;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    int UserId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);

                    switch (e.State)
                    {
                        case EntityState.Added:
                            ((BaseEntity)e.Entity).CreatedUserId = UserId;
                            ((BaseEntity)e.Entity).CreatedDate = DateTime.UtcNow;
                            ((BaseEntity)e.Entity).ModifiedUserId = UserId;
                            ((BaseEntity)e.Entity).ModifiedDate = DateTime.UtcNow;
                            ((BaseEntity)e.Entity).IsActive = true;
                            break;
                        case EntityState.Modified:
                            ((BaseEntity)e.Entity).ModifiedDate = DateTime.UtcNow;
                            ((BaseEntity)e.Entity).ModifiedUserId = UserId;
                            break;
                        case EntityState.Deleted:
                            ((BaseEntity)e.Entity).IsActive = false;
                            break;
                        default:
                            break;
                    }
                }
            });

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<User>? Users { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<MallInfo> MallInfos { get; set; }

        public DbSet<Snapshot> Snapshots { get; set; }

        public DbSet<Store> Stores { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
    }
}
