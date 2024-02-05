using Core.Entities.Base;

namespace Entities.Concrete
{
    public class Category : BaseEntity, IEntity
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public ICollection<Subcategory>? Subcategories { get; set; }
        public ICollection<Brand> Brands { get; set; }
    }
}
