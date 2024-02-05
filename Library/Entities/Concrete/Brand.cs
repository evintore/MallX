using Core.Entities.Base;

namespace Entities.Concrete
{
    public class Brand : BaseEntity, IEntity
    {
        public string BrandName { get; set; }

        public virtual Category? Category { get; set; }
        public int? CategoryId { get; set; }

        public virtual Subcategory? SubCategory { get; set; }
        public int? SubCategoryId { get; set; }

        public virtual ICollection<Store> Stores { get; set; }
    }
}
