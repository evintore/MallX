using Core.Entities.Base;

namespace Entities.Concrete
{
    public class Subcategory : BaseEntity, IEntity
    {
        public int SubategoryId { get; set; }
        public int CategoryId { get; set; } 
        public string? SubcategoryName { get; set; }
        public virtual Category? Category { get; set; }
        public ICollection<Brand>? Brands { get; set; }
    }
}
